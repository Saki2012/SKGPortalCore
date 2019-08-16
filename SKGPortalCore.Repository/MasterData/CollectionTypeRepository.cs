using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData;

namespace SKGPortalCore.Repository.MasterData
{
    public class CollectionTypeRepository : BasicRepository<CollectionTypeSet>
    {
        public CollectionTypeRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
