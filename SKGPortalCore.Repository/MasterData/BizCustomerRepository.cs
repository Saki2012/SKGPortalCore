using System.Runtime.InteropServices;
using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Repository.Entity;
using SKGPortalCore.Interface.IRepository.MasterData;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Repository.SKGPortalCore.Business.MasterData;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 商戶資料庫
    /// </summary>
    [ProgId(SystemCP.ProgId_BillTerm)]
    public class BizCustomerRepository : BasicRepository<BizCustomerSet>, IBizCustomerRepository
    {
        public BizCustomerRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }

        protected override void AfterSetEntity(BizCustomerSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            BizBizCustomer.CheckData(Message, set);
            BizBizCustomer.SetData(set);
        }
    }
}
