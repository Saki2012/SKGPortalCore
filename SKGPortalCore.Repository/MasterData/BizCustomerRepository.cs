using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 商戶資料庫
    /// </summary>
    public class BizCustomerRepository : BasicRepository<BizCustomerSet>
    {
        public BizCustomerRepository(ApplicationDbContext dataAccess ) : base(dataAccess) { }
    }
}
