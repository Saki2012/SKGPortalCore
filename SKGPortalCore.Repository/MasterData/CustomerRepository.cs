using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData;

namespace SKGPortalCore.Repository.MasterData
{
    public class CustomerRepository : BasicRepository<CustomerSet>
    {
        public CustomerRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
