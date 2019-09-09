using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SKGPortalCore.Model.MasterData;

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
        public BillModel Bill { get; set; }
        /// <summary>
        /// 帳單明細
        /// </summary>
        public List<BillDetailModel> BillDetail { get; set; }
        /// <summary>
        /// 帳單收款明細
        /// </summary>
        public List<BillReceiptDetailModel> BillReceiptDetail { get; set; }
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
        [Description("帳單編號"), Key, MaxLength(20)]
        public string BillNo { get; set; }
        [ForeignKey("CustomerCode,BillTermId")]
        public BillTermModel BillTerm { get; set; }
        /// <summary>
        /// 期別
        /// </summary>
        [Description("期別")]
        public string BillTermId { get; set; }
        [ForeignKey("CustomerId")]
        public CustomerModel Customer { get; set; }
        /// <summary>
        /// 客戶統編
        /// </summary>
        [Description("客戶統編")]
        public string CustomerId { get; set; }
        [ForeignKey("CustomerCode")]
        public BizCustomerModel BizCustomer { get; set; }
        /// <summary>
        /// 企業編號
        /// </summary>
        [Description("企業編號")]
        public string CustomerCode { get; set; }
        [ForeignKey("CustomerId,PayerId")]
        public PayerModel Payer { get; set; }
        /// <summary>
        /// 會員
        /// </summary>
        [Description("會員")]
        public string PayerId { get; set; }
        /// <summary>
        /// 繳款人類別
        /// </summary>
        [Description("繳款人類別")]
        public PayerType PayerType { get; set; }
        /// <summary>
        /// 匯入批號
        /// </summary>
        [Description("匯入批號"), Required,MaxLength(15)]
        public string ImportBatchNo { get; set; }
        /// <summary>
        /// 銀行銷帳編號(不含檢碼)
        /// </summary>
        [Description("銀行銷帳編號"), Required]
        public string CompareCodeForCheck { get; set; }
        /// <summary>
        /// 銀行條碼
        /// </summary>
        [Description("銀行條碼"), Required]
        public string BankBarCode { get; set; }
        /// <summary>
        /// 超商條碼1
        /// </summary>
        [Description("超商條碼1"), Required]
        public string MarketBarCode1 { get; set; }
        /// <summary>
        /// 超商條碼2
        /// </summary>
        [Description("超商條碼2"), Required]
        public string MarketBarCode2 { get; set; }
        /// <summary>
        /// 超商條碼3
        /// </summary>
        [Description("超商條碼3"), Required]
        public string MarketBarCode3 { get; set; }
        /// <summary>
        /// 郵局條碼1
        /// </summary>
        [Description("郵局條碼1"), Required]
        public string PostBarCode1 { get; set; }
        /// <summary>
        /// 郵局條碼2
        /// </summary>
        [Description("郵局條碼2"), Required]
        public string PostBarCode2 { get; set; }
        /// <summary>
        /// 郵局條碼3
        /// </summary>
        [Description("郵局條碼3"), Required]
        public string PostBarCode3 { get; set; }
        /// <summary>
        /// 繳款截止日
        /// </summary>
        [Description("繳款截止日"), Required]
        public DateTime PayEndDate { get; set; }
        /// <summary>
        /// 應繳金額
        /// </summary>
        [Description("應繳金額")]
        public decimal PayAmount { get; set; }
        /// <summary>
        /// 已繳金額
        /// </summary>
        [Description("已繳金額")]
        public decimal HasPayAmount { get; set; }
        /// <summary>
        /// 繳款狀態
        /// </summary>
        [Description("繳款狀態")]
        public PayStatus PayStatus { get; set; }
        /// <summary>
        /// 帳單備註1
        /// </summary>
        [Description("帳單備註1"), Required]
        public string Memo1 { get; set; }
        /// <summary>
        /// 帳單備註2
        /// </summary>
        [Description("帳單備註2"), Required]
        public string Memo2 { get; set; }
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
        [Description("序號"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RowId { get; set; }
        /// <summary>
        /// 期別行鍵
        /// </summary>
        [Description("期別行鍵")]
        public int BillTermRowId { get; set; }
        /// <summary>
        /// 應繳金額
        /// </summary>
        [Description("應繳金額")]
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
        /// <summary>
        /// 序號
        /// </summary>
        [Description("序號"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RowId { get; set; }
        [ForeignKey("ReceiptBillNo")]
        public ReceiptBillModel ReceiptBill { get; set; }
        /// <summary>
        /// 收款單號
        /// </summary>
        [Description("收款單號"), Required]
        public string ReceiptBillNo { get; set; }
    }
}
