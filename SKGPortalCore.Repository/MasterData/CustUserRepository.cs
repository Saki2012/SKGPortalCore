using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData.OperateSystem;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 前台用戶庫
    /// </summary>
    public class CustUserRepository : BasicRepository<CustUserSet>
    {
        public CustUserRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
