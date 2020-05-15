using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.Model.User;
using SKGPortalCore.Core.Repository.Entity;
using SKGPortalCore.Interface.IRepository.MasterData;
using System.Runtime.InteropServices;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 客戶庫
    /// </summary>
    [ProgId(SystemCP.ProgId_Customer)]
    public class CustomerRepository : BasicRepository<CustomerSet>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
