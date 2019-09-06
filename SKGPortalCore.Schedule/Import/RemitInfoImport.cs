using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SKGPortalCore.Business.BillData;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Repository.BillData;

namespace SKGPortalCore.Schedule.Import
{
    public class RemitInfoImport : IImportData
    {
        #region Property
        public ApplicationDbContext DataAccess { get; }
        public MessageLog Message { get; }
        /// <summary>
        /// 
        /// </summary>
        private const int StrLen = 128;
        /// <summary>
        /// 原檔案存放位置
        /// </summary>
        private const string srcPath = @"D:\iBankRoot\Ftp_SKGPortalCore\ACCFTT\";
        /// <summary>
        /// 成功檔案存放位置
        /// </summary>
        private const string successPath = @"D:\iBankRoot\Ftp_SKGPortalCore\SuccessFolder\ACCFTT\";
        /// <summary>
        /// 失敗檔案存放位置
        /// </summary>
        private const string failPath = @"D:\iBankRoot\Ftp_SKGPortalCore\ErrorFolder\ACCFTT\";
        /// <summary>
        /// 原資料
        /// </summary>
        private string SrcFile { get { return $"{srcPath}ACCFTT.{DateTime.Now.ToString("yyyyMMdd")}"; } }
        /// <summary>
        /// 成功資料
        /// </summary>
        private string SuccFile { get { return $"{successPath}ACCFTT.{DateTime.Now.ToString("yyyyMMdd")}{LibData.GenRandomString(3)}"; } }
        /// <summary>
        /// 失敗資料
        /// </summary>
        private string FailFile { get { return $"{failPath}ACCFTT.{DateTime.Now.ToString("yyyyMMdd")}{LibData.GenRandomString(3)}"; } }
        #endregion
        #region Construct
        public RemitInfoImport(ApplicationDbContext dataAccess) { DataAccess = dataAccess; Message = new MessageLog(SystemOperator.SysOperator); }
        #endregion
        #region Implement
        /// <summary>
        /// 讀資訊流檔
        /// </summary>
        /// <returns></returns>
        Dictionary<int, string> IImportData.ReadFile()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            string strRow; int line = 1;
            using StreamReader sr = new StreamReader(SrcFile, Encoding.GetEncoding(950));
            while (sr.Peek() > 0)
            {
                strRow = sr.ReadLine();
                line++;
                if (0 == strRow.Length) continue;
                if (StrLen != LibData.ByteLen(strRow)) { /*第N行 Error:長度不符*/}
                switch (LibData.ByteSubString(strRow, 0, 1))
                {
                    case "H"://表頭、檢查是否今日，並且Count歸零
                        break;
                    case "T"://表尾、檢查是否筆數正確
                        break;
                    default://明細
                        result.Add(line, strRow);
                        break;
                }
            }
            return result;
        }
        /// <summary>
        /// 分析資訊流
        /// </summary>
        /// <param name="sources"></param>
        /// <returns></returns>
        IList IImportData.AnalyzeFile(Dictionary<int, string> sources)
        {
            List<RemitInfoModel> result = new List<RemitInfoModel>();
            DateTime now = DateTime.Now;
            string importBatchNo = $"RT{now.ToString("yyyyMMddhhmmss")}";
            foreach (int line in sources.Keys)
                result.Add(new RemitInfoModel() { Id = line, Source = sources[line], ImportBatchNo = importBatchNo });
            return result;
        }
        /// <summary>
        /// 新增繳款資訊
        /// </summary>
        /// <param name="modelSources"></param>
        void IImportData.CreateData(IList modelSources)
        {
            List<RemitInfoModel> srcs = modelSources as List<RemitInfoModel>;
            using BizRemitInfo biz = new BizRemitInfo(Message);
            using CashFlowBillRepository repo = new CashFlowBillRepository(DataAccess);
            foreach (var model in srcs)
            {
                CashFlowBillSet set = biz.GetCashFlowBillSet(model);
                repo.Create(set);
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
                    file = isSuccess ? SuccFile : FailFile;
                } while (File.Exists(file));
                File.Move(SrcFile, file);
            }
        }
        #endregion
    }
}
