using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.System;
using SKGPortalCore.Repository.SKGPortalCore.Business.MasterData;
using System.Runtime.InteropServices;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 商戶資料庫
    /// </summary>
    [ProgId(SystemCP.ProgId_BillTerm)]
    public class BizCustomerRepository : BasicRepository<BizCustomerSet>
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
