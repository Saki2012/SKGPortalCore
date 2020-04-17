using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 部門庫
    /// </summary>
    [ProgId(SystemCP.ProgId_Dept)]
    public class DeptRepository : BasicRepository<DeptSet>
    {
        public DeptRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
