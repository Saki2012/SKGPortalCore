using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Model.Report;
using SKGPortalCore.Model.System;
using SKGPortalCore.Repository.MasterData;
using SKGPortalCore.Repository.SKGPortalCore.Business.BillData;

namespace SKGPortalCore.Repository.BillData
{
    /// <summary>
    /// 收款單庫
    /// </summary>
    [ProgId(SystemCP.ProgId_ReceiptBill)]
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
            InitWorkDic(DateTime.Now, 9);
        }
        #endregion

        #region Public
        public void InitWorkDic(DateTime date, int months)
        {
            WorkDic = DataAccess.Set<WorkDateModel>().Where(p => p.Date >= date.AddMonths(-Math.Abs(months)) && p.Date <= date.AddMonths(Math.Abs(months))).ToDictionary(key => key.Date, value => value.IsWorkDate);
        }
        /*Rpt*/
        /// <summary>
        /// 無帳單主檔報表
        /// (舊：銷帳報表列印)
        /// </summary>
        public List<NoBillReceiptRptModel> NoBillReceiptRpt(string customerCode)
        {
            return BizReceiptBill.NoBillReceiptRpt(DataAccess, customerCode);
        }
        /// <summary>
        /// 無帳單主檔報表
        /// (舊：銷帳報表列印)
        /// </summary>
        public void NoBillReceiptRptDoc(string customerCode)
        {
            List<NoBillReceiptRptModel> rpt = BizReceiptBill.NoBillReceiptRpt(DataAccess, customerCode);
            using LibDocument doc = new LibDocument(); doc.ExportExcel(rpt);
        }

        /// <summary>
        /// 通路手續費月結報表
        /// (舊：手續費報表)
        /// </summary>
        public void ChannelTotalFeeRpt()
        {
            DataTable rpt = BizReceiptBill.GetChannelTotalFeeRpt(DataAccess);
            using LibDocument doc = new LibDocument(); doc.ExportExcel(rpt);

        }
        /// <summary>
        /// 通路手續費月結收據
        /// (舊：手續費報表)
        /// </summary>
        public void ChannelTotalFeeReceiptRpt()
        {
            BizReceiptBill.GetChannelTotalFeeRpt(DataAccess);
        }




        /// <summary>
        /// 收款明細報表
        /// (舊：銷帳明細資料查詢)
        /// </summary>
        public void ReceiptRpt()
        {
            //BizReceiptBill.GetReceiptRpt(DataAccess);
        }
        /// <summary>
        /// 總收款報表-客戶別
        /// </summary>
        public void TotalReceipt_Customer()
        {
            BizReceiptBill.GetTotalReceipt_Customer(DataAccess);

        }
        /// <summary>
        /// 總收款報表-通路別
        /// </summary>
        public void TotalReceipt_Channel()
        {
            BizReceiptBill.GetTotalReceipt_Channel(DataAccess);
        }
        #endregion

        #region Protected
        protected override void AfterSetEntity(ReceiptBillSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            BizReceiptBill.SetData(set, DataAccess, BizCustSetDic, ColSetDic, WorkDic);
            BizReceiptBill.PostingData(DataAccess, User,/* action, null,*/ set);
        }
        #endregion
    }
}
