using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 代收通路庫
    /// </summary>
    public class ChannelRepository : BasicRepository<ChannelSet>
    {
        public ChannelRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
