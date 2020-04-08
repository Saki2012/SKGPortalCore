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
    /// 入金機
    /// </summary>
    [Description("入金機")]
    public class DepositBillSet
    {
        /// <summary>
        /// 入金機
        /// </summary>
        public DepositBillModel DepositBill { get; set; } = new DepositBillModel();
        /// <summary>
        /// 入金機收款明細
        /// </summary>
        public List<DepositBillReceiptDetailModel> DepositBillReceiptDetail { get; set; } = new List<DepositBillReceiptDetailModel>();
    }
    /// <summary>
    /// 入金機
    /// </summary>
    [Description("入金機")]
    public class DepositBillModel
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
        /// 櫃位名稱
        /// </summary>
        [Description("櫃位名稱"), MaxLength(CP.NormalLen)] public string StoreName { get; set; }
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
    /// 入金機收款明細
    /// </summary>
    [Description("入金機收款明細")]
    public class DepositBillReceiptDetailModel : DetailRowState
    {
        [ForeignKey("BillNo")] public DepositBillModel Bill { get; set; }
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
