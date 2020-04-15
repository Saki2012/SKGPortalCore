using SKGPortalCore.Lib;
using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Model.System;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model.MasterData
{
    /// <summary>
    /// 繳款人
    /// </summary>
    [Description(SystemCP.DESC_Payer)]
    public class PayerSet
    {
        /// <summary>
        /// 繳款人
        /// </summary>
        [Description(SystemCP.DESC_Payer)] public PayerModel Payer { get; set; } = new PayerModel();
    }

    /// <summary>
    /// 繳款人
    /// </summary>
    [Description(SystemCP.DESC_Payer)]
    public class PayerModel : MasterDataModel
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
        /// 繳款人代號
        /// </summary>
        [Description(SystemCP.DESC_PayerId), Key, MaxLength(SystemCP.DataIdLen)] public string PayerId { get; set; }
        /// <summary>
        /// 繳款人名稱
        /// </summary>
        [Description(SystemCP.DESC_PayerName), Required, InputField, MaxLength(SystemCP.NormalLen)] public string PayerName { get; set; }
        /// <summary>
        /// 繳款人編碼
        /// </summary>
        [Description(SystemCP.DESC_PayerNo), Required, InputField, MaxLength(SystemCP.NormalLen)] public string PayerNo { get; set; }
        /// <summary>
        /// 繳款人類別
        /// </summary>
        [Description(SystemCP.DESC_PayerType), InputField] public PayerType PayerType { get; set; }
    }
}
