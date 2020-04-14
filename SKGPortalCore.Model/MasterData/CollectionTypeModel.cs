using SKGPortalCore.Lib;
using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Model.System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model.MasterData
{
    /// <summary>
    /// 代收項目
    /// </summary>
    [Description(SystemCP.DESC_CollectionType)]
    public class CollectionTypeSet
    {
        /// <summary>
        /// 代收項目
        /// </summary>
        [Description(SystemCP.DESC_CollectionType)] public CollectionTypeModel CollectionType { get; set; } = new CollectionTypeModel();
        /// <summary>
        /// 代收項目費率明細
        /// </summary>
        [Description(SystemCP.DESC_CollectionTypeDt)] public List<CollectionTypeDetailModel> CollectionTypeDetail { get; set; } = new List<CollectionTypeDetailModel>();
        /// <summary>
        /// 通路核銷週期明細
        /// </summary>
        [Description(SystemCP.DESC_CollectionType)] public List<CollectionTypeVerifyPeriodModel> CollectionTypeVerifyPeriod { get; set; } = new List<CollectionTypeVerifyPeriodModel>();
    }
    /// <summary>
    /// 代收項目
    /// </summary>
    [Description(SystemCP.DESC_CollectionType)]
    public class CollectionTypeModel : MasterDataModel
    {
        /// <summary>
        /// 代收項目代號
        /// </summary>
        [Description(SystemCP.DESC_CollectionTypeId), Key, MaxLength(SystemCP.DataIdLen)] public string CollectionTypeId { get; set; }
        /// <summary>
        /// 代收項目名稱
        /// </summary>
        [Description(SystemCP.DESC_CollectionTypeName), Required, InputField, MaxLength(SystemCP.NormalLen)] public string CollectionTypeName { get; set; }
        /// <summary>
        /// 通路手續費清算方式
        /// </summary>
        [Description(SystemCP.DESC_ChargePayType), InputField] public ChargePayType ChargePayType { get; set; }
    }
    /// <summary>
    /// 代收項目費率明細
    /// </summary>
    [Description(SystemCP.DESC_CollectionTypeDt)]
    public class CollectionTypeDetailModel : DetailRowState
    {
        /// <summary>
        /// 代收項目
        /// </summary>
        [ForeignKey(nameof(CollectionTypeId))] public CollectionTypeModel CollectionType { get; set; }
        /// <summary>
        /// 代收項目代號
        /// </summary>
        [Description(SystemCP.DESC_CollectionTypeId), Key] public string CollectionTypeId { get; set; }
        /// <summary>
        /// 序號
        /// </summary>
        [Description(SystemCP.DESC_RowId), Key] public int RowId { get; set; }
        /// <summary>
        /// 代收通路
        /// </summary>
        [ForeignKey(nameof(ChannelId))] public ChannelModel Channel { get; set; }
        /// <summary>
        /// 代收通路代號
        /// </summary>
        [Description(SystemCP.DESC_ChannelId), Required, InputField] public string ChannelId { get; set; }
        /// <summary>
        /// 收款區間(起)
        /// </summary>
        [Description(SystemCP.DESC_SRange), InputField] public decimal SRange { get; set; }
        /// <summary>
        /// 收款區間(迄)
        /// </summary>
        [Description(SystemCP.DESC_ERange), InputField] public decimal ERange { get; set; }
        /// <summary>
        /// 通路手續費
        /// </summary>
        [Description(SystemCP.DESC_ChannelFee), InputField] public decimal ChannelFee { get; set; }
        /// <summary>
        /// 通路回饋手續費
        /// </summary>
        [Description(SystemCP.DESC_ChannelFeedBackFee), InputField] public decimal ChannelFeedBackFee { get; set; }
        /// <summary>
        /// 通路回扣手續費
        /// </summary>
        [Description(SystemCP.DESC_ChannelRebateFee), InputField] public decimal ChannelRebateFee { get; set; }
        /// <summary>
        /// 通路總手續費
        /// </summary>
        [Description(SystemCP.DESC_ChannelTotalFee)] public decimal ChannelTotalFee { get; set; }
    }
    /// <summary>
    /// 通路核銷週期明細
    /// </summary>
    [Description(SystemCP.DESC_CollectionTypeVerifyPeriod)]
    public class CollectionTypeVerifyPeriodModel : DetailRowState
    {
        /// <summary>
        /// 代收項目
        /// </summary>
        [ForeignKey(nameof(CollectionTypeId))] public CollectionTypeModel CollectionType { get; set; }
        /// <summary>
        /// 代收類別代號
        /// </summary>
        [Description(SystemCP.DESC_CollectionTypeId), Key] public string CollectionTypeId { get; set; }
        /// <summary>
        /// 代收通路
        /// </summary>
        [ForeignKey(nameof(ChannelId))] public ChannelModel Channel { get; set; }
        /// <summary>
        /// 代收通路代號
        /// </summary>
        [Description(SystemCP.DESC_ChannelId), Key] public string ChannelId { get; set; }
        /// <summary>
        /// 通路帳務核銷週期
        /// </summary>
        [Description(SystemCP.DESC_PayPeriodType), InputField] public PayPeriodType PayPeriodType { get; set; }
    }
}
