using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Linq;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.System;
using SKGPortalCore.Repository.SKGPortalCore.Business.BillData;

namespace SKGPortalCore.Repository.BillData
{
    /// <summary>
    /// 通路帳簿庫
    /// </summary>
    [ProgId("ChannelEAccountBill"), Description("通路帳簿")]
    public class ChannelEAccountBillRepository : BasicRepository<ChannelEAccountBillSet>
    {
        #region Construct
        public ChannelEAccountBillRepository(ApplicationDbContext dataAccess) : base(dataAccess)
        {
            SetFlowNo = new Action<ChannelEAccountBillSet>(p =>
            {
                if (p.ChannelEAccountBill.BillNo.IsNullOrEmpty())
                {
                    string billNo = $"EAcct{DateTime.Today.ToString("yyyyMMdd")}{(++DataFlowNo.FlowNo).ToString().PadLeft(5, '0')}";
                    p.ChannelEAccountBill.BillNo = billNo;
                    if (null != p.ChannelEAccountBill)
                        p.ChannelEAccountBillDetail.ForEach(p => p.BillNo = billNo);
                }
            });
        }
        #endregion

        #region Protected
        protected override void AfterSetEntity(ChannelEAccountBillSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            BizChannelEAccountBill.SetData(set);
        }
        #endregion
    }
}
