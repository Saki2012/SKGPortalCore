﻿using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Model.System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model.BillData
{

    public class DisbursementBillSet
    {
        public DisbursementBillModel DisbursementBill { get; set; }
        //public List<DisbursementDetailModel> DisbursementDetail { get; set; }
    }


    /// <summary>
    /// 撥款單
    /// </summary>
    [Description("撥款單")]
    public class DisbursementBillModel : BillDataModel
    {
        /// <summary>
        /// 單據編號
        /// </summary>
        [Description("單據編號"), Key, MaxLength(ConstParameter.BillNoLen)]
        public string BillNo { get; set; }
        [ForeignKey("ChannelWriteOfBillNo")]
        public ChannelWriteOfBillModel ChannelWriteOfBill { get; set; }
        /// <summary>
        /// 通路帳款核銷單號
        /// </summary>
        [Description("通路帳款核銷單號")]
        public string ChannelWriteOfBillNo { get; set; }
    }
}
