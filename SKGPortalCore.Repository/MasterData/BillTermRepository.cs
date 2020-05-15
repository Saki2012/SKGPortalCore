using System;
using System.Globalization;
using System.Runtime.InteropServices;
using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.Libary;
using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Repository.Entity;
using SKGPortalCore.Interface.IRepository.MasterData;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Repository.SKGPortalCore.Business.MasterData;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 期別庫
    /// </summary>
    [ProgId(SystemCP.ProgId_BillTerm)]
    public class BillTermRepository : BasicRepository<BillTermSet>, IBillTermRepository
    {
        #region Construct
        public BillTermRepository(ApplicationDbContext dataAccess) : base(dataAccess)
        {
            DataFlowNo = null;
            SetFlowNo = new Action<BillTermSet>(p =>
            {
                if (p.BillTerm.BillTermId.IsNullOrEmpty())
                {
                    string billTermId = $"Term{DateTime.Today.ToString("yyyyMMdd", CultureInfo.InvariantCulture)}{(++DataFlowNo.FlowNo).ToString().PadLeft(5, '0')}";
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
