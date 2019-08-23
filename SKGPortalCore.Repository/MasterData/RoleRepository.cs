using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData.OperateSystem;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 角色權限庫
    /// </summary>
    public class RoleRepository : BasicRepository<RoleSet>
    {
        public RoleRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
