using System;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.System;
using SKGPortalCore.Repository.SKGPortalCore.Business.MasterData;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 繳款人庫
    /// </summary>
    public class PayerRepository : BasicRepository<PayerSet>
    {
        public PayerRepository(ApplicationDbContext dataAccess) : base(dataAccess) 
        {
            DataFlowNo = null;
            SetFlowNo = new Action<PayerSet>(p =>
            {
                if (p.Payer.PayerId.IsNullOrEmpty())
                {
                    string billNo = $"Payer{DateTime.Today.ToString("yyyyMMdd")}{(++DataFlowNo.FlowNo).ToString().PadLeft(5, '0')}";
                    p.Payer.PayerId = billNo;
                }
            });
        }

        protected override void AfterSetEntity(PayerSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            BizPayer.CheckData( Message, set);
        }
    }
}
