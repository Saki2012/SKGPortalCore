using SKGPortalCore.Lib;
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
    /// 變更日誌
    /// </summary>
    [Description(SystemCP.DESC_DataChangeLog)]
    public class DataChangeLogSet
    {
        /// <summary>
        /// 變更日誌
        /// </summary>
        [Description(SystemCP.DESC_DataChangeLog)] public DataChangeLog DataChangeLog { get; set; }
        /// <summary>
        /// 變更日誌明細
        /// </summary>
        [Description(SystemCP.DESC_DataChangeLogDt)] public List<DataChangeLogDetail> DataChangeLogDetail { get; set; }
    }

    /// <summary>
    /// 變更日誌
    /// </summary>
    [Description(SystemCP.DESC_DataChangeLog)]
    public class DataChangeLog
    {
        /// <summary>
        /// 序號
        /// </summary>
        [Description(SystemCP.DESC_Id), Key] public long DataChangeId { get; set; }
        /// <summary>
        /// 使用者ID
        /// </summary>
        [Description(SystemCP.DESC_UserId)] public string UserId { get; set; }
        /// <summary>
        /// ProgId
        /// </summary>
        [Description(SystemCP.DESC_ProgId)] public string ProgId { get; set; }
        /// <summary>
        /// 表單內部標識號
        /// </summary>
        [Description(SystemCP.DESC_InternalId)] public string InternalId { get; set; }
        /// <summary>
        /// 變更時間
        /// </summary>
        [Description(SystemCP.DESC_DataChangeTime)] public DateTime DataChangeTime { get; set; }
    }
    /// <summary>
    /// 變更日誌明細
    /// </summary>
    [Description(SystemCP.DESC_DataChangeLogDt)]
    public class DataChangeLogDetail
    {
        /// <summary>
        /// 變更日誌
        /// </summary>
        [ForeignKey(nameof(DataChangeId))] public DataChangeLog DataChangeLog { get; set; }
        /// <summary>
        /// 序號
        /// </summary>
        [Description(SystemCP.DESC_Id), Key] public long DataChangeId { get; set; }
        /// <summary>
        /// 行序號
        /// </summary>
        [Description(SystemCP.DESC_RowId), Key] public long RowId { get; set; }
        /// <summary>
        /// 表單索引
        /// </summary>
        [Description(SystemCP.DESC_TableIndex)] public int TableIndex { get; set; }
        /// <summary>
        /// 變更前後資料
        /// </summary>
        [Description(SystemCP.DESC_ChangeData)] public byte[] ChangeData { get; set; }
        /// <summary>
        /// 行狀態
        /// </summary>
        [Description(SystemCP.DESC_RowState)] public RowState RowState { get; set; }
    }

}
