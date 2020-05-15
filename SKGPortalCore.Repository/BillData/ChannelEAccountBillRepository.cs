using System;
using System.Globalization;
using System.Runtime.InteropServices;
using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.Libary;
using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Repository.Entity;
using SKGPortalCore.Interface.IRepository.BillData;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Repository.SKGPortalCore.Business.BillData;

namespace SKGPortalCore.Repository.BillData
{
    /// <summary>
    /// 通路帳簿庫
    /// </summary>
    [ProgId(SystemCP.ProgId_ChannelEAccountBill)]
    public class ChannelEAccountBillRepository : BasicRepository<ChannelEAccountBillSet>, IChannelEAccountBillRepository
    {
        #region Construct
        public ChannelEAccountBillRepository(ApplicationDbContext dataAccess) : base(dataAccess)
        {
            SetFlowNo = new Action<ChannelEAccountBillSet>(p =>
            {
                if (p.ChannelEAccountBill.BillNo.IsNullOrEmpty())
                {
                    string billNo = $"EAcct{DateTime.Today.ToString("yyyyMMdd", CultureInfo.InvariantCulture)}{(++DataFlowNo.FlowNo).ToString().PadLeft(5, '0')}";
                    p.ChannelEAccountBill.BillNo = billNo;
                    p.ChannelEAccountBillDetail?.ForEach(p => p.BillNo = billNo);
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
