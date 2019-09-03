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

namespace SKGPortalCore.Schedule
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
        #endregion
        #region Construct
        public RemitInfoImport(ApplicationDbContext dataAccess) { DataAccess = dataAccess; Message = new MessageLog(SystemOperator.SysOperator); }
        #endregion
        #region Public
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="importBatchNo"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        public RemitInfoModel AnalyzeSource(int line, string source, string importBatchNo)
        {
            return new RemitInfoModel()
            {
                Id = line,
                RemitDate = LibData.ByteSubString(source, 0, 8),
                RemitTime = LibData.ByteSubString(source, 8, 6),
                Channel = LibData.ByteSubString(source, 14, 2),
                CollectionType = LibData.ByteSubString(source, 16, 3),
                Amount = LibData.ByteSubString(source, 19, 11),
                BatchNo = LibData.ByteSubString(source, 30, 2),
                Empty = LibData.ByteSubString(source, 32, 96),
                ImportBatchNo = importBatchNo,
                Source = source
            };
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
                result.Add(AnalyzeSource(line, sources[line], importBatchNo));
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
}
