using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SKGPortalCore.Core;
using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Model;

namespace SKGPortalCore.Model.BillData
{
    /// <summary>
    /// 通路帳款核銷單
    /// </summary>
    [Description(SystemCP.DESC_ChannelWriteOfBill)]

    public class ChannelWriteOfBillSet
    {
        [Description(SystemCP.DESC_ChannelWriteOfBill)] public ChannelWriteOfBillModel ChannelWriteOfBill { get; set; } = new ChannelWriteOfBillModel();
        [Description(SystemCP.DESC_ChannelWriteOfDt)] public List<ChannelWriteOfDetailModel> ChannelWriteOfDetail { get; set; } = new List<ChannelWriteOfDetailModel>();
        [Description(SystemCP.DESC_CashFlowWriteOfDt)] public List<CashFlowWriteOfDetailModel> CashFlowWriteOfDetail { get; set; } = new List<CashFlowWriteOfDetailModel>();
    }
    /// <summary>
    /// 通路帳款核銷單
    /// </summary>  
    [Description(SystemCP.DESC_ChannelWriteOfBill)]
    public class ChannelWriteOfBillModel : BillDataModel
    {
        /// <summary>
        /// 單據編號
        /// </summary>
        [Description(SystemCP.DESC_BillNo), Key, MaxLength(SystemCP.BillNoLen)] public string BillNo { get; set; }
        /// <summary>
        /// 核銷狀態
        /// </summary>
        [Description(SystemCP.DESC_WriteOfStatus)] public WriteOfStatus WriteOfStatus { get; set; }
        /// <summary>
        /// 預計撥款金額
        /// </summary>
        [Description(SystemCP.DESC_PrePayAmount)] public decimal PrePayAmount { get; set; }
        /// <summary>
        /// 撥款單
        /// </summary>
        [ForeignKey(nameof(DisBillNo))] public DisbursementBillModel DisBill { get; set; }
        /// <summary>
        /// 撥款單號
        /// </summary>
        [Description(SystemCP.DESC_DisBillNo)] public string DisBillNo { get; set; }
    }
    /// <summary>
    /// 通路帳目明細
    /// </summary>
    [Description(SystemCP.DESC_ChannelWriteOfDt)]
    public class ChannelWriteOfDetailModel : DetailRowState
    {
        /// <summary>
        /// 通路帳款核銷單
        /// </summary>
        [ForeignKey(nameof(BillNo))] public ChannelWriteOfBillModel Bill { get; set; }
        /// <summary>
        /// 單據編號
        /// </summary>
        [Description(SystemCP.DESC_BillNo), Key] public string BillNo { get; set; }
        /// <summary>
        /// 序號
        /// </summary>
        [Description(SystemCP.DESC_RowId), Key] public int RowId { get; set; }
        /// <summary>
        /// 通路帳簿
        /// </summary>
        [ForeignKey(nameof(ChannelEAccountBillNo))] public ChannelEAccountBillModel ChannelEAccountBill { get; set; }
        /// <summary>
        /// 通路帳簿單號
        /// </summary>
        [Description(SystemCP.DESC_ChannelEAccountBillNo)] public string ChannelEAccountBillNo { get; set; }
    }
    /// <summary>
    /// 金流帳目明細
    /// </summary>
    [Description(SystemCP.DESC_CashFlowWriteOfDt)]
    public class CashFlowWriteOfDetailModel : DetailRowState
    {
        /// <summary>
        /// 通路帳款核銷單
        /// </summary>
        [ForeignKey(nameof(BillNo))] public ChannelWriteOfBillModel Bill { get; set; }
        /// <summary>
        /// 單據編號
        /// </summary>
        [Description(SystemCP.DESC_BillNo), Key] public string BillNo { get; set; }
        /// <summary>
        /// 序號
        /// </summary>
        [Description(SystemCP.DESC_RowId), Key] public int RowId { get; set; }
        /// <summary>
        /// 金流帳簿
        /// </summary>
        [ForeignKey(nameof(CashFlowBillNo))] public CashFlowBillModel CashFlowBill { get; set; }
        /// <summary>
        /// 金流帳簿單號
        /// </summary>
        [Description(SystemCP.DESC_CashFlowBillNo)] public string CashFlowBillNo { get; set; }
    }
}
