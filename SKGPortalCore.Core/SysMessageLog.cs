﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using GraphQL;
using SKGPortalCore.Core.Libary;
using SKGPortalCore.Core.Model.User;

namespace SKGPortalCore.Core
{
    /// <summary>
    /// 訊息紀錄系統
    /// </summary>
    public class SysMessageLog
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
                foreach (StackFrame flame in stack.GetFrames())
                {
                    if (null != flame.GetFileName())
                    {
                        sb.Append(new StackTrace(flame).ToString());
                    }
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
        public List<MessageCode> MsgCodeList { get; set; } = new List<MessageCode>();
        #endregion
        #region Construct
        public SysMessageLog(IUserModel user, string logPath = SystemCP.LogDefaultPath, string logFileName = SystemCP.LogDefaultFileName)
        {
            Errors = new ExecutionErrors();
            LogPath = logPath;
            LogFileName = logFileName;
            Prefix = string.Empty;
            User = user ?? SystemOperator.SysOperator;
        }
        #endregion
        #region Public
        /// <summary>
        /// 添加自定義錯誤訊息
        /// </summary>
        /// <param name="messageCode"></param>
        /// <param name="args"></param>
        public void AddCustErrorMessage(MessageCode messageCode, params object[] args)
        {
            ExecutionError err = new ExecutionError(string.Format($"{Prefix}{messageCode}:{ResxManage.GetDescription(messageCode)}", args)) { Code = SystemCP.CustomerMessageCode, Source = ErrStack };
            Errors.Add(err);
            MsgCodeList.Add(messageCode);
        }
        /// <summary>
        /// 添加異常狀況
        /// </summary>
        /// <param name="ex"></param>
        public void AddExceptionError(Exception ex)
        {
            Exception innerEx = ex.GetInnermostException();
            ExecutionError exErr = new ExecutionError(SystemCP.ExpectionMessage, innerEx) { Source = innerEx.ToString() };
            Errors.Add(exErr);
        }
        /// <summary>
        /// 異常紀錄寫入檔案內
        /// </summary>
        public void WriteLogTxt()
        {
            if (Errors.Count == 0)
            {
                return;
            }

            DateTime now = DateTime.Now;
            StringBuilder str = new StringBuilder();
            foreach (ExecutionError msg in Errors)
            {
                str.AppendLine($"{now.ToString()} User:{User.KeyId}, {User.UserName} Message:{msg.Message}");
                if (null != msg.Source)
                {
                    if (msg.Code.CompareTo(SystemCP.CustomerMessageCode) != 0) //略過一般操作上錯誤StackMessage，若有需要看其Stack可註解掉。
                    {
                        str.AppendLine($" Stack Message:{msg.Source}");
                    }
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
    /// <summary>
    /// 自定義錯誤訊息
    /// </summary>
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
        /// {0}:{1}已存在，請確認是否將已存在資料進行作廢或結案！
        /// </summary>
        [Description("{0}:{1}已存在，請確認是否將已存在資料進行作廢或結案！")]
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
        /// <summary>
        /// 通路[{0}]收款區間已重疊，請確認！
        /// </summary>
        [Description("通路[{0}]收款區間已重疊，請確認！")]
        Code1013,
        /// <summary>
        /// 通路[{0}]尚未填寫核銷規則，請確認！
        /// </summary>
        [Description("通路[{0}]尚未填寫核銷規則，請確認！")]
        Code1014,
        /// <summary>
        /// 未啟用代收類別「{0}」，請確認！
        /// </summary>
        [Description("未啟用代收類別「{0}」，請確認！")]
        Code1015,
        /// <summary>
        /// 應繳金額不在代收類別「{0}」收費區間內，請確認！
        /// </summary>
        [Description("應繳金額不在代收類別「{0}」收費區間內，請確認！")]
        Code1016,
        /// <summary>
        /// 繳款人「{0}」不為「{1}」類型，請確認！
        /// </summary>
        [Description("繳款人「{0}」不為「{1}」類型，請確認！")]
        Code1017,
        /// <summary>
        /// 系統商/Hitrust用戶只能擇一，請確認「商戶手續費管理明細」設置。
        /// </summary>
        [Description("系統商/Hitrust用戶只能擇一，請確認「商戶手續費管理明細」設置。")]
        Code1018,
        /// <summary>
        /// 介紹商企業代號請選擇商戶類行是「{0}」的企業代號
        /// </summary>
        [Description("介紹商企業代號請選擇商戶類行是「{0}」的企業代號")]
        Code1019,
        /// <summary>
        /// 您尚未登入帳號，請重新登入。
        /// </summary>
        [Description("您尚未登入帳號，請重新登入。")]
        Code1020,
    }
}
