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
    /// 約定扣款單庫
    /// </summary>
    [ProgId("AutoDebitBill"), Description("約定扣款單")]
    public class AutoDebitBillRepository : BasicRepository<AutoDebitBillSet>
    {
        #region Construct
        public AutoDebitBillRepository(ApplicationDbContext dataAccess) : base(dataAccess)
        {
            SetFlowNo = new Action<AutoDebitBillSet>(p =>
            {
                if (p.AutoDebitBill.BillNo.IsNullOrEmpty())
                {
                    string billNo = $"Auto{DateTime.Today.ToString("yyyyMMdd")}{(++DataFlowNo.FlowNo).ToString().PadLeft(5, '0')}";
                    p.AutoDebitBill.BillNo = billNo;
                    p.AutoDebitBillReceiptDetail?.ForEach(p => p.BillNo = billNo);
                }
            });
        }
        #endregion
        #region Protected
        protected override void AfterSetEntity(AutoDebitBillSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            BizAutoDebitBill.CheckData(set, Message, DataAccess);
            BizAutoDebitBill.SetData(set, ProgId, DataAccess);
        }
        #endregion

    }
}
