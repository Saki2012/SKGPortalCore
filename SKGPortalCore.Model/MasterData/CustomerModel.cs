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
        [Description("客戶統編"), Key]
        public string CustomerId { get; set; }
        /// <summary>
        /// 客戶名稱
        /// </summary>
        [Description("客戶名稱")]
        public string CustomerName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [Description("地址")]
        public string Address { get; set; }
        /// <summary>
        /// 電話
        /// </summary>
        [Description("電話")]
        public string Tel { get; set; }
        /// <summary>
        /// 傳真
        /// </summary>
        [Description("傳真")]
        public string Fax { get; set; }
        /// <summary>
        /// 郵遞區號
        /// </summary>
        [Description("郵遞區號")]
        public string ZipCode { get; set; }
        /// <summary>
        /// 郵簡許可單位
        /// </summary>
        [Description("郵簡許可單位")]
        public string ZipUnit { get; set; }
        /// <summary>
        /// 郵簡字號
        /// </summary>
        [Description("郵簡字號")]
        public string ZipNum { get; set; }
    }
}
