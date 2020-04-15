using SKGPortalCore.Lib;
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
    [Description(SystemCP.DESC_Dept)]
    public class DeptSet
    {
        /// <summary>
        /// 部門
        /// </summary>
        [Description(SystemCP.DESC_Dept)] public DeptModel Dept { get; set; } = new DeptModel();
    }
    /// <summary>
    /// 部門資料
    /// </summary>
    [Description(SystemCP.DESC_Dept)]
    public class DeptModel : MasterDataModel
    {
        /// <summary>
        /// 部門代號
        /// </summary>
        [Description(SystemCP.DESC_DeptId), Key, MaxLength(SystemCP.DataIdLen)] public string DeptId { get; set; }
        /// <summary>
        /// 部門名稱
        /// </summary>
        [Description(SystemCP.DESC_DeptName), Required, InputField, MaxLength(SystemCP.NormalLen)] public string DeptName { get; set; }
        /// <summary>
        /// 是否分行
        /// </summary>
        [Description(SystemCP.DESC_IsBranch), InputField] public bool IsBranch { get; set; }
    }
}
