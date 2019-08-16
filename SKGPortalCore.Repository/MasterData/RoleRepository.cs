using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData.OperateSystem;

namespace SKGPortalCore.Repository.MasterData
{
    public class RoleRepository : BasicRepository<RoleSet>
    {
        public RoleRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
