using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SKGPortalCore.Repository.Report
{
    /// <summary>
    /// 系統使用相關報表
    /// </summary>
    [ProgId(SystemCP.ProgId_SystemRpt)]
    public class SystemRptRepository : BasicRptRepository
    {
        #region Construct
        public SystemRptRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
        #endregion
    }
}
