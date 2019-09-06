using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using GraphQL;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData.OperateSystem;

namespace SKGPortalCore.Data
{
    /// <summary>
    /// 訊息紀錄
    /// </summary>
    public class MessageLog
    {
        #region Property
        private static readonly Mutex mut = new Mutex();
        private IUserModel User { get; set; }
        private string ErrStack
        {
            get
            {
                StackTrace stack = new StackTrace(2, true);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine();
                foreach (var flame in stack.GetFrames())
                {
                    if (null != flame.GetFileName())
                        sb.Append(new StackTrace(flame).ToString());
                }
                return sb.ToString();
            }
        }
        private readonly string LogPath;
        private readonly string LogFileName;
        public ExecutionErrors Errors { get; }
        /// <summary>
        /// 錯誤訊息前綴
        /// </summary>
        public string Prefix { get; set; }
        #endregion
        #region Construct
        public MessageLog(IUserModel user, string logPath = @"./Log/", string logFileName = "SKGPortalCore")
        {
            Errors = new ExecutionErrors();
            LogPath = logPath;
            LogFileName = logFileName;
            Prefix = string.Empty;
            User = user ?? SystemOperator.SysOperator;
        }
        #endregion
        #region Public
        public void AddErrorMessage(MessageCode messageCode, params object[] args)
        {
            var err = new ExecutionError(string.Format($"{Prefix}{messageCode}:{ResxManage.GetDescription(messageCode)}", args)) { Code = "CustomerMessageCode", Source = ErrStack };
            Errors.Add(err);
        }
        /// <summary>
        /// 獲取異常狀況
        /// </summary>
        /// <param name="ex"></param>
        public void AddExceptionError(Exception ex)
        {
            var innerEx = ex.GetInnermostException();
            var exErr = new ExecutionError("異常發生，請洽客服人員", innerEx) { Source = innerEx.ToString() };
            Errors.Add(exErr);
        }
        /// <summary>
        /// 異常紀錄寫入檔案內
        /// </summary>
        public void WriteLogTxt()
        {
            if (Errors.Count == 0) return;
            DateTime now = DateTime.Now;
            StringBuilder str = new StringBuilder();
            foreach (var msg in Errors)
            {
                str.AppendLine($"{now.ToString()} User:{User.KeyId}, {User.UserName} Message:{msg.Message}");
                if (null != msg.Source)
                {
                    if (msg.Code.CompareTo("CustomerMessageCode") != 0) //略過一般操作上錯誤StackMessage，若有需要看其Stack可註解掉。
                        str.AppendLine($" Stack Message:{msg.Source}");
                }
            }
            if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
            }
            mut.WaitOne();
            try
            {
                using StreamWriter sw = new StreamWriter($"{LogPath}{LogFileName}{now.ToString("yyyyMMdd")}.log", true);
                sw.Write(str.ToString());
                sw.Close();
            }
            finally
            {
                mut.ReleaseMutex();
            }
        }
        #endregion
    }

    public enum MessageCode
    {
        /// <summary>
        /// {0}
        /// </summary>
        [Description("{0}")]
        Code0000,
        /// <summary>
        /// {0}不允許為空
        /// </summary>
        [Description("{0}不允許為空")]
        Code0001,
        /// <summary>
        /// 您沒有權限對{0}進行{1}
        /// </summary>
        [Description("您沒有權限對{0}進行{1}")]
        Code0002,
        /// <summary>
        /// 銀行銷帳編號長度({0})與組合方式({1})不符!
        /// </summary>
        [Description("銀行銷帳編號長度({0})與組合方式({1})不符!")]
        Code1001,
        /// <summary>
        /// 檢核方式錯誤!
        /// </summary>
        [Description("檢核方式錯誤!")]
        Code1002,
        /// <summary>
        /// 第{0}行：資料長度不符{1}碼
        /// </summary>
        [Description("第{0}行：資料長度不符{1}碼")]
        Code1003,
        /// <summary>
        /// 檔案不存在！({0})
        /// </summary>
        [Description("檔案不存在！({0})")]
        Code1004,
        /// <summary>
        /// {0}長度不符，應為{1}碼！
        /// </summary>
        [Description("{0}長度不符，應為{1}碼！")]
        Code1005,
        /// <summary>
        /// {0}只能輸入數字！
        /// </summary>
        [Description("{0}只能輸入數字！")]
        Code1006,
        /// <summary>
        /// 銀行銷帳編號長度不符，應為{0}碼，但目前產生資料為{1}碼，請檢查客戶參數設定與代收申請書設定是否正確。
        /// </summary>
        [Description("銀行銷帳編號長度不符，應為{0}碼，但目前產生資料為{1}碼，請檢查客戶參數設定與代收申請書設定是否正確。")]
        Code1007,
        /// <summary>
        /// 銷帳編號{0}已存在，請確認！
        /// </summary>
        [Description("銷帳編號{0}已存在，請確認！")]
        Code1008,
        /// <summary>
        /// 第{0}行：{1} {2} 不為有效數字
        /// </summary>
        [Description("第{0}行：{1} {2} 不為有效數字")]
        Code1009,
        /// <summary>
        /// 第{0}行：{1} 不允許為空
        /// </summary>
        [Description("第{0}行：{1} 不允許為空")]
        Code1010,
        /// <summary>
        /// 第{0}行：{1} 不為有效日期時間
        /// </summary>
        [Description("第{0}行：{1} 不為有效日期時間")]
        Code1011,
        /// <summary>
        /// 第{0}行：存提別不為±
        /// </summary>
        [Description("第{0}行：存提別不為±")]
        Code1012,
    }
}
