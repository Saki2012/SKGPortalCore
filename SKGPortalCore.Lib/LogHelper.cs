using GraphQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace SKGPortalCore.Lib
{
    public class LogHelper
    {
        private string Message;
        private string MessageCode;
        private ExecutionErrors Errors;
        public List<ExecutionError> ErrorMessages = new List<ExecutionError>();
        public LogHelper(ExecutionErrors errors)
        {
            Errors = errors;
        }

        public void AddErrorMessage(MessageCode messageCode, params object[] args)
        {
            MessageCode MessageCode = messageCode;
            Message = string.Format($"{messageCode}:{ResxManage.GetDescription(messageCode)}", args);
            Errors.Add(new ExecutionError(Message));
        }
    }

    public enum MessageCode
    {
        /// <summary>
        /// {0}不允許為空
        /// </summary>
        [Description("{0}不允許為空")]
        Code0001,
        /// <summary>
        /// 您沒有權限執行{0}
        /// </summary>
        [Description("您沒有權限執行{0}")]
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
    }
}
