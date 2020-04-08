using SKGPortalCore.Model.SourceData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SKGPortalCore.Model.SystemTable
{
    /// <summary>
    /// 有效虛擬帳號表
    /// </summary>
    [Description("有效虛擬帳號表")]
    public class VirtualAccountCodeModel
    {
        /// <summary>
        /// 虛擬帳號
        /// </summary>
        [Description("虛擬帳號"), Key, MaxLength(CP.NormalLen)] public string VirtualAccountCode { get; set; }
        /// <summary>
        /// 來源ProgId
        /// </summary>
        [Description("來源ProgId"), Required, MaxLength(CP.NormalLen)] public string SourceProgId { get; set; }
        /// <summary>
        /// 來源單號
        /// </summary>
        [Description("來源單號"), Required, MaxLength(CP.BillNoLen)] public string SourceBillNo { get; set; }
    }
}
