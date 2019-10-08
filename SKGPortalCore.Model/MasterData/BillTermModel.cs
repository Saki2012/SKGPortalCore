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
        [Description("期別代號"), Key, MaxLength(20)]
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
        /// <summary>
        /// 期別流水碼
        /// </summary>
        [Description("期別流水碼")]
        public int Seq { get; set; }
        /// <summary>
        /// 帳單基本資料1
        /// </summary>
        [Description("帳單基本資料1")]
        public BillInfo BillInfo1 { get; set; }
        /// <summary>
        /// 帳單基本資料2
        /// </summary>
        [Description("帳單基本資料2")]
        public BillInfo BillInfo2 { get; set; }
        /// <summary>
        /// 帳單基本資料3
        /// </summary>
        [Description("帳單基本資料3")]
        public BillInfo BillInfo3 { get; set; }
        /// <summary>
        /// 帳單基本資料4
        /// </summary>
        [Description("帳單基本資料4")]
        public BillInfo BillInfo4 { get; set; }
        /// <summary>
        /// 帳單基本資料5
        /// </summary>
        [Description("帳單基本資料5")]
        public BillInfo BillInfo5 { get; set; }
        /// <summary>
        /// 帳單基本資料6
        /// </summary>
        [Description("帳單基本資料6")]
        public BillInfo BillInfo6 { get; set; }
        /// <summary>
        /// 公告事項
        /// </summary>
        [Description("公告事項")]
        public string Announcement { get; set; }
        /// <summary>
        /// 期間(從)
        /// </summary>
        [Description("期間(從)")]
        public DateTime DateFrom { get; set; }
        /// <summary>
        /// 期間(至)
        /// </summary>
        [Description("期間(至)")]
        public DateTime DateTo { get; set; }
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
        [Description("序號"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
