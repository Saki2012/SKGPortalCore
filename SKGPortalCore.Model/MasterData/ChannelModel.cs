using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Model.System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model.MasterData
{
    /// <summary>
    /// 代收通路
    /// </summary>
    [Description("代收通路")]
    public class ChannelSet
    {
        /// <summary>
        /// 代收通路資料
        /// </summary>
        [Description("代收通路資料")] public ChannelModel Channel { get; set; }
        /// <summary>
        /// 交易代號與平台通路代號關聯表
        /// </summary>
        [Description("交易代號與平台通路代號關聯表")] public List<ChannelMapModel> ChannelMap { get; set; }
    }
    /// <summary>
    /// 代收通路資料
    /// </summary>
    [Description("代收通路資料")]
    public class ChannelModel : MasterDataModel
    {
        /// <summary>
        /// 代收通路代號
        /// </summary>
        [Description("代收通路代號"), Key, MaxLength(CP.DataIdLen)] public string ChannelId { get; set; }
        /// <summary>
        /// 代收通路名稱
        /// </summary>
        [Description("代收通路名稱"), InputField, MaxLength(CP.NormalLen)] public string ChannelName { get; set; }
        /// <summary>
        /// 通路類別
        /// </summary>
        [Description("通路類別"), InputField] public ChannelGroupType ChannelGroupType { get; set; }
    }
    /// <summary>
    /// 交易代號與平台通路代號關聯表
    /// </summary>
    [Description("交易代號與平台通路代號關聯表")]
    public class ChannelMapModel : DetailRowState
    {
        [ForeignKey("ChannelId")] public ChannelModel Channel { get; set; }
        /// <summary>
        /// 代收通路代號
        /// </summary>
        [Description("代收通路代號"), Key] public string ChannelId { get; set; }
        /// <summary>
        /// 交易代號
        /// </summary>
        [Description("交易代號"), Key, MaxLength(CP.DataIdLen)] public string TransCode { get; set; }
    }
}
