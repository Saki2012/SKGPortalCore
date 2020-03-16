using System;
using System.Collections.Generic;
using System.Text;

namespace SKGPortalCore.Model.SourceData
{
    /// <summary>
    /// 固定參數
    /// </summary>
    public static class ConstParameter
    {
        /// <summary>
        /// 組態路徑
        /// </summary>
        public static readonly string AppSettingsJsonPath = @"D:\Proj\SKGPortalCore\SKGPortalCore";
        /// <summary>
        /// 組態檔名
        /// </summary>
        public static readonly string AppSettingsJson = "appsettings.json";
        /// <summary>
        /// 系統操作
        /// </summary>
        public static readonly string SysOperator = "SysOperator";
        /// <summary>
        /// 銀行-代收類別代號
        /// </summary>
        public static readonly string BankCollectionTypeId = "Bank999";
        /// <summary>
        /// 郵局-代收類別代號
        /// </summary>
        public static readonly string PostCollectionTypeId = "50084884";
        /// <summary>
        /// 單據編號長度
        /// </summary>
        public const int BillNoLen = 30;
        /// <summary>
        /// 資料主鍵長度
        /// </summary>
        public const int DataIdLen = 15;
        /// <summary>
        /// 一般資料長度
        /// </summary>
        public const int NormalLen = 20;
        /// <summary>
        /// 大筆資料長度
        /// </summary>
        public const int LongLen = 100;
    }
}
