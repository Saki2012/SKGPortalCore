using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData;

namespace SKGPortalCore.Repository.MasterData
{
    public class PayerRepository : BasicRepository<PayerSet>
    {
        public PayerRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
