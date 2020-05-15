using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.LibAttribute;
using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Model;
using SKGPortalCore.Core.Model.User;
using SKGPortalCore.Core.Repository.Entity;
using SKGPortalCore.Repository.MasterData.User;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 後臺用戶庫
    /// </summary>
    [ProgId(SystemCP.ProgId_BackendUser), EndPoint(EndType.Backend)]
    public class BackendUserRepository : BasicRepository<BackendUserSet>, IBackendUserRepository
    {
        public BackendUserRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="account"></param>
        /// <param name="pasuwado"></param>
        /// <returns></returns>
        public List<PermissionTokenModel> Login(ISessionWrapper session, string account, string pasuwado)
        {
            if (null == session || null != session.User) return null;
            BackendUserSet set = QueryData(new object[] { account });
            if (null == set) return null;
            if (!BizAccountLogin.CheckADAccountPasuwado(set, pasuwado)) return null;
            using RoleRepository rep = new RoleRepository(DataAccess);
            foreach (var backendUserRole in set.BackendUserRoleList) backendUserRole.Permissions = rep.QueryData(new object[] { backendUserRole.RoleId }).RolePermission;
            List<IRoleModel> UserRoles = set.BackendUserRoleList.Cast<IRoleModel>().ToList();
            session.User = set.BackendUser;
            List<PermissionTokenModel> permissions = BizAccountLogin.GetRolePermissionsToken(session.SessionId, UserRoles);
            return permissions;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public void Logout(ISessionWrapper session)
        {
            if (null == session) return;
            session.Clear();
        }
    }
}
