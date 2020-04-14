using SKGPortalCore.Lib;
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
    [Description(SystemCP.DESC_VirtualAccountCodeModel)]
    public class VirtualAccountCodeModel
    {
        /// <summary>
        /// 虛擬帳號
        /// </summary>
        [Description(SystemCP.DESC_VirtualAccountCode), Key, MaxLength(SystemCP.NormalLen)] public string VirtualAccountCode { get; set; }
        /// <summary>
        /// 來源ProgId
        /// </summary>
        [Description(SystemCP.DESC_SrcProgId), Required, MaxLength(SystemCP.NormalLen)] public string SrcProgId { get; set; }
        /// <summary>
        /// 來源單號
        /// </summary>
        [Description(SystemCP.DESC_SrcBillNo), Required, MaxLength(SystemCP.BillNoLen)] public string SrcBillNo { get; set; }
    }
}
