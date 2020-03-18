using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData.OperateSystem;
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

        public BackendUserSet Login()
        {
            string customerId = string.Empty; string userId = string.Empty; string pasuwado = string.Empty;

            string sessionId = string.Empty;
            string key = $"{customerId},{userId}";
            Func<BackendUserModel, bool> where1 = new Func<BackendUserModel, bool>(p => p.KeyId == key);
            Func<BackendUserRoleModel, bool> where2 = new Func<BackendUserRoleModel, bool>(p => p.KeyId == key);
            List<IRoleModel> UserRoles = DataAccess.Set<BackendUserRoleModel>().Include(p => p.Role).ThenInclude(role => role.Permissions).Where(where2).Cast<IRoleModel>().ToList();
            BackendUserSet user = new BackendUserSet() { User = DataAccess.Set<BackendUserModel>().Find(key), UserRoles = UserRoles };
            if (!BizAccountLogin.CheckAccountPasuwado(new CustUserSet() /*user*/, pasuwado))
            {
                return null;
            }
            Dictionary<string, string> permissions = BizAccountLogin.GetRolePermissionsToken(sessionId, UserRoles);

            return user;
        }
    }
}
