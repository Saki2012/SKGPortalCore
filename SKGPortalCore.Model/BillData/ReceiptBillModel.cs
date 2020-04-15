using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Model.System;

namespace SKGPortalCore.Model.BillData
{
    /// <summary>
    /// 收款單
    /// </summary>
    [Description(SystemCP.DESC_ReceiptBill)]
    public class ReceiptBillSet
    {
        /// <summary>
        /// 收款單
        /// </summary>
        [Description(SystemCP.DESC_ReceiptBill)] public ReceiptBillModel ReceiptBill { get; set; } = new ReceiptBillModel();
        /// <summary>
        /// 收款單異動說明表
        /// </summary>
        [Description(SystemCP.DESC_ReceiptBillChange)] public List<ReceiptBillChangeModel> ReceiptBillChange { get; set; } = new List<ReceiptBillChangeModel>();
    }
    /// <summary>
    /// 收款單
    /// </summary>
    [Description(SystemCP.DESC_ReceiptBill)]
    public class ReceiptBillModel : BillDataModel
    {
        /// <summary>
        /// 單據編號
        /// </summary>
        [Description(SystemCP.DESC_BillNo), Key, MaxLength(SystemCP.BillNoLen)] public string BillNo { get; set; }
        /// <summary>
        /// 商戶資料
        /// </summary>
        [ForeignKey(nameof(CustomerCode))] public BizCustomerModel Customer { get; set; }
        /// <summary>
        /// 企業編號
        /// </summary>
        [Description(SystemCP.DESC_CustomerCode)] public string CustomerCode { get; set; }
        /// <summary>
        /// 代收類別
        /// </summary>
        [ForeignKey(nameof(CollectionTypeId))] public CollectionTypeModel CollectionType { get; set; }
        /// <summary>
        /// 代收項目
        /// </summary>
        [Description(SystemCP.DESC_CollectionTypeId)] public string CollectionTypeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(ChannelId))] public ChannelModel Channel { get; set; }
        /// <summary>
        /// 代收通路
        /// </summary>
        [Description(SystemCP.DESC_ChannelId)] public string ChannelId { get; set; }
        /// <summary>
        /// 交易日期
        /// </summary>
        [Description(SystemCP.DESC_TradeDate)] public DateTime TradeDate { get; set; }
        /// <summary>
        /// 傳輸日期
        /// </summary>
        [Description(SystemCP.DESC_TransDate)] public DateTime TransDate { get; set; }
        /// <summary>
        /// 預計匯款日
        /// </summary>
        [Description(SystemCP.DESC_ExpectRemitDate)] public DateTime ExpectRemitDate { get; set; }
        /// <summary>
        /// 實繳金額
        /// </summary>
        [Description(SystemCP.DESC_PaidAmount)] public decimal PayAmount { get; set; }
        /// <summary>
        /// 虛擬帳號
        /// </summary>
        [Description(SystemCP.DESC_VirtualAccountCode), MaxLength(SystemCP.NormalLen)] public string VirtualAccountCode { get; set; }
        /// <summary>
        /// 通路手續費清算方式
        /// </summary>
        [Description(SystemCP.DESC_ChargePayType)] public ChargePayType ChargePayType { get; set; }
        /// <summary>
        /// 銀行手續費類型
        /// </summary>
        [Description(SystemCP.DESC_BankFeeType)] public BankFeeType BankFeeType { get; set; }
        /// <summary>
        /// 銀行手續費
        /// </summary>
        [Description(SystemCP.DESC_BankFee)] public decimal BankFee { get; set; }
        /// <summary>
        /// 介紹商手續費
        /// </summary>
        [Description(SystemCP.DESC_IntroFee)] public decimal ThirdFee { get; set; }
        /// <summary>
        /// 通路回饋手續費
        /// </summary>
        [Description(SystemCP.DESC_ChannelFeedBackFee)] public decimal ChannelFeedBackFee { get; set; }
        /// <summary>
        /// 通路回扣手續費
        /// </summary>
        [Description(SystemCP.DESC_ChannelRebateFee)] public decimal ChannelRebateFee { get; set; }
        /// <summary>
        /// 通路手續費
        /// </summary>
        [Description(SystemCP.DESC_ChannelFee)] public decimal ChannelFee { get; set; }
        /// <summary>
        /// 通路總手續費
        /// </summary>
        [Description(SystemCP.DESC_ChannelTotalFee)] public decimal ChannelTotalFee { get; set; }
        /// <summary>
        /// 總手續費(供商戶查看用)
        /// </summary>
        [Description(SystemCP.DESC_TotalFee)] public decimal TotalFee { get; set; }
        /// <summary>
        /// ProgId
        /// 為分辨【帳單】【入金機】【約定扣款】等使用
        /// </summary>
        [Description(SystemCP.DESC_ProgId), Required, MaxLength(SystemCP.NormalLen)] public string BillProgId { get; set; }
        /// <summary>
        /// 來源單據編號
        /// </summary>
        [Description(SystemCP.DESC_SrcBillNo), Required, MaxLength(SystemCP.BillNoLen)] public string ToBillNo { get; set; }
        /// <summary>
        /// 匯入批號
        /// </summary>
        [Description(SystemCP.DESC_ImportBatchNo), Required, MaxLength(SystemCP.NormalLen)] public string ImportBatchNo { get; set; }
        /// <summary>
        /// 來源
        /// </summary>
        [Description(SystemCP.DESC_Source), MaxLength(200)] public string Source { get; set; }
        /// <summary>
        /// 異常資料
        /// </summary>
        [Description(SystemCP.DESC_ErrData)] public bool IsErrData { get; set; }
        /// <summary>
        /// 異常訊息
        /// </summary>
        [Description(SystemCP.DESC_ErrMessage)] public string ErrMessage { get; set; }
    }
    /// <summary>
    /// 收款單異動說明表
    /// </summary>
    [Description(SystemCP.DESC_ReceiptBillChange)]
    public class ReceiptBillChangeModel : DetailRowState
    {
        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(BillNo))] public ReceiptBillModel Bill { get; set; }
        /// <summary>
        /// 單據編號
        /// </summary>
        [Description(SystemCP.DESC_BillNo), Key] public string BillNo { get; set; }
        /// <summary>
        /// 序號
        /// </summary>
        [Description(SystemCP.DESC_RowId), Key] public int RowId { get; set; }
        /// <summary>
        /// 異動時間
        /// </summary>
        [Description(SystemCP.DESC_ChangeTime)] public DateTime ChangeTime { get; set; }
        /// <summary>
        /// 單據狀態
        /// </summary>
        [Description(SystemCP.DESC_FormStatus)] public FormStatus FormStatus { get; set; }
        /// <summary>
        /// 異動原因
        /// </summary>
        [Description(SystemCP.DESC_Reason), MaxLength(SystemCP.LongLen)] public string Reason { get; set; }
    }
}
