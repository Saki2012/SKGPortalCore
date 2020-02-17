using SKGPortalCore.Model.Enum;
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
        [Description("代收通路資料")]
        public ChannelModel Channel { get; set; }
        /// <summary>
        /// 交易代號與平台通路代號關聯表
        /// </summary>
        [Description("交易代號與平台通路代號關聯表")]
        public List<ChannelMapModel> ChannelMap { get; set; }
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
        [Description("代收通路代號"), Key]
        public string ChannelId { get; set; }
        /// <summary>
        /// 代收通路名稱
        /// </summary>
        [Description("代收通路名稱")]
        public string ChannelName { get; set; }
        /// <summary>
        /// 通路類別
        /// </summary>
        [Description("通路類別")]
        public ChannelGroupType ChannelGroupType { get; set; }
    }
    /// <summary>
    /// 交易代號與平台通路代號關聯表
    /// </summary>
    [Description("交易代號與平台通路代號關聯表")]
    public class ChannelMapModel : DetailRowState
    {
        [ForeignKey("ChannelId")]
        public ChannelModel Channel { get; set; }
        /// <summary>
        /// 代收通路代號
        /// </summary>
        [Description("代收通路代號"), Key]
        public string ChannelId { get; set; }
        /// <summary>
        /// 交易代號
        /// </summary>
        [Description("交易代號"), Key]
        public string TransCode { get; set; }
    }

    /// <summary>
    /// 平台通路代號
    /// </summary>
    public static class ChannelValue
    {
        /// <summary>
        /// 銀行臨櫃
        /// </summary>
        public const string Cash = "00";
        /// <summary>
        /// 7-11
        /// </summary>
        public const string Mart711 = "01";
        /// <summary>
        /// 全家
        /// </summary>
        public const string MartFamily = "02";
        /// <summary>
        /// OK
        /// </summary>
        public const string MartOK = "03";
        /// <summary>
        /// 萊爾富
        /// </summary>
        public const string MartLIFE = "04";
        /// <summary>
        /// 郵局臨櫃
        /// </summary>
        public const string Post = "05";
        /// <summary>
        /// 自動化交易(ATM)
        /// </summary>
        public const string ATM = "06";
        /// <summary>
        /// 約定扣款(主機端)
        /// </summary>
        public const string Deduct_Server = "07";
        /// <summary>
        /// 票交所(ACH)
        /// </summary>
        public const string ACHForPay = "09";
        /// <summary>
        /// 中信平台繳學費
        /// </summary>
        public const string CTBC = "10";
        /// <summary>
        /// 農業金庫
        /// </summary>
        public const string Farm = "12";
        /// <summary>
        /// 郵局網路平台
        /// </summary>
        public const string Post_Net = "A1";
        /// <summary>
        /// 匯款
        /// </summary>
        public const string Remit = "A2";
        /// <summary>
        /// 信用卡
        /// </summary>
        public const string Credit = "A3";
        /// <summary>
        /// ACH(代扣)
        /// </summary>
        public const string ACH = "A4";
        /// <summary>
        /// 約定扣款(平台端)
        /// </summary>
        public const string Deduct_Client = "A5";
        /// <summary>
        /// 全國性e繳費
        /// </summary>
        public const string EB = "EB";
    }
}
