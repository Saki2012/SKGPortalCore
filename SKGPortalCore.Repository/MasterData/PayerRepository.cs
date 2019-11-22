using System;
using SKGPortalCore.Business.MasterData;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData;

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
