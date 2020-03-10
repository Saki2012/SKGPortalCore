using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.System;
using SKGPortalCore.Repository.SKGPortalCore.Business.BillData;
using SKGPortalCore.Repository.SKGPortalCore.Business.Func;

namespace SKGPortalCore.Repository.BillData
{
    /// <summary>
    /// 帳單庫
    /// </summary>
    [ProgId("Bill"), Description("帳單")]
    public class BillRepository : BasicRepository<BillSet>
    {
        #region Construct
        public BillRepository(ApplicationDbContext dataAccess) : base(dataAccess)
        {
            SetFlowNo = new Action<BillSet>(p =>
            {
                if (p.Bill.BillNo.IsNullOrEmpty())
                {
                    string billNo = $"Bill{DateTime.Today.ToString("yyyyMMdd")}{(++DataFlowNo.FlowNo).ToString().PadLeft(5, '0')}";
                    p.Bill.BillNo = billNo;
                    p.BillDetail?.ForEach(p => p.BillNo = billNo);
                    p.BillReceiptDetail?.ForEach(p => p.BillNo = billNo);
                }
            });
            IsSetRefModel = true;
        }
        #endregion

        #region Protected
        protected override void AfterSetEntity(BillSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            BizBill.CheckData(set, Message, DataAccess);
            BizBill.SetData(set);
            BizVirtualAccountCode.AddVirtualAccountCode(DataAccess, ProgId, set.Bill.BillNo, set.Bill.VirtualAccountCode);
        }
        #endregion
    }
}
