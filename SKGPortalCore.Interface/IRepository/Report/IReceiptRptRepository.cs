using System;
using System.Collections.Generic;
using System.Text;
using SKGPortalCore.Model.Report;

namespace SKGPortalCore.Interface.IRepository.Report
{
   public interface IReceiptRptRepository
    {
        /// <summary>
        /// 無帳單主檔報表
        /// (舊：銷帳報表列印)
        /// </summary>
        public List<NoBillReceiptRptModel> NoBillReceiptRpt(string customerCode);
        /// <summary>
        /// 無帳單主檔報表
        /// (舊：銷帳報表列印)
        /// </summary>
        public void NoBillReceiptRptDoc(string customerCode);
        /// <summary>
        /// 通路手續費月結報表
        /// (舊：手續費報表)
        /// </summary>
        public void ChannelTotalFeeRpt(string customerId);
        /// <summary>
        /// 通路手續費月結收據
        /// (舊：手續費報表)
        /// </summary>
        public void ChannelTotalFeeReceiptRpt(string customerId);
        /// <summary>
        /// 收款明細報表
        /// (舊：銷帳明細資料查詢)
        /// </summary>
        public List<ReceiptRptModel> ReceiptRpt(string customerId, string customerCode, string[] channelIds, DateTime beginDate, DateTime endDate);
        /// <summary>
        /// 總收款報表-客戶別
        /// </summary>
        public void TotalReceipt_Customer(DateTime tradeDate);
        /// <summary>
        /// 總收款報表-通路別
        /// </summary>
        public void TotalReceipt_Channel(DateTime tradeDate);
    }
}
