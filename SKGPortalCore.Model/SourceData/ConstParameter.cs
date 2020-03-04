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
    }
}
