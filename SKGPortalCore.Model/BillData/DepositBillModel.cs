using SKGPortalCore.Core;
using SKGPortalCore.Core.Model;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.SourceData;
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
    [Description(SystemCP.DESC_DepositBill)]
    public class DepositBillSet
    {
        /// <summary>
        /// 入金機
        /// </summary>
        [Description(SystemCP.DESC_DepositBill)]
        public DepositBillModel DepositBill { get; set; } = new DepositBillModel();
        /// <summary>
        /// 入金機收款明細
        /// </summary>
        [Description(SystemCP.DESC_DepositBillReceiptDt)]
        public List<DepositBillReceiptDetailModel> DepositBillReceiptDetail { get; set; } = new List<DepositBillReceiptDetailModel>();
    }
    /// <summary>
    /// 入金機
    /// </summary>
    [Description(SystemCP.DESC_DepositBill)]
    public class DepositBillModel
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
        /// 櫃位名稱
        /// </summary>
        [Description(SystemCP.DESC_StoreName), MaxLength(SystemCP.NormalLen)] public string StoreName { get; set; }
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
    /// 入金機收款明細
    /// </summary>
    [Description(SystemCP.DESC_DepositBillReceiptDt)]
    public class DepositBillReceiptDetailModel : DetailRowState
    {
        /// <summary>
        /// 入金機
        /// </summary>
        [ForeignKey(nameof(BillNo))] public DepositBillModel Bill { get; set; }
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
