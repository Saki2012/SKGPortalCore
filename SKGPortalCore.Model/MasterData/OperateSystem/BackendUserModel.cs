using SKGPortalCore.Lib;
using SKGPortalCore.Model.System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model.MasterData.OperateSystem
{
    /// <summary>
    /// 後臺使用者
    /// </summary>
    [Description(SystemCP.DESC_BackendUser)]
    public class BackendUserSet
    {
        /// <summary>
        /// 後臺使用者
        /// </summary>
        [Description(SystemCP.DESC_BackendUser)] public BackendUserModel BackendUser { get; set; }
        /// <summary>
        /// 後臺使用者角色權限清單
        /// </summary>
        [Description(SystemCP.DESC_BackendUserRoleList)] public List<BackendUserRoleModel> BackendUserRoleList { get; set; } = new List<BackendUserRoleModel>();
    }
    /// <summary>
    /// 後臺使用者
    /// </summary>
    [Description(SystemCP.DESC_BackendUser)]
    public class BackendUserModel : MasterDataModel, IUserModel
    {
        /// <summary>
        /// 員工編號
        /// </summary>
        [Description(SystemCP.DESC_MemberId), Key] public string KeyId { get; set; }
        /// <summary>
        /// 使用者名稱
        /// </summary>
        [Description(SystemCP.DESC_UserName)] public string UserName { get; set; }
        /// <summary>
        /// 部門
        /// </summary>
        [ForeignKey(nameof(DeptId))] public DeptModel Dept { get; set; }
        /// <summary>
        /// 部門代號
        /// </summary>
        [Description(SystemCP.DESC_DeptId)] public string DeptId { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [Description(SystemCP.DESC_Email)] public string Email { get; set; }
        /// <summary>
        /// 帳戶狀態
        /// </summary>
        [Description(SystemCP.DESC_AccountStatus)] public AccountStatus AccountStatus { get; set; }
    }
    /// <summary>
    /// 後臺使用者角色權限清單
    /// </summary>
    [Description(SystemCP.DESC_BackendUserRoleList)]
    public class BackendUserRoleModel : DetailRowState, IRoleModel
    {
        /// <summary>
        /// 後臺使用者
        /// </summary>
        [ForeignKey(nameof(KeyId))] public BackendUserModel Key { get; set; }
        /// <summary>
        /// 員工編號
        /// </summary>
        [Description(SystemCP.DESC_MemberId), Key] public string KeyId { get; set; }
        /// <summary>
        /// 角色權限
        /// </summary>
        [ForeignKey(nameof(RoleId))] public RoleModel Role { get; set; }
        /// <summary>
        /// 角色權限代號
        /// </summary>
        [Description(SystemCP.DESC_RoleId), Key] public string RoleId { get; set; }
        /// <summary>
        /// 權限表
        /// </summary>
        [NotMapped] public List<RolePermissionModel> Permissions { get; set; }
    }
}
