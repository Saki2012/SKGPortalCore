using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Data;
using SKGPortalCore.Model.BillData;

namespace SKGPortalCore.Repository.BillData
{
    public class ReceiptBillRepository : BasicRepository<ReceiptBillSet>
    {
        public ReceiptBillRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
    }
}
