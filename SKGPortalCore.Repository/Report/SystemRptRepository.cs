using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.Repository.Entity;
using SKGPortalCore.Interface.IRepository.Report;
using System.Runtime.InteropServices;

namespace SKGPortalCore.Repository.Report
{
    /// <summary>
    /// 系統使用相關報表
    /// </summary>
    [ProgId(SystemCP.ProgId_SystemRpt)]
    public class SystemRptRepository : BasicRptRepository, ISystemRptRepository
    {
        #region Construct
        public SystemRptRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
        #endregion
    }
}
