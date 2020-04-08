using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SKGPortalCore.Model.SourceData
{
    /// <summary>
    /// 固定參數
    /// </summary>
    public static class CP
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

        #region Model
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
        #endregion

        #region GraphQL
        /// <summary>
        /// 查看表單
        /// </summary>
        [Description("查看表單")] public static readonly string GQL_QueryData = "queryData";
        /// <summary>
        /// 清單列表查詢
        /// </summary>
        [Description("清單列表查詢")] public static readonly string GQL_QueryList = "queryList";
        /// <summary>
        /// 新增表單
        /// </summary>
        [Description("新增表單")] public static readonly string GQL_Create = "create";
        /// <summary>
        /// 修改表單
        /// </summary>
        [Description("修改表單")] public static readonly string GQL_Update = "update";
        /// <summary>
        /// 刪除表單
        /// </summary>
        [Description("刪除表單")] public static readonly string GQL_Delete = "delete";
        /// <summary>
        /// 審核表單
        /// </summary>
        [Description("審核表單")] public static readonly string GQL_Approve = "approve";
        /// <summary>
        /// 作廢表單
        /// </summary>
        [Description("作廢表單")] public static readonly string GQL_Invalid = "invalid";
        /// <summary>
        /// 結案表單
        /// </summary>
        [Description("結案表單")] public static readonly string GQL_EndCase = "endCase";
        /// <summary>
        /// 表單資料
        /// </summary>
        [Description("表單資料")] public static readonly string GQL_Set = "set";
        /// <summary>
        /// 主鍵
        /// </summary>
        [Description("主鍵")] public static readonly string GQL_KeyVal = "keyVal";
        /// <summary>
        /// Json Web Token
        /// </summary>
        [Description("Json Web Token")] public static readonly string GQL_JWT = "jwt";
        /// <summary>
        /// 過濾條件
        /// </summary>
        [Description("過濾條件")] public static readonly string GQL_Condition = "condition";
        /// <summary>
        /// 狀態
        /// </summary>
        [Description("狀態")] public static readonly string GQL_Status = "status";
        #endregion
    }
}
