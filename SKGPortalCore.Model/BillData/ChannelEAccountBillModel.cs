using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SKGPortalCore.Model.MasterData;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model.BillData
{
    public class ChannelEAccountBillSet
    {
        public ChannelEAccountBillModel ChannelEAccountBill { get; set; }
        public List<ChannelEAccountBillDetailModel> ChannelEAccountBillDetail { get; set; }
    }

    /// <summary>
    /// 通路帳簿
    /// </summary>
    [Description("通路帳簿")]
    public class ChannelEAccountBillModel : BillDataModel
    {
        /// <summary>
        /// 單據編號
        /// </summary>
        [Description("單據編號"), Key]
        public string BillNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("ChannelId")]
        public ChannelModel Channel { get; set; }
        /// <summary>
        /// 通路代號
        /// </summary>
        [Description("通路代號"), Index]
        public string ChannelId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("CollectionTypeId")]
        public CollectionTypeModel CollectionType { get; set; }
        /// <summary>
        /// 代收類別代號
        /// </summary>
        [Description("代收類別代號"), Index]
        public string CollectionTypeId { get; set; }
        /// <summary>
        /// 預計匯款日期
        /// </summary>
        [Description("預計匯款日期"), Index]
        public DateTime ExpectRemitDate { get; set; }
        /// <summary>
        /// 遞延天數
        /// </summary>
        [Description("遞延天數")]
        public int PostponeDays { get; set; }
        /// <summary>
        /// 資訊流金額
        /// </summary>
        [Description("資訊流金額")]
        public decimal Amount { get; set; }
        /// <summary>
        /// 通路手續費
        /// </summary>
        [Description("通路手續費")]
        public decimal Fee { get; set; }
        /// <summary>
        /// 預計匯款金額
        /// </summary>
        [Description("預計匯款金額")]
        public decimal ExpectRemitAmount { get; set; }
        /// <summary>
        /// 筆數
        /// </summary>
        [Description("筆數")]
        public int PayCount { get; set; }
    }
    /// <summary>
    /// 通路收款明細帳簿
    /// </summary>
    [Description("通路收款明細帳簿")]
    public class ChannelEAccountBillDetailModel : DetailRowState
    {
        public ChannelEAccountBillModel Bill { get; set; }
        /// <summary>
        /// 單據編號
        /// </summary>
        [Description("單據編號"), Key]
        public string BillNo { get; set; }
        [ForeignKey("ReceiptBillNo")]
        public ReceiptBillModel ReceiptBill { get; set; }
        /// <summary>
        /// 收款單號
        /// </summary>
        [Description("收款單號"), Key]
        public string ReceiptBillNo { get; set; }
    }
}
