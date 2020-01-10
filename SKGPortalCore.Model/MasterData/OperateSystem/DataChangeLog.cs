using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SKGPortalCore.Model.MasterData.OperateSystem
{
    /// <summary>
    /// 變更日誌
    /// </summary>
    [Description("變更日誌")]
    public class DataChangeLog
    {
        /// <summary>
        /// ID
        /// </summary>
        [Description("ID"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        /// <summary>
        /// 使用者ID
        /// </summary>
        [Description("使用者ID")]
        public string UserId { get; set; }
        /// <summary>
        /// ProgId
        /// </summary>
        [Description("ProgId")]
        public string ProgId { get; set; }
        /// <summary>
        /// 表單內部標識號
        /// </summary>
        [Description("表單內部標識號")]
        public string InternalId { get; set; }
        /// <summary>
        /// 變更前後資料
        /// </summary>
        [Description("變更前後資料")]
        public byte[] ChangeData { get; set; }
    }
}
