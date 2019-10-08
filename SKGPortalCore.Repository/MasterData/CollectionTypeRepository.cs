using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 代收類別庫
    /// </summary>
    public class CollectionTypeRepository : BasicRepository<CollectionTypeSet>
    {
        public CollectionTypeRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
