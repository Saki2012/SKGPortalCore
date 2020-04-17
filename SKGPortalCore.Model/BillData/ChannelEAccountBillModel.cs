using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Model.System;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model.BillData
{
    /// <summary>
    /// 通路帳簿
    /// </summary>
    [Description(SystemCP.DESC_ChannelEAccountBill)]
    public class ChannelEAccountBillSet
    {
        /// <summary>
        /// 通路帳簿
        /// </summary>
        [Description(SystemCP.DESC_ChannelEAccountBill)] public ChannelEAccountBillModel ChannelEAccountBill { get; set; } = new ChannelEAccountBillModel();
        /// <summary>
        /// 通路收款明細帳簿
        /// </summary>
        [Description(SystemCP.DESC_ChannelEAccountBillDt)] public List<ChannelEAccountBillDetailModel> ChannelEAccountBillDetail { get; set; } = new List<ChannelEAccountBillDetailModel>();
    }

    /// <summary>
    /// 通路帳簿
    /// </summary>
    [Description(SystemCP.DESC_ChannelEAccountBill)]
    public class ChannelEAccountBillModel : BillDataModel
    {
        /// <summary>
        /// 單據編號
        /// </summary>
        [Description(SystemCP.DESC_BillNo), Key, MaxLength(SystemCP.BillNoLen)] public string BillNo { get; set; }
        /// <summary>
        /// 代收通路
        /// </summary>
        [ForeignKey(nameof(ChannelId))] public ChannelModel Channel { get; set; }
        /// <summary>
        /// 通路代號
        /// </summary>
        [Description(SystemCP.DESC_ChannelId), Index(SystemCP.IX_ChannelId_CollectionTypeId_ExpectRemitDate)] public string ChannelId { get; set; }
        /// <summary>
        /// 代收項目
        /// </summary>
        [ForeignKey(nameof(CollectionTypeId))] public CollectionTypeModel CollectionType { get; set; }
        /// <summary>
        /// 代收項目
        /// </summary>
        [Description(SystemCP.DESC_CollectionTypeId), Index(SystemCP.IX_ChannelId_CollectionTypeId_ExpectRemitDate)] public string CollectionTypeId { get; set; }
        /// <summary>
        /// 預計匯款日期
        /// </summary>
        [Description(SystemCP.DESC_ExpectRemitDate), Index(SystemCP.IX_ChannelId_CollectionTypeId_ExpectRemitDate)] public DateTime ExpectRemitDate { get; set; }
        /// <summary>
        /// 遞延天數
        /// </summary>
        [Description(SystemCP.DESC_PostponeDays)] public int PostponeDays { get; set; }
        /// <summary>
        /// 交易金額
        /// </summary>
        [Description(SystemCP.DESC_Amount)] public decimal Amount { get; set; }
        /// <summary>
        /// 通路手續費
        /// </summary>
        [Description(SystemCP.DESC_ChannelFee)] public decimal ChannelFee { get; set; }
        /// <summary>
        /// 預計匯款金額
        /// </summary>
        [Description(SystemCP.DESC_ExpectRemitAmount)] public decimal ExpectRemitAmount { get; set; }
        /// <summary>
        /// 筆數
        /// </summary>
        [Description(SystemCP.DESC_TotalCount)] public int TotalCount { get; set; }
    }
    /// <summary>
    /// 通路收款明細帳簿
    /// </summary>
    [Description(SystemCP.DESC_ChannelEAccountBillDt)]
    public class ChannelEAccountBillDetailModel : DetailRowState
    {
        /// <summary>
        /// 通路帳簿
        /// </summary>
        [ForeignKey(nameof(BillNo))] public ChannelEAccountBillModel Bill { get; set; }
        /// <summary>
        /// 單據編號
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
