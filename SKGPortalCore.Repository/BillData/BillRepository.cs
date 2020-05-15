using System;
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
    /// 帳單庫
    /// </summary>
    [ProgId(SystemCP.ProgId_Bill)]
    public class BillRepository : BasicRepository<BillSet>, IBillRepository
    {
        #region Construct
        public BillRepository(ApplicationDbContext dataAccess) : base(dataAccess)
        {
            SetFlowNo = new Action<BillSet>(p => DoSetFlowNo(p));
            IsSetRefModel = true;
        }
        #endregion

        #region Protected
        /// <summary>
        /// 
        /// </summary>
        /// <param name="set"></param>
        /// <param name="action"></param>
        protected override void AfterSetEntity(BillSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            BizBill.CheckData(set, Message, DataAccess);
            BizBill.SetData(set, ProgId, DataAccess);
        }
        #endregion

        #region Private
        /// <summary>
        /// 設置流水編號
        /// </summary>
        /// <param name="p"></param>
        private void DoSetFlowNo(BillSet p)
        {
            if (p.Bill.BillNo.IsNullOrEmpty())
            {
                string billNo = $"Bill{DateTime.Today.ToString("yyyyMMdd")}{(++DataFlowNo.FlowNo).ToString().PadLeft(5, '0')}";
                p.Bill.BillNo = billNo;
                p.BillDetail?.ForEach(p => p.BillNo = billNo);
                p.BillReceiptDetail?.ForEach(p => p.BillNo = billNo);
            }
        }
        #endregion
    }
}
