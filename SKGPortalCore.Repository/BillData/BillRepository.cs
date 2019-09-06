using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using SKGPortalCore.Business.BillData;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.BillData;

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
            DataFlowNo = null;
            SetFlowNo = new Action<BillSet>(p =>
            {
                if (p.Bill.BillNo.IsNullOrEmpty())
                {
                    string billNo = $"Bill{DateTime.Today.ToString("yyyyMMdd")}{(++DataFlowNo.FlowNo).ToString().PadLeft(5, '0')}";
                    p.Bill.BillNo = billNo;
                    if (null != p.BillDetail) p.BillDetail.ForEach(p => p.BillNo = billNo);
                    if (null != p.BillReceiptDetail) p.BillReceiptDetail.ForEach(p => p.BillNo = billNo);
                }
            });
        }
        #endregion
        #region Protected
        protected override void AfterSetEntity(BillSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            using BizBill biz = new BizBill(Message, DataAccess, User);
            biz.SetData(set/*, action*/);
            biz.CheckData(set);
        }
        #endregion
    }
}
