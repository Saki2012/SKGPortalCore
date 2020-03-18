using System;
using System.Collections.Generic;
using System.Text;

namespace SKGPortalCore.Repository.MasterData.User
{
    interface IUSerRepository<TSet>
    {
        public TSet Login();
    }
}
