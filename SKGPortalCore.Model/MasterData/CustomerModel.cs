using SKGPortalCore.Lib;
using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Model.System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SKGPortalCore.Model.MasterData
{
    /// <summary>
    /// 客戶資料
    /// </summary>
    [Description(SystemCP.DESC_Customer)]
    public class CustomerSet
    {
        /// <summary>
        /// 客戶資料
        /// </summary>
        [Description(SystemCP.DESC_Customer)] public CustomerModel Customer { get; set; }
    }
    /// <summary>
    /// 客戶資料
    /// </summary>
    [Description(SystemCP.DESC_Customer)]
    public class CustomerModel : MasterDataModel
    {
        /// <summary>
        /// 客戶統編
        /// </summary>
        [Description(SystemCP.DESC_CustomerId), Key, MaxLength(SystemCP.DataIdLen)] public string CustomerId { get; set; }
        /// <summary>
        /// 客戶名稱
        /// </summary>
        [Description(SystemCP.DESC_CustomerName), Required, InputField, MaxLength(SystemCP.NormalLen)] public string CustomerName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [Description(SystemCP.DESC_Address), Required, InputField, MaxLength(SystemCP.LongLen)] public string Address { get; set; }
        /// <summary>
        /// 電話
        /// </summary>
        [Description(SystemCP.DESC_Tel), Required, InputField, MaxLength(SystemCP.NormalLen)] public string Tel { get; set; }
        /// <summary>
        /// 傳真
        /// </summary>
        [Description(SystemCP.DESC_Fax), Required, InputField, MaxLength(SystemCP.NormalLen)] public string Fax { get; set; }
        /// <summary>
        /// 郵遞區號
        /// </summary>
        [Description(SystemCP.DESC_ZipCode), Required, InputField, MaxLength(SystemCP.NormalLen)] public string ZipCode { get; set; }
        /// <summary>
        /// 郵簡許可單位
        /// </summary>
        [Description(SystemCP.DESC_ZipUnit), Required, InputField, MaxLength(SystemCP.NormalLen)] public string ZipUnit { get; set; }
        /// <summary>
        /// 郵簡字號
        /// </summary>
        [Description(SystemCP.DESC_ZipNum), Required, InputField, MaxLength(SystemCP.NormalLen)] public string ZipNum { get; set; }
    }
}
