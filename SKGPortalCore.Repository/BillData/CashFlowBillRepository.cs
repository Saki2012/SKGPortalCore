using System.ComponentModel;
using System.Runtime.InteropServices;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.System;
using SKGPortalCore.Repository.SKGPortalCore.Business.BillData;

namespace SKGPortalCore.Repository.BillData
{
    /// <summary>
    /// 金流帳簿庫
    /// </summary>
    [ProgId(SystemCP.ProgId_CashFlowBill)]
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
