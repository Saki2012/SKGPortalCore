using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SKGPortalCore.Business.BillData;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Repository.BillData;

namespace SKGPortalCore.Schedule
{
    public class RemitInfoImport
    {

        private ApplicationDbContext DataAccess;

        /// <summary>
        /// 
        /// </summary>
        private const int StrLen = 128;
        /// <summary>
        /// 執行金流匯款檔導入
        /// </summary>
        public void ExecuteImport()
        {
            Dictionary<int, string> sources = ReadFile();
            List<RemitInfoModel> sets = AnalyzeFile(sources);
            CreateCashFlowBill(sets);
        }
        /// <summary>
        /// 讀資訊流檔
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, string> ReadFile()
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
        private List<RemitInfoModel> AnalyzeFile(Dictionary<int, string> sources)
        {
            List<RemitInfoModel> result = new List<RemitInfoModel>();
            DateTime now = DateTime.Now;
            string importBatchNo = $"RT{now.ToString("yyyyMMddhhmmss")}";
            foreach (int line in sources.Keys)
                result.Add(AnalyzeSource(line, sources[line], importBatchNo));
            return result;
        }
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
        /// <summary>
        /// 新增繳款資訊
        /// </summary>
        /// <param name="modelSources"></param>
        private void CreateCashFlowBill(List<RemitInfoModel> modelSources)
        {
            var msg = new MessageLog(new GraphQL.ExecutionErrors());
            using BizRemitInfo biz = new BizRemitInfo(msg);
            using CashFlowBillRepository repo = new CashFlowBillRepository(DataAccess);
            foreach (var model in modelSources)
            {
                CashFlowBillSet set = biz.GetCashFlowBillSet(model);
                repo.Create(set);
            }
            repo.CommitData(FuncAction.Create);
        }
    }
}
