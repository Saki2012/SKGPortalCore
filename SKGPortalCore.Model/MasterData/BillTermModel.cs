﻿using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Model.System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model.MasterData
{
    /// <summary>
    /// 期別
    /// </summary>
    [Description("期別")]
    public class BillTermSet
    {
        /// <summary>
        /// 期別資料
        /// </summary>
        [Description("期別資料")]
        public BillTermModel BillTerm { get; set; }
        /// <summary>
        /// 期別費用明細
        /// </summary>
        [Description("期別費用明細")]
        public List<BillTermDetailModel> BillTermDetail { get; set; }
    }
    /// <summary>
    /// 期別
    /// </summary>
    [Description("期別資料")]
    public class BillTermModel : MasterDataModel
    {
        [ForeignKey("CustomerCode")]
        public BizCustomerModel BizCustomer { get; set; }
        /// <summary>
        /// 企業代號
        /// </summary>
        [Description("企業代號"), Key]
        public string CustomerCode { get; set; }
        /// <summary>
        /// 期別代號
        /// </summary>
        [Description("期別代號"), Key, MaxLength(ConstParameter.DataIdLen)]
        public string BillTermId { get; set; }
        /// <summary>
        /// 期別名稱
        /// </summary>
        [Description("期別名稱")]
        public string BillTermName { get; set; }
        /// <summary>
        /// 期別編號
        /// </summary>
        [Description("期別編號")]
        public string BillTermNo { get; set; }
    }
    /// <summary>
    /// 期別費用明細
    /// </summary>
    [Description("期別費用明細")]
    public class BillTermDetailModel : DetailRowState
    {
        [ForeignKey("CustomerCode,BillTermId")]
        public BillTermModel BillTerm { get; set; }
        /// <summary>
        /// 企業代號
        /// </summary>
        [Description("企業代號"), Key]
        public string CustomerCode { get; set; }
        /// <summary>
        /// 期別代號
        /// </summary>
        [Description("期別代號"), Key]
        public string BillTermId { get; set; }
        /// <summary>
        /// 序號
        /// </summary>
        [Description("序號"), Key]
        public int RowId { get; set; }
        /// <summary>
        /// 費用名稱
        /// </summary>
        [Description("費用名稱")]
        public string FeeName { get; set; }
        /// <summary>
        /// 減項
        /// </summary>
        [Description("減項")]
        public bool IsDeduction { get; set; }
    }
}
