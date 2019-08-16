using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData;

namespace SKGPortalCore.Repository.MasterData
{
    public class ChannelRepository : BasicRepository<ChannelSet>
    {
        public ChannelRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
