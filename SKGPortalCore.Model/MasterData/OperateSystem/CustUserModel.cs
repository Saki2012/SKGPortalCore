using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model.MasterData.OperateSystem
{
    /// <summary>
    /// 前台用戶
    /// </summary>
    [Description("前台用戶")]
    public class CustUserSet
    {
        /// <summary>
        /// 前台用戶資料
        /// </summary>
        public CustUserModel User { get; set; }
        public List<CustUserRoleModel> UserRoles { get; set; }
    }
    /// <summary>
    /// 前台用戶資料
    /// </summary>
    [Description("用戶資料")]
    public class CustUserModel : MasterDataModel, IUserModel
    {
        /// <summary>
        /// KeyId為客戶代號,使用者代號
        /// </summary>
        [Description, Key]
        public string KeyId { get; set; }
        public CustomerModel Customer { get; set; }
        /// <summary>
        /// 客戶代號(統編)
        /// </summary>
        [Description("客戶代號")]
        public string CustomerId { get; set; }
        /// <summary>
        /// 使用者代號
        /// </summary>
        [Description("用戶代號")]
        public string UserId { get; set; }
        /// <summary>
        /// 使用者名稱
        /// </summary>
        [Description("用戶名稱")]
        public string UserName { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        [Description("密碼")]
        public string Pasuwado { get; set; }
        /// <summary>
        /// 帳戶狀態
        /// </summary>
        [Description("帳戶狀態")]
        public AccountStatus AccountStatus { get; set; }
        /// <summary>
        /// 登入失敗次數
        /// </summary>
        [Description("登入失敗次數")]
        public byte LoginErrorCount { get; set; }
        /// <summary>
        /// 密碼過期時間
        /// </summary>
        [Description("密碼過期時間")]
        public DateTime PasuwadoExpiredDate { get; set; }
    }
    /// <summary>
    /// 前台使用者角色權限清單
    /// </summary>
    [Description("前台用戶角色權限清單")]
    public class CustUserRoleModel : DetailRowState, IRoleModel
    {
        [ForeignKey("KeyId")]
        public CustUserModel Key { get; set; }
        [Description, Key]
        public string KeyId { get; set; }
        [ForeignKey("RoleId")]
        public RoleModel Role { get; set; }
        /// <summary>
        /// 角色權限代號
        /// </summary>
        [Description("角色權限代號"), Key]
        public string RoleId { get; set; }
    }
}
