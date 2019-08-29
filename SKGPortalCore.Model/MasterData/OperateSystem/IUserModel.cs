using System.Collections.Generic;

namespace SKGPortalCore.Model.MasterData.OperateSystem
{
    public interface IUserModel
    {
        string KeyId { get; set; }
        string UserName { get; set; }
    }
    public interface IRoleModel
    {
        RoleModel Role { get; set; }
        string RoleId { get; set; }
    }
    /// <summary>
    /// 系統操作
    /// </summary>
    public class SystemOperator
    {
        public BackendUserModel SysOperator { get; }
        public SystemOperator()
        {
            SysOperator = new BackendUserModel() { KeyId = "SysOperator", UserId = "SysOperator", UserName = "系統操作", AccountStatus = AccountStatus.Enable, DeptId = null, Email = string.Empty };
        }
    }
}
