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
        public CollectionTypeModel CollectionType { get; set; }
        public List<CollectionTypeDetailModel> CollectionTypeDetail { get; set; }
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
        [Description("代收類別代號"), Key]
        public string CollectionTypeId { get; set; }
        /// <summary>
        /// 代收類別名稱
        /// </summary>
        [Description("代收類別名稱")]
        public string CollectionTypeName { get; set; }
        /// <summary>
        /// 通路手續費清算方式
        /// </summary>
        [Description("通路手續費清算方式")]
        public ChargePayType ChargePayType { get; set; }
    }
    /// <summary>
    /// 代收類別費率明細
    /// </summary>
    [Description("代收類別費率明細")]
    public class CollectionTypeDetailModel:DetailRowState
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
        [Description("通路代號")]
        public string ChannelId { get; set; }
        /// <summary>
        /// 收款區間(起)
        /// </summary>
        [Description("收款區間(起)")]
        public decimal SRange { get; set; }
        /// <summary>
        /// 收款區間(迄)
        /// </summary>
        [Description("收款區間(迄)")]
        public decimal ERange { get; set; }
        /// <summary>
        /// 通路手續費
        /// </summary>
        [Description("通路手續費")]
        public decimal Fee { get; set; }
    }
}
