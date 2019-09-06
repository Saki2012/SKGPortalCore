﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GraphQL;
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
        private const int StrLen = 128;
        /// <summary>
        /// 
        /// </summary>
        public ApplicationDbContext DataAccess { get; }
        /// <summary>
        /// 
        /// </summary>
        public MessageLog Message { get; }
        #endregion
        #region Construct
        public ReceiptInfoImportBANK(ApplicationDbContext dataAccess)
        {
            DataAccess = dataAccess;
            Message = new MessageLog(SystemOperator.SysOperator);
        }
        #endregion
        #region Public
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="importBatchNo"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        public ReceiptInfoBillBankModel AnalyzeSource(int line, string source, string importBatchNo)
        {
            return new ReceiptInfoBillBankModel()
            {
                Id = line,
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
        #endregion
        #region Implement
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<int, string> IImportData.ReadFile()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            string filePath = "", strRow;
            using StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding(950));
            int line = 1;
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
                result.Add(AnalyzeSource(line, sources[line], importBatchNo));
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelSources"></param>
        void IImportData.CreateData(IList modelSources)
        {
            List<ReceiptInfoBillBankModel> models = modelSources as List<ReceiptInfoBillBankModel>;
            using BizReceiptInfoBillBANK biz = new BizReceiptInfoBillBANK(Message);
            using ReceiptBillRepository repo = new ReceiptBillRepository(DataAccess) { Message = Message };
            foreach (var model in models)
            {
                biz.CheckData(model);
                BizCustomerSet bizCust = ReceiptInfoImportComm.GetBizCustomerSet(DataAccess, Message, model.CompareCode, out string compareCodeForCheck);
                ReceiptInfoImportComm.GetCollectionTypeSet(DataAccess, "Bank999", model.Channel, model.Amount.ToDecimal(), out ChargePayType chargePayType, out decimal channelFee);
                ReceiptBillSet set = biz.GetReceiptBillSet(model, bizCust, chargePayType, channelFee, compareCodeForCheck);
                repo.Create(set);
            }
            repo.CommitData(FuncAction.Create);
        }
        /// <summary>
        /// 
        /// </summary>
        void IImportData.MoveToSuccessFolder()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        void IImportData.MoveToFailFolder()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    /// <summary>
    /// 資訊流導入-郵局
    /// </summary>
    public class ReceiptInfoImportPOST : IImportData
    {
        #region Property
        /// <summary>
        /// 
        /// </summary>
        private const int StrLen = 110;
        /// <summary>
        /// 
        /// </summary>
        public ApplicationDbContext DataAccess { get; }
        /// <summary>
        /// 
        /// </summary>
        public MessageLog Message { get; }
        #endregion
        #region Construct
        public ReceiptInfoImportPOST(ApplicationDbContext dataAccess) { DataAccess = dataAccess; }
        #endregion
        #region Public
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="importBatchNo"></param>
        /// <returns></returns>
        public ReceiptInfoBillPostModel AnalyzeSource(int line, string source, string importBatchNo)
        {
            return new ReceiptInfoBillPostModel()
            {
                Id = line,
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
        #endregion
        #region Implement
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<int, string> IImportData.ReadFile()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            string filePath = "", strRow;
            using StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding(950));
            int line = 0;
            while (sr.Peek() > 0)
            {
                strRow = sr.ReadLine();
                line++;
                if (0 == LibData.ByteLen(strRow)) continue;
                if (StrLen != strRow.Length) { /*第N行 Error:長度不符*/}
                result.Add(line, strRow);
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
            List<ReceiptInfoBillPostModel> result = new List<ReceiptInfoBillPostModel>();
            DateTime now = DateTime.Now;
            string importBatchNo = $"POST{now.ToString("yyyyMMddhhmmss")}";
            foreach (int line in sources.Keys)
                result.Add(AnalyzeSource(line, sources[line], importBatchNo));
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelSources"></param>
        void IImportData.CreateData(IList modelSources)
        {
            List<ReceiptInfoBillPostModel> models = modelSources as List<ReceiptInfoBillPostModel>;
            using BizReceiptInfoBillPOST biz = new BizReceiptInfoBillPOST(Message);
            using ReceiptBillRepository repo = new ReceiptBillRepository(DataAccess);
            foreach (var model in models)
            {
                biz.CheckData(model);
                BizCustomerSet bizCust = ReceiptInfoImportComm.GetBizCustomerSet(DataAccess, Message, model.CompareCode.Substring(7).TrimStart('0'), out string compareCodeForCheck);
                ReceiptInfoImportComm.GetCollectionTypeSet(DataAccess, model.CollectionType, model.Channel, model.Amount.ToDecimal(), out ChargePayType chargePayType, out decimal channelFee);
                repo.Create(biz.GetReceiptBillSet(model, bizCust, chargePayType, channelFee, compareCodeForCheck));
            }
            repo.CommitData(FuncAction.Create);
        }

        void IImportData.MoveToSuccessFolder()
        {
            throw new NotImplementedException();
        }

        void IImportData.MoveToFailFolder()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    /// <summary>
    /// 資訊流導入-超商
    /// </summary>
    public class ReceiptInfoImportMARKET : IImportData
    {
        #region Property
        /// <summary>
        /// 
        /// </summary>1
        private const int StrLen = 120;
        /// <summary>
        /// 
        /// </summary>
        public ApplicationDbContext DataAccess { get; }
        /// <summary>
        /// 
        /// </summary>
        public MessageLog Message { get; }
        #endregion
        #region Construct
        public ReceiptInfoImportMARKET(ApplicationDbContext dataAccess) { DataAccess = dataAccess; }
        #endregion
        #region Public
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="importBatchNo"></param>
        /// <returns></returns>
        public ReceiptInfoBillMarketModel AnalyzeSource(int line, string source, string importBatchNo)
        {
            return new ReceiptInfoBillMarketModel()
            {
                Id = line,
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
        #endregion
        #region Implement
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<int, string> IImportData.ReadFile()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            string filePath = "", strRow;
            using StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding(950));
            int line = 0;
            while (sr.Peek() > 0)
            {
                strRow = sr.ReadLine();
                line++;
                if (0 == strRow.Length) continue;
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
            List<ReceiptInfoBillMarketModel> result = new List<ReceiptInfoBillMarketModel>();
            DateTime now = DateTime.Now;
            string importBatchNo = $"MARKET{now.ToString("yyyyMMddhhmmss")}";
            foreach (int line in sources.Keys)
                result.Add(AnalyzeSource(line, sources[line], importBatchNo));
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelSources"></param>
        void IImportData.CreateData(IList modelSources)
        {
            List<ReceiptInfoBillMarketModel> models = modelSources as List<ReceiptInfoBillMarketModel>;
            using BizReceiptInfoBillMARKET biz = new BizReceiptInfoBillMARKET(Message);
            using ReceiptBillRepository repo = new ReceiptBillRepository(DataAccess);
            foreach (var model in models)
            {
                biz.CheckData(model);
                BizCustomerSet bizCust = ReceiptInfoImportComm.GetBizCustomerSet(DataAccess, Message, model.Barcode2.TrimStart('0'), out string compareCodeForCheck);
                ReceiptInfoImportComm.GetCollectionTypeSet(DataAccess, model.CollectionType, model.Channel, model.Barcode3.ToDecimal(), out ChargePayType chargePayType, out decimal channelFee);
                repo.Create(biz.GetReceiptBillSet(model, bizCust, chargePayType, channelFee, compareCodeForCheck));
            }
            repo.CommitData(FuncAction.Create);
        }

        void IImportData.MoveToSuccessFolder()
        {
            throw new NotImplementedException();
        }

        void IImportData.MoveToFailFolder()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    /// <summary>
    /// 資訊流導入-超商產險
    /// </summary>
    public class ReceiptInfoImportMARKETSPI : IImportData
    {
        #region Property
        /// <summary>
        /// 
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
        #endregion
        #region Construct
        public ReceiptInfoImportMARKETSPI(ApplicationDbContext dataAccess) { DataAccess = dataAccess; }
        #endregion
        #region Public
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="importBatchNo"></param>
        /// <returns></returns>
        public ReceiptInfoBillMarketSPIModel AnalyzeSource(int line, string source, string importBatchNo)
        {
            return new ReceiptInfoBillMarketSPIModel()
            {
                Id = line,
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
        #endregion
        #region Implement
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<int, string> IImportData.ReadFile()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            string filePath = "", strRow;
            using StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding(950));
            int line = 0;
            while (sr.Peek() > 0)
            {
                strRow = sr.ReadLine();
                line++;
                if (0 == strRow.Length) continue;
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
            List<ReceiptInfoBillMarketSPIModel> result = new List<ReceiptInfoBillMarketSPIModel>();
            DateTime now = DateTime.Now;
            string importBatchNo = $"MARKET{now.ToString("yyyyMMddhhmmss")}";
            foreach (int line in sources.Keys)
                result.Add(AnalyzeSource(line, sources[line], importBatchNo));
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelSources"></param>
        void IImportData.CreateData(IList modelSources)
        {
            List<ReceiptInfoBillMarketSPIModel> models = modelSources as List<ReceiptInfoBillMarketSPIModel>;
            using BizReceiptInfoBillMARKETSPI biz = new BizReceiptInfoBillMARKETSPI(Message);
            using ReceiptBillRepository repo = new ReceiptBillRepository(DataAccess);
            foreach (var model in models)
            {
                biz.CheckData(model);
                BizCustomerSet bizCust = ReceiptInfoImportComm.GetBizCustomerSet(DataAccess, Message, model.Barcode2.TrimStart('0'), out string compareCodeForCheck);
                ReceiptInfoImportComm.GetCollectionTypeSet(DataAccess, model.ISC.Trim(), model.Channel, model.Barcode3_Amount.ToDecimal(), out ChargePayType chargePayType, out decimal channelFee);
                repo.Create(biz.GetReceiptBillSet(model, bizCust, chargePayType, channelFee, compareCodeForCheck));
            }
            repo.CommitData(FuncAction.Create);
        }

        void IImportData.MoveToSuccessFolder()
        {
            throw new NotImplementedException();
        }

        void IImportData.MoveToFailFolder()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    /// <summary>
    /// 資訊流導入-農金
    /// </summary>
    public class ReceiptInfoImportFARM : IImportData
    {
        #region Property
        /// <summary>
        /// 
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
        #endregion
        #region Construct
        public ReceiptInfoImportFARM(ApplicationDbContext dataAccess) { DataAccess = dataAccess; }
        #endregion
        #region Public
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="importBatchNo"></param>
        /// <returns></returns>
        public ReceiptInfoBillFarmModel AnalyzeSource(int line, string source, string importBatchNo)
        {
            return new ReceiptInfoBillFarmModel()
            {
                Id = line,
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
        #endregion
        #region Implement
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<int, string> IImportData.ReadFile()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            string filePath = "", strRow;
            using StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding(950));
            int line = 0;
            while (sr.Peek() > 0)
            {
                strRow = sr.ReadLine();
                line++;
                if (0 == strRow.Length) continue;
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
                result.Add(AnalyzeSource(line, sources[line], importBatchNo));
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelSources"></param>
        void IImportData.CreateData(IList modelSources)
        {
            List<ReceiptInfoBillFarmModel> models = modelSources as List<ReceiptInfoBillFarmModel>;
            using BizReceiptInfoBillFARM biz = new BizReceiptInfoBillFARM(Message);
            using ReceiptBillRepository repo = new ReceiptBillRepository(DataAccess);
            foreach (var model in models)
            {
                biz.CheckData(model);
                BizCustomerSet bizCust = ReceiptInfoImportComm.GetBizCustomerSet(DataAccess, Message, model.Barcode2.TrimStart('0'), out string compareCodeForCheck);
                ReceiptInfoImportComm.GetCollectionTypeSet(DataAccess, model.CollectionType, model.Channel, model.Barcode3.ToDecimal(), out ChargePayType chargePayType, out decimal channelFee);
                repo.Create(biz.GetReceiptBillSet(model, bizCust, chargePayType, channelFee, compareCodeForCheck));
            }
            repo.CommitData(FuncAction.Create);
        }

        void IImportData.MoveToSuccessFolder()
        {
            throw new NotImplementedException();
        }

        void IImportData.MoveToFailFolder()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    /// <summary>
    /// 資訊流導入-共用方法
    /// </summary>
    public static class ReceiptInfoImportComm
    {
        internal static BizCustomerSet GetBizCustomerSet(ApplicationDbContext dataAccess, MessageLog message, string compareCode, out string compareCodeForCheck)
        {
            compareCodeForCheck = string.Empty;
            using BizCustomerRepository biz = new BizCustomerRepository(dataAccess) { Message = message };
            var bizCust = biz.QueryData(new object[] { compareCode.Substring(0, 6) });
            if (null == bizCust)
                bizCust = biz.QueryData(new object[] { compareCode.Substring(0, 4) });
            if (null == bizCust)
                bizCust = biz.QueryData(new object[] { compareCode.Substring(0, 3) });
            if (null == bizCust) return null;
            if (bizCust.BizCustomer.AccountStatus == AccountStatus.Unable) compareCodeForCheck = compareCode;
            else compareCodeForCheck = bizCust.BizCustomer.VirtualAccount3 == VirtualAccount3.NoverifyCode ? compareCode : compareCode[0..^1];
            return bizCust;
        }
        internal static void GetCollectionTypeSet(ApplicationDbContext DataAccess, string collectionTypeId, string channelId, decimal amount, out ChargePayType chargePayType, out decimal channelFee)
        {
            channelFee = 0;
            chargePayType = DataAccess.Set<CollectionTypeModel>().Find(collectionTypeId).ChargePayType;
            var c = DataAccess.Set<CollectionTypeDetailModel>().Where("CollectionTypeId={0} And ChannelId={1} And {2} Between SRange And ERange", collectionTypeId, channelId, amount);
            if (null != c) channelFee = ((List<CollectionTypeDetailModel>)c)[0].Fee;
        }
    }
}