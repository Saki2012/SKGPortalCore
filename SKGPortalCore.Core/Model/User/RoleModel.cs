using SKGPortalCore.Core.LibEnum;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Core.Model.User
{
    /// <summary>
    /// 角色權限
    /// </summary>
    [Description(SystemCP.DESC_Role)]
    public class RoleSet
    {
        /// <summary>
        /// 角色權限資料
        /// </summary>
        [Description(SystemCP.DESC_Role)] public RoleModel Role { get; set; } = new RoleModel();
        /// <summary>
        /// 權限設置
        /// </summary>
        [Description(SystemCP.DESC_RolePermission)] public List<RolePermissionModel> RolePermission { get; set; } = new List<RolePermissionModel>();
    }
    /// <summary>
    /// 角色權限資料
    /// </summary>
    [Description(SystemCP.DESC_Role)]
    public class RoleModel : MasterDataModel
    {
        /// <summary>
        /// 角色權限代號
        /// </summary>
        [Description(SystemCP.DESC_RoleId), Key] public string RoleId { get; set; }
        /// <summary>
        /// 角色權限名稱
        /// </summary>
        [Description(SystemCP.DESC_RoleName)] public string RoleName { get; set; }
        /// <summary>
        /// 前/後台
        /// </summary>
        [Description(SystemCP.DESC_EndType)] public EndType EndType { get; set; }
        /// <summary>
        /// 是否為管理者
        /// </summary>
        [Description(SystemCP.DESC_IsAdmin)] public bool IsAdmin { get; set; }
        /// <summary>
        /// 權限列表
        /// </summary>
        [Description(SystemCP.DESC_Permissions)] public List<RolePermissionModel> Permissions { get; set; }
    }
    /// <summary>
    /// 功能權限設置
    /// </summary>
    [Description(SystemCP.DESC_RolePermission)]
    public class RolePermissionModel : DetailRowState
    {
        /// <summary>
        /// 角色權限
        /// </summary>
        [ForeignKey(nameof(RoleId))] public RoleModel Role { get; set; }
        /// <summary>
        /// 角色權限代號
        /// </summary>
        [Description(SystemCP.DESC_RoleId), Key] public string RoleId { get; set; }
        /// <summary>
        /// 行序號
        /// </summary>
        [Description(SystemCP.DESC_RowId), Key] public int RowId { get; set; }
        /// <summary>
        /// 功能名稱
        /// </summary>
        [Description(SystemCP.DESC_FuncName)] public string FuncName { get; set; }
        /// <summary>
        /// 權限
        /// /*FuncAction*/
        /// </summary>
        [Description(SystemCP.DESC_FuncAction)] public int FuncAction { get; set; }
    }
}
