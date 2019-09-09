using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Business.MasterData;
using SKGPortalCore.Data;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 商戶資料庫
    /// </summary>
    public class BizCustomerRepository : BasicRepository<BizCustomerSet>
    {
        public BizCustomerRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }

        protected override void AfterSetEntity(BizCustomerSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            using BizBizCustomer bizCustomer = new BizBizCustomer(Message);
            bizCustomer.CheckData(set);
        }
    }
}
