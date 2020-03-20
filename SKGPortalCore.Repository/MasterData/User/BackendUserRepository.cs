using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Model.System;
using SKGPortalCore.Repository.SKGPortalCore.Business.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SKGPortalCore.Repository.MasterData.User
{
    /// <summary>
    /// 後臺用戶庫
    /// </summary>
    public class BackendUserRepository : BasicRepository<BackendUserSet>, IUSerRepository<BackendUserSet>
    {
        public BackendUserRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }

        public List<PermissionToken> Login(ISessionWrapper session, string account, string pasuwado)
        {
            BackendUserSet set = QueryData(new object[] { account });
            if (null == set) return null;
            if (!BizAccountLogin.CheckADAccountPasuwado(set, pasuwado)) return null;
            using RoleRepository rep = new RoleRepository(DataAccess);
            foreach (var backendUserRole in set.BackendUserRole) backendUserRole.Permissions = rep.QueryData(new object[] { backendUserRole.RoleId }).RolePermission;
            List<IRoleModel> UserRoles = set.BackendUserRole.Cast<IRoleModel>().ToList();
            session.User = set.BackendUser;
            List<PermissionToken> permissions = BizAccountLogin.GetRolePermissionsToken(session.SessionId, UserRoles);
            return permissions;
        }
    }
}
