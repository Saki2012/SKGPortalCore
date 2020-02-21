using System;
using System.ComponentModel;

namespace SKGPortalCore.Model.MasterData
{
    /// <summary>
    /// 客戶Admin密碼函
    /// </summary>
    internal class CustomerAdminPaswadoModel
    {
        /// <summary>
        /// 客戶統編
        /// </summary>
        [Description("客戶統編")]
        public string CustomerId { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        public string Pasuwado { get; set; }
        /// <summary>
        /// 密碼到期日
        /// </summary>
        public DateTime ExpiredDate { get; set; }
    }
}
