using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SKGPortalCore.Model.SourceData
{
    /// <summary>
    /// 金流匯款檔
    /// </summary>
    public class RemitInfoModel
    {
        /// <summary>
        /// 檔案中的第N行
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 1. 匯款日期(8)
        /// </summary>
        [Description("匯款日期"), StringLength(8)]
        public string RemitDate { get; set; }
        /// <summary>
        /// 2. 匯款時間(6)
        /// </summary>
        [Description("交易日期"), StringLength(6)]
        public string RemitTime { get; set; }
        /// <summary>
        /// 3. 超商通路(2)
        /// </summary>
        [Description("超商通路"), StringLength(2)]
        public string Channel { get; set; }
        /// <summary>
        /// 4. 代收項目(3)
        /// </summary>
        [Description("代收項目"), StringLength(3)]
        public string CollectionType { get; set; }
        /// <summary>
        /// 5. 交易金額(11)
        /// </summary>
        [Description("交易金額"), StringLength(11)]
        public string Amount { get; set; }
        /// <summary>
        /// 6. 批號(2)
        /// </summary>
        [Description("批號"), StringLength(2)]
        public string BatchNo { get; set; }
        /// <summary>
        /// 7. 保留(96)
        /// </summary>
        [Description("保留"), StringLength(96)]
        public string Empty { get; set; }
        /// <summary>
        /// 導入批號
        /// </summary>
        [Description("導入批號")]
        public string ImportBatchNo { get; set; }
        /// <summary>
        /// 原Source
        /// </summary>
        [Description("原Source")]
        public string Source { get; set; }
    }
}
