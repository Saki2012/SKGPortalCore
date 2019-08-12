using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SKGPortalCore.Model.MasterData
{
    /// <summary>
    /// 通路核銷週期
    /// </summary>
    class ChannelVerifyPeriodModel : MasterDataModel
    {
        public ChannelModel Channel { get; set; }
        /// <summary>
        /// 通路代號
        /// </summary>
        [Description("通路代號")]
        public string ChannelId { get; set; }
        public CollectionTypeModel CollectionType { get; set; }
        /// <summary>
        /// 代收類別代號
        /// </summary>
        [Description("代收類別代號")]
        public string CollectionTypeId { get; set; }
        /// <summary>
        /// 通路帳務核銷週期
        /// </summary>
        [Description("通路帳務核銷週期")]
        public PayPeriodType PayPeriodType { get; set; }
        /// <summary>
        /// T+N日
        /// </summary>
        [Description("T+N日")]
        public int NDay { get; set; }
        public int NDayType { get; set; }
        public int NDayPayPeriod { get; set; }
        public int WeeklyWeekDay { get; set; }
        public int WeeklyWeekDayPayPeriod { get; set; }
        public int TenDayDay1 { get; set; }
        public int TenDayDay1PeriodStart { get; set; }
        public int TenDayDay2 { get; set; }
        public int TenDayDay2PeriodStart { get; set; }
        public int TenDayDay2PeriodEnd { get; set; }
        public int TenDayDay3 { get; set; }
        public int TenDayDay3PeriodStart { get; set; }
        public int TenDayDay3PeriodEnd { get; set; }
        public int MonthlyDay { get; set; }
    }
}
