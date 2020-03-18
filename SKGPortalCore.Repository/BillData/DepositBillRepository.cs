using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.System;
using SKGPortalCore.Repository.SKGPortalCore.Business.BillData;
using SKGPortalCore.Repository.SKGPortalCore.Business.Func;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace SKGPortalCore.Repository.BillData
{
    /// <summary>
    /// 入金機庫
    /// </summary>
    [ProgId("DepositBill"), Description("入金機")]
    public class DepositBillRepository : BasicRepository<DepositBillSet>
    {
        #region Construct
        public DepositBillRepository(ApplicationDbContext dataAccess) : base(dataAccess)
        {
            SetFlowNo = new Action<DepositBillSet>(p =>
            {
                if (p.DepositBill.BillNo.IsNullOrEmpty())
                {
                    string billNo = $"Deposit{DateTime.Today.ToString("yyyyMMdd")}{(++DataFlowNo.FlowNo).ToString().PadLeft(5, '0')}";
                    p.DepositBill.BillNo = billNo;
                    p.DepositBillReceiptDetail?.ForEach(p => p.BillNo = billNo);
                }
            });
        }
        #endregion
        #region Protected
        protected override void AfterSetEntity(DepositBillSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            BizDepositBill.CheckData(set, Message, DataAccess);
            BizDepositBill.SetData(set,ProgId,DataAccess);
        }
        #endregion
    }
}
