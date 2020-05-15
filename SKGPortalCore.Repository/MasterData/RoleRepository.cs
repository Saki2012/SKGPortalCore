using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.Model.User;
using SKGPortalCore.Core.Repository.Entity;
using SKGPortalCore.Interface.IRepository.MasterData;
using System.Runtime.InteropServices;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 角色權限庫
    /// </summary>
    [ProgId(SystemCP.ProgId_Role)]
    public class RoleRepository : BasicRepository<RoleSet>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
