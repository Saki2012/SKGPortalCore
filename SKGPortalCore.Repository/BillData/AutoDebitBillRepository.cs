using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.Libary;
using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Repository.Entity;
using SKGPortalCore.Interface.IRepository.BillData;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Repository.SKGPortalCore.Business.BillData;
using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace SKGPortalCore.Repository.BillData
{
    /// <summary>
    /// 約定扣款單庫
    /// </summary>
    [ProgId(SystemCP.ProgId_AutoDebitBill)]
    public class AutoDebitBillRepository : BasicRepository<AutoDebitBillSet>, IAutoDebitBillRepository
    {
        #region Construct
        public AutoDebitBillRepository(ApplicationDbContext dataAccess) : base(dataAccess)
        {
            SetFlowNo = new Action<AutoDebitBillSet>(p =>
            {
                if (p.AutoDebitBill.BillNo.IsNullOrEmpty())
                {
                    string billNo = $"Auto{DateTime.Today.ToString("yyyyMMdd", CultureInfo.InvariantCulture)}{(++DataFlowNo.FlowNo).ToString().PadLeft(5, '0')}";
                    p.AutoDebitBill.BillNo = billNo;
                    p.AutoDebitBillReceiptDetail?.ForEach(p => p.BillNo = billNo);
                }
            });
        }
        #endregion

        #region Protected
        protected override void AfterSetEntity(AutoDebitBillSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            BizAutoDebitBill.CheckData(set, Message, DataAccess);
            BizAutoDebitBill.SetData(set, ProgId, DataAccess);
        }
        #endregion
    }
}
