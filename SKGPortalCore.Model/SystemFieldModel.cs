using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model
{
    /// <summary>
    /// (單據)系統欄位
    /// </summary>
    public class BillDataModel : BasicDataModel
    {
        /// <summary>
        /// 單據日期
        /// </summary>
        [Description("單據日期")]
        public DateTime BillDate { get; set; }
    }
    /// <summary>
    /// (主資料)系統欄位
    /// </summary>
    public class MasterDataModel : BasicDataModel
    {
        /// <summary>
        /// 資料狀態
        /// </summary>
        //[Description("資料狀態")]
        //public DataStatus DataStatus { get; set; }
    }
    /// <summary>
    /// 基本系統欄位
    /// </summary>
    public class BasicDataModel
    {
        /// <summary>
        /// 創建人員
        /// </summary>
        [Description("創建人員")]
        public string CreateStaff { get; set; }
        /// <summary>
        /// 創建時間
        /// </summary>
        [Description("創建時間")/*, Column(TypeName = "datetime")*/]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改人員
        /// </summary>
        [Description("修改人員")]
        public string ModifyStaff { get; set; }
        /// <summary>
        /// 修改時間
        /// </summary>
        [Description("修改時間")]
        public DateTime ModifyTime { get; set; }
        /// <summary>
        /// 審核人員
        /// </summary>
        [Description("審核人員")]
        public string ApproveStaff { get; set; }
        /// <summary>
        /// 審核時間
        /// </summary>
        [Description("審核時間")]
        public DateTime ApproveTime { get; set; }
        /// <summary>
        /// 結案人員
        /// </summary>
        [Description("結案人員")]
        public string EndCaseStaff { get; set; }
        /// <summary>
        /// 結案時間
        /// </summary>
        [Description("結案時間")]
        public DateTime EndCaseTime { get; set; }
        /// <summary>
        /// 作廢人員
        /// </summary>
        [Description("作廢人員")]
        public string InvalidStaff { get; set; }
        /// <summary>
        /// 作廢時間
        /// </summary>
        [Description("作廢時間")]
        public DateTime InvalidTime { get; set; }
        /// <summary>
        /// 單據狀態
        /// </summary>
        [Description("表單狀態")]
        public FormStatus FormStatus { get; set; }
    }
    /// <summary>
    /// 明細行狀態
    /// </summary>
    public class DetailRowState
    {
        [Description("行狀態"), NotMapped]
        public RowState RowState { get; set; }
    }

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
        [Description("使用者ID")]
        public string UserId { get; set; }
        /// <summary>
        /// 登入IP位置
        /// </summary>
        [Description("登入IP位置")]
        public string IP { get; set; }
        /// <summary>
        /// 瀏覽器資訊
        /// </summary>
        [Description("瀏覽器資訊")]
        public string Browser { get; set; }
        /// <summary>
        /// ProgId
        /// </summary>
        [Description("ProgId")]
        public string ProgId { get; set; }
        /// <summary>
        /// 資料主鍵
        /// </summary>
        [Description("資料主鍵")]
        public string PK { get; set; }
        /// <summary>
        /// 操作時間
        /// </summary>
        [Description("操作時間")]
        public DateTime OperateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 動作
        /// </summary>
        [Description("動作")]
        public string Action { get; set; }

    }
    /// <summary>
    /// 編碼規則表
    /// </summary>
    [Description("編碼規則表")]
    public class DataFlowNo
    {
        /// <summary>
        /// ProgId
        /// </summary>
        [Description("ProgId"), Key, MaxLength(30)]
        public string ProgId { get; set; }
        /// <summary>
        /// 流水號日期
        /// </summary>
        [Description("流水號日期")]
        public DateTime FlowDate { get; set; }
        /// <summary>
        /// 流水號
        /// </summary>
        [Description("流水號")]
        public int FlowNo { get; set; }
    }

    /// <summary>
    /// 錯誤日誌
    /// </summary>
    [Description("錯誤日誌")]
    public class ErrorLog
    {
        [Key]
        public long Id { get; set; }
        public string UserId { get; set; }
        public string ProgId { get; set; }
        public string MessageCode { get; set; }
        public string Message { get; set; }
        public DateTime OperateTime { get; set; }
    }
}
