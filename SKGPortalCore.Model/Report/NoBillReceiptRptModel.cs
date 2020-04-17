using SKGPortalCore.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SKGPortalCore.Model.Report
{
    public class NoBillReceiptRptModel
    {
        /// <summary>
        /// 單據編號
        /// </summary>
        [Description(SystemCP.DESC_BillNo)] public string BillNo { get; set; } = string.Empty;
        /// <summary>
        /// 虛擬帳號
        /// </summary>
        [Description(SystemCP.DESC_VirtualAccountCode)] public string VirtualAccountCode { get; set; } = string.Empty;
        /// <summary>
        /// 實繳金額
        /// </summary>
        [Description(SystemCP.DESC_HadPayAmount)] public decimal HadPayAmount { get; set; } = decimal.Zero;
        /// <summary>
        /// 交易日期
        /// </summary>
        [Description(SystemCP.DESC_TradeDate)] public DateTime TradeDate { get; set; } = DateTime.MinValue;
        /// <summary>
        /// 預計匯款日期
        /// </summary>
        [Description(SystemCP.DESC_ExpectRemitDate)] public DateTime ExpectRemitDate { get; set; } = DateTime.MinValue;
        /// <summary>
        /// 代收通路代號
        /// </summary>
        [Description(SystemCP.DESC_ChannelId)] public string ChannelId { get; set; } = string.Empty;
        /// <summary>
        /// 代收通路名稱
        /// </summary>
        [Description(SystemCP.DESC_ChannelName)] public string ChannelName { get; set; } = string.Empty;
    }
}
