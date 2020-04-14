﻿using SKGPortalCore.Lib;
using SKGPortalCore.Model.System;

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
    public static class SystemOperator
    {
        public static BackendUserModel SysOperator = new BackendUserModel() { KeyId = SystemCP.SysOperator, UserName = SystemCP.SysOperatorName, AccountStatus = AccountStatus.Enable, DeptId = null, Email = string.Empty };
    }
}
