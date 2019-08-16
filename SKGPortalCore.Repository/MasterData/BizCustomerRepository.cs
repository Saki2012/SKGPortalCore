using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData;

namespace SKGPortalCore.Repository.MasterData
{
    public class BizCustomerRepository : BasicRepository<BizCustomerSet>
    {
        public BizCustomerRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
