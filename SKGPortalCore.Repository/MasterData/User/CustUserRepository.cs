using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData.OperateSystem;
using System;

namespace SKGPortalCore.Repository.MasterData.User
{
    /// <summary>
    /// 前臺用戶庫
    /// </summary>
    public class CustUserRepository : BasicRepository<CustUserSet>, IUSerRepository<CustUserSet>
    {
        public CustUserRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }

        public CustUserSet Login()
        {
            throw new NotImplementedException();
        }
    }
}
