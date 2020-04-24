using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.Report;
using SKGPortalCore.Repository.SKGPortalCore.Business.BillData;
using SKGPortalCore.Repository.SKGPortalCore.Business.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;

namespace SKGPortalCore.Repository.Report
{
    /// <summary>
    /// 收款單相關報表
    /// </summary>
    [ProgId(SystemCP.ProgId_ReceiptBillRpt)]
    public class ReceiptRptRepository : BasicRptRepository
    {
        #region Construct
        public ReceiptRptRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
        #endregion
        /// <summary>
        /// 無帳單主檔報表
        /// (舊：銷帳報表列印)
        /// </summary>
        public List<NoBillReceiptRptModel> NoBillReceiptRpt(string customerCode)
        {
            return BizReceiptBillRpt.NoBillReceiptRpt(DataAccess, customerCode);
        }
        /// <summary>
        /// 無帳單主檔報表
        /// (舊：銷帳報表列印)
        /// </summary>
        public void NoBillReceiptRptDoc(string customerCode)
        {
            List<NoBillReceiptRptModel> rpt = BizReceiptBillRpt.NoBillReceiptRpt(DataAccess, customerCode);
            using LibDocument doc = new LibDocument(); doc.ExportExcel(rpt);
        }
        /// <summary>
        /// 通路手續費月結報表
        /// (舊：手續費報表)
        /// </summary>
        public void ChannelTotalFeeRpt(string customerId)
        {
            DataTable rpt = BizReceiptBillRpt.GetChannelTotalFeeRpt(DataAccess, customerId);
            using LibDocument doc = new LibDocument(); doc.ExportExcel(rpt);

        }
        /// <summary>
        /// 通路手續費月結收據
        /// (舊：手續費報表)
        /// </summary>
        public void ChannelTotalFeeReceiptRpt(string customerId)
        {
            BizReceiptBillRpt.GetChannelTotalFeeRpt(DataAccess, customerId);
        }
        /// <summary>
        /// 收款明細報表
        /// (舊：銷帳明細資料查詢)
        /// </summary>
        public List<ReceiptRptModel> ReceiptRpt(string customerId, string customerCode, string[] channelIds, DateTime beginDate, DateTime endDate)
        {
            return BizReceiptBillRpt.GetReceiptRpt(DataAccess, customerId, customerCode, channelIds, beginDate, endDate);
        }
        /// <summary>
        /// 總收款報表-客戶別
        /// </summary>
        public void TotalReceipt_Customer(DateTime tradeDate)
        {
            BizReceiptBillRpt.GetTotalReceipt_Customer(DataAccess, tradeDate);
        }
        /// <summary>
        /// 總收款報表-通路別
        /// </summary>
        public void TotalReceipt_Channel(DateTime tradeDate)
        {
            BizReceiptBillRpt.GetTotalReceipt_Channel(DataAccess, tradeDate);
        }
    }
}
