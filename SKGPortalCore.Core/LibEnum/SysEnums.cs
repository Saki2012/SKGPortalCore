using System;
using System.ComponentModel;

namespace SKGPortalCore.Core.LibEnum
{
    /// <summary>
    /// 帳戶狀態
    /// </summary>
    [Description("帳戶狀態")]
    public enum AccountStatus : byte
    {
        /// <summary>
        /// 停用
        /// </summary>
        [Description("停用")] Unable = 0,
        /// <summary>
        /// 啟用
        /// </summary>
        [Description("啟用")] Enable = 1,
        /// <summary>
        /// 凍結
        /// </summary>
        [Description("凍結")] Freeze = 2,
        /// <summary>
        /// 密碼過期
        /// </summary>
        [Description("密碼過期")] PasuwadoExpired = 3,
        /// <summary>
        /// 主機預設密碼
        /// </summary>
        [Description("主機預設密碼")] HostDefault = 4,
    }
    /// <summary>
    /// 前/後台
    /// </summary>
    [Description("前/後台")]
    public enum EndType : byte
    {
        /// <summary>
        /// 後台
        /// </summary>
        [Description("後台")] Backend = 0,
        /// <summary>
        /// 前台
        /// </summary>
        [Description("前台")] Frontend = 1
    }
    /// <summary>
    /// 功能權限動作
    /// </summary>
    [Description("功能權限動作"), Flags]
    public enum FuncAction : int
    {
        /// <summary>
        /// 使用
        /// </summary>
        [Description("使用")] Use = 1,
        /// <summary>
        /// 查詢
        /// </summary>
        [Description("查詢")] Query = 2,
        /// <summary>
        /// 新增
        /// </summary>
        [Description("新增")] Create = 4,
        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")] Update = 8,
        /// <summary>
        /// 刪除
        /// </summary>
        [Description("刪除")] Delete = 16,
        /// <summary>
        /// 審核
        /// </summary>
        [Description("審核")] Approve = 32,
        /// <summary>
        /// 結案
        /// </summary>
        [Description("結案")] EndCase = 64,
        /// <summary>
        /// 作廢
        /// </summary>
        [Description("作廢")] Invalid = 128,
        /// <summary>
        /// 全部
        /// </summary>
        All = 255,
    }
    /// <summary>
    /// 單據狀態
    /// </summary>
    [Description("單據狀態")]
    public enum FormStatus : byte
    {
        /// <summary>
        /// 保存
        /// </summary>
        [Description("保存")] Saved = 0,
        /// <summary>
        /// 審核
        /// </summary>
        [Description("審核")] Approved = 1,
        /// <summary>
        /// 結案
        /// </summary>
        [Description("結案")] EndCase = 2,
        /// <summary>
        /// 作廢
        /// </summary>
        [Description("作廢")] Obsoleted = 3,
    }
    /// <summary>
    /// 資料狀態
    /// </summary>
    [Description("資料狀態")]
    public enum DataStatus : byte
    {
        /// <summary>
        /// 未生效
        /// </summary>
        [Description("未生效")] NotEffective = 0,
        /// <summary>
        /// 生效
        /// </summary>
        [Description("生效")] Effective = 1
    }
    /// <summary>
    /// 單據動作
    /// </summary>
    [Description("單據動作"), Flags]
    public enum SetOpportunity : int
    {
        [Description("新增")] Create = 1,
        [Description("修改")] Edit = 2,
        [Description("審核")] Approve = 4,
        [Description("結案")] EndCase = 8
    }
    /// <summary>
    /// 行狀態
    /// </summary>
    [Description("行狀態")]
    public enum RowState : byte
    {
        /// <summary>
        /// 無異動
        /// </summary>
        [Description("無異動")] None = 0,
        /// <summary>
        /// 新增
        /// </summary>
        [Description("新增")] Insert = 1,
        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")] Update = 2,
        /// <summary>
        /// 刪除
        /// </summary>
        [Description("刪除")] Delete = 3,
    }
    /// <summary>
    /// 過帳狀態
    /// </summary>
    [Description("過帳狀態")]
    public enum TransStatus : byte
    {
        /// <summary>
        /// 無
        /// </summary>
        [Description("無")] None = 0,
        /// <summary>
        /// 正過帳
        /// </summary>
        [Description("正過帳")] Increase = 1,
        /// <summary>
        /// 差異過帳
        /// </summary>
        [Description("差異過帳")] Difference = 2,
        /// <summary>
        /// 反過帳
        /// </summary>
        [Description("反過帳")] Invert = 3,
    }
}
