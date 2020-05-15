using System;
using System.Globalization;
using System.Runtime.InteropServices;
using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.Libary;
using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Repository.Entity;
using SKGPortalCore.Interface.IRepository.BillData;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Repository.SKGPortalCore.Business.BillData;

namespace SKGPortalCore.Repository.BillData
{
    /// <summary>
    /// 入金機庫
    /// </summary>
    [ProgId(SystemCP.ProgId_DepositBill)]
    public class DepositBillRepository : BasicRepository<DepositBillSet>, IDepositBillRepository
    {
        #region Construct
        public DepositBillRepository(ApplicationDbContext dataAccess) : base(dataAccess)
        {
            SetFlowNo = new Action<DepositBillSet>(p =>
            {
                if (p.DepositBill.BillNo.IsNullOrEmpty())
                {
                    string billNo = $"Deposit{DateTime.Today.ToString("yyyyMMdd", CultureInfo.InvariantCulture)}{(++DataFlowNo.FlowNo).ToString().PadLeft(5, '0')}";
                    p.DepositBill.BillNo = billNo;
                    p.DepositBillReceiptDetail?.ForEach(p => p.BillNo = billNo);
                }
            });
        }
        #endregion

        #region Protected
        protected override void AfterSetEntity(DepositBillSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            BizDepositBill.CheckData(set, Message, DataAccess);
            BizDepositBill.SetData(set, ProgId, DataAccess);
        }
        #endregion
    }
}
