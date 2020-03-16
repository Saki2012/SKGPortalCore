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
    [Description("繳款人")]
    public class PayerSet
    {
        /// <summary>
        /// 繳款人資料
        /// </summary>
        [Description("繳款人資料")]
        public PayerModel Payer { get; set; }
    }

    /// <summary>
    /// 繳款人資料
    /// </summary>
    [Description("繳款人資料")]
    public class PayerModel : MasterDataModel
    {
        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("CustomerCode")]
        public BizCustomerModel BizCustomer { get; set; }
        /// <summary>
        /// 企業代號
        /// </summary>
        [Description("企業代號"), Key]
        public string CustomerCode { get; set; }
        /// <summary>
        /// 繳款人代號
        /// </summary>
        [Description("繳款人代號"), Key, MaxLength(ConstParameter.DataIdLen)]
        public string PayerId { get; set; }
        /// <summary>
        /// 繳款人名稱
        /// </summary>
        [Description("繳款人名稱"),Required, InputField, MaxLength(ConstParameter.NormalLen)]
        public string PayerName { get; set; }
        /// <summary>
        /// 繳款人編碼
        /// </summary>
        [Description("繳款人編碼"),Required, InputField, MaxLength(ConstParameter.NormalLen)]
        public string PayerNo { get; set; }
        /// <summary>
        /// 繳款人類別
        /// </summary>
        [Description("繳款人類別"), InputField]
        public PayerType PayerType { get; set; }
    }
}
