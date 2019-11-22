using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using SKGPortalCore.Business.MasterData;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 期別庫
    /// </summary>
    [ProgId("BillTerm"), Description("期別")]
    public class BillTermRepository : BasicRepository<BillTermSet>
    {
        #region Construct
        public BillTermRepository(ApplicationDbContext dataAccess) : base(dataAccess)
        {
            DataFlowNo = null;
            SetFlowNo = new Action<BillTermSet>(p =>
            {
                if (p.BillTerm.BillTermId.IsNullOrEmpty())
                {
                    string billNo = $"Term{DateTime.Today.ToString("yyyyMMdd")}{(++DataFlowNo.FlowNo).ToString().PadLeft(5, '0')}";
                    p.BillTerm.BillTermId = billNo;
                    if (null != p.BillTermDetail)
                    {
                        p.BillTermDetail.ForEach(p => p.BillTermId = billNo);
                    }
                }
            });
        }
        #endregion
        #region Protected
        protected override void AfterSetEntity(BillTermSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            BizBillTerm.CheckData( Message, set);
        }
        #endregion
    }
}
