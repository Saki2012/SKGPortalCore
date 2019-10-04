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
        public ApplicationDbContext DataAccess { get; }
        /// <summary>
        /// 
        /// </summary>
        public MessageLog Message { get; }
        /// <summary>
        /// 
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
        private string SrcFile { get { return $"{SrcPath}{FileName}.{DateTime.Now.ToString("yyyyMMdd")}"; } }
        /// <summary>
        /// 成功資料
        /// </summary>
        private string SuccessFile { get { return $"{SuccessPath}{FileName}.{DateTime.Now.ToString("yyyyMMdd")}{LibData.GenRandomString(3)}"; } }
        /// <summary>
        /// 失敗資料
        /// </summary>
        private string FailFile { get { return $"{FailPath}{FileName}.{DateTime.Now.ToString("yyyyMMdd")}{LibData.GenRandomString(3)}"; } }

        #endregion
        #region Construct
        public ReceiptInfoImportBANK(ApplicationDbContext dataAccess)
        {
            DataAccess = dataAccess;
            Message = new MessageLog(SystemOperator.SysOperator);
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
                result.Add(new ReceiptInfoBillBankModel() { Id = line, Source = sources[line], ImportBatchNo = importBatchNo });
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelSources"></param>
        void IImportData.CreateData(IList modelSources)
        {
            List<ReceiptInfoBillBankModel> models = modelSources as List<ReceiptInfoBillBankModel>;
            using BizReceiptInfoBillBANK biz = new BizReceiptInfoBillBANK(Message, DataAccess);
            using ReceiptBillRepository repo = new ReceiptBillRepository(DataAccess) { Message = Message, User = SystemOperator.SysOperator };
            foreach (var model in models)
            {
                biz.CheckData(model);
                BizCustomerSet bizCust = ReceiptInfoImportComm.GetBizCustomerSet(DataAccess, Message, model.CompareCode, out string compareCodeForCheck);
                biz.GetCollectionTypeSet("Bank999", model.Channel, model.Amount.ToDecimal(), out ChargePayType chargePayType, out decimal channelFee);
                ReceiptBillSet set = biz.GetReceiptBillSet(model, bizCust, chargePayType, channelFee, compareCodeForCheck);
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
                    file = isSuccess ? SuccessFile : FailFile;
                } while (File.Exists(file));
                File.Move(SrcFile, file);
            }
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
        /// <summary>
        /// 檔案名稱
        /// </summary>
        private const string FileName = "SKG_POST";
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
        private string SrcFile { get { return $"{SrcPath}{FileName}.{DateTime.Now.ToString("yyyyMMdd")}"; } }
        /// <summary>
        /// 成功資料
        /// </summary>
        private string SuccessFile { get { return $"{SuccessPath}{FileName}.{DateTime.Now.ToString("yyyyMMdd")}{LibData.GenRandomString(3)}"; } }
        /// <summary>
        /// 失敗資料
        /// </summary>
        private string FailFile { get { return $"{FailPath}{FileName}.{DateTime.Now.ToString("yyyyMMdd")}{LibData.GenRandomString(3)}"; } }
        #endregion
        #region Construct
        public ReceiptInfoImportPOST(ApplicationDbContext dataAccess) { DataAccess = dataAccess; }
        #endregion
        #region Implement
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<int, string> IImportData.ReadFile()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            string strRow; int line = 0;
            using StreamReader sr = new StreamReader(SrcFile, Encoding.GetEncoding(950));
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
                result.Add(new ReceiptInfoBillPostModel() { Id = line, Source = sources[line], ImportBatchNo = importBatchNo });
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelSources"></param>
        void IImportData.CreateData(IList modelSources)
        {
            List<ReceiptInfoBillPostModel> models = modelSources as List<ReceiptInfoBillPostModel>;
            using BizReceiptInfoBillPOST biz = new BizReceiptInfoBillPOST(Message, DataAccess);
            using ReceiptBillRepository repo = new ReceiptBillRepository(DataAccess) { Message = Message, User = SystemOperator.SysOperator };
            foreach (var model in models)
            {
                biz.CheckData(model);
                BizCustomerSet bizCust = ReceiptInfoImportComm.GetBizCustomerSet(DataAccess, Message, model.CompareCode.Substring(7).TrimStart('0'), out string compareCodeForCheck);
                biz.GetCollectionTypeSet(model.CollectionType, model.Channel, model.Amount.ToDecimal(), out ChargePayType chargePayType, out decimal channelFee);
                repo.Create(biz.GetReceiptBillSet(model, bizCust, chargePayType, channelFee, compareCodeForCheck));
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
        /// <summary>
        /// 檔案名稱
        /// </summary>
        private const string FileName = "SKG_MART";
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
        private string SrcFile { get { return $"{SrcPath}{FileName}.{DateTime.Now.ToString("yyyyMMdd")}"; } }
        /// <summary>
        /// 成功資料
        /// </summary>
        private string SuccessFile { get { return $"{SuccessPath}{FileName}.{DateTime.Now.ToString("yyyyMMdd")}{LibData.GenRandomString(3)}"; } }
        /// <summary>
        /// 失敗資料
        /// </summary>
        private string FailFile { get { return $"{FailPath}{FileName}.{DateTime.Now.ToString("yyyyMMdd")}{LibData.GenRandomString(3)}"; } }
        #endregion
        #region Construct
        public ReceiptInfoImportMARKET(ApplicationDbContext dataAccess) { DataAccess = dataAccess; }
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
                result.Add(new ReceiptInfoBillMarketModel() { Id = line, Source = sources[line], ImportBatchNo = importBatchNo });
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelSources"></param>
        void IImportData.CreateData(IList modelSources)
        {
            List<ReceiptInfoBillMarketModel> models = modelSources as List<ReceiptInfoBillMarketModel>;
            using BizReceiptInfoBillMARKET biz = new BizReceiptInfoBillMARKET(Message, DataAccess);
            using ReceiptBillRepository repo = new ReceiptBillRepository(DataAccess) { User = SystemOperator.SysOperator };
            foreach (var model in models)
            {
                biz.CheckData(model);
                BizCustomerSet bizCust = ReceiptInfoImportComm.GetBizCustomerSet(DataAccess, Message, model.Barcode2.TrimStart('0'), out string compareCodeForCheck);
                biz.GetCollectionTypeSet(model.CollectionType, model.Channel, model.Barcode3.ToDecimal(), out ChargePayType chargePayType, out decimal channelFee);
                repo.Create(biz.GetReceiptBillSet(model, bizCust, chargePayType, channelFee, compareCodeForCheck));
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
        private string SrcFile { get { return $"{SrcPath}{FileName}.{DateTime.Now.ToString("yyyyMMdd")}"; } }
        /// <summary>
        /// 成功資料
        /// </summary>
        private string SuccessFile { get { return $"{SuccessPath}{FileName}.{DateTime.Now.ToString("yyyyMMdd")}{LibData.GenRandomString(3)}"; } }
        /// <summary>
        /// 失敗資料
        /// </summary>
        private string FailFile { get { return $"{FailPath}{FileName}.{DateTime.Now.ToString("yyyyMMdd")}{LibData.GenRandomString(3)}"; } }
        #endregion
        #region Construct
        public ReceiptInfoImportMARKETSPI(ApplicationDbContext dataAccess) { DataAccess = dataAccess; }
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
                result.Add(new ReceiptInfoBillMarketSPIModel() { Id = line, Source = sources[line], ImportBatchNo = importBatchNo });
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelSources"></param>
        void IImportData.CreateData(IList modelSources)
        {
            List<ReceiptInfoBillMarketSPIModel> models = modelSources as List<ReceiptInfoBillMarketSPIModel>;
            using BizReceiptInfoBillMARKETSPI biz = new BizReceiptInfoBillMARKETSPI(Message, DataAccess);
            using ReceiptBillRepository repo = new ReceiptBillRepository(DataAccess) { User = SystemOperator.SysOperator };
            foreach (var model in models)
            {
                biz.CheckData(model);
                BizCustomerSet bizCust = ReceiptInfoImportComm.GetBizCustomerSet(DataAccess, Message, model.Barcode2.TrimStart('0'), out string compareCodeForCheck);
                biz.GetCollectionTypeSet(model.ISC.Trim(), model.Channel, model.Barcode3_Amount.ToDecimal(), out ChargePayType chargePayType, out decimal channelFee);
                repo.Create(biz.GetReceiptBillSet(model, bizCust, chargePayType, channelFee, compareCodeForCheck));
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
        private string SrcFile { get { return $"{SrcPath}{FileName}.{DateTime.Now.ToString("yyyyMMdd")}"; } }
        /// <summary>
        /// 成功資料
        /// </summary>
        private string SuccessFile { get { return $"{SuccessPath}{FileName}.{DateTime.Now.ToString("yyyyMMdd")}{LibData.GenRandomString(3)}"; } }
        /// <summary>
        /// 失敗資料
        /// </summary>
        private string FailFile { get { return $"{FailPath}{FileName}.{DateTime.Now.ToString("yyyyMMdd")}{LibData.GenRandomString(3)}"; } }
        #endregion
        #region Construct
        public ReceiptInfoImportFARM(ApplicationDbContext dataAccess) { DataAccess = dataAccess; }
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
                result.Add(new ReceiptInfoBillFarmModel() { Id = line, Source = sources[line], ImportBatchNo = importBatchNo });
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelSources"></param>
        void IImportData.CreateData(IList modelSources)
        {
            List<ReceiptInfoBillFarmModel> models = modelSources as List<ReceiptInfoBillFarmModel>;
            using BizReceiptInfoBillFARM biz = new BizReceiptInfoBillFARM(Message, DataAccess);
            using ReceiptBillRepository repo = new ReceiptBillRepository(DataAccess) { User = SystemOperator.SysOperator };
            foreach (var model in models)
            {
                biz.CheckData(model);
                BizCustomerSet bizCust = ReceiptInfoImportComm.GetBizCustomerSet(DataAccess, Message, model.Barcode2.TrimStart('0'), out string compareCodeForCheck);
                biz.GetCollectionTypeSet(model.CollectionType, model.Channel, model.Barcode3.ToDecimal(), out ChargePayType chargePayType, out decimal channelFee);
                repo.Create(biz.GetReceiptBillSet(model, bizCust, chargePayType, channelFee, compareCodeForCheck));
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
    }
}
