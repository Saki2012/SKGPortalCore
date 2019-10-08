using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model.MasterData.OperateSystem
{
    /// <summary>
    /// 角色權限
    /// </summary>
    [Description("角色權限")]
    public class RoleSet
    {
        /// <summary>
        /// 角色權限資料
        /// </summary>
        [Description("角色權限資料")]
        public RoleModel Role { get; set; }
        /// <summary>
        /// 權限設置
        /// </summary>
        [Description("權限設置")]
        public List<RolePermissionModel> RolePermission { get; set; }
    }
    /// <summary>
    /// 角色權限資料
    /// </summary>
    public class RoleModel : MasterDataModel
    {
        /// <summary>
        /// 角色權限代號
        /// </summary>
        [Description("角色權限代號"), Key]
        public string RoleId { get; set; }
        /// <summary>
        /// 角色權限名稱
        /// </summary>
        [Description("角色權限名稱")]
        public string RoleName { get; set; }
        /// <summary>
        /// 前/後台
        /// </summary>
        [Description("前/後台")]
        public EndType EndType { get; set; }
        /// <summary>
        /// 是否為管理者
        /// </summary>
        [Description("是否為管理者")]
        public bool IsAdmin { get; set; }
        //public byte RoleLv { get; set; } 角色位階(待考慮)
        /// <summary>
        /// 權限列表
        /// </summary>
        [Description("權限列表")]
        public List<RolePermissionModel> Permissions { get; set; }
    }
    /// <summary>
    /// 功能權限設置
    /// </summary>
    public class RolePermissionModel : DetailRowState
    {
        [ForeignKey("RoleId")]
        public RoleModel Role { get; set; }
        /// <summary>
        /// 角色權限代號
        /// </summary>
        [Description("角色權限代號"), Key]
        public string RoleId { get; set; }
        /// <summary>
        /// 前/後台
        /// </summary>
        [Description("前/後台")]
        public EndType EndType { get; set; }
        /// <summary>
        /// 序號
        /// </summary>
        [Description("序號"), Key]
        public int RowId { get; set; }
        /// <summary>
        /// 功能名稱
        /// </summary>
        [Description("功能名稱")]
        public string FuncName { get; set; }
        /// <summary>
        /// 權限
        /// </summary>
        [Description("權限")]
        public int /*FuncAction*/ FuncAction { get; set; }
    }
}
