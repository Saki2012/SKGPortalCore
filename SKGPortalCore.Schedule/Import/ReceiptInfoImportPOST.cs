using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SKGPortalCore.Business.BillData;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Repository.BillData;
namespace SKGPortalCore.Schedule.Import
{
    /// <summary>
    /// 資訊流導入-郵局
    /// </summary>
    public class ReceiptInfoImportPOST : IImportData
    {
        #region Property
        /// <summary>
        /// 資訊流長度(byte)
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
        public ReceiptInfoImportPOST(ApplicationDbContext dataAccess, MessageLog messageLog = null)
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
            string strRow; int line = 0;
            using StreamReader sr = new StreamReader(SrcFile, Encoding.GetEncoding(950));
            while (sr.Peek() > 0)
            {
                strRow = sr.ReadLine();
                line++;
                if (0 == LibData.ByteLen(strRow))
                {
                    continue;
                }

                if (StrLen != strRow.Length) { /*第N行 Error:長度不符*/}
                result.Add(line, strRow);
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
            List<ReceiptInfoBillPostModel> result = new List<ReceiptInfoBillPostModel>();
            DateTime now = DateTime.Now;
            string importBatchNo = $"POST{now.ToString("yyyyMMddhhmmss")}";
            foreach (int line in sources.Keys)
            {
                result.Add(new ReceiptInfoBillPostModel() { Id = line, Source = sources[line], ImportBatchNo = importBatchNo });
            }

            return result;
        }
        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="modelSources"></param>
        void IImportData.CreateData(IList modelSources)
        {
            List<ReceiptInfoBillPostModel> models = modelSources as List<ReceiptInfoBillPostModel>;
            using BizReceiptInfoBillPOST biz = new BizReceiptInfoBillPOST(Message, DataAccess);
            using ReceiptBillRepository repo = new ReceiptBillRepository(DataAccess) { Message = Message, User = SystemOperator.SysOperator };
            foreach (ReceiptInfoBillPostModel model in models)
            {
                biz.CheckData(model);
                BizCustomerSet bizCust = ReceiptInfoImportComm.GetBizCustomerSet(DataAccess, Message, model.CompareCode.Substring(7).TrimStart('0'), out string compareCodeForCheck);
                biz.GetCollectionTypeSet(model.CollectionType, model.Channel, model.Amount.ToDecimal(), out ChargePayType chargePayType, out decimal channelFee);
                repo.Create(biz.GetReceiptBillSet(model, bizCust, chargePayType, channelFee, compareCodeForCheck));
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
