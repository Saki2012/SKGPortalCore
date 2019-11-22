using SKGPortalCore.Data;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Repository.SKGPortalCore.Business.MasterData;

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
            BizBizCustomer.CheckData(set);
        }
    }
}
