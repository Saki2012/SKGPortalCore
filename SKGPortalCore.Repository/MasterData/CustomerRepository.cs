using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 客戶庫
    /// </summary>
    public class CustomerRepository : BasicRepository<CustomerSet>
    {
        public CustomerRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
