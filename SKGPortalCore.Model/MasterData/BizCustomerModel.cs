﻿using SKGPortalCore.Model.Enum;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model.MasterData
{
    /// <summary>
    /// 商戶資料
    /// </summary>
    [Description("商戶資料")]
    public class BizCustomerSet
    {
        /// <summary>
        /// 商戶資料
        /// </summary>
        [Description("商戶資料")]
        public BizCustomerModel BizCustomer { get; set; }
        /// <summary>
        /// 商戶手續費管理明細
        /// </summary>
        [Description("商戶手續費管理明細")]
        public List<BizCustomerFeeDetailModel> BizCustomerFeeDetail { get; set; }
    }
    /// <summary>
    /// 商戶資料
    /// </summary>
    [Description("商戶資料")]
    public class BizCustomerModel : MasterDataModel
    {
        /// <summary>
        /// 企業編號
        /// </summary>
        [Description("企業編號"), Key]
        public string CustomerCode { get; set; }
        [ForeignKey("CustomerId")]
        public CustomerModel Customer { get; set; }
        /// <summary>
        /// 客戶統編
        /// </summary>
        [Description("客戶統編")]
        public string CustomerId { get; set; }
        /// <summary>
        /// 帳務分行
        /// </summary>
        [Description("帳務分行")]
        public string AccountDeptId { get; set; }
        /// <summary>
        /// 實體帳號
        /// </summary>
        [Description("實體帳號")]
        public string RealAccount { get; set; }
        /// <summary>
        /// 銷帳編號長度
        /// </summary>
        [Description("銷帳編號長度")]
        public byte VirtualAccountLen { get; set; }
        /// <summary>
        /// 期別編號長度
        /// </summary>
        [Description("期別編號長度")]
        public byte BillTermLen { get; set; }
        /// <summary>
        /// 繳款人編號長度
        /// </summary>
        [Description("繳款人編號長度")]
        public byte PayerNoLen { get; set; }
        /// <summary>
        /// 自組銷帳編號1
        /// </summary>
        [Description("自組銷帳編號1")]
        public VirtualAccount1 VirtualAccount1 { get; set; }
        /// <summary>
        /// 自組銷帳編號2
        /// </summary>
        [Description("自組銷帳編號2")]
        public VirtualAccount2 VirtualAccount2 { get; set; }
        /// <summary>
        /// 自組銷帳編號3
        /// </summary>
        [Description("自組銷帳編號3")]
        public VirtualAccount3 VirtualAccount3 { get; set; }
        /// <summary>
        /// 啟用通路
        /// (逗號分割)
        /// </summary>
        [Description("啟用通路")]
        public string ChannelIds { get; set; }
        /// <summary>
        /// 啟用代收類別
        /// (逗號分割)
        /// </summary>
        [Description("啟用代收類別")]
        public string CollectionTypeIds { get; set; }
        /// <summary>
        /// 啟用超商通路
        /// </summary>
        [Description("啟用超商通路")]
        public bool MarketEnable { get; set; }
        /// <summary>
        /// 啟用郵局通路
        /// </summary>
        [Description("啟用郵局通路")]
        public bool PostEnable { get; set; }
        /// <summary>
        /// 帳戶狀態
        /// </summary>
        [Description("帳戶狀態")]
        public AccountStatus AccountStatus { get; set; }
        /// <summary>
        /// 導入批號
        /// </summary>
        [Description("導入批號")]
        public string ImportBatchNo { get; set; }
        /// <summary>
        /// 來源
        /// </summary>
        [Description("來源")]
        public string Source { get; set; }
    }
    /// <summary>
    /// 商戶手續費管理明細
    /// </summary>
    [Description("商戶手續費管理明細")]
    public class BizCustomerFeeDetailModel : DetailRowState
    {
        [ForeignKey("CustomerCode")]
        public BizCustomerModel BizCustomer { get; set; }
        /// <summary>
        /// 企業編號
        /// </summary>
        [Description("企業編號"), Key]
        public string CustomerCode { get; set; }
        /// <summary>
        /// 通路類別
        /// </summary>
        [Description("通路類別"), Key]
        public ChannelGroupType ChannelType { get; set; }
        /// <summary>
        /// 手續費類型
        /// (只記錄1:清算手續費、4:每筆總手續費)
        /// </summary>
        [Description("手續費類型")]
        public FeeType FeeType { get; set; }
        /// <summary>
        /// 手續費
        /// </summary>
        [Description("手續費")]
        public decimal Fee { get; set; }
        /// <summary>
        /// 分潤%
        /// </summary>
        [Description("分潤%")]
        public decimal Percent { get; set; }
    }

    /*
     View啟用通路費用表：(條件取自表頭：啟用通路、啟用代收類別)、
     商戶手續費管理檢查：1. 同通路類別的每筆總手續費不應小於最大的通路總手續費、
                         2. 通路類別不允許重複

     代收類別
     清算方式
     代收通路
     通路類別
     收款區間
     通路總手續費
     */
}
