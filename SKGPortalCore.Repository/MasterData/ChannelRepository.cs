using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.Repository.Entity;
using SKGPortalCore.Interface.IRepository.MasterData;
using SKGPortalCore.Model.MasterData;
using System.Runtime.InteropServices;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 代收通路庫
    /// </summary>
    [ProgId(SystemCP.ProgId_Channel)]
    public class ChannelRepository : BasicRepository<ChannelSet>, IChannelRepository
    {
        public ChannelRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
