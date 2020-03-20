using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Model.System;
using SKGPortalCore.Repository.SKGPortalCore.Business.Func;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SKGPortalCore.Repository.MasterData.User
{
    /// <summary>
    /// 前臺用戶庫
    /// </summary>
    public class CustUserRepository : BasicRepository<CustUserSet>, IUSerRepository<CustUserSet>
    {
        public CustUserRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }

        public List<PermissionToken> Login(ISessionWrapper session, string account, string pasuwado)
        {
            CustUserSet set = QueryData(new object[] { account });
            if (null == set) return null;
            if (!BizAccountLogin.CheckAccountPasuwado(set, pasuwado)) return null;
            using RoleRepository rep = new RoleRepository(DataAccess);
            foreach (var custUserRole in set.CustUserRole) custUserRole.Permissions = rep.QueryData(new object[] { custUserRole.RoleId }).RolePermission;
            List<IRoleModel> UserRoles = set.CustUserRole.Cast<IRoleModel>().ToList();
            session.User = set.CustUser;
            List<PermissionToken> permissions = BizAccountLogin.GetRolePermissionsToken(session.SessionId, UserRoles);
            return permissions;
        }

        public void Logout(ISessionWrapper session)
        {
            session.Clear();
        }
    }
}
