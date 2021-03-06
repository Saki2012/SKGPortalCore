﻿using System;
using System.ComponentModel;

namespace SKGPortalCore.Core.SystemTable
{
    /// <summary>
    /// 客戶Admin密碼函
    /// </summary>
    internal class CustomerAdminPaswadoModel
    {
        /// <summary>
        /// 客戶統編
        /// </summary>
        [Description(SystemCP.DESC_CustomerId)] public string CustomerId { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        [Description(SystemCP.DESC_Pasuwado)]
        public string Pasuwado { get; set; }
        /// <summary>
        /// 密碼到期日
        /// </summary>
        public DateTime ExpiredDate { get; set; }
    }
}
