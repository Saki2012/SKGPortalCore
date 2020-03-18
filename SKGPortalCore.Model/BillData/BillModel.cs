using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Model.System;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model.BillData
{
    /// <summary>
    /// 帳單
    /// </summary>
    [Description("帳單")]
    public class BillSet
    {
        /// <summary>
        /// 帳單主檔
        /// </summary>
        public BillModel Bill { get; set; } = new BillModel();
        /// <summary>
        /// 帳單明細
        /// </summary>
        public List<BillDetailModel> BillDetail { get; set; } = new List<BillDetailModel>();
        /// <summary>
        /// 帳單收款明細
        /// </summary>
        public List<BillReceiptDetailModel> BillReceiptDetail { get; set; } = new List<BillReceiptDetailModel>();
    }
    /// <summary>
    /// 帳單主檔
    /// </summary>
    [Description("帳單主檔")]
    public class BillModel : BillDataModel
    {
        /// <summary>
        /// 帳單編號
        /// </summary>  
        [Description("帳單編號"), Key, MaxLength(ConstParameter.BillNoLen)]
        public string BillNo { get; set; }
        /// <summary>
        /// 企業編號
        /// </summary>
        [ForeignKey("CustomerCode")]
        public BizCustomerModel BizCustomer { get; set; }
        /// <summary>
        /// 企業編號
        /// </summary>
        [Description("企業編號"), Required, InputField]
        public string CustomerCode { get; set; }
        /// <summary>
        /// 期別
        /// </summary>
        [ForeignKey("CustomerCode,BillTermId")]
        public BillTermModel BillTerm { get; set; }
        /// <summary>
        /// 期別
        /// </summary>
        [Description("期別"), InputField]
        public string BillTermId { get; set; }
        /// <summary>
        /// 繳款人
        /// </summary>
        [ForeignKey("CustomerCode,PayerId")]
        public PayerModel Payer { get; set; }
        /// <summary>
        /// 繳款人
        /// </summary>
        [Description("繳款人"), InputField]
        public string PayerId { get; set; }
        /// <summary>
        /// 繳款截止日
        /// </summary>
        [Description("繳款截止日"), Required, InputField]
        public DateTime PayEndDate { get; set; }
        /// <summary>
        /// 代收項目
        /// </summary>
        [ForeignKey("CollectionTypeId")]
        public CollectionTypeModel CollectionType { get; set; }
        /// <summary>
        /// 代收項目
        /// </summary>
        [Description("代收項目"), InputField]
        public string CollectionTypeId { get; set; }
        /// <summary>
        /// 應繳金額
        /// </summary>
        [Description("應繳金額")]
        public decimal PayAmount { get; set; }
        /// <summary>
        /// 已繳金額
        /// </summary>
        [Description("已繳金額")]
        public decimal HadPayAmount { get; set; }
        /// <summary>
        /// 繳款狀態
        /// </summary>
        [Description("繳款狀態")]
        public PayStatus PayStatus { get; set; }
        /// <summary>
        /// 虛擬帳號
        /// </summary>
        [Description("虛擬帳號"), Required, Index, MaxLength(ConstParameter.NormalLen)]
        public string VirtualAccountCode { get; set; }
        /// <summary>
        /// 匯入批號
        /// </summary>
        [Description("匯入批號"), Required, MaxLength(ConstParameter.NormalLen)]
        public string ImportBatchNo { get; set; } = string.Empty;
    }
    /// <summary>
    /// 帳單明細
    /// </summary>
    [Description("帳單明細")]
    public class BillDetailModel : DetailRowState
    {
        [ForeignKey("BillNo")]
        public BillModel Bill { get; set; }
        /// <summary>
        /// 帳單編號
        /// </summary>
        [Description("帳單編號"), Key]
        public string BillNo { get; set; }
        /// <summary>
        /// 序號
        /// </summary>
        [Description("序號"), Key]
        public int RowId { get; set; }
        /// <summary>
        /// 費用
        /// </summary>
        [Description("費用"), Required, InputField, MaxLength(ConstParameter.NormalLen)]
        public string FeeName { get; set; }
        /// <summary>
        /// 應繳金額
        /// </summary>
        [Description("應繳金額"), InputField]
        public decimal PayAmount { get; set; }
    }
    /// <summary>
    /// 帳單收款明細
    /// </summary>
    [Description("帳單收款明細")]
    public class BillReceiptDetailModel : DetailRowState
    {
        [ForeignKey("BillNo")]
        public BillModel Bill { get; set; }
        /// <summary>
        /// 帳單編號
        /// </summary>
        [Description("帳單編號"), Key]
        public string BillNo { get; set; }
        [ForeignKey("ReceiptBillNo")]
        public ReceiptBillModel ReceiptBill { get; set; }
        /// <summary>
        /// 收款單號
        /// </summary>
        [Description("收款單號"), Key]
        public string ReceiptBillNo { get; set; }
    }
}
