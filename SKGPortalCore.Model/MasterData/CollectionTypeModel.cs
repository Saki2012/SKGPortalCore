using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Model.System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model.MasterData
{
    /// <summary>
    /// 代收類別
    /// </summary>
    [Description("代收類別")]
    public class CollectionTypeSet
    {
        public CollectionTypeModel CollectionType { get; set; } = new CollectionTypeModel();
        public List<CollectionTypeDetailModel> CollectionTypeDetail { get; set; } = new List<CollectionTypeDetailModel>();
        public List<CollectionTypeVerifyPeriodModel> CollectionTypeVerifyPeriod { get; set; } = new List<CollectionTypeVerifyPeriodModel>();
    }
    /// <summary>
    /// 代收類別資料
    /// </summary>
    [Description("代收類別資料")]
    public class CollectionTypeModel : MasterDataModel
    {
        /// <summary>
        /// 代收類別代號
        /// </summary>
        [Description("代收類別代號"), Key, MaxLength(ConstParameter.DataIdLen)]
        public string CollectionTypeId { get; set; }
        /// <summary>
        /// 代收類別名稱
        /// </summary>
        [Description("代收類別名稱"), Required, InputField, MaxLength(ConstParameter.NormalLen)]
        public string CollectionTypeName { get; set; }
        /// <summary>
        /// 通路手續費清算方式
        /// </summary>
        [Description("通路手續費清算方式"), InputField]
        public ChargePayType ChargePayType { get; set; }
    }
    /// <summary>
    /// 代收類別費率明細
    /// </summary>
    [Description("代收類別費率明細")]
    public class CollectionTypeDetailModel : DetailRowState
    {
        [ForeignKey("CollectionTypeId")]
        public CollectionTypeModel CollectionType { get; set; }
        /// <summary>
        /// 代收類別代號
        /// </summary>
        [Description("代收類別代號"), Key]
        public string CollectionTypeId { get; set; }
        /// <summary>
        /// 序號
        /// </summary>
        [Description("序號"), Key]
        public int RowId { get; set; }
        public ChannelModel Channel { get; set; }
        /// <summary>
        /// 通路代號
        /// </summary>
        [Description("通路代號"), Required, InputField]
        public string ChannelId { get; set; }
        /// <summary>
        /// 收款區間(起)
        /// </summary>
        [Description("收款區間(起)"), InputField]
        public decimal SRange { get; set; }
        /// <summary>
        /// 收款區間(迄)
        /// </summary>
        [Description("收款區間(迄)"), InputField]
        public decimal ERange { get; set; }
        /// <summary>
        /// 通路手續費
        /// </summary>
        [Description("通路手續費"), InputField]
        public decimal ChannelFee { get; set; }
        /// <summary>
        /// 通路回饋手續費
        /// </summary>
        [Description("通路回饋手續費"), InputField]
        public decimal ChannelFeedBackFee { get; set; }
        /// <summary>
        /// 通路回扣手續費
        /// </summary>
        [Description("通路回扣手續費"), InputField]
        public decimal ChannelRebateFee { get; set; }
        /// <summary>
        /// 通路總手續費
        /// </summary>
        [Description("通路總手續費")]
        public decimal ChannelTotalFee { get; set; }
    }
    /// <summary>
    /// 通路核銷週期明細
    /// </summary>
    [Description("通路核銷週期明細")]
    public class CollectionTypeVerifyPeriodModel : DetailRowState
    {
        /// <summary>
        /// 代收類別
        /// </summary>
        [ForeignKey("CollectionTypeId")]
        public CollectionTypeModel CollectionType { get; set; }
        /// <summary>
        /// 代收類別代號
        /// </summary>
        [Description("代收類別代號"), Key]
        public string CollectionTypeId { get; set; }
        /// <summary>
        /// 代收通路
        /// </summary>
        [ForeignKey("ChannelId")]
        public ChannelModel Channel { get; set; }
        /// <summary>
        /// 通路代號
        /// </summary>
        [Description("通路代號"), Key]
        public string ChannelId { get; set; }
        /// <summary>
        /// 通路帳務核銷週期
        /// </summary>
        [Description("通路帳務核銷週期"), InputField]
        public PayPeriodType PayPeriodType { get; set; }
    }
}
