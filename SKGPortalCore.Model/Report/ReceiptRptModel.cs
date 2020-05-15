using SKGPortalCore.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SKGPortalCore.Model.Report
{
    /// <summary>
    /// 收款明細報表
    /// </summary>
    [Description(SystemCP.DESC_ReceiptRpt)]
    public class ReceiptRptModel
    {
        /// <summary>
        /// 單據編號
        /// </summary>
        [Description(SystemCP.DESC_BillNo)] public string BillNo { get; set; } = string.Empty;
        /// <summary>
        /// 交易日期
        /// </summary>
        [Description(SystemCP.DESC_TradeDate)] public DateTime TradeDate { get; set; } = DateTime.MinValue;
        /// <summary>
        /// 傳輸日期
        /// </summary>
        [Description(SystemCP.DESC_TransDate)] public DateTime TransDate { get; set; } = DateTime.MinValue;
        /// <summary>
        /// 帳務分行
        /// </summary>
        [Description(SystemCP.DESC_AccountDeptId)] public string AccountDeptId { get; set; } = string.Empty;
        /// <summary>
        /// 已繳金額
        /// </summary>
        [Description(SystemCP.DESC_HadPayAmount)] public decimal HadPayAmount { get; set; } = decimal.Zero;
        /// <summary>
        /// 手續費
        /// </summary>
        [Description(SystemCP.DESC_Fee)] public decimal Fee { get; set; } = decimal.Zero;
        /// <summary>
        /// 入帳金額
        /// </summary>
        [Description(SystemCP.DESC_IncomeAmount)] public decimal IncomeAmount { get; set; } = decimal.Zero;
        /// <summary>
        /// 虛擬帳號
        /// </summary>
        [Description(SystemCP.DESC_VirtualAccountCode)] public string VirtualAccountCode { get; set; } = string.Empty;
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
