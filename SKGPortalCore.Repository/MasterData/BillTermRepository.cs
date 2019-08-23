using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Business.MasterData;
using SKGPortalCore.Data;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 期別庫
    /// </summary>
    public class BillTermRepository : BasicRepository<BillTermSet>
    {
        public BillTermRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }

        protected override void AfterSetEntity(BillTermSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            using BizBillTerm biz = new BizBillTerm(Message, DataAccess);
            biz.CheckData(set);
        }
    }
}
