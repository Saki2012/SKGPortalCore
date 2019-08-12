using SKGPortalCore.Model.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        public BizCustomerModel Customer { get; set; }
        /// <summary>
        /// 客戶統編
        /// </summary>
        [Description("客戶統編")]
        public string CustomerId { get; set; }
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
        public CollectionTypeDetailModel CollectionTypeDetail { get; set; }
        /// <summary>
        /// 代收類別明細序號
        /// </summary>
        [Description("代收類別明細序號")]
        public int CollectionTypeDetailRowId { get; set; }
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
        public DateTime RemitDate { get; set; }
        /// <summary>
        /// 實繳金額
        /// </summary>
        [Description("實繳金額")]
        public decimal PayAmount { get; set; }
        /// <summary>
        /// 期別
        /// </summary>
        [Description("期別")]
        public string BillTermId { get; set; }
        /// <summary>
        /// 銀行銷帳編號
        /// </summary>
        [Description("銀行銷帳編號")]
        public string BankCode { get; set; }
        /// <summary>
        /// 銷帳條碼
        /// </summary>
        [Description("銷帳條碼")]
        public string CompareCode { get; set; }
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
        /// 通路手續費
        /// </summary>
        [Description("清算手續費")]
        public decimal BankFee { get; set; }
        /// <summary>
        /// 介紹商手續費(系統商手續費)
        /// </summary>
        [Description("介紹商手續費")]
        public decimal ThirdFee { get; set; }
        /// <summary>
        /// HiTrust手續費
        /// </summary>
        [Description("HiTrust手續費")]
        public decimal HiTrustFee { get; set; }
        /// <summary>
        /// 來源模型
        /// </summary>
        [Description("來源模型")]
        public string Source { get; set; }
        /// <summary>
        /// 來源主鍵
        /// </summary>
        [Description("來源主鍵")]
        public int SourceId { get; set; }
    }
}
