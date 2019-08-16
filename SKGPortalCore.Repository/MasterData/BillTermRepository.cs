using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData;

namespace SKGPortalCore.Repository.MasterData
{
    public class BillTermRepository : BasicRepository<BillTermSet>
    {
        public BillTermRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
