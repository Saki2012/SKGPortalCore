﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.Report;
using SKGPortalCore.Model.System;
using SKGPortalCore.Repository.SKGPortalCore.Business.BillData;
using SKGPortalCore.Repository.SKGPortalCore.Business.Func;

namespace SKGPortalCore.Repository.BillData
{
    /// <summary>
    /// 帳單庫
    /// </summary>
    [ProgId(SystemCP.ProgId_Bill)]
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

        #region Public
        /// <summary>
        /// 繳費進度報表
        /// (舊：銷帳報表列印)
        /// </summary>
        public List<BillPayProgressRptModel> BillPayProgressRpt(string customerCode, string billTermId)
        {
            return BizBill.GetBillPayProgressRpt(DataAccess, customerCode, billTermId);
        }
        /// <summary>
        /// 
        /// </summary>
        public void BillPayProgressRptDoc(string customerCode, string billTermId)
        {
            List<BillPayProgressRptModel> rpt = BizBill.GetBillPayProgressRpt(DataAccess, customerCode, billTermId);
            using LibDocument doc = new LibDocument(); doc.ExportExcel(rpt);
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
    }
}
