using SKGPortalCore.Model.System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SKGPortalCore.Model.MasterData.OperateSystem
{

    /// <summary>
    /// 後臺使用者
    /// </summary>
    [Description("後臺使用者")]
    public class DataChangeLogSet
    {
        /// <summary>
        /// 後臺使用者資料
        /// </summary>
        [Description("變更日誌")]
        public DataChangeLog DataChangeLog { get; set; }
        /// <summary>
        /// 後臺使用者角色權限清單
        /// </summary>
        [Description("變更日誌明細")]
        public List<DataChangeLogDetail> DataChangeLogDetail { get; set; }
    }

    /// <summary>
    /// 變更日誌
    /// </summary>
    [Description("變更日誌")]
    public class DataChangeLog
    {
        /// <summary>
        /// ID
        /// </summary>
        [Description("ID"), Key]
        public long DataChangeId { get; set; }
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
        /// 變更時間
        /// </summary>
        [Description("變更時間")]
        public DateTime DataChangeTime { get; set; }
    }
    /// <summary>
    /// 變更日誌明細
    /// </summary>
    [Description("變更日誌明細")]
    public class DataChangeLogDetail
    {
        [ForeignKey("Id")]
        public DataChangeLog DataChangeLog { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        [Description("ID"), Key]
        public long DataChangeId { get; set; }
        [Description("RowID"), Key]
        public long RowId { get; set; }
        /// <summary>
        /// 表單索引
        /// </summary>
        [Description("表單索引")]
        public int TableIndex { get; set; }
        /// <summary>
        /// 變更前後資料
        /// </summary>
        [Description("變更前後資料")]
        public byte[] ChangeData { get; set; }
        /// <summary>
        /// 行狀態
        /// </summary>
        [Description("行狀態")]
        public RowState RowStatus { get; set; }
    }

}
