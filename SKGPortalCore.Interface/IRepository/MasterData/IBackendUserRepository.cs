using SKGPortalCore.Core;
using SKGPortalCore.Core.Model;
using SKGPortalCore.Core.Model.User;
using SKGPortalCore.Core.Repository.Interface;
using System.Collections.Generic;

namespace SKGPortalCore.Repository.MasterData.User
{
    public interface IBackendUserRepository : IBasicRepository<BackendUserSet>
    {
        public List<PermissionTokenModel> Login(ISessionWrapper session, string account, string pasuwado);
        public void Logout(ISessionWrapper session);
    }
}
