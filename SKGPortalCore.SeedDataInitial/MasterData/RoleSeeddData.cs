using System;
using System.Collections.Generic;
using System.Text;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Model.System;
using SKGPortalCore.Repository.MasterData;

namespace SKGPortalCore.SeedDataInitial.MasterData
{
    public static class RoleSeeddData
    {
        /// <summary>
        /// 新增「角色權限」-初始資料
        /// </summary>
        /// <param name="dataAccess"></param>
        public static void CreateRole(SysMessageLog Message, ApplicationDbContext DataAccess)
        {
            try
            {
                Message.Prefix = "新增「角色權限」-初始資料：";
                using RoleRepository repo = new RoleRepository(DataAccess) { User = SystemOperator.SysOperator, Message = Message }; ;
                List<RoleSet> roles = new List<RoleSet>() {
                    new RoleSet() { Role = new RoleModel() { RoleId = "BackEndAdmin",  RoleName = "後台管理員", EndType = EndType.Frontend, IsAdmin = true } },
                    new RoleSet() { Role = new RoleModel() { RoleId = "FrontEndAdmin", RoleName = "前台管理員", EndType = EndType.Backend,  IsAdmin = true } },
                 };
                roles.ForEach(role =>
                 {
                     if (null == repo.QueryData(new object[] { role.Role.RoleId }))
                     {
                         repo.Create(role);
                     }
                 });
                repo.CommitData(FuncAction.Create);
            }
            finally
            {
                Message.Prefix = string.Empty;
            }
        }
    }
}
