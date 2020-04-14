using SKGPortalCore.Lib;
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
    [Description(SystemCP.DESC_AutoDebitBill)]
    public class AutoDebitBillSet
    {
        /// <summary>
        /// 約定扣款單
        /// </summary>
        [Description(SystemCP.DESC_AutoDebitBill)] public AutoDebitBillModel AutoDebitBill { get; set; } = new AutoDebitBillModel();
        /// <summary>
        /// 約定扣款單收款明細
        /// </summary>
        [Description(SystemCP.DESC_AutoDebitDt)] public List<AutoDebitBillReceiptDetailModel> AutoDebitBillReceiptDetail { get; set; } = new List<AutoDebitBillReceiptDetailModel>();
    }

    /// <summary>
    /// 約定扣款單
    /// </summary>
    [Description(SystemCP.DESC_AutoDebitBill)]
    public class AutoDebitBillModel
    {
        /// <summary>
        /// 帳單編號
        /// </summary>
        [Description(SystemCP.DESC_BillNo), Key, MaxLength(SystemCP.BillNoLen)] public string BillNo { get; set; }
        /// <summary>
        /// 企業編號
        /// </summary>
        [ForeignKey(nameof(CustomerCode))] public BizCustomerModel BizCustomer { get; set; }
        /// <summary>
        /// 企業編號
        /// </summary>
        [Description(SystemCP.DESC_CustomerCode), Required] public string CustomerCode { get; set; }
        /// <summary>
        /// 繳款人
        /// </summary>
        [ForeignKey(nameof(CustomerCode) + "," + nameof(PayerId))] public PayerModel Payer { get; set; }
        /// <summary>
        /// 繳款人
        /// </summary>
        [Description(SystemCP.DESC_PayerId)] public string PayerId { get; set; }
        /// <summary>
        /// 應繳金額
        /// </summary>
        [Description(SystemCP.DESC_ShouldPayAmount)] public decimal PayAmount { get; set; }
        /// <summary>
        /// 虛擬帳號
        /// </summary>
        [Description(SystemCP.DESC_VirtualAccountCode), Required, Index, MaxLength(SystemCP.NormalLen)] public string VirtualAccountCode { get; set; }
        /// <summary>
        /// 匯入批號
        /// </summary>
        [Description(SystemCP.DESC_ImportBatchNo), Required, MaxLength(SystemCP.NormalLen)] public string ImportBatchNo { get; set; } = string.Empty;
    }
    /// <summary>
    /// 約定扣款單收款明細
    /// </summary>
    [Description(SystemCP.DESC_AutoDebitDt)]
    public class AutoDebitBillReceiptDetailModel : DetailRowState
    {
        /// <summary>
        /// 帳單
        /// </summary>
        [ForeignKey(nameof(BillNo))] public AutoDebitBillModel Bill { get; set; }
        /// <summary>
        /// 帳單編號
        /// </summary>
        [Description(SystemCP.DESC_BillNo), Key] public string BillNo { get; set; }
        /// <summary>
        /// 收款單
        /// </summary>
        [ForeignKey(nameof(ReceiptBillNo))] public ReceiptBillModel ReceiptBill { get; set; }
        /// <summary>
        /// 收款單號
        /// </summary>
        [Description(SystemCP.DESC_ReceiptBillNo), Key] public string ReceiptBillNo { get; set; }
    }
}
