using SKGPortalCore.Model.Enum;
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
        [ForeignKey("CustomerId")]
        public CustomerModel Customer { get; set; }
        /// <summary>
        /// 客戶統編
        /// </summary>
        [Description("客戶統編"), Key]
        public string CustomerId { get; set; }
        /// <summary>
        /// 繳款人代號
        /// </summary>
        [Description("繳款人代號"), Key]
        public string PayerId { get; set; }
        /// <summary>
        /// 繳款人名稱
        /// </summary>
        [Description("繳款人名稱")]
        public string PayerName { get; set; }
        /// <summary>
        /// 繳款人類別
        /// </summary>
        [Description("繳款人類別")]
        public PayerType PayerType { get; set; }
        /// <summary>
        /// 繳款人編碼
        /// </summary>
        [Description("繳款人編碼")]
        public string PayerNo { get; set; }
        /// <summary>
        /// 身份證字號
        /// </summary>
        [Description("身份證字號")]
        public string IDCard { get; set; }
        /// <summary>
        /// 電話
        /// </summary>
        [Description("電話")]
        public string Tel { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [Description("地址")]
        public string Address { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [Description("備註")]
        public string Memo { get; set; }
        /// <summary>
        /// 卡號
        /// </summary>
        [Description("卡號")]
        public string CardNum { get; set; }
        /// <summary>
        /// 有效期限-月
        /// </summary>
        [Description("有效期限-月")]
        public byte CardValidateMonth { get; set; }
        /// <summary>
        /// 有效期限-年
        /// </summary>
        [Description("有效期限-年")]
        public byte CardValidateYear { get; set; }
        /// <summary>
        /// CVV
        /// </summary>
        [Description("CVV")]
        public string CVV { get; set; }
    }
}
