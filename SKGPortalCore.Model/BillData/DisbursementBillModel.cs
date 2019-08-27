using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SKGPortalCore.Model.BillData
{

    public class DisbursementBillSet
    {
        public DisbursementBillModel DisbursementBill { get; set; }
        public List<DisbursementDetailModel> DisbursementDetail { get; set; }
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
        [Description("單據編號"), Key]
        public string BillNo { get; set; }
        public ChannelWriteOfBillModel ChannelWriteOfBill { get; set; }
        /// <summary>
        /// 通路帳款核銷單號
        /// </summary>
        public string ChannelWriteOfBillNo { get; set; }
    }

    public class DisbursementDetailModel : DetailRowState
    {

    }
}
