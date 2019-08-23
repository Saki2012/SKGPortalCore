using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SKGPortalCore.Business.BillData;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Repository.BillData;

namespace SKGPortalCore.Schedule
{
    /// <summary>
    /// 資訊流導入
    /// </summary>
    public interface IReceiptInfoImport
    {
        /// <summary>
        /// 
        /// </summary>
        public ApplicationDbContext DataAccess { get; set; }
        /// <summary>
        /// 執行資訊流導入
        /// </summary>
        public void ExecuteImport()
        {
            List<string> sources = ReadFile();
            IList sets = AnalyzeFile(sources);
            CreateReceiptInfoBill(sets);
        }
        /// <summary>
        /// 讀資訊流檔
        /// </summary>
        /// <returns></returns>
        private protected List<string> ReadFile();
        /// <summary>
        /// 分析資訊流
        /// </summary>
        /// <param name="sources"></param>
        /// <returns></returns>
        private protected IList AnalyzeFile(List<string> sources);
        /// <summary>
        /// 新增繳款資訊
        /// </summary>
        /// <param name="modelSources"></param>
        private protected void CreateReceiptInfoBill(IList modelSources);
    }
    /// <summary>
    /// 資訊流導入-銀行
    /// </summary>
    public class ReceiptInfoImportBANK : IReceiptInfoImport
    {
        /// <summary>
        /// 
        /// </summary>
        private const int StrLen = 128;
        /// <summary>
        /// 
        /// </summary>
        public ApplicationDbContext DataAccess { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<string> IReceiptInfoImport.ReadFile()
        {
            List<string> result = new List<string>();
            string filePath = "", strRow;
            using StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding(950));
            while (sr.Peek() > 0)
            {
                strRow = sr.ReadLine();
                if (0 == strRow.Length) continue;
                if (StrLen != strRow.Length) { /*第N行 Error:長度不符*/}
                switch (LibData.ByteSubString(strRow, 0, 1))
                {
                    case "H"://表頭、檢查是否今日，並且Count歸零
                        break;
                    case "T"://表尾、檢查是否筆數正確
                        break;
                    default://明細
                        result.Add(strRow);
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
        IList IReceiptInfoImport.AnalyzeFile(List<string> sources)
        {
            List<ReceiptInfoBillBankModel> result = new List<ReceiptInfoBillBankModel>();
            DateTime now = DateTime.Now;
            string importBatchNo = $"BANK{now.ToString("yyyyMMddhhmmss")}";
            foreach (string source in sources)
            {
                result.Add(AnalyzeSource(source, importBatchNo));
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="importBatchNo"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        public ReceiptInfoBillBankModel AnalyzeSource(string source, string importBatchNo)
        {
            return new ReceiptInfoBillBankModel()
            {
                RealAccount = LibData.ByteSubString(source, 0, 13),
                TradeDate = LibData.ByteSubString(source, 13, 8),
                TradeTime = LibData.ByteSubString(source, 21, 6),
                CompareCode = LibData.ByteSubString(source, 27, 16),
                PN = LibData.ByteSubString(source, 43, 1),
                Amount = LibData.ByteSubString(source, 44, 10),
                Summary = LibData.ByteSubString(source, 54, 10),
                Branch = LibData.ByteSubString(source, 64, 4),
                TradeChannel = LibData.ByteSubString(source, 68, 2),
                Channel = LibData.ByteSubString(source, 70, 2),
                ChangeDate = LibData.ByteSubString(source, 72, 8),
                BizDate = LibData.ByteSubString(source, 80, 8),
                Serial = LibData.ByteSubString(source, 88, 6),
                CustomerCode = LibData.ByteSubString(source, 94, 6),
                Fee = LibData.ByteSubString(source, 100, 3),
                Empty = LibData.ByteSubString(source, 103, 25),
                ImportBatchNo = importBatchNo,
                Source = source
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelSources"></param>
        void IReceiptInfoImport.CreateReceiptInfoBill(IList modelSources)
        {
            List<ReceiptInfoBillBankModel> models = modelSources as List<ReceiptInfoBillBankModel>;
            var msg = new MessageLog(new GraphQL.ExecutionErrors());
            using BizReceiptInfoBill biz = new BizReceiptInfoBill(msg);
            using ReceiptBillRepository repo = new ReceiptBillRepository(DataAccess);
            foreach (var model in models)
            {
                biz.Bank.CheckData(model);
                repo.Create(biz.Bank.GetReceiptBillSet(model));
            }
            repo.CommitData(Model.FuncAction.Create);
        }
    }
    /// <summary>
    /// 資訊流導入-郵局
    /// </summary>
    public class ReceiptInfoImportPOST : IReceiptInfoImport
    {
        /// <summary>
        /// 
        /// </summary>
        private const int StrLen = 110;
        /// <summary>
        /// 
        /// </summary>
        public ApplicationDbContext DataAccess { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<string> IReceiptInfoImport.ReadFile()
        {
            List<string> result = new List<string>();
            string filePath = "", strRow;
            using StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding(950));
            while (sr.Peek() > 0)
            {
                strRow = sr.ReadLine();
                if (0 == strRow.Length) continue;
                if (StrLen != strRow.Length) { /*第N行 Error:長度不符*/}
                result.Add(strRow);
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sources"></param>
        /// <returns></returns>
        IList IReceiptInfoImport.AnalyzeFile(List<string> sources)
        {
            List<ReceiptInfoBillPostModel> result = new List<ReceiptInfoBillPostModel>();
            DateTime now = DateTime.Now;
            string importBatchNo = $"POST{now.ToString("yyyyMMddhhmmss")}";
            foreach (string source in sources)
            {
                result.Add(AnalyzeSource(source, importBatchNo));
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="importBatchNo"></param>
        /// <returns></returns>
        public ReceiptInfoBillPostModel AnalyzeSource(string source, string importBatchNo)
        {
            return new ReceiptInfoBillPostModel()
            {
                CollectionType = LibData.ByteSubString(source, 0, 8),
                TradeDate = LibData.ByteSubString(source, 8, 7),
                Branch = LibData.ByteSubString(source, 15, 6),
                Channel = LibData.ByteSubString(source, 21, 4),
                TradeSer = LibData.ByteSubString(source, 25, 7),
                PN = LibData.ByteSubString(source, 32, 1),
                Amount = LibData.ByteSubString(source, 33, 11),
                CompareCode = LibData.ByteSubString(source, 44, 24),
                Empty = LibData.ByteSubString(source, 68, 42),
                ImportBatchNo = importBatchNo,
                Source = source
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelSources"></param>
        void IReceiptInfoImport.CreateReceiptInfoBill(IList modelSources)
        {
            List<ReceiptInfoBillPostModel> models = modelSources as List<ReceiptInfoBillPostModel>;
            var msg = new MessageLog(new GraphQL.ExecutionErrors());
            using BizReceiptInfoBill biz = new BizReceiptInfoBill(msg);
            using ReceiptBillRepository repo = new ReceiptBillRepository(DataAccess);
            foreach (var model in models)
            {
                biz.Post.CheckData(model);
                repo.Create(biz.Post.GetReceiptBillSet(model));
            }
            repo.CommitData(Model.FuncAction.Create);
        }
    }
    /// <summary>
    /// 資訊流導入-超商
    /// </summary>
    public class ReceiptInfoImportMARKET : IReceiptInfoImport
    {
        /// <summary>
        /// 
        /// </summary>
        private const int StrLen = 120;
        /// <summary>
        /// 
        /// </summary>
        public ApplicationDbContext DataAccess { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<string> IReceiptInfoImport.ReadFile()
        {
            List<string> result = new List<string>();
            string filePath = "", strRow;
            using StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding(950));
            while (sr.Peek() > 0)
            {
                strRow = sr.ReadLine();
                if (0 == strRow.Length) continue;
                if (StrLen != strRow.Length) { /*第N行 Error:長度不符*/}
                switch (LibData.ByteSubString(strRow, 0, 1))
                {
                    case "1"://表頭、檢查是否今日，並且Count歸零
                        break;
                    case "2"://明細
                        result.Add(strRow);
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
        IList IReceiptInfoImport.AnalyzeFile(List<string> sources)
        {
            List<ReceiptInfoBillMarketModel> result = new List<ReceiptInfoBillMarketModel>();
            DateTime now = DateTime.Now;
            string importBatchNo = $"MARKET{now.ToString("yyyyMMddhhmmss")}";
            foreach (string source in sources)
                result.Add(AnalyzeSource(source, importBatchNo));
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="importBatchNo"></param>
        /// <returns></returns>
        public ReceiptInfoBillMarketModel AnalyzeSource(string source, string importBatchNo)
        {
            return new ReceiptInfoBillMarketModel()
            {
                Idx = LibData.ByteSubString(source, 0, 1),
                CollectionType = LibData.ByteSubString(source, 1, 8),
                Channel = LibData.ByteSubString(source, 9, 8),
                Store = LibData.ByteSubString(source, 17, 8),
                TransAccount = LibData.ByteSubString(source, 25, 14),
                TransType = LibData.ByteSubString(source, 39, 3),
                PayStatus = LibData.ByteSubString(source, 42, 2),
                AccountingDay = LibData.ByteSubString(source, 44, 8),
                PayDate = LibData.ByteSubString(source, 52, 8),
                Barcode1 = LibData.ByteSubString(source, 60, 9),
                Barcode2 = LibData.ByteSubString(source, 69, 20),
                Barcode3 = LibData.ByteSubString(source, 89, 15),
                Empty = LibData.ByteSubString(source, 104, 16),
                ImportBatchNo = importBatchNo,
                Source = source,
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelSources"></param>
        void IReceiptInfoImport.CreateReceiptInfoBill(IList modelSources)
        {
            List<ReceiptInfoBillMarketModel> models = modelSources as List<ReceiptInfoBillMarketModel>;
            var msg = new MessageLog(new GraphQL.ExecutionErrors());
            using BizReceiptInfoBill biz = new BizReceiptInfoBill(msg);
            using ReceiptBillRepository repo = new ReceiptBillRepository(DataAccess);
            foreach (var model in models)
            {
                biz.Market.CheckData(model);
                repo.Create(biz.Market.GetReceiptBillSet(model));
            }
            repo.CommitData(Model.FuncAction.Create);
        }
    }
    /// <summary>
    /// 資訊流導入-超商產險
    /// </summary>
    public class ReceiptInfoImportMARKETSPI : IReceiptInfoImport
    {
        /// <summary>
        /// 
        /// </summary>
        private const int StrLen = 120;
        /// <summary>
        /// 
        /// </summary>
        public ApplicationDbContext DataAccess { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<string> IReceiptInfoImport.ReadFile()
        {
            List<string> result = new List<string>();
            string filePath = "", strRow;
            using StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding(950));
            while (sr.Peek() > 0)
            {
                strRow = sr.ReadLine();
                if (0 == strRow.Length) continue;
                if (StrLen != strRow.Length) { /*第N行 Error:長度不符*/}
                switch (LibData.ByteSubString(strRow, 0, 1))
                {
                    case "1"://表頭、檢查是否今日，並且Count歸零
                        break;
                    case "2"://明細
                        result.Add(strRow);
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
        IList IReceiptInfoImport.AnalyzeFile(List<string> sources)
        {
            List<ReceiptInfoBillMarketSPIModel> result = new List<ReceiptInfoBillMarketSPIModel>();
            DateTime now = DateTime.Now;
            string importBatchNo = $"MARKET{now.ToString("yyyyMMddhhmmss")}";
            foreach (string source in sources)
                result.Add(AnalyzeSource(source, importBatchNo));
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="importBatchNo"></param>
        /// <returns></returns>
        public ReceiptInfoBillMarketSPIModel AnalyzeSource(string source, string importBatchNo)
        {
            return new ReceiptInfoBillMarketSPIModel()
            {
                Idx = LibData.ByteSubString(source, 0, 1),
                Channel = LibData.ByteSubString(source, 1, 8),
                ISC = LibData.ByteSubString(source, 9, 8),
                TransDate = LibData.ByteSubString(source, 17, 8),
                PayDate = LibData.ByteSubString(source, 25, 8),
                Barcode2 = LibData.ByteSubString(source, 33, 16),
                Barcode3_Date = LibData.ByteSubString(source, 49, 4),
                Barcode3_CompareCode = LibData.ByteSubString(source, 53, 2),
                Barcode3_Amount = LibData.ByteSubString(source, 55, 9),
                Empty1 = LibData.ByteSubString(source, 64, 18),
                Store = LibData.ByteSubString(source, 82, 6),
                Empty2 = LibData.ByteSubString(source, 88, 32),
                ImportBatchNo = importBatchNo,
                Source = source,
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelSources"></param>
        void IReceiptInfoImport.CreateReceiptInfoBill(IList modelSources)
        {
            List<ReceiptInfoBillMarketSPIModel> models = modelSources as List<ReceiptInfoBillMarketSPIModel>;
            var msg = new MessageLog(new GraphQL.ExecutionErrors());
            using BizReceiptInfoBill biz = new BizReceiptInfoBill(msg);
            using ReceiptBillRepository repo = new ReceiptBillRepository(DataAccess);
            foreach (var model in models)
            {
                biz.MarketSPI.CheckData(model);
                repo.Create(biz.MarketSPI.GetReceiptBillSet(model));
            }
            repo.CommitData(Model.FuncAction.Create);
        }
    }
    /// <summary>
    /// 資訊流導入-農金
    /// </summary>
    public class ReceiptInfoImportFARM : IReceiptInfoImport
    {
        /// <summary>
        /// 
        /// </summary>
        private const int StrLen = 120;
        /// <summary>
        /// 
        /// </summary>
        public ApplicationDbContext DataAccess { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<string> IReceiptInfoImport.ReadFile()
        {
            List<string> result = new List<string>();
            string filePath = "", strRow;
            using StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding(950));
            while (sr.Peek() > 0)
            {
                strRow = sr.ReadLine();
                if (0 == strRow.Length) continue;
                if (StrLen != strRow.Length) { /*第N行 Error:長度不符*/}
                switch (LibData.ByteSubString(strRow, 0, 1))
                {
                    case "1"://表頭、檢查是否今日，並且Count歸零
                        break;
                    case "2"://明細
                        result.Add(strRow);
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
        IList IReceiptInfoImport.AnalyzeFile(List<string> sources)
        {
            List<ReceiptInfoBillFarmModel> result = new List<ReceiptInfoBillFarmModel>();
            DateTime now = DateTime.Now;
            string importBatchNo = $"FARM{now.ToString("yyyyMMddhhmmss")}";
            foreach (string source in sources)
            {
                result.Add(AnalyzeSource(source, importBatchNo));
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="importBatchNo"></param>
        /// <returns></returns>
        public ReceiptInfoBillFarmModel AnalyzeSource(string source, string importBatchNo)
        {
            return new ReceiptInfoBillFarmModel()
            {
                Idx = LibData.ByteSubString(source, 0, 1),
                CollectionType = LibData.ByteSubString(source, 1, 8),
                Channel = LibData.ByteSubString(source, 9, 8),
                Store = LibData.ByteSubString(source, 17, 8),
                TransAccount = LibData.ByteSubString(source, 25, 14),
                TransType = LibData.ByteSubString(source, 39, 3),
                PayStatus = LibData.ByteSubString(source, 42, 2),
                AccountingDay = LibData.ByteSubString(source, 44, 8),
                PayDate = LibData.ByteSubString(source, 52, 8),
                Barcode1 = LibData.ByteSubString(source, 60, 9),
                Barcode2 = LibData.ByteSubString(source, 69, 20),
                Barcode3 = LibData.ByteSubString(source, 89, 15),
                Empty = LibData.ByteSubString(source, 104, 16),
                ImportBatchNo = importBatchNo,
                Source = source,
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelSources"></param>
        void IReceiptInfoImport.CreateReceiptInfoBill(IList modelSources)
        {
            List<ReceiptInfoBillFarmModel> models = modelSources as List<ReceiptInfoBillFarmModel>;
            var msg = new MessageLog(new GraphQL.ExecutionErrors());
            using BizReceiptInfoBill biz = new BizReceiptInfoBill(msg);
            using ReceiptBillRepository repo = new ReceiptBillRepository(DataAccess);
            foreach (var model in models)
            {
                biz.Farm.CheckData(model);
                repo.Create(biz.Farm.GetReceiptBillSet(model));
            }
            repo.CommitData(Model.FuncAction.Create);
        }
    }
}
