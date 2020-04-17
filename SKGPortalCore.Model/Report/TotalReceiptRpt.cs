using SKGPortalCore.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SKGPortalCore.Model.Report
{
    /// <summary>
    /// 總收款報表
    /// </summary>
    [Description(SystemCP.DESC_TotalReceiptRpt)]
    public class TotalReceiptRpt
    {
        /// <summary>
        /// 客戶統編
        /// </summary>
        [Description(SystemCP.DESC_CustomerId)] public string CustomerId { get; set; } = string.Empty;
        /// <summary>
        /// 客戶名稱
        /// </summary>
        [Description(SystemCP.DESC_CustomerName)] public string CustomerName { get; set; } = string.Empty;
        /// <summary>
        /// 帳務分行
        /// </summary>
        [Description(SystemCP.DESC_AccountDeptId)] public string AccountDeptId { get; set; } = string.Empty;
        /// <summary>
        /// 部門名稱
        /// </summary>
        [Description(SystemCP.DESC_DeptName)] public string DeptName { get; set; } = string.Empty;
        /// <summary>
        /// 申請月份
        /// </summary>
        [Description(SystemCP.DESC_SyncDateTime)] public DateTime CreateTime { get; set; } = DateTime.MinValue;
        /// <summary>
        /// 交易月份
        /// </summary>
        [Description(SystemCP.DESC_TradeDate)] public DateTime TradeDate { get; set; } = DateTime.MinValue;
        /// <summary>
        /// 交易筆數
        /// </summary>
        [Description(SystemCP.DESC_TotalCount)] public long TotalCount { get; set; } = 1;
        /// <summary>
        /// 代收通路代號
        /// </summary>
        [Description(SystemCP.DESC_ChannelId)] public string ChannelId { get; set; } = string.Empty;
        /// <summary>
        /// 代收通路名稱
        /// </summary>
        [Description(SystemCP.DESC_ChannelName)] public string ChannelName { get; set; } = string.Empty;
        /// <summary>
        /// 交易金額
        /// </summary>
        [Description(SystemCP.DESC_HadPayAmount)] public decimal PayAmount { get; set; } = decimal.Zero;
        /// <summary>
        /// 通路手續費
        /// </summary>
        [Description(SystemCP.DESC_ChannelTotalFee)] public decimal ChannelTotalFee { get; set; } = decimal.Zero;
    }
}
