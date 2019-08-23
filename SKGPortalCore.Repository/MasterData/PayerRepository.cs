using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Business.MasterData;
using SKGPortalCore.Data;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 繳款人庫
    /// </summary>
    public class PayerRepository : BasicRepository<PayerSet>
    {
        public PayerRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }

        protected override void AfterSetEntity(PayerSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            using BizPayer biz = new BizPayer(Message);
            biz.CheckData(set);
        }
    }
}
