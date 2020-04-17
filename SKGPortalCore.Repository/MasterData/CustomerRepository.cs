using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData;
using System.Runtime.InteropServices;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 客戶庫
    /// </summary>
    [ProgId(SystemCP.ProgId_Customer)]
    public class CustomerRepository : BasicRepository<CustomerSet>
    {
        public CustomerRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
