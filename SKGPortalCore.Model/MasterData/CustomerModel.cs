using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Model.System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SKGPortalCore.Model.MasterData
{
    /// <summary>
    /// 客戶
    /// </summary>
    [Description("客戶基本資料")]
    public class CustomerSet
    {
        /// <summary>
        /// 客戶資料
        /// </summary>
        public CustomerModel Customer { get; set; }
    }
    /// <summary>
    /// 客戶資料
    /// </summary>
    [Description("客戶基本資料")]
    public class CustomerModel : MasterDataModel
    {
        /// <summary>
        /// 客戶統編
        /// </summary>
        [Description("客戶統編"), Key, MaxLength(ConstParameter.DataIdLen)]
        public string CustomerId { get; set; }
        /// <summary>
        /// 客戶名稱
        /// </summary>
        [Description("客戶名稱"), Required, InputField]
        public string CustomerName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [Description("地址"), Required, InputField]
        public string Address { get; set; }
        /// <summary>
        /// 電話
        /// </summary>
        [Description("電話"), Required, InputField]
        public string Tel { get; set; }
        /// <summary>
        /// 傳真
        /// </summary>
        [Description("傳真"), Required, InputField]
        public string Fax { get; set; }
        /// <summary>
        /// 郵遞區號
        /// </summary>
        [Description("郵遞區號"), Required, InputField]
        public string ZipCode { get; set; }
        /// <summary>
        /// 郵簡許可單位
        /// </summary>
        [Description("郵簡許可單位"), Required, InputField]
        public string ZipUnit { get; set; }
        /// <summary>
        /// 郵簡字號
        /// </summary>
        [Description("郵簡字號"), Required, InputField]
        public string ZipNum { get; set; }
    }
}
