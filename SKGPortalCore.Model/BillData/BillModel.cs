using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Model.System;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model.BillData
{
    /// <summary>
    /// 帳單
    /// </summary>
    [Description(SystemCP.DESC_Bill)]
    public class BillSet
    {
        /// <summary>
        /// 帳單
        /// </summary>
        [Description(SystemCP.DESC_Bill)] public BillModel Bill { get; set; } = new BillModel();
        /// <summary>
        /// 帳單明細
        /// </summary>
        [Description(SystemCP.DESC_BillDt)] public List<BillDetailModel> BillDetail { get; set; } = new List<BillDetailModel>();
        /// <summary>
        /// 帳單收款明細
        /// </summary>
        [Description(SystemCP.DESC_BillReceiptDt)] public List<BillReceiptDetailModel> BillReceiptDetail { get; set; } = new List<BillReceiptDetailModel>();
    }
    /// <summary>
    /// 帳單
    /// </summary>
    [Description(SystemCP.DESC_Bill)]
    public class BillModel : BillDataModel
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
        [Description(SystemCP.DESC_CustomerCode), Required, InputField] public string CustomerCode { get; set; }
        /// <summary>
        /// 期別
        /// </summary>
        [ForeignKey(nameof(CustomerCode) + "," + nameof(BillTermId))] public BillTermModel BillTerm { get; set; }
        /// <summary>
        /// 期別
        /// </summary>
        [Description(SystemCP.DESC_BillTermId), InputField] public string BillTermId { get; set; }
        /// <summary>
        /// 繳款人
        /// </summary>
        [ForeignKey(nameof(CustomerCode) + "," + nameof(PayerId))] public PayerModel Payer { get; set; }
        /// <summary>
        /// 繳款人
        /// </summary>
        [Description(SystemCP.DESC_PayerId), InputField] public string PayerId { get; set; }
        /// <summary>
        /// 繳款截止日
        /// </summary>
        [Description(SystemCP.DESC_PayEndDate), Required, InputField] public DateTime PayEndDate { get; set; }
        /// <summary>
        /// 代收項目
        /// </summary>
        [ForeignKey(nameof(CollectionTypeId))] public CollectionTypeModel CollectionType { get; set; }
        /// <summary>
        /// 代收項目
        /// </summary>
        [Description(SystemCP.DESC_CollectionTypeId), InputField] public string CollectionTypeId { get; set; }
        /// <summary>
        /// 應繳金額
        /// </summary>
        [Description(SystemCP.DESC_ShouldPayAmount)] public decimal PayAmount { get; set; }
        /// <summary>
        /// 已繳金額
        /// </summary>
        [Description(SystemCP.DESC_HadPayAmount)] public decimal HadPayAmount { get; set; }
        /// <summary>
        /// 繳款狀態
        /// </summary>
        [Description(SystemCP.DESC_PayStatus)] public PayStatus PayStatus { get; set; }
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
    /// 帳單明細
    /// </summary>
    [Description(SystemCP.DESC_BillDt)]
    public class BillDetailModel : DetailRowState
    {
        /// <summary>
        /// 帳單
        /// </summary>
        [ForeignKey(nameof(BillNo))] public BillModel Bill { get; set; }
        /// <summary>
        /// 帳單編號
        /// </summary>
        [Description(SystemCP.DESC_BillNo), Key] public string BillNo { get; set; }
        /// <summary>
        /// 序號
        /// </summary>
        [Description(SystemCP.DESC_RowId), Key] public int RowId { get; set; }
        /// <summary>
        /// 費用
        /// </summary>
        [Description(SystemCP.DESC_FeeName), Required, InputField, MaxLength(SystemCP.NormalLen)] public string FeeName { get; set; }
        /// <summary>
        /// 應繳金額
        /// </summary>
        [Description(SystemCP.DESC_ShouldPayAmount), InputField] public decimal PayAmount { get; set; }
    }
    /// <summary>
    /// 帳單收款明細
    /// </summary>
    [Description(SystemCP.DESC_BillReceiptDt)]
    public class BillReceiptDetailModel : DetailRowState
    {
        /// <summary>
        /// 帳單
        /// </summary>
        [ForeignKey(nameof(BillNo))] public BillModel Bill { get; set; }
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
