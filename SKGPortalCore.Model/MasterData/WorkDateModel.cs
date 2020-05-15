using SKGPortalCore.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SKGPortalCore.Model.MasterData
{
    public class HolidayOpenData
    {
        public bool success { get; set; }
        public Result result { get; set; }
    }
    public class Result
    {
        public string resource_id { get; set; }
        public int limit { get; set; }
        public int total { get; set; }
        public Field[] fields { get; set; }
        public WorkDateModel[] records { get; set; }
    }
    public class Field
    {
        public string type { get; set; }
        public string id { get; set; }
    }
    /// <summary>
    /// 營業日
    /// 源於：http://data.ntpc.gov.tw/api/v1/rest/datastore/382000000A-000077-002
    /// </summary>
    [Description(SystemCP.DESC_WorkDate)]
    public class WorkDateModel
    {
        /// <summary>
        /// 日期
        /// </summary>
        [Description(SystemCP.DESC_Date), Key] public DateTime Date { get; set; }
        /// <summary>
        /// 是否營業日
        /// </summary>
        [Description(SystemCP.DESC_IsWorkDate)] public bool IsWorkDate { get; set; }
        /// <summary>
        /// 假別名
        /// </summary>
        [Description(SystemCP.DESC_HolidayName)] public string HolidayName { get; set; }
        /// <summary>
        /// 假類別
        /// </summary>
        [Description(SystemCP.DESC_HolidayCategory)] public string HolidayCategory { get; set; }
        /// <summary>
        /// 說明
        /// </summary>
        [Description(SystemCP.DESC_Description)] public string Description { get; set; }
    }
}
