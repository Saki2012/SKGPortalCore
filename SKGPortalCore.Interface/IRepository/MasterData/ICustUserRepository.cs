using SKGPortalCore.Core;
using SKGPortalCore.Core.Model;
using SKGPortalCore.Core.Model.User;
using SKGPortalCore.Core.Repository.Interface;
using System.Collections.Generic;

namespace SKGPortalCore.Interface.IRepository.MasterData
{
    public interface ICustUserRepository : IBasicRepository<CustUserSet>
    {
        public List<PermissionTokenModel> Login(ISessionWrapper session, string account, string pasuwado);
        public void Logout(ISessionWrapper session);
    }
}
