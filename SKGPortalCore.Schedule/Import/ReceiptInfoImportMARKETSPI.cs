using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Repository.BillData;
using SKGPortalCore.Repository.MasterData;
using SKGPortalCore.Repository.SKGPortalCore.Business.BillData;

namespace SKGPortalCore.Schedule.Import
{

    /// <summary>
    /// 資訊流導入-超商產險
    /// </summary>
    public class ReceiptInfoImportMARKETSPI : IImportData
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
        public SysMessageLog Message { get; }
        /// <summary>
        /// 檔案名稱
        /// </summary>
        private const string FileName = "SKG_MARTSPI";
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
        public ReceiptInfoImportMARKETSPI(ApplicationDbContext dataAccess, SysMessageLog messageLog = null)
        {
            DataAccess = dataAccess;
            Message = messageLog ?? new SysMessageLog(SystemOperator.SysOperator);
            Directory.CreateDirectory(SrcPath);
            Directory.CreateDirectory(SuccessPath);
            Directory.CreateDirectory(FailPath);
        }
        #endregion
        #region Implement
        /// <summary>
        /// 讀資訊流檔
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
                    continue;

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
        /// 分析資訊流
        /// </summary>
        /// <param name="sources"></param>
        /// <returns></returns>
        IList IImportData.AnalyzeFile(Dictionary<int, string> sources)
        {
            List<ReceiptInfoBillMarketSPIModel> result = new List<ReceiptInfoBillMarketSPIModel>();
            DateTime now = DateTime.Now;
            string importBatchNo = $"MARKET{now.ToString("yyyyMMddhhmmss")}";
            foreach (int line in sources.Keys)
            {
                result.Add(new ReceiptInfoBillMarketSPIModel() { Id = line, Source = sources[line], ImportBatchNo = importBatchNo });
            }

            return result;
        }
        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="modelSources"></param>
        void IImportData.CreateData(IList modelSources)
        {
            List<ReceiptInfoBillMarketSPIModel> models = modelSources as List<ReceiptInfoBillMarketSPIModel>;
            using BizCustomerRepository bizCustRepo = new BizCustomerRepository(DataAccess) { Message = Message };
            using ReceiptBillRepository repo = new ReceiptBillRepository(DataAccess) { User = SystemOperator.SysOperator };
            foreach (ReceiptInfoBillMarketSPIModel model in models)
            {
                BizReceiptInfoBillMARKETSPI.CheckData(model);
                BizCustomerSet bizCust = ReceiptInfoImportComm.GetBizCustomerSet(bizCustRepo, model.Barcode2.TrimStart('0'), out string compareCodeForCheck);
                repo.Create(BizReceiptInfoBillMARKETSPI.GetReceiptBillSet(model));
            }
            repo.CommitData(FuncAction.Create);
        }
        /// <summary>
        /// 將檔案移動至成功/失敗資料夾中
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
