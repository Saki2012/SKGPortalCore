using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SKGPortalCore.Model.Enum;
using SKGPortalCore.Model.MasterData;

namespace SKGPortalCore.Model.BillData
{
    /// <summary>
    /// 收款單
    /// </summary>
    [Description("收款單")]
    public class ReceiptBillSet
    {
        /// <summary>
        /// 收款單主檔
        /// </summary>
        public ReceiptBillModel ReceiptBill { get; set; }
    }
    /// <summary>
    /// 收款單主檔
    /// </summary>
    [Description("收款單主檔")]
    public class ReceiptBillModel : BillDataModel
    {
        /// <summary>
        /// 單據編號
        /// </summary>
        [Description("單據編號"), Key]
        public string BillNo { get; set; }
        [ForeignKey("CustomerCode")]
        public BizCustomerModel Customer { get; set; }
        /// <summary>
        /// 企業編號
        /// </summary>
        [Description("企業編號")]
        public string CustomerCode { get; set; }
        public CollectionTypeModel CollectionType { get; set; }
        /// <summary>
        /// 代收類別代號
        /// </summary>
        [Description("代收類別代號")]
        public string CollectionTypeId { get; set; }
        [ForeignKey("ChannelId")]
        public ChannelModel Channel { get; set; }
        /// <summary>
        /// 代收通路
        /// </summary>
        [Description("代收通路")]
        public string ChannelId { get; set; }
        /// <summary>
        /// 傳輸日期
        /// </summary>
        [Description("傳輸日期")]
        public DateTime TransDate { get; set; }
        /// <summary>
        /// 交易日期
        /// </summary>
        [Description("交易日期")]
        public DateTime TradeDate { get; set; }
        /// <summary>
        /// 預計匯款日
        /// </summary>
        [Description("預計匯款日")]
        public DateTime ExpectRemitDate { get; set; }
        /// <summary>
        /// 實繳金額
        /// </summary>
        [Description("實繳金額")]
        public decimal PayAmount { get; set; }
        /// <summary>
        /// 銀行銷帳編號
        /// </summary>
        [Description("銀行銷帳編號")]
        public string BankBarCode { get; set; }
        /// <summary>
        /// 資訊流資料異常狀態
        /// </summary>
        [Description("資訊流資料異常狀態")]
        public TransDataUnusual TransDataUnusual { get; set; }
        /// <summary>
        /// 通路手續費清算方式
        /// </summary>
        [Description("通路手續費清算方式")]
        public ChargePayType ChargePayType { get; set; }
        /// <summary>
        /// 通路手續費
        /// </summary>
        [Description("通路手續費")]
        public decimal ChannelFee { get; set; }
        /// <summary>
        /// 清算手續費
        /// </summary>
        [Description("清算手續費")]
        public decimal BankFee { get; set; }
        /// <summary>
        /// 介紹商手續費(系統商手續費)
        /// </summary>
        [Description("介紹商手續費")]
        public decimal ThirdFee { get; set; }
        [ForeignKey("ToBillNo")]
        public BillModel Bill { get; set; }
        /// <summary>
        /// 帳單編號
        /// </summary>
        [Description("帳單編號")]
        public string ToBillNo { get; set; }
        public ReceiptBillModel RushBill { get; set; }
        /// <summary>
        /// 沖抵單號
        /// </summary>
        [Description("沖抵單號")]
        public string RushBillNo { get; set; }
        [ForeignKey("BeRushedBillNo")]
        public ReceiptBillModel BeRushedBill { get; set; }
        /// <summary>
        /// 被沖抵單號
        /// </summary>
        [Description("被沖抵單號")]
        public string BeRushedBillNo { get; set; }
        /// <summary>
        /// 匯入批號
        /// </summary>
        [Description("匯入批號")]
        public string ImportBatchNo { get; set; }
        /// <summary>
        /// 來源
        /// </summary>
        [Description("來源")]
        public string Source { get; set; }
        /// <summary>
        /// 異常資料
        /// </summary>
        [Description("異常資料")]
        public bool ErrData { get; set; }
        /// <summary>
        /// 異常訊息
        /// </summary>
        [Description("異常訊息")]
        public string ErrMessage { get; set; }
    }
}
