using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GraphQL;
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
        /// 
        /// </summary>
        private const int StrLen = 256;
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
        public ACCFTTImport(ApplicationDbContext dataAccess)
        {
            DataAccess = dataAccess;
            Message = new MessageLog(SystemOperator.SysOperator);
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
            using StreamReader sr = new StreamReader(SrcFile, Encoding.GetEncoding(950));
            int line = 1;
            while (sr.Peek() > 0)
            {
                strRow = sr.ReadLine();
                line++;
                if (0 == strRow.Length) continue;
                if (StrLen != strRow.ByteLen()) { Message.AddErrorMessage(MessageCode.Code1003, line, StrLen); }
                result.Add(line, strRow);
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
                result.Add(new ACCFTT() { Id = line, Source = sources[line], ImportBatchNo = importBatchNo });
            return result;
        }
        /// <summary>
        /// 導入商戶資料
        /// </summary>
        /// <param name="modelSources"></param>
        void IImportData.CreateData(IList modelSources)
        {
            List<ACCFTT> srcs = modelSources as List<ACCFTT>;
            BizCustomerRepository bizCustRepo = new BizCustomerRepository(DataAccess) { Message = Message };
            CustomerRepository custRepo = new CustomerRepository(DataAccess) { Message = Message };
            foreach (ACCFTT model in srcs)
            {
                Message.Prefix = $"第{model.Id}行:";
                switch (model.APPLYSTAT.ToInt32())
                {
                    case 0:
                        {
                            GetCustomerInfo(model, bizCustRepo, custRepo, out BizCustomerSet bizCustomerSet, out CustomerSet customerSet);
                            if (CheckCustExist(model.IDCODE))
                                custRepo.Create(customerSet);
                            else
                                custRepo.Update(customerSet);
                            if (CheckBizCustExist(model.KEYNO))
                                bizCustRepo.Create(bizCustomerSet);
                            else
                                bizCustRepo.Update(bizCustomerSet);
                        }
                        break;
                    case 1:
                    case 9:
                        UnableBizCustomer(model.KEYNO);
                        break;
                }
            }
            bizCustRepo.CommitData(FuncAction.Create);
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
                    file = isSuccess ? SuccFile : FailFile;
                } while (File.Exists(file));
                File.Move(SrcFile, file);
            }
        }
        #endregion
        #region Private
        /// <summary>
        /// 確認商戶資料是否存在
        /// </summary>
        /// <param name="custCode"></param>
        /// <returns></returns>
        private bool CheckBizCustExist(string custCode)
        {
            return null == DataAccess.Set<BizCustomerModel>().Find(custCode);
        }
        /// <summary>
        /// 確認客戶資料是否存在
        /// </summary>
        /// <param name="custId"></param>
        /// <returns></returns>
        private bool CheckCustExist(string custId)
        {
            return null == DataAccess.Set<CustomerModel>().Find(custId);
        }
        /// <summary>
        /// 獲取客戶資料
        /// </summary>
        /// <param name="model"></param>
        /// <param name="bizCustomerSet"></param>
        /// <param name="customerSet"></param>
        private void GetCustomerInfo(ACCFTT model, BizCustomerRepository bizCustRepo, CustomerRepository custRepo, out BizCustomerSet bizCustomerSet, out CustomerSet customerSet)
        {
            bizCustomerSet = bizCustRepo.QueryData(new object[] { model.KEYNO });
            customerSet = custRepo.QueryData(new object[] { model.IDCODE });
        }
        /// <summary>
        /// 停用商戶
        /// </summary>
        /// <param name="customerCode"></param>
        private void UnableBizCustomer(string customerCode)
        {
            BizCustomerModel bizCustomer = DataAccess.Set<BizCustomerModel>().Find(customerCode);
            if (null != bizCustomer) bizCustomer.AccountStatus = AccountStatus.Unable;
            DataAccess.Set<BizCustomerModel>().Update(bizCustomer);
        }
        #endregion
    }
}
