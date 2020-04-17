using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData;
using System.Runtime.InteropServices;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 代收通路庫
    /// </summary>
    [ProgId(SystemCP.ProgId_Channel)]
    public class ChannelRepository : BasicRepository<ChannelSet>
    {
        public ChannelRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
