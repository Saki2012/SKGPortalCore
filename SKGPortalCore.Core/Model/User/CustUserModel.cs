using SKGPortalCore.Core.LibEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Core.Model.User
{
    /// <summary>
    /// 前臺使用者
    /// </summary>
    [Description(SystemCP.DESC_CustUser)]
    public class CustUserSet
    {
        /// <summary>
        /// 前臺使用者
        /// </summary>
        [Description(SystemCP.DESC_CustUser)] public CustUserModel CustUser { get; set; } = new CustUserModel();
        /// <summary>
        /// 前臺使用者角色權限清單
        /// </summary>
        [Description(SystemCP.DESC_CustUserRoleList)] public List<CustUserRoleModel> CustUserRoleList { get; set; } = new List<CustUserRoleModel>();
    }
    /// <summary>
    /// 前臺使用者
    /// </summary>
    [Description(SystemCP.DESC_CustUser)]
    public class CustUserModel : MasterDataModel, IUserModel
    {
        /// <summary>
        /// KeyId為客戶代號,使用者代號
        /// </summary>
        [Description, Key] public string KeyId { get; set; }
        /// <summary>
        /// 客戶資料
        /// </summary>
        [ForeignKey(nameof(CustomerId))] public CustomerModel Customer { get; set; }
        /// <summary>
        /// 客戶代號(統編)
        /// </summary>
        [Description(SystemCP.DESC_CustomerId)] public string CustomerId { get; set; }
        /// <summary>
        /// 使用者代號
        /// </summary>
        [Description(SystemCP.DESC_UserId)] public string UserId { get; set; }
        /// <summary>
        /// 使用者名稱
        /// </summary>
        [Description(SystemCP.DESC_UserName)] public string UserName { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        [Description(SystemCP.DESC_Pasuwado)] public string Pasuwado { get; set; }
        /// <summary>
        /// 帳戶狀態
        /// </summary>
        [Description(SystemCP.DESC_AccountStatus)] public AccountStatus AccountStatus { get; set; }
        /// <summary>
        /// 登入失敗次數
        /// </summary>
        [Description(SystemCP.DESC_LoginErrorCount)] public byte LoginErrorCount { get; set; }
        /// <summary>
        /// 密碼過期時間
        /// </summary>
        [Description(SystemCP.DESC_PasuwadoExpiredDate)] public DateTime PasuwadoExpiredDate { get; set; }
    }
    /// <summary>
    /// 前台使用者角色權限清單
    /// </summary>
    [Description(SystemCP.DESC_CustUserRoleList)]
    public class CustUserRoleModel : DetailRowState, IRoleModel
    {
        /// <summary>
        /// 前臺使用者
        /// </summary>
        [ForeignKey(nameof(KeyId))] public CustUserModel Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Description, Key] public string KeyId { get; set; }
        /// <summary>
        /// 角色權限
        /// </summary>
        [ForeignKey(nameof(RoleId))] public RoleModel Role { get; set; }
        /// <summary>
        /// 角色權限代號
        /// </summary>
        [Description(SystemCP.DESC_RoleId), Key] public string RoleId { get; set; }
        /// <summary>
        /// 權限列表
        /// </summary>
        [NotMapped] public List<RolePermissionModel> Permissions { get; set; }
    }
}
