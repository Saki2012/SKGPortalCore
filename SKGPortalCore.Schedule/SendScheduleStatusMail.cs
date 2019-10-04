using System;
using System.Collections.Generic;
using System.Text;

namespace SKGPortalCore.Schedule
{
    /// <summary>
    /// 發送排程狀況Mail至IT人員
    /// </summary>
    public class SendScheduleStatusMail
    {
        #region Property
        /// <summary>
        /// 主旨
        /// </summary>
        public string Title { get; }
        /// <summary>
        /// 內容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 收件者
        /// </summary>
        public string Recipient { get; set; }
        /// <summary>
        /// 副本
        /// </summary>
        public string CC { get; set; }
        #endregion
    }
}
