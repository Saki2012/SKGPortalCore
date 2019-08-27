using SKGPortalCore.Data;
using SKGPortalCore.Model.BillData;

namespace SKGPortalCore.Repository.BillData
{
    public class CashFlowBillRepository : BasicRepository<CashFlowBillSet>
    {
        #region Construct
        public CashFlowBillRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
        #endregion
    }
}
