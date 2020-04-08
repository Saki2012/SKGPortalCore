using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Model.System;

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
        /// <summary>
        /// 收款單異動說明表
        /// </summary>
        public List<ReceiptBillChangeModel> ReceiptBillChange { get; set; } = new List<ReceiptBillChangeModel>();
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
        [Description("單據編號"), Key, MaxLength(CP.BillNoLen)] public string BillNo { get; set; }
        [ForeignKey("CustomerCode")] public BizCustomerModel Customer { get; set; }
        /// <summary>
        /// 企業編號
        /// </summary>
        [Description("企業編號")] public string CustomerCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("CollectionTypeId")] public CollectionTypeModel CollectionType { get; set; }
        /// <summary>
        /// 代收類別代號
        /// </summary>
        [Description("代收類別代號")] public string CollectionTypeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("ChannelId")] public ChannelModel Channel { get; set; }
        /// <summary>
        /// 代收通路
        /// </summary>
        [Description("代收通路")] public string ChannelId { get; set; }
        /// <summary>
        /// 交易日期
        /// </summary>
        [Description("交易日期")] public DateTime TradeDate { get; set; }
        /// <summary>
        /// 傳輸日期
        /// </summary>
        [Description("傳輸日期")] public DateTime TransDate { get; set; }
        /// <summary>
        /// 預計匯款日
        /// </summary>
        [Description("預計匯款日")] public DateTime ExpectRemitDate { get; set; }
        /// <summary>
        /// 實繳金額
        /// </summary>
        [Description("實繳金額")] public decimal PayAmount { get; set; }
        /// <summary>
        /// 虛擬帳號
        /// </summary>
        [Description("虛擬帳號"), MaxLength(CP.NormalLen)] public string VirtualAccountCode { get; set; }
        /// <summary>
        /// 通路手續費清算方式
        /// </summary>
        [Description("通路手續費清算方式")] public ChargePayType ChargePayType { get; set; }
        /// <summary>
        /// 銀行手續費類型
        /// </summary>
        [Description("銀行手續費類型")] public BankFeeType BankFeeType { get; set; }
        /// <summary>
        /// 銀行手續費
        /// </summary>
        [Description("銀行手續費")] public decimal BankFee { get; set; }
        /// <summary>
        /// 介紹商手續費(系統商手續費)
        /// </summary>
        [Description("介紹商手續費")] public decimal ThirdFee { get; set; }
        /// <summary>
        /// 通路回饋手續費
        /// </summary>
        [Description("通路回饋手續費")] public decimal ChannelFeedBackFee { get; set; }
        /// <summary>
        /// 通路回扣手續費
        /// </summary>
        [Description("通路回扣手續費")] public decimal ChannelRebateFee { get; set; }
        /// <summary>
        /// 通路手續費
        /// </summary>
        [Description("通路手續費")] public decimal ChannelFee { get; set; }
        /// <summary>
        /// 通路總手續費
        /// </summary>
        [Description("通路總手續費")] public decimal ChannelTotalFee { get; set; }
        /// <summary>
        /// 總手續費(供商戶查看用)
        /// </summary>
        [Description("總手續費")] public decimal TotalFee { get; set; }
        /// <summary>
        /// 帳單ProgId
        /// 為分辨【帳單】【入金機】【約定扣款】等使用
        /// </summary>
        [Description("帳單ProgId"), Required, MaxLength(CP.NormalLen)] public string BillProgId { get; set; }
        /// <summary>
        /// 帳單編號
        /// </summary>
        [Description("帳單編號"), Required, MaxLength(CP.BillNoLen)] public string ToBillNo { get; set; }
        /// <summary>
        /// 匯入批號
        /// </summary>
        [Description("匯入批號"), Required, MaxLength(CP.NormalLen)] public string ImportBatchNo { get; set; }
        /// <summary>
        /// 來源
        /// </summary>
        [Description("來源"), MaxLength(200)] public string Source { get; set; }
        /// <summary>
        /// 異常資料
        /// </summary>
        [Description("異常資料")] public bool IsErrData { get; set; }
        /// <summary>
        /// 異常訊息
        /// </summary>
        [Description("異常訊息")] public string ErrMessage { get; set; }
    }
    /// <summary>
    /// 收款單異動說明表
    /// </summary>
    [Description("收款單異動說明表")]
    public class ReceiptBillChangeModel : DetailRowState
    {
        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("BillNo")] public ReceiptBillModel Bill { get; set; }
        /// <summary>
        /// 單據編號
        /// </summary>
        [Description("單據編號"), Key] public string BillNo { get; set; }
        /// <summary>
        /// 序號
        /// </summary>
        [Description("序號"), Key] public int RowId { get; set; }
        /// <summary>
        /// 異動時間
        /// </summary>
        [Description("異動時間")] public DateTime ChangeTime { get; set; }
        /// <summary>
        /// 狀態
        /// </summary>
        [Description("狀態")] public FormStatus FormStatus { get; set; }
        /// <summary>
        /// 異動原因
        /// </summary>
        [Description("異動原因"), MaxLength(CP.LongLen)] public string Reason { get; set; }
    }
}
