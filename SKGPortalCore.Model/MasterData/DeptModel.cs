using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SKGPortalCore.Model.MasterData
{
    /// <summary>
    /// 部門
    /// </summary>
    [Description("部門")]
    public class DeptSet
    {
        /// <summary>
        /// 部門資料
        /// </summary>
        [Description("部門資料")]
        public DeptModel Dept { get; set; }
    }
    /// <summary>
    /// 部門資料
    /// </summary>
    [Description("部門資料")]
    public class DeptModel : MasterDataModel
    {
        /// <summary>
        /// 部門代號
        /// </summary>
        [Description("部門代號"),Key]
        public string DeptId { get; set; }
        /// <summary>
        /// 部門名稱
        /// </summary>
        [Description("部門名稱")]
        public string DeptName { get; set; }
        /// <summary>
        /// 是否分行
        /// </summary>
        [Description("是否分行")]
        public bool IsBranch { get; set; }
        /// <summary>
        /// 啟用狀態
        /// </summary>
        [Description("啟用狀態")]
        public bool IsEnable { get; set; }
    }
}
