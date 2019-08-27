using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SKGPortalCore.Model.BillData
{
    public class CashFlowBillSet
    {
        public CashFlowBillModel CashFlowBill { get; set; }
    }

    /// <summary>
    /// 金流帳簿
    /// (源於核心金流檔)
    /// </summary>
    public class CashFlowBillModel : BillDataModel
    {
        public string BillNo { get; set; }
        /// <summary>
        /// 匯款日期
        /// </summary>
        [Description("匯款日期")]
        public DateTime RemitTime { get; set; }
        /// <summary>
        /// 通路代號
        /// </summary>
        [Description("通路代號")]
        public string ChannelId { get; set; }
        /// <summary>
        /// 代收類別代號
        /// </summary>
        [Description("代收類別代號")]
        public string CollectionTypeId { get; set; }
        /// <summary>
        /// 交易金額
        /// </summary>
        [Description("交易金額")]
        public decimal Amount { get; set; }
        /// <summary>
        /// 導入批號
        /// </summary>
        [Description("導入批號")]
        public string ImportBatchNo { get;set;}
        /// <summary>
        /// 來源
        /// </summary>
        [Description("來源")]
        public string Source { get;set;}
    }
}
