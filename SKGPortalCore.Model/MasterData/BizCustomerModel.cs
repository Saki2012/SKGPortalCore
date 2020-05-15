using SKGPortalCore.Core;
using SKGPortalCore.Core.LibAttribute;
using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Model;
using SKGPortalCore.Core.Model.User;
using SKGPortalCore.Model.SourceData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model.MasterData
{
    /// <summary>
    /// 商戶資料
    /// </summary>
    [Description(SystemCP.DESC_BizCustomer)]
    public class BizCustomerSet
    {
        /// <summary>
        /// 商戶資料
        /// </summary>
        [Description(SystemCP.DESC_BizCustomer)] public BizCustomerModel BizCustomer { get; set; } = new BizCustomerModel();
        /// <summary>
        /// 商戶手續費管理明細
        /// </summary>
        [Description(SystemCP.DESC_BizCustomerFeeDt)] public List<BizCustomerFeeDetailModel> BizCustomerFeeDetail { get; set; } = new List<BizCustomerFeeDetailModel>();
    }
    /// <summary>
    /// 商戶資料
    /// </summary>
    [Description(SystemCP.DESC_BizCustomer)]
    public class BizCustomerModel : MasterDataModel
    {
        /// <summary>
        /// 企業編號
        /// </summary>
        [Description(SystemCP.DESC_CustomerCode), Key, MaxLength(SystemCP.DataIdLen)] public string CustomerCode { get; set; }
        /// <summary>
        /// 客戶資料
        /// </summary>
        [ForeignKey(nameof(CustomerId))] public CustomerModel Customer { get; set; }
        /// <summary>
        /// 客戶統編
        /// </summary>
        [Description(SystemCP.DESC_CustomerId), Required] public string CustomerId { get; set; }
        /// <summary>
        /// 部門資料
        /// </summary>
        [ForeignKey(nameof(AccountDeptId))] public DeptModel AccountDept { get; set; }
        /// <summary>
        /// 帳務分行
        /// </summary>
        [Description(SystemCP.DESC_AccountDeptId), InputField] public string AccountDeptId { get; set; }
        /// <summary>
        /// 實體帳號
        /// </summary>
        [Description(SystemCP.DESC_RealAccount), Required, InputField, MaxLength(SystemCP.NormalLen)] public string RealAccount { get; set; }
        /// <summary>
        /// 虛擬帳號長度
        /// </summary>
        [Description(SystemCP.DESC_VirtualAccountLen), InputField] public VirtualAccountLen VirtualAccountLen { get; set; }
        /// <summary>
        /// 期別編號長度
        /// </summary>
        [Description(SystemCP.DESC_BillTermLen), InputField] public byte BillTermLen { get; set; }
        /// <summary>
        /// 繳款人編號長度
        /// </summary>
        [Description(SystemCP.DESC_PayerNoLen), InputField] public byte PayerNoLen { get; set; }
        /// <summary>
        /// 自組銷帳編號1
        /// </summary>
        [Description(SystemCP.DESC_VirtualAccount + "1"), InputField] public VirtualAccount1 VirtualAccount1 { get; set; }
        /// <summary>
        /// 自組銷帳編號2
        /// </summary>
        [Description(SystemCP.DESC_VirtualAccount + "2"), InputField] public VirtualAccount2 VirtualAccount2 { get; set; }
        /// <summary>
        /// 自組銷帳編號3
        /// </summary>
        [Description(SystemCP.DESC_VirtualAccount + "3"), InputField] public VirtualAccount3 VirtualAccount3 { get; set; }
        /// <summary>
        /// 啟用通路
        /// (逗號分割)
        /// </summary>
        [Description(SystemCP.DESC_ChannelIds), Required, InputField, MaxLength(SystemCP.LongLen)] public string ChannelIds { get; set; }
        /// <summary>
        /// 啟用代收項目
        /// (逗號分割)
        /// </summary>
        [Description(SystemCP.DESC_CollectionTypeIds), Required, InputField, MaxLength(100)] public string CollectionTypeIds { get; set; }
        /// <summary>
        /// 啟用超商通路
        /// </summary>
        [Description(SystemCP.DESC_MarketEnable)] public bool MarketEnable { get; set; }
        /// <summary>
        /// 啟用郵局通路
        /// </summary>
        [Description(SystemCP.DESC_PostEnable)] public bool PostEnable { get; set; }
        /// <summary>
        /// 商戶類型
        /// </summary>
        [Description(SystemCP.DESC_BizCustType), InputField] public BizCustType BizCustType { get; set; }
        /// <summary>
        /// 介紹商企業
        /// </summary>
        [ForeignKey(nameof(IntroCustomerCode))] public BizCustomerModel IntroCustomer { get; set; }
        /// <summary>
        /// 介紹商企業代號
        /// </summary>
        [Description(SystemCP.DESC_IntroCustomerCode), InputField] public string IntroCustomerCode { get; set; }
        /// <summary>
        /// 帳戶狀態
        /// </summary>
        [Description(SystemCP.DESC_AccountStatus), InputField] public AccountStatus AccountStatus { get; set; }
        /// <summary>
        /// 導入時間
        /// </summary>
        [Description(SystemCP.DESC_SyncDateTime)] public DateTime SyncDateTime { get; set; }
        /// <summary>
        /// 匯入批號
        /// </summary>
        [Description(SystemCP.DESC_ImportBatchNo), Required, MaxLength(SystemCP.NormalLen)] public string ImportBatchNo { get; set; } = string.Empty;
        /// <summary>
        /// 來源
        /// </summary>
        [Description(SystemCP.DESC_Source), MaxLength(200)] public string Source { get; set; }
    }
    /// <summary>
    /// 商戶手續費管理明細
    /// </summary>
    [Description(SystemCP.DESC_BizCustomerFeeDt)]
    public class BizCustomerFeeDetailModel : DetailRowState
    {
        [ForeignKey(nameof(CustomerCode))] public BizCustomerModel BizCustomer { get; set; }
        /// <summary>
        /// 企業編號
        /// </summary>
        [Description(SystemCP.DESC_CustomerCode), Key] public string CustomerCode { get; set; }
        /// <summary>
        /// 通路類別
        /// </summary>
        [Description(SystemCP.DESC_ChannelGroupType), Key] public ChannelGroupType ChannelGroupType { get; set; }
        /// <summary>
        /// 銀行手續費類型
        /// </summary>
        [Description(SystemCP.DESC_BankFeeType), InputField] public BankFeeType BankFeeType { get; set; }
        /// <summary>
        /// 手續費
        /// </summary>
        [Description(SystemCP.DESC_Fee), InputField] public decimal Fee { get; set; }
        /// <summary>
        /// 介紹商手續費/分潤%
        /// </summary>
        [Description(SystemCP.DESC_IntroPercent), InputField] public decimal IntroPercent { get; set; }
    }

    /*
     View啟用通路費用表：(條件取自表頭：啟用通路、啟用代收類別)、
     商戶手續費管理檢查：1. 同通路類別的每筆總手續費不應小於最大的通路總手續費、

     代收通路
     通路類別
      
     通路清算方式(內扣外加)
     收款區間
     通路總手續費
     */
}
