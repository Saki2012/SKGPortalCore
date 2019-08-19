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
        /// <summary>
        /// 期別編號長度
        /// </summary>
        [Description("期別編號長度")]
        public byte BillTermLen { get; set; }
        /// <summary>
        /// 繳款人編號長度
        /// </summary>
        [Description("繳款人編號長度")]
        public byte PayerNoLen { get; set; }
        /// <summary>
        /// 所屬分行
        /// </summary>
        [Description("所屬分行")]
        public string DeptId { get; set; }
        /// <summary>
        /// 繳款人可查看帳單資料
        /// </summary>
        [Description("繳款人可查看帳單資料")]
        public bool PayerAuthorize { get; set; }
        /// <summary>
        /// 是否系統商
        /// </summary>
        [Description("是否系統商")]
        public bool IsSysCust { get; set; }
    }
}
