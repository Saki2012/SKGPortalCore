using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SKGPortalCore.Model.MasterData
{

    public class ChannelVerifyPeriodSet
    {
        public ChannelVerifyPeriodModel ChannelVerifyPeriod { get; set; }
    }


    /// <summary>
    /// 通路核銷週期
    /// </summary>
    public class ChannelVerifyPeriodModel : MasterDataModel
    {
        public ChannelModel Channel { get; set; }
        /// <summary>
        /// 通路代號
        /// </summary>
        [Description("通路代號"),Key]
        public string ChannelId { get; set; }
        public CollectionTypeModel CollectionType { get; set; }
        /// <summary>
        /// 代收類別代號
        /// </summary>
        [Description("代收類別代號"), Key]
        public string CollectionTypeId { get; set; }
        /// <summary>
        /// 通路帳務核銷週期
        /// </summary>
        [Description("通路帳務核銷週期")]
        public PayPeriodType PayPeriodType { get; set; }
        /// <summary>
        /// 日結日
        /// </summary>
        [Description("日結日")]
        public byte NDay { get; set; }
        /// <summary>
        /// 週結日
        /// </summary>
        public byte WeeklyWeekDay { get; set; }
        /// <summary>
        /// 上旬結日
        /// </summary>
        public byte TenDayDay1 { get; set; }
        /// <summary>
        /// 中旬結日
        /// </summary>
        public byte TenDayDay2 { get; set; }
        /// <summary>
        /// 下旬結日
        /// </summary>
        public byte TenDayDay3 { get; set; }
        /// <summary>
        /// 月結日
        /// </summary>
        public byte MonthlyDay { get; set; }
    }
}
