using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.Libary;
using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Repository.Entity;
using SKGPortalCore.Interface.IRepository.BillData;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Repository.SKGPortalCore.Business.BillData;

namespace SKGPortalCore.Repository.BillData
{
    /// <summary>
    /// 收款單庫
    /// </summary>
    [ProgId(SystemCP.ProgId_ReceiptBill)]
    public class ReceiptBillRepository : BasicRepository<ReceiptBillSet>, IReceiptBillRepository
    {
        #region Property
        public Dictionary<string, BizCustomerSet> BizCustSetDic { get; } = new Dictionary<string, BizCustomerSet>();
        public Dictionary<string, CollectionTypeSet> ColSetDic { get; } = new Dictionary<string, CollectionTypeSet>();
        private Dictionary<DateTime, bool> WorkDic { get; set; }
        #endregion

        #region Construct
        public ReceiptBillRepository(ApplicationDbContext dataAccess) : base(dataAccess)
        {
            SetFlowNo = new Action<ReceiptBillSet>(p =>
            {
                if (p.ReceiptBill.BillNo.IsNullOrEmpty())
                {
                    string billNo = $"Rec{DateTime.Today.ToString("yyyyMMdd", CultureInfo.InvariantCulture)}{(++DataFlowNo.FlowNo).ToString().PadLeft(5, '0')}";
                    p.ReceiptBill.BillNo = billNo;
                }
            });
            //InitWorkDic(DateTime.Now, 9);
        }
        #endregion

        #region Protected
        protected override void AfterSetEntity(ReceiptBillSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            BizReceiptBill.SetData(set, DataAccess, BizCustSetDic, ColSetDic, WorkDic);
        }
        protected override void AfterUpdate(ReceiptBillSet oldSet, ReceiptBillSet newSet, TransStatus status)
        {
            base.AfterUpdate(oldSet, newSet, status);
            BizReceiptBill.PostingData(DataAccess, User, status, oldSet, newSet);
        }
        #endregion
    }
}
