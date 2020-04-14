using SKGPortalCore.Lib;
using SKGPortalCore.Model.SourceData;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model.System
{
    /// <summary>
    /// (單據)系統欄位
    /// </summary>
    public class BillDataModel : BasicDataModel
    {
        /// <summary>
        /// 單據日期
        /// </summary>
        //[Description("單據日期")]        //public DateTime BillDate { get; set; }
    }
    /// <summary>
    /// (主資料)系統欄位
    /// </summary>
    public class MasterDataModel : BasicDataModel
    {
        /// <summary>
        /// 資料狀態
        /// </summary>
        //[Description("資料狀態")]        //public DataStatus DataStatus { get; set; }
    }
    /// <summary>
    /// 基本系統欄位
    /// </summary>
    public class BasicDataModel
    {
        /// <summary>
        /// 創建人員
        /// </summary>
        [Description(SystemCP.DESC_CreateStaff), Required, MaxLength(SystemCP.DataIdLen)] public string CreateStaff { get; set; } = string.Empty;
        /// <summary>
        /// 創建時間
        /// </summary>
        [Description(SystemCP.DESC_CreateTime)] public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改人員
        /// </summary>
        [Description(SystemCP.DESC_ModifyStaff), Required, MaxLength(SystemCP.DataIdLen)] public string ModifyStaff { get; set; } = string.Empty;
        /// <summary>
        /// 修改時間
        /// </summary>
        [Description(SystemCP.DESC_ModifyTime)] public DateTime ModifyTime { get; set; }
        /// <summary>
        /// 審核人員
        /// </summary>
        [Description(SystemCP.DESC_ApproveStaff), Required, MaxLength(SystemCP.DataIdLen)] public string ApproveStaff { get; set; } = string.Empty;
        /// <summary>
        /// 審核時間
        /// </summary>
        [Description(SystemCP.DESC_ApproveTime)] public DateTime ApproveTime { get; set; }
        /// <summary>
        /// 結案人員
        /// </summary>
        [Description(SystemCP.DESC_EndCaseStaff), Required, MaxLength(SystemCP.DataIdLen)] public string EndCaseStaff { get; set; } = string.Empty;
        /// <summary>
        /// 結案時間
        /// </summary>
        [Description(SystemCP.DESC_EndCaseTime)] public DateTime EndCaseTime { get; set; }
        /// <summary>
        /// 作廢人員
        /// </summary>
        [Description(SystemCP.DESC_InvalidStaff), Required, MaxLength(SystemCP.DataIdLen)] public string InvalidStaff { get; set; } = string.Empty;
        /// <summary>
        /// 作廢時間
        /// </summary>
        [Description(SystemCP.DESC_InvalidTime)] public DateTime InvalidTime { get; set; }
        /// <summary>
        /// 單據狀態
        /// </summary>
        [Description(SystemCP.DESC_FormStatus)] public FormStatus FormStatus { get; set; }
        /// <summary>
        /// 內部唯一標識號
        /// (主鍵可能變化，此欄位不允許被修改)
        /// </summary>
        [Description(SystemCP.DESC_InternalId)] public string InternalId { get; set; }
    }
    /// <summary>
    /// 明細行狀態
    /// </summary>
    public class DetailRowState
    {
        [Description(SystemCP.DESC_RowState), NotMapped, InputField] public RowState RowState { get; set; }
    }

    /// <summary>
    /// 權限Token
    /// </summary>
    public class PermissionTokenModel
    {
        public string FuncName { get; set; }
        public string Token { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class FileInfoModel
    {
        /// <summary>
        /// 檔案名稱
        /// </summary>
        [Description("檔案名稱"), InputField] public string FileName { get; set; }
        /// <summary>
        /// 網際網路媒體型式
        /// </summary>
        [Description("網際網路媒體型式"), InputField] public string MimeType { get; set; }
        /// <summary>
        /// 編碼
        /// </summary>
        [Description("編碼"), InputField] public string Encoding { get; set; }
        /// <summary>
        /// 路徑
        /// </summary>
        [Description("路徑"), InputField] public string Path { get; set; }
        /// <summary>
        /// 內容
        /// </summary>
        [Description("內容")] public int[] Content { get; set; }
    }
}
