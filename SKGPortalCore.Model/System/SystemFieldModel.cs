﻿using System;
using System.ComponentModel;
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
        //[Description("單據日期")]
        //public DateTime BillDate { get; set; }
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
        /// 創建人員
        /// </summary>
        [Description("創建人員"), Column(Order = int.MaxValue - 11)]
        public string CreateStaff { get; set; }
        /// <summary>
        /// 創建時間
        /// </summary>
        [Description("創建時間"), Column(Order = int.MaxValue - 10)]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改人員
        /// </summary>
        [Description("修改人員"), Column(Order = int.MaxValue - 9)]
        public string ModifyStaff { get; set; }
        /// <summary>
        /// 修改時間
        /// </summary>
        [Description("修改時間"), Column(Order = int.MaxValue - 8)]
        public DateTime ModifyTime { get; set; }
        /// <summary>
        /// 審核人員
        /// </summary>
        [Description("審核人員"), Column(Order = int.MaxValue - 7)]
        public string ApproveStaff { get; set; }
        /// <summary>
        /// 審核時間
        /// </summary>
        [Description("審核時間"), Column(Order = int.MaxValue - 6)]
        public DateTime ApproveTime { get; set; }
        /// <summary>
        /// 結案人員
        /// </summary>
        [Description("結案人員"), Column(Order = int.MaxValue - 5)]
        public string EndCaseStaff { get; set; }
        /// <summary>
        /// 結案時間
        /// </summary>
        [Description("結案時間"), Column(Order = int.MaxValue - 4)]
        public DateTime EndCaseTime { get; set; }
        /// <summary>
        /// 作廢人員
        /// </summary>
        [Description("作廢人員"), Column(Order = int.MaxValue - 3)]
        public string InvalidStaff { get; set; }
        /// <summary>
        /// 作廢時間
        /// </summary>
        [Description("作廢時間"), Column(Order = int.MaxValue - 2)]
        public DateTime InvalidTime { get; set; }
        /// <summary>
        /// 單據狀態
        /// </summary>
        [Description("表單狀態"), Column(Order = int.MaxValue - 1)]
        public FormStatus FormStatus { get; set; }
        /// <summary>
        /// 內部唯一標識號
        /// (主鍵可能變化，此欄位不允許被修改)
        /// </summary>
        [Description("內部唯一標識號"), Column(Order = int.MaxValue)]
        public string InternalId { get; set; }
    }
    /// <summary>
    /// 明細行狀態
    /// </summary>
    public class DetailRowState
    {
        [Description("行狀態"), NotMapped]
        public RowState RowState { get; set; }
    }
}