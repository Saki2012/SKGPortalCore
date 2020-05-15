using SKGPortalCore.Core;
using SKGPortalCore.Core.Model;
using SKGPortalCore.Model.SourceData;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SKGPortalCore.Model.BillData
{
    /// <summary>
    /// 金流帳簿
    /// </summary>
    [Description(SystemCP.DESC_CashFlowBill)]
    public class CashFlowBillSet
    {
        /// <summary>
        /// 金流帳簿
        /// </summary>
        [Description(SystemCP.DESC_CashFlowBill)] public CashFlowBillModel CashFlowBill { get; set; } = new CashFlowBillModel();
    }

    /// <summary>
    /// 金流帳簿
    /// (源於核心金流檔)
    /// </summary>
    [Description(SystemCP.DESC_CashFlowBill)]
    public class CashFlowBillModel : BillDataModel
    {
        /// <summary>
        /// 單據編號
        /// </summary>
        [Description(SystemCP.DESC_BillNo), Key, MaxLength(SystemCP.BillNoLen)] public string BillNo { get; set; }
        /// <summary>
        /// 匯款日期
        /// </summary>
        [Description(SystemCP.DESC_RemitTime)] public DateTime RemitTime { get; set; }
        /// <summary>
        /// 通路代號
        /// </summary>
        [Description(SystemCP.DESC_ChannelId)] public string ChannelId { get; set; }
        /// <summary>
        /// 代收項目
        /// </summary>
        [Description(SystemCP.DESC_CollectionTypeId)] public string CollectionTypeId { get; set; }
        /// <summary>
        /// 交易金額
        /// </summary>
        [Description(SystemCP.DESC_Amount)] public decimal Amount { get; set; }
        /// <summary>
        /// 匯入批號
        /// </summary>
        [Description(SystemCP.DESC_ImportBatchNo), Required, MaxLength(SystemCP.NormalLen)] public string ImportBatchNo { get; set; }
        /// <summary>
        /// 來源
        /// </summary>
        [Description(SystemCP.DESC_Source), MaxLength(200)] public string Source { get; set; }
    }
}
