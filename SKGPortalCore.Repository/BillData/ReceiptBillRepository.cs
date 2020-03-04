using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Model.System;
using SKGPortalCore.Repository.MasterData;
using SKGPortalCore.Repository.SKGPortalCore.Business.BillData;

namespace SKGPortalCore.Repository.BillData
{
    /// <summary>
    /// 收款單庫
    /// </summary>
    [ProgId("ReceiptBill"), Description("收款單")]
    public class ReceiptBillRepository : BasicRepository<ReceiptBillSet>
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
                    string billNo = $"Rec{DateTime.Today.ToString("yyyyMMdd")}{(++DataFlowNo.FlowNo).ToString().PadLeft(5, '0')}";
                    p.ReceiptBill.BillNo = billNo;
                }
            });
            //IsSetRefModel = false;
            InitWorkDic(DateTime.Now, 9);
        }
        #endregion

        #region Public
        public void InitWorkDic(DateTime date, int months)
        {
            WorkDic = DataAccess.Set<WorkDateModel>().Where(p => p.Date >= date.AddMonths(-Math.Abs(months)) && p.Date <= date.AddMonths(Math.Abs(months))).ToDictionary(key => key.Date, value => value.IsWorkDate);
        }
        #endregion

        #region Protected
        protected override void AfterSetEntity(ReceiptBillSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            BizReceiptBill.SetData(set, DataAccess, BizCustSetDic, ColSetDic, WorkDic);
            BizReceiptBill.PostingData(DataAccess, User, action, null, set);
        }
        #endregion
    }
}
