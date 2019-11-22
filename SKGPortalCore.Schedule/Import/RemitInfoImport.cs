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

namespace SKGPortalCore.Schedule.Import
{
    public class RemitInfoImport : IImportData
    {
        #region Property
        public ApplicationDbContext DataAccess { get; }
        public MessageLog Message { get; }
        /// <summary>
        /// 資訊流長度(byte)
        /// </summary>
        private const int StrLen = 128;
        /// <summary>
        /// 檔案名稱
        /// </summary>
        private const string FileName = "SKG_RT";
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
        public RemitInfoImport(ApplicationDbContext dataAccess, MessageLog messageLog = null)
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
        /// 讀資訊流檔
        /// </summary>
        /// <returns></returns>
        Dictionary<int, string> IImportData.ReadFile()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            string strRow; int line = 1;
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
            {
                result.Add(new RemitInfoModel() { Id = line, Source = sources[line], ImportBatchNo = importBatchNo });
            }

            return result;
        }
        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="modelSources"></param>
        void IImportData.CreateData(IList modelSources)
        {
            List<RemitInfoModel> srcs = modelSources as List<RemitInfoModel>;
            using CashFlowBillRepository repo = new CashFlowBillRepository(DataAccess) { User = SystemOperator.SysOperator };
            foreach (RemitInfoModel model in srcs)
            {
                CashFlowBillSet set = BizRemitInfo.GetCashFlowBillSet(model);
                repo.Create(set);
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
