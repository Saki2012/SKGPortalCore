using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using SKGPortalCore.Business.BillData;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Repository.BillData;
using SKGPortalCore.Repository.MasterData;

namespace SKGPortalCore.Schedule.Import
{
    /// <summary>
    /// 資訊流導入-銀行
    /// </summary>
    public class ReceiptInfoImportBANK : IImportData
    {
        #region Propety
        /// <summary>
        /// 
        /// </summary>
        public ApplicationDbContext DataAccess { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MessageLog Message { get; }
        /// <summary>
        /// 資訊流長度(byte)
        /// </summary>
        private const int StrLen = 128;
        /// <summary>
        /// 檔案名稱
        /// </summary>
        private const string FileName = "SKG_BANK";
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
        public ReceiptInfoImportBANK(ApplicationDbContext dataAccess, MessageLog messageLog = null)
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
            int line = 1; string strRow;
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
        /// 
        /// </summary>
        /// <param name="sources"></param>
        /// <returns></returns>
        IList IImportData.AnalyzeFile(Dictionary<int, string> sources)
        {
            List<ReceiptInfoBillBankModel> result = new List<ReceiptInfoBillBankModel>();
            DateTime now = DateTime.Now;
            string importBatchNo = $"BANK{now.ToString("yyyyMMddhhmmss")}";
            foreach (int line in sources.Keys)
            {
                result.Add(new ReceiptInfoBillBankModel() { Id = line, Source = sources[line], ImportBatchNo = importBatchNo });
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelSources"></param>
        void IImportData.CreateData(IList modelSources)
        {
            List<ReceiptInfoBillBankModel> models = modelSources as List<ReceiptInfoBillBankModel>;
            List<ReceiptInfoBillBankModel> split = new List<ReceiptInfoBillBankModel>();
            for (int i = 0; i < models.Count; i++)
            {
                split.Add(models[i]);
                if (i != 0 && i % 299 == i)
                {
                    Thread thread1 = new Thread(new ParameterizedThreadStart(CT));
                    thread1.Start(models);
                    split.Clear();
                }
            }
            Thread thread = new Thread(new ParameterizedThreadStart(CT));
            thread.Start(split);


        }

        public static void CT(object list)
        {
            MessageLog Message = new MessageLog(SystemOperator.SysOperator);
            using BizReceiptInfoBillBANK receiptRepo = new BizReceiptInfoBillBANK(Message, LibDataAccess.CreateDataAccess());
            using ReceiptBillRepository billRepo = new ReceiptBillRepository(LibDataAccess.CreateDataAccess()) { Message = Message, User = SystemOperator.SysOperator };
            List<ReceiptInfoBillBankModel> models = list as List<ReceiptInfoBillBankModel>;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            int i = models.Count;
            sw.Reset();
            sw.Start();
            models.ForEach(model =>
            {
                receiptRepo.CheckData(model);
                ReceiptBillSet set = receiptRepo.GetReceiptBillSet(model);
                billRepo.Create(set);
                Console.WriteLine($"Create:{sw.Elapsed.TotalSeconds},");
            });
            billRepo.CommitData(FuncAction.Create);
            Console.WriteLine($"CommitData Time:{sw.Elapsed.TotalSeconds}");
            sw.Stop();

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

    /// <summary>
    /// 資訊流導入-共用方法
    /// </summary>
    public static class ReceiptInfoImportComm
    {
        internal static BizCustomerSet GetBizCustomerSet(BizCustomerRepository biz, string compareCode, out string compareCodeForCheck)
        {
            compareCodeForCheck = string.Empty;
            BizCustomerSet bizCust = biz.QueryData(new object[] { compareCode.Substring(0, 6) });
            if (null == bizCust)
                bizCust = biz.QueryData(new object[] { compareCode.Substring(0, 4) });
            if (null == bizCust)
                bizCust = biz.QueryData(new object[] { compareCode.Substring(0, 3) });
            if (null == bizCust)
                return null;
            if (bizCust.BizCustomer.AccountStatus == AccountStatus.Unable)
                compareCodeForCheck = compareCode;
            else
                compareCodeForCheck = bizCust.BizCustomer.VirtualAccount3 == VirtualAccount3.NoverifyCode ? compareCode : compareCode[0..^1];
            return bizCust;
        }
    }
}
