using System.ComponentModel;
using System.Runtime.InteropServices;
using SKGPortalCore.Data;
using SKGPortalCore.Model;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Repository.SKGPortalCore.Business.BillData;

namespace SKGPortalCore.Repository.BillData
{
    /// <summary>
    /// 金流帳簿庫
    /// </summary>
    [ProgId("CashFlowBill"), Description("金流帳簿")]
    public class CashFlowBillRepository : BasicRepository<CashFlowBillSet>
    {
        #region Construct
        public CashFlowBillRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
        #endregion

        #region Protected
        protected override void AfterSaveChanges(FuncAction action)
        {
            base.AfterSaveChanges(action);
            if (action == FuncAction.Create) BizCashFlowBill.CreateChannelWriteOfBill();
        }
        #endregion
    }
}
