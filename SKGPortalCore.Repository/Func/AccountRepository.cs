using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Business.Func;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData.OperateSystem;

namespace SKGPortalCore.Repository.Func
{
    public class AccountRepository
    {
        ApplicationDbContext DataAccess { get; }
        public AccountRepository(ApplicationDbContext dataAccess)
        {
            DataAccess = dataAccess;
        }
        public CustUserSet Login(string customerId, string userId, string pasuwado)
        {
            string key = $"{customerId},{userId}";
            Func<CustUserModel, bool> where1 = new Func<CustUserModel, bool>(p => p.KeyId == key);
            Func<CustUserRoleModel, bool> where2 = new Func<CustUserRoleModel, bool>(p => p.KeyId == key);
            var UserRoles = DataAccess.Set<CustUserRoleModel>().Include(p => p.Role).ThenInclude(role => role.Permissions).Where(where2).ToList();
            CustUserSet user = new CustUserSet() { User = DataAccess.Set<CustUserModel>().Find(key), UserRoles = UserRoles };
            if (!AccountLogin.CheckAccountPasuwado(user, pasuwado)) return null;
            return user;
        }
    }
}
