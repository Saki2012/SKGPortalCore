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

        public string Pasuwado { get; set; }

        public DateTime ExpiredDate { get; set; }
    }
}
