using SKGPortalCore.Core;
using SKGPortalCore.Core.Model;
using SKGPortalCore.Model.SourceData;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model.BillData
{
    /// <summary>
    /// 撥款單
    /// </summary>
    [Description(SystemCP.DESC_DisbursementBill)]
    public class DisbursementBillSet
    {
        /// <summary>
        /// 撥款單
        /// </summary>
        [Description(SystemCP.DESC_DisbursementBill)] public DisbursementBillModel DisbursementBill { get; set; } = new DisbursementBillModel();
    }


    /// <summary>
    /// 撥款單
    /// </summary>
    [Description(SystemCP.DESC_DisbursementBill)]
    public class DisbursementBillModel : BillDataModel
    {
        /// <summary>
        /// 單據編號
        /// </summary>
        [Description(SystemCP.DESC_BillNo), Key, MaxLength(SystemCP.BillNoLen)] public string BillNo { get; set; }
        /// <summary>
        /// 通路帳款核銷單
        /// </summary>
        [ForeignKey(nameof(ChannelWriteOfBillNo))] public ChannelWriteOfBillModel ChannelWriteOfBill { get; set; }
        /// <summary>
        /// 通路帳款核銷單號
        /// </summary>
        [Description(SystemCP.DESC_ChannelWriteOfBillNo)] public string ChannelWriteOfBillNo { get; set; }
    }
}
