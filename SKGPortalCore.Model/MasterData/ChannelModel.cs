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
    /// 代收通路
    /// </summary>
    [Description(SystemCP.DESC_Channel)]
    public class ChannelSet
    {
        /// <summary>
        /// 代收通路
        /// </summary>
        [Description(SystemCP.DESC_Channel)] public ChannelModel Channel { get; set; }
        /// <summary>
        /// 交易代號與平台通路代號關聯表
        /// </summary>
        [Description(SystemCP.DESC_ChannelMap)] public List<ChannelMapModel> ChannelMap { get; set; }
    }
    /// <summary>
    /// 代收通路
    /// </summary>
    [Description(SystemCP.DESC_Channel)]
    public class ChannelModel : MasterDataModel
    {
        /// <summary>
        /// 代收通路代號
        /// </summary>
        [Description(SystemCP.DESC_ChannelId), Key, MaxLength(SystemCP.DataIdLen)] public string ChannelId { get; set; }
        /// <summary>
        /// 代收通路名稱
        /// </summary>
        [Description(SystemCP.DESC_ChannelName), InputField, MaxLength(SystemCP.NormalLen)] public string ChannelName { get; set; }
        /// <summary>
        /// 通路類別
        /// </summary>
        [Description(SystemCP.DESC_ChannelGroupType), InputField] public ChannelGroupType ChannelGroupType { get; set; }
    }
    /// <summary>
    /// 交易代號與平台通路代號關聯表
    /// </summary>
    [Description(SystemCP.DESC_ChannelMap)]
    public class ChannelMapModel : DetailRowState
    {
        /// <summary>
        /// 代收通路
        /// </summary>
        [ForeignKey(nameof(ChannelId))] public ChannelModel Channel { get; set; }
        /// <summary>
        /// 代收通路代號
        /// </summary>
        [Description(SystemCP.DESC_ChannelId), Key] public string ChannelId { get; set; }
        /// <summary>
        /// 交易代號
        /// </summary>
        [Description(SystemCP.DESC_TransCode), Key, MaxLength(SystemCP.DataIdLen)] public string TransCode { get; set; }
    }
}
