using SKGPortalCore.Lib;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model.MasterData.OperateSystem
{
    /// <summary>
    /// 操作日誌
    /// </summary>
    [Description(SystemCP.DESC_OperateLog)]
    public class OperateLog
    {
        /// <summary>
        /// 序號
        /// </summary>
        [Description(SystemCP.DESC_Id), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public long Id { get; set; }
        /// <summary>
        /// 使用者ID
        /// </summary>
        [Description(SystemCP.DESC_UserId), Required] public string UserId { get; set; }
        /// <summary>
        /// 登入IP位置
        /// </summary>
        [Description(SystemCP.DESC_IP), Required] public string IP { get; set; }
        /// <summary>
        /// 瀏覽器資訊
        /// </summary>
        [Description(SystemCP.DESC_Browser), Required] public string Browser { get; set; }
        /// <summary>
        /// ProgId
        /// </summary>
        [Description(SystemCP.DESC_ProgId), Required] public string ProgId { get; set; }
        /// <summary>
        /// 資料主鍵
        /// </summary>
        [Description(SystemCP.DESC_PK), Required] public string PK { get; set; }
        /// <summary>
        /// 操作時間
        /// </summary>
        [Description(SystemCP.DESC_OperateTime), Required] public DateTime OperateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 動作
        /// </summary>
        [Description(SystemCP.DESC_Action), Required] public string Action { get; set; }
        /// <summary>
        /// 備註
        /// (SysOperator處理時，該欄位不允許為空)
        /// </summary>
        [Description(SystemCP.DESC_Memo), Required] public string Memo { get; set; }
    }
}
