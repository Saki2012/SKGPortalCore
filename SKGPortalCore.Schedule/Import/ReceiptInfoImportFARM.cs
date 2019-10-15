﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SKGPortalCore.Business.BillData;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Repository.BillData;
using SKGPortalCore.Repository.MasterData;

namespace SKGPortalCore.Schedule.Import
{

    /// <summary>
    /// 資訊流導入-農金
    /// </summary>
    public class ReceiptInfoImportFARM : IImportData
    {
        #region Property
        /// <summary>
        /// 資訊流長度(byte)
        /// </summary>
        private const int StrLen = 120;
        /// <summary>
        /// 
        /// </summary>
        public ApplicationDbContext DataAccess { get; }
        /// <summary>
        /// 
        /// </summary>
        public MessageLog Message { get; }
        /// <summary>
        /// 檔案名稱
        /// </summary>
        private const string FileName = "SKG_FARM";
        /// <summary>
        /// 原檔案存放位置
        /// </summary>
        private const string SrcPath = @"D:\iBankRoot\Ftp_SKGPortalCore\TransactionListDaily\";
        /// <summary>
        /// 成功檔案存放位置
        /// </summary>
        private const string SuccessPath = @"D:\iBankRoot\Ftp_SKGPortalCore\SuccessFolder\TransactionListDaily\";
        /// <summary>
        /// 失敗檔案存放位置
        /// </summary>
        private const string FailPath = @"D:\iBankRoot\Ftp_SKGPortalCore\ErrorFolder\TransactionListDaily\";
        /// <summary>
        /// 原資料
        /// </summary>
        private string SrcFile => $"{SrcPath}{FileName}.{DateTime.Now.ToString("yyyyMMdd")}";
        /// <summary>
        /// 成功資料
        /// </summary>
        private string SuccessFile => $"{SuccessPath}{FileName}.{DateTime.Now.ToString("yyyyMMdd")}{LibData.GenRandomString(3)}";
        /// <summary>
        /// 失敗資料
        /// </summary>
        private string FailFile => $"{FailPath}{FileName}.{DateTime.Now.ToString("yyyyMMdd")}{LibData.GenRandomString(3)}";
        #endregion
        #region Construct
        public ReceiptInfoImportFARM(ApplicationDbContext dataAccess,MessageLog messageLog=null)
        {
            DataAccess = dataAccess;
            Message = messageLog ?? new MessageLog(SystemOperator.SysOperator);
            Directory.CreateDirectory(SrcPath);
            Directory.CreateDirectory(SuccessPath);
            Directory.CreateDirectory(FailPath);
        }
        #endregion
        #region Implement
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<int, string> IImportData.ReadFile()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            int line = 0; string strRow;
            using StreamReader sr = new StreamReader(SrcFile, Encoding.GetEncoding(950));
            while (sr.Peek() > 0)
            {
                strRow = sr.ReadLine();
                line++;
                if (0 == strRow.Length)
                {
                    continue;
                }

                if (StrLen != LibData.ByteLen(strRow)) { /*第N行 Error:長度不符*/}
                switch (LibData.ByteSubString(strRow, 0, 1))
                {
                    case "1"://表頭、檢查是否今日，並且Count歸零
                        break;
                    case "2"://明細
                        result.Add(line, strRow);
                        break;
                    case "3"://表尾、檢查是否筆數正確
                        break;
                }
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sources"></param>
        /// <returns></returns>
        IList IImportData.AnalyzeFile(Dictionary<int, string> sources)
        {
            List<ReceiptInfoBillFarmModel> result = new List<ReceiptInfoBillFarmModel>();
            DateTime now = DateTime.Now;
            string importBatchNo = $"FARM{now.ToString("yyyyMMddhhmmss")}";
            foreach (int line in sources.Keys)
            {
                result.Add(new ReceiptInfoBillFarmModel() { Id = line, Source = sources[line], ImportBatchNo = importBatchNo });
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelSources"></param>
        void IImportData.CreateData(IList modelSources)
        {
            List<ReceiptInfoBillFarmModel> models = modelSources as List<ReceiptInfoBillFarmModel>;
            using BizCustomerRepository bizCustRepo = new BizCustomerRepository(DataAccess) { Message = Message };
            using BizReceiptInfoBillFARM biz = new BizReceiptInfoBillFARM(Message, DataAccess);
            using ReceiptBillRepository repo = new ReceiptBillRepository(DataAccess) { User = SystemOperator.SysOperator };
            foreach (ReceiptInfoBillFarmModel model in models)
            {
                biz.CheckData(model);
                BizCustomerSet bizCust = ReceiptInfoImportComm.GetBizCustomerSet(bizCustRepo,  model.Barcode2.TrimStart('0'), out string compareCodeForCheck);
                repo.Create(biz.GetReceiptBillSet(model));
            }
            repo.CommitData(FuncAction.Create);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSuccess"></param>
        void IImportData.MoveToOverFolder(bool isSuccess)
        {
            if (File.Exists(SrcFile))
            {
                string file;
                do
                {
                    file = isSuccess ? SuccessFile : FailFile;
                } while (File.Exists(file));
                File.Move(SrcFile, file);
            }
        }
        #endregion
    }
}
