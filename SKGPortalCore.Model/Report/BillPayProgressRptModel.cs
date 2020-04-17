using SKGPortalCore.Lib;
using SKGPortalCore.Model.System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SKGPortalCore.Model.Report
{
    /// <summary>
    /// 帳單繳費進度報表
    /// </summary>
    [Description(SystemCP.DESC_BillPayProgressRpt)]
    public class BillPayProgressRptModel
    {
        /// <summary>
        /// 單據編號
        /// </summary>
        [Description(SystemCP.DESC_BillNo)] public string BillNo { get; set; } = string.Empty;
        /// <summary>
        /// 繳款截止日
        /// </summary>
        [Description(SystemCP.DESC_PayEndDate)] public DateTime PayEndDate { get; set; } = DateTime.MinValue;
        /// <summary>
        /// 繳款人代號
        /// </summary>
        [Description(SystemCP.DESC_PayerId)] public string PayerId { get; set; } = string.Empty;
        /// <summary>
        /// 繳款人名稱
        /// </summary>
        [Description(SystemCP.DESC_PayerName)] public string PayerName { get; set; } = string.Empty;
        /// <summary>
        /// 繳款人類別
        /// </summary>
        [Description(SystemCP.DESC_PayerType)] public string PayerType { get; set; } = string.Empty;
        /// <summary>
        /// 虛擬帳號
        /// </summary>
        [Description(SystemCP.DESC_VirtualAccountCode)] public string VirtualAccountCode { get; set; } = string.Empty;
        /// <summary>
        /// 繳款狀態
        /// </summary>
        [Description(SystemCP.DESC_PayStatus)] public string PayStatus { get; set; } = string.Empty;
        /// <summary>
        /// 應繳金額
        /// </summary>
        [Description(SystemCP.DESC_ShouldPayAmount)] public decimal PayAmount { get; set; } = decimal.Zero;
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
