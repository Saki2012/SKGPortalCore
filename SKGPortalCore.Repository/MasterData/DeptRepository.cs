using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.Repository.Entity;
using SKGPortalCore.Interface.IRepository.MasterData;
using SKGPortalCore.Model.MasterData;
using System.Runtime.InteropServices;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 部門庫
    /// </summary>
    [ProgId(SystemCP.ProgId_Dept)]
    public class DeptRepository : BasicRepository<DeptSet>, IDeptRepository
    {
        public DeptRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
