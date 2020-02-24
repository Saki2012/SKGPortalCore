using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 部門庫
    /// </summary>
    public class DeptRepository : BasicRepository<DeptSet>
    {
        public DeptRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
