using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.LibAttribute;
using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Model;
using SKGPortalCore.Core.Model.User;
using SKGPortalCore.Core.Repository.Entity;
using SKGPortalCore.Interface.IRepository.MasterData;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 前臺用戶庫
    /// </summary>
    [ProgId(SystemCP.ProgId_CustUser), EndPoint(EndType.Frontend)]
    public class CustUserRepository : BasicRepository<CustUserSet>, ICustUserRepository
    {
        public CustUserRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }

        public List<PermissionTokenModel> Login(ISessionWrapper session, string account, string pasuwado)
        {
            if (null == session) return null;
            CustUserSet set = QueryData(new object[] { account });
            if (null == set) return null;
            if (!BizAccountLogin.CheckAccountPasuwado(set, pasuwado)) return null;
            using RoleRepository rep = new RoleRepository(DataAccess);
            foreach (var custUserRole in set.CustUserRoleList) custUserRole.Permissions = rep.QueryData(new object[] { custUserRole.RoleId }).RolePermission;
            List<IRoleModel> UserRoles = set.CustUserRoleList.Cast<IRoleModel>().ToList();
            session.User = set.CustUser;
            List<PermissionTokenModel> permissions = BizAccountLogin.GetRolePermissionsToken(session.SessionId, UserRoles);
            return permissions;
        }

        public void Logout(ISessionWrapper session)
        {
            if (null == session) return;
            session.Clear();
        }
    }
}
