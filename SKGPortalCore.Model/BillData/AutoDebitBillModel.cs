using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Model.System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model.BillData
{
    /// <summary>
    /// 約定扣款單
    /// </summary>
    [Description("約定扣款單")]
    public class AutoDebitBillSet
    {
        /// <summary>
        /// 約定扣款單
        /// </summary>
        public AutoDebitBillModel AutoDebitBill { get; set; } = new AutoDebitBillModel();
        /// <summary>
        /// 約定扣款單收款明細
        /// </summary>
        public List<AutoDebitBillReceiptDetailModel> AutoDebitBillReceiptDetail { get; set; } = new List<AutoDebitBillReceiptDetailModel>();
    }

    /// <summary>
    /// 約定扣款單
    /// </summary>
    [Description("約定扣款單")]
    public class AutoDebitBillModel
    {
        /// <summary>
        /// 帳單編號
        /// </summary>
        [Description("帳單編號"), Key, MaxLength(CP.BillNoLen)] public string BillNo { get; set; }
        /// <summary>
        /// 企業編號
        /// </summary>
        [ForeignKey("CustomerCode")] public BizCustomerModel BizCustomer { get; set; }
        /// <summary>
        /// 企業編號
        /// </summary>
        [Description("企業編號"), Required] public string CustomerCode { get; set; }
        /// <summary>
        /// 繳款人
        /// </summary>
        [ForeignKey("CustomerCode,PayerId")] public PayerModel Payer { get; set; }
        /// <summary>
        /// 繳款人
        /// </summary>
        [Description("繳款人")] public string PayerId { get; set; }
        /// <summary>
        /// 應繳金額
        /// </summary>
        [Description("應繳金額")] public decimal PayAmount { get; set; }
        /// <summary>
        /// 虛擬帳號
        /// </summary>
        [Description("虛擬帳號"), Required, Index, MaxLength(CP.NormalLen)] public string VirtualAccountCode { get; set; }
        /// <summary>
        /// 匯入批號
        /// </summary>
        [Description("匯入批號"), Required, MaxLength(CP.NormalLen)] public string ImportBatchNo { get; set; } = string.Empty;
    }
    /// <summary>
    /// 約定扣款單收款明細
    /// </summary>
    [Description("約定扣款單收款明細")]
    public class AutoDebitBillReceiptDetailModel : DetailRowState
    {
        [ForeignKey("BillNo")] public AutoDebitBillModel Bill { get; set; }
        /// <summary>
        /// 帳單編號
        /// </summary>
        [Description("帳單編號"), Key] public string BillNo { get; set; }
        [ForeignKey("ReceiptBillNo")] public ReceiptBillModel ReceiptBill { get; set; }
        /// <summary>
        /// 收款單號
        /// </summary>
        [Description("收款單號"), Key] public string ReceiptBillNo { get; set; }
    }
}
