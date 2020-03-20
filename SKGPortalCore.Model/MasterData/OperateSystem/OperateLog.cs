using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model.MasterData.OperateSystem
{
    /// <summary>
    /// 操作日誌
    /// </summary>
    [Description("操作日誌")]
    public class OperateLog
    {
        /// <summary>
        /// ID
        /// </summary>
        [Description("ID"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        /// <summary>
        /// 使用者ID
        /// </summary>
        [Description("使用者ID"), Required]
        public string UserId { get; set; }
        /// <summary>
        /// 登入IP位置
        /// </summary>
        [Description("登入IP位置"), Required]
        public string IP { get; set; }
        /// <summary>
        /// 瀏覽器資訊
        /// </summary>
        [Description("瀏覽器資訊"), Required]
        public string Browser { get; set; }
        /// <summary>
        /// ProgId
        /// </summary>
        [Description("ProgId"), Required]
        public string ProgId { get; set; }
        /// <summary>
        /// 資料主鍵
        /// </summary>
        [Description("資料主鍵"), Required]
        public string PK { get; set; }
        /// <summary>
        /// 操作時間
        /// </summary>
        [Description("操作時間"), Required]
        public DateTime OperateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 動作
        /// </summary>
        [Description("動作"), Required]
        public string Action { get; set; }
        /// <summary>
        /// 備註
        /// (SysOperator處理時，該欄位不允許為空)
        /// </summary>
        [Description("備註"), Required]
        public string Memo { get; set; }
    }
}
