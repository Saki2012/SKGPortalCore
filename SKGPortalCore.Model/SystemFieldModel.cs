using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model
{
    /// <summary>
    /// (單據)系統欄位
    /// </summary>
    public class BillDataModel : BasicDataModel
    {
        /// <summary>
        /// 單據狀態
        /// </summary>
        [Description("單據狀態")]
        public BillStatus BillStatus { get; set; }
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
        /// 創建時間
        /// </summary>
        [Description("創建時間")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改時間
        /// </summary>
        [Description("修改時間")]
        public DateTime ModifyTime { get; set; }
        /// <summary>
        /// 創建人員
        /// </summary>
        [Description("創建人員")]
        public string CreateStaff { get; set; }
        /// <summary>
        /// 修改人員
        /// </summary>
        [Description("修改人員")]
        public string ModifyStaff { get; set; }
    }
    /// <summary>
    /// 明細行狀態
    /// </summary>
    public class DetailRowState
    {
        [Description("行狀態") ,NotMapped]
        public RowState RowState { get; set; }

        //public int RowId { get; set; }//待考慮
    }

}
