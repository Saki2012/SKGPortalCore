using SKGPortalCore.Data;
using SKGPortalCore.Model.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SKGPortalCore.Repository.MasterData.User
{
    interface IUSerRepository<TSet>
    {
        public List<PermissionToken> Login(ISessionWrapper session, string account, string pasuwado);
    }
}
