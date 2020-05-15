using System;
using System.Runtime.InteropServices;
using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.Libary;
using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Repository.Entity;
using SKGPortalCore.Interface.IRepository.MasterData;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Repository.SKGPortalCore.Business.MasterData;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 繳款人庫
    /// </summary>
    [ProgId(SystemCP.DESC_Payer)]
    public class PayerRepository : BasicRepository<PayerSet>, IPayerRepository
    {
        public PayerRepository(ApplicationDbContext dataAccess) : base(dataAccess)
        {
            DataFlowNo = null;
            SetFlowNo = new Action<PayerSet>(p =>
            {
                if (p.Payer.PayerId.IsNullOrEmpty())
                {
                    string billNo = $"Payer{p.Payer.CustomerCode}{(++DataFlowNo.FlowNo).ToString().PadLeft(5, '0')}";
                    p.Payer.PayerId = billNo;
                }
            });
        }

        protected override void AfterSetEntity(PayerSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            BizPayer.CheckData(Message, DataAccess, set);
        }
    }
}
