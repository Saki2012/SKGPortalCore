using SKGPortalCore.Lib;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SKGPortalCore.Model.MasterData.OperateSystem
{

    /// <summary>
    /// 編碼規則表
    /// </summary>
    [Description(SystemCP.DESC_DataFlowNo)]
    public class DataFlowNo
    {
        /// <summary>
        /// ProgId
        /// </summary>
        [Description(SystemCP.DESC_ProgId), Key, MaxLength(30)] public string ProgId { get; set; }
        /// <summary>
        /// 流水號日期
        /// </summary>
        [Description(SystemCP.DESC_FlowDate)] public DateTime FlowDate { get; set; }
        /// <summary>
        /// 流水號
        /// </summary>
        [Description(SystemCP.DESC_FlowNo)] public int FlowNo { get; set; }
    }
}
