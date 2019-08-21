﻿using System.Collections.Generic;
using System.ComponentModel;
using GraphQL;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;

namespace SKGPortalCore.Data
{
    public class MessageLog
    {
        //private string MessageCode;
        private string Message;
        private readonly ExecutionErrors Errors;
        public List<ExecutionError> ErrorMessages = new List<ExecutionError>();
        public MessageLog(ExecutionErrors errors)
        {
            Errors = errors;
        }

        public void AddErrorMessage(MessageCode messageCode, params object[] args)
        {
            //MessageCode MessageCode = messageCode;
            Message = string.Format($"{messageCode}:{ResxManage.GetDescription(messageCode)}", args);
            Errors.Add(new ExecutionError(Message));
            //ErrorLog
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
        /// 銀行銷帳編號長度不符，應為{0}碼，但不前產生資料為{1}碼，請檢查客戶參數設定與代收申請書設定是否正確。
        /// </summary>
        [Description("銀行銷帳編號長度不符，應為{0}碼，但不前產生資料為{1}碼，請檢查客戶參數設定與代收申請書設定是否正確。")]
        Code1007,
        /// <summary>
        /// 銷帳編號{0}已存在，請確認！
        /// </summary>
        [Description("銷帳編號{0}已存在，請確認！")]
        Code1008,
    }
}
