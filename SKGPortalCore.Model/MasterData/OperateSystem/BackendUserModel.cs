using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model.MasterData.OperateSystem
{
    /// <summary>
    /// 後臺使用者
    /// </summary>
    [Description("後臺使用者")]
    public class BackendUserSet
    {
        /// <summary>
        /// 後臺使用者資料
        /// </summary>
        [Description("後臺使用者資料")]
        public BackendUserModel User { get; set; }
        /// <summary>
        /// 後臺使用者角色權限清單
        /// </summary>
        [Description("後臺使用者角色權限清單")]
        public List<BackendUserRoleModel> UserRoles { get; set; }
    }
    /// <summary>
    /// 後臺使用者資料
    /// </summary>
    [Description("後臺使用者資料")]
    public class BackendUserModel : MasterDataModel, IUserModel
    {
        [Key]
        public string KeyId { get; set; }
        /// <summary>
        /// 使用者代號
        /// </summary>
        [Description("使用者代號")]
        public string UserId { get; set; }
        /// <summary>
        /// 使用者名稱
        /// </summary>
        [Description("使用者名稱")]
        public string UserName { get; set; }
        /// <summary>
        /// 部門代號
        /// </summary>
        [Description("部門代號")]
        public string DeptId { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [Description("Email")]
        public string Email { get; set; }
        /// <summary>
        /// 帳戶狀態
        /// </summary>
        [Description("帳戶狀態")]
        public AccountStatus AccountStatus { get; set; }
    }
    /// <summary>
    /// 後臺使用者角色權限清單
    /// </summary>
    [Description("後臺使用者角色權限清單")]
    public class BackendUserRoleModel :  DetailRowState,IRoleModel
    {
        public BackendUserModel Key { get; set; }
        [Key]
        public string KeyId { get; set; }
        [ForeignKey("RoleId")]
        public RoleModel Role { get; set; }
        /// <summary>
        /// 角色權限代號
        /// </summary>
        [Description("角色權限代號"), Key]
        public string RoleId { get; set; }
        [NotMapped]
        public List<RolePermissionModel> Permissions { get; set; }
    }
}
