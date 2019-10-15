using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData;

namespace SKGPortalCore.Repository.MasterData
{
    public class ChannelVerifyPeriodRepository : BasicRepository<ChannelVerifyPeriodSet>
    {
        public ChannelVerifyPeriodRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
