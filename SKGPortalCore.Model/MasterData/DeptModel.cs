using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Model.System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Description("部門代號"), Key, MaxLength(ConstParameter.DataIdLen)]
        public string DeptId { get; set; }
        /// <summary>
        /// 部門名稱
        /// </summary>
        [Description("部門名稱"),Required, InputField]
        public string DeptName { get; set; }
        /// <summary>
        /// 是否分行
        /// </summary>
        [Description("是否分行"), InputField]
        public bool IsBranch { get; set; }
    }
}
