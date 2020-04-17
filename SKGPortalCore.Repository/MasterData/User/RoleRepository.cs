using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData.OperateSystem;
using System.Runtime.InteropServices;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 角色權限庫
    /// </summary>
    [ProgId(SystemCP.ProgId_Role)]
    public class RoleRepository : BasicRepository<RoleSet>
    {
        public RoleRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
