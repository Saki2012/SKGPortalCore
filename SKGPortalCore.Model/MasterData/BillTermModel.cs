using SKGPortalCore.Core;
using SKGPortalCore.Core.LibAttribute;
using SKGPortalCore.Core.Model;
using SKGPortalCore.Model.SourceData;
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
    [Description(SystemCP.DESC_BillTerm)]
    public class BillTermSet
    {
        /// <summary>
        /// 期別資料
        /// </summary>
        [Description(SystemCP.DESC_BillTerm)] public BillTermModel BillTerm { get; set; } = new BillTermModel();
        /// <summary>
        /// 期別費用明細
        /// </summary>
        [Description(SystemCP.DESC_BillTermDt)] public List<BillTermDetailModel> BillTermDetail { get; set; } = new List<BillTermDetailModel>();
    }
    /// <summary>
    /// 期別
    /// </summary>
    [Description(SystemCP.DESC_BillTerm)]
    public class BillTermModel : MasterDataModel
    {
        /// <summary>
        /// 商戶資料
        /// </summary>
        [ForeignKey(nameof(CustomerCode))] public BizCustomerModel BizCustomer { get; set; }
        /// <summary>
        /// 企業代號
        /// </summary>
        [Description(SystemCP.DESC_CustomerCode), Key] public string CustomerCode { get; set; }
        /// <summary>
        /// 期別代號
        /// </summary>
        [Description(SystemCP.DESC_BillTermId), Key, MaxLength(SystemCP.DataIdLen)] public string BillTermId { get; set; }
        /// <summary>
        /// 期別名稱
        /// </summary>
        [Description(SystemCP.DESC_BillTermName), Required, InputField, MaxLength(SystemCP.NormalLen)] public string BillTermName { get; set; }
        /// <summary>
        /// 期別編號
        /// </summary>
        [Description(SystemCP.DESC_BillTermNo), Required, InputField, MaxLength(SystemCP.NormalLen)] public string BillTermNo { get; set; }
        /// <summary>
        /// 繳款截止日
        /// 註：對帳單的繳款截止日進行CheckField
        /// </summary>
        [Description(SystemCP.DESC_PayEndDate), Required, InputField] public DateTime PayEndDate { get; set; }
    }
    /// <summary>
    /// 期別費用明細
    /// </summary>
    [Description(SystemCP.DESC_BillTermDt)]
    public class BillTermDetailModel : DetailRowState
    {
        /// <summary>
        /// 期別
        /// </summary>
        [ForeignKey(nameof(CustomerCode) + "," + nameof(BillTermId))] public BillTermModel BillTerm { get; set; }
        /// <summary>
        /// 企業代號
        /// </summary>
        [Description(SystemCP.DESC_CustomerCode), Key] public string CustomerCode { get; set; }
        /// <summary>
        /// 期別代號
        /// </summary>
        [Description(SystemCP.DESC_BillTermId), Key] public string BillTermId { get; set; }
        /// <summary>
        /// 序號
        /// </summary>
        [Description(SystemCP.DESC_RowId), Key] public int RowId { get; set; }
        /// <summary>
        /// 費用名稱
        /// 註：對帳單明細的費用名稱進行CheckField
        /// </summary>
        [Description(SystemCP.DESC_FeeName), Required, InputField, MaxLength(SystemCP.NormalLen)] public string FeeName { get; set; }
        /// <summary>
        /// 應繳金額
        /// 註：對帳單明細的應繳金額進行CheckField
        /// </summary>
        [Description(SystemCP.DESC_ShouldPayAmount), InputField] public decimal PayAmount { get; set; }
    }
}
