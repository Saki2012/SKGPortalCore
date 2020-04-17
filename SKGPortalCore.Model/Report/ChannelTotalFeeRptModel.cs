using SKGPortalCore.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SKGPortalCore.Model.Report
{
    /// <summary>
    /// 客戶手續費報表
    /// </summary>
    [Description(SystemCP.DESC_ChannelTotalFeeRpt)]
    public class ChannelTotalFeeRptModel
    {
        /// <summary>
        /// 企業代號
        /// </summary>
        [Description(SystemCP.DESC_CustomerCode)] public string CustomerCode { get; set; }
        /// <summary>
        /// 客戶名稱
        /// </summary>
        [Description(SystemCP.DESC_CustomerName)] public string CustomerName { get; set; }
        /// <summary>
        /// 實體帳號
        /// </summary>
        [Description(SystemCP.DESC_RealAccount)] public string RealAccount { get; set; }
        /// <summary>
        /// 代收通路代號
        /// </summary>
        [Description(SystemCP.DESC_ChannelId)] public string ChannelId { get; set; }
        /// <summary>
        /// 代收通路名稱
        /// </summary>
        [Description(SystemCP.DESC_ChannelName)] public string ChannelName { get; set; }
        /// <summary>
        /// 總手續費
        /// </summary>
        [Description(SystemCP.DESC_TotalFee)] public decimal TotalFee { get; set; }
    }
}
