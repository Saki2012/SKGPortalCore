using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SKGPortalCore.Business.Func;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Repository.MasterData;

namespace SKGPortalCore.Schedule.Import
{
    public class ACCFTTImport : IImportData
    {
        #region Property
        /// <summary>
        /// 
        /// </summary>
        public ApplicationDbContext DataAccess { get; }
        /// <summary>
        /// 
        /// </summary>
        public MessageLog Message { get; }
        /// <summary>
        /// 資訊流長度(byte)
        /// </summary>
        private const int StrLen = 256;
        /// <summary>
        /// 檔案名稱
        /// </summary>
        private const string FileName = "ACCFTT";
        /// <summary>
        /// 原檔案存放位置
        /// </summary>
        private const string SrcPath = @"D:\iBankRoot\Ftp_SKGPortalCore\ACCFTT\";
        /// <summary>
        /// 成功檔案存放位置
        /// </summary>
        private const string SuccessPath = @"D:\iBankRoot\Ftp_SKGPortalCore\SuccessFolder\ACCFTT\";
        /// <summary>
        /// 失敗檔案存放位置
        /// </summary>
        private const string FailPath = @"D:\iBankRoot\Ftp_SKGPortalCore\ErrorFolder\ACCFTT\";
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
        public ACCFTTImport(ApplicationDbContext dataAccess, MessageLog messageLog = null)
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
        /// 讀服務申請書主檔
        /// </summary>
        /// <returns></returns>
        Dictionary<int, string> IImportData.ReadFile()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            string strRow;
            using StreamReader sr = new StreamReader(SrcFile/*, Encoding.GetEncoding(950)*/);
            int line = 1;
            while (sr.Peek() > 0)
            {
                strRow = sr.ReadLine();
                if (0 == strRow.Length)
                {
                    continue;
                }

                if (StrLen != strRow.ByteLen()) { Message.AddErrorMessage(MessageCode.Code1003, line, StrLen); }
                result.Add(line++, strRow);
            }
            return result;
        }
        /// <summary>
        /// 分析服務申請書主檔
        /// </summary>
        /// <param name="sources"></param>
        /// <returns></returns>
        IList IImportData.AnalyzeFile(Dictionary<int, string> sources)
        {
            List<ACCFTT> result = new List<ACCFTT>();
            DateTime now = DateTime.Now;
            string importBatchNo = $"ACCFTT{now.ToString("yyyyMMddhhmmss")}";
            foreach (int line in sources.Keys)
            {
                result.Add(new ACCFTT() { Id = line, Source = sources[line], ImportBatchNo = importBatchNo });
            }

            return result;
        }
        /// <summary>
        /// 導入商戶資料
        /// </summary>
        /// <param name="modelSources"></param>
        void IImportData.CreateData(IList modelSources)
        {
            List<ACCFTT> srcs = modelSources as List<ACCFTT>;
            using BizACCFTT bizACCFTT = new BizACCFTT(Message);
            using BizCustomerRepository bizCustRepo = new BizCustomerRepository(DataAccess) { Message = Message, User = SystemOperator.SysOperator };
            using CustomerRepository custRepo = new CustomerRepository(DataAccess) { Message = Message, User = SystemOperator.SysOperator };
            using CustUserRepository custUserRepo = new CustUserRepository(DataAccess) { Message = Message, User = SystemOperator.SysOperator };
            foreach (ACCFTT model in srcs)
            {
                Message.Prefix = $"第{model.Id}行:";
                switch (model.APPLYSTAT.ToInt32())
                {
                    case 0:
                        {
                            DataAccess.Database.BeginTransaction();
                            try
                            {
                                GetCustomerInfo(model, bizCustRepo, custRepo, custUserRepo, out BizCustomerSet bizCustomerSet, out CustomerSet customerSet, out CustUserSet custUserSet);
                                if (null == customerSet)
                                {
                                    custRepo.Create(bizACCFTT.SetCustomer(model, customerSet));
                                }
                                else
                                {
                                    custRepo.Update(bizACCFTT.SetCustomer(model, customerSet));
                                }
                                custRepo.CommitData(FuncAction.Create);
                                if (null == bizCustomerSet)
                                {
                                    bizCustRepo.Create(bizACCFTT.SetBizCustomer(model, bizCustomerSet));
                                }
                                else
                                {
                                    bizCustRepo.Update(bizACCFTT.SetBizCustomer(model, bizCustomerSet));
                                }
                                bizCustRepo.CommitData(FuncAction.Create);
                                if (null == custUserSet)
                                {
                                    custUserRepo.Create(bizACCFTT.AddAdminAccount(model));
                                    custUserRepo.CommitData(FuncAction.Create);
                                }
                                DataAccess.Database.CommitTransaction();
                            }
                            catch
                            {
                                DataAccess.Database.RollbackTransaction();
                            }
                        }
                        break;
                    case 1:
                    case 9:
                        UnableBizCustomer(model.KEYNO);
                        break;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
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
        #region Private
        /// <summary>
        /// 獲取客戶資料
        /// </summary>
        /// <param name="model"></param>
        /// <param name="bizCustomerSet"></param>
        /// <param name="customerSet"></param>
        private void GetCustomerInfo(ACCFTT model, BizCustomerRepository bizCustRepo, CustomerRepository custRepo, CustUserRepository custUserRepo, out BizCustomerSet bizCustomerSet, out CustomerSet customerSet, out CustUserSet custUserSet)
        {
            bizCustomerSet = bizCustRepo.QueryData(new object[] { model.KEYNO });
            customerSet = custRepo.QueryData(new object[] { model.IDCODE.TrimStart('0') });
            custUserSet = custUserRepo.QueryData(new object[] { $"{model.IDCODE.TrimStart('0')},admin" });
        }
        /// <summary>
        /// 停用商戶
        /// </summary>
        /// <param name="customerCode"></param>
        private void UnableBizCustomer(string customerCode)
        {
            BizCustomerModel bizCustomer = DataAccess.Set<BizCustomerModel>().Find(customerCode);
            if (null != bizCustomer)
            {
                bizCustomer.AccountStatus = AccountStatus.Unable;
            }

            DataAccess.Set<BizCustomerModel>().Update(bizCustomer);
        }
        #endregion
    }
}
