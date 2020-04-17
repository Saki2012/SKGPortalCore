using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.System;
using SKGPortalCore.Repository.SKGPortalCore.Business.MasterData;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 期別庫
    /// </summary>
    [ProgId(SystemCP.ProgId_BillTerm)]
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
                    string billTermId = $"Term{DateTime.Today.ToString("yyyyMMdd")}{(++DataFlowNo.FlowNo).ToString().PadLeft(5, '0')}";
                    p.BillTerm.BillTermId = billTermId;
                    p.BillTermDetail?.ForEach(p => p.BillTermId = billTermId);
                }
            });
        }
        #endregion

        #region Protected
        protected override void AfterSetEntity(BillTermSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            BizBillTerm.CheckData(DataAccess, Message, set);
        }
        #endregion
    }
}
