using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SKGPortalCore.Model.MasterData.OperateSystem
{

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
}
