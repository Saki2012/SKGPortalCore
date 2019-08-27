using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SKGPortalCore.Model.BillData
{
    public class ChannelWriteOfBillSet
    {
        public ChannelWriteOfBillModel ChannelWriteOfBill { get; set; }
        public List<ChannelWriteOfDetailModel> ChannelWriteOfDetail { get; set; }
        public List<CashFlowWriteOfDetailModel> CashFlowWriteOfDetail { get; set; }
    }
    /// <summary>
    /// 通路帳款核銷單
    /// </summary>  
    [Description("通路帳款核銷單")]
    public class ChannelWriteOfBillModel : BillDataModel
    {
        /// <summary>
        /// 單據編號
        /// </summary>
        [Description("單據編號"), Key]
        public string BillNo { get; set; }
        /// <summary>
        /// 核銷狀態
        /// </summary>
        [Description("核銷狀態")]
        public WriteOfStatus WriteOfStatus { get; set; }
        /// <summary>
        /// 預計撥款金額
        /// </summary>
        [Description("預計撥款金額")]
        public decimal PrePayAmount { get; set; }

        public DisbursementBillModel DisBill { get; set; }
        /// <summary>
        /// 撥款單號
        /// </summary>
        [Description("撥款單號")]
        public string DisBillNo { get; set; }
    }
    /// <summary>
    /// 通路帳目明細
    /// </summary>
    [Description("通路帳目明細")]
    public class ChannelWriteOfDetailModel
    {
        public ChannelWriteOfBillModel Bill { get; set; }
        /// <summary>
        /// 單據編號
        /// </summary>
        [Description("單據編號"), Key]
        public string BillNo { get; set; }
        /// <summary>
        /// 序號
        /// </summary>
        [Description("序號"), Key]
        public int RowId { get; set; }
        public ChannelEAccountBillModel ChannelEAccountBill { get; set; }
        /// <summary>
        /// 通路帳簿單號
        /// </summary>
        [Description("通路帳簿單號")]
        public string ChannelEAccountBillNo { get; set; }
    }
    /// <summary>
    /// 金流帳目明細
    /// </summary>
    [Description("金流帳目明細")]
    public class CashFlowWriteOfDetailModel
    {
        public ChannelWriteOfBillModel Bill { get; set; }
        /// <summary>
        /// 單據編號
        /// </summary>
        [Description("單據編號"), Key]
        public string BillNo { get; set; }
        /// <summary>
        /// 序號
        /// </summary>
        [Description("序號"), Key]
        public int RowId { get; set; }
        public CashFlowBillModel CashFlowBill { get; set; }
        /// <summary>
        /// 金流帳簿單號
        /// </summary>
        [Description("金流帳簿單號")]
        public string CashFlowBillNo { get; set; }
    }
}
