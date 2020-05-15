using System.Runtime.InteropServices;
using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Repository.Entity;
using SKGPortalCore.Interface.IRepository.BillData;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Repository.SKGPortalCore.Business.BillData;

namespace SKGPortalCore.Repository.BillData
{
    /// <summary>
    /// 金流帳簿庫
    /// </summary>
    [ProgId(SystemCP.ProgId_CashFlowBill)]
    public class CashFlowBillRepository : BasicRepository<CashFlowBillSet>, ICashFlowBillRepository
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
