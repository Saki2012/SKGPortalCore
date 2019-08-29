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
using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Repository.MasterData;

namespace SKGPortalCore.Schedule
{
    public class ACCFTTImport: IImportData
    {
        #region Property
        public ApplicationDbContext DataAccess { get; }
        /// <summary>
        /// 
        /// </summary>
        private const int StrLen = 256;
        #endregion
        #region Construct
        public ACCFTTImport(ApplicationDbContext dataAccess) { DataAccess = dataAccess; }
        #endregion
        #region Public
        /// <summary>
        /// 分析服務申請書主檔
        /// </summary>
        /// <param name="source"></param>
        /// <param name="importBatchNo"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        public ACCFTT AnalyzeSource(string source, int line = 0, string importBatchNo = "")
        {
            return new ACCFTT()
            {
                Id = line,
                KEYNO = source.ByteSubString(0, 6),
                ACCIDNO = source.ByteSubString(6, 13),
                CUSTNAME = source.ByteSubString(19, 40),
                APPBECODE = source.ByteSubString(59, 4),
                BRCODE = source.ByteSubString(63, 4),
                IDCODE = source.ByteSubString(67, 11),
                APPLYDATE = source.ByteSubString(78, 8),
                CHGDATE = source.ByteSubString(86, 8),
                APPLYSTAT = source.ByteSubString(94, 1),
                CHKNUMFLAG = source.ByteSubString(95, 1),
                CHKAMTFLAG = source.ByteSubString(96, 1),
                DUETERM = source.ByteSubString(97, 1),
                CHANNEL = source.ByteSubString(98, 1),
                FEE = source.ByteSubString(99, 3),
                RSTORE1 = source.ByteSubString(102, 1),
                RSTORE2 = source.ByteSubString(103, 1),
                RSTORE3 = source.ByteSubString(104, 1),
                RSTORE4 = source.ByteSubString(105, 1),
                RECVITEM1 = source.ByteSubString(106, 3),
                RECVITEM2 = source.ByteSubString(109, 3),
                RECVITEM3 = source.ByteSubString(112, 3),
                RECVITEM4 = source.ByteSubString(115, 3),
                RECVITEM5 = source.ByteSubString(118, 3),
                ACTFEE = source.ByteSubString(121, 2),
                MARTFEE1 = source.ByteSubString(123, 2),
                MARTFEE2 = source.ByteSubString(125, 2),
                MARTFEE3 = source.ByteSubString(127, 2),
                POSTFLAG = source.ByteSubString(129, 1),
                ACTFEEPT = source.ByteSubString(130, 2),
                POSTFEE = source.ByteSubString(132, 2),
                HIFLAG = source.ByteSubString(134, 1),
                HIFARE = source.ByteSubString(135, 3),
                NETDATE = source.ByteSubString(138, 8),
                AUTOFLAG = source.ByteSubString(146, 1),
                EBFLAG = source.ByteSubString(147, 1),
                EBDATE = source.ByteSubString(148, 8),
                EBFEEFLAG = source.ByteSubString(156, 1),
                EBFEE = source.ByteSubString(157, 3),
                EBACTTYPE = source.ByteSubString(160, 1),
                CHKDUPPAY = source.ByteSubString(161, 1),
                CUSTID = source.ByteSubString(162, 7),
                FUNC = source.ByteSubString(169, 1),
                MAFARE = source.ByteSubString(170, 3),
                NOFARE = source.ByteSubString(173, 3),
                CTBCFLAG = source.ByteSubString(176, 1),
                SHAREBNFTFLG = source.ByteSubString(177, 1),
                SHAREBEFTPERCENT = source.ByteSubString(178, 2),
                ACTFEEBEFT = source.ByteSubString(180, 2),
                ACTFEEMART = source.ByteSubString(182, 2),
                SHAREACTFLG = source.ByteSubString(184, 1),
                ACTPERCENT = source.ByteSubString(185, 2),
                CLEARFEEMART1 = source.ByteSubString(187, 2),
                CLEARFEEMART2 = source.ByteSubString(189, 2),
                CLEARFEEMART3 = source.ByteSubString(191, 2),
                CLEARFEEMART4 = source.ByteSubString(193, 2),
                CLEARFEEMART5 = source.ByteSubString(195, 2),
                PAYKINDPOST = source.ByteSubString(197, 1),
                ACTFEEPOST = source.ByteSubString(198, 2),
                SHAREPOSTFLG = source.ByteSubString(200, 1),
                POSTPERCENT = source.ByteSubString(201, 2),
                AGRIFLAG = source.ByteSubString(203, 1),
                AGRIFEE = source.ByteSubString(204, 2),
                FILLER = source.ByteSubString(206, 50),
                ImportBatchNo = importBatchNo,
                Source = source
            };
        }
        #endregion
        #region Private
        /// <summary>
        /// 讀服務申請書主檔
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
                result.Add(AnalyzeSource(sources[line], line, importBatchNo));
            return result;
        }
        /// <summary>
        /// 導入商戶資料
        /// </summary>
        /// <param name="modelSources"></param>
        void IImportData.CreateData(IList modelSources)
        {
            List<ACCFTT> srcs = modelSources as List<ACCFTT>;
            BizCustomerRepository bizCustRepo = new BizCustomerRepository(DataAccess);
            CustomerRepository custRepo = new CustomerRepository(DataAccess);
            foreach (ACCFTT model in srcs)
            {
                switch (model.APPLYSTAT.ToInt32())
                {
                    case 0:
                        {
                            GetCustomerInfo(model, out BizCustomerSet bizCustomerSet, out CustomerSet customerSet);
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
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="bizCustomerSet"></param>
        /// <param name="customerSet"></param>
        private void GetCustomerInfo(ACCFTT model, out BizCustomerSet bizCustomerSet, out CustomerSet customerSet)
        {
            bizCustomerSet = new BizCustomerSet() { };
            customerSet = new CustomerSet() { };
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
