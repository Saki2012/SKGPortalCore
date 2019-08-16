using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Business.BillData;
using SKGPortalCore.Data;
using SKGPortalCore.Models.BillData;

namespace SKGPortalCore.Repository.BillData
{
    public class BillRepository : BasicRepository<BillSet>
    {
        public BillRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
        protected override void BeforeSetEntity(BillSet set)
        {
            base.BeforeSetEntity(set);
            using BizBill biz = new BizBill(DataAccess);
            biz.CheckData(set);
            biz.SetData(set);
        }
        protected override void AfterSetEntity(BillSet set)
        {
            base.AfterSetEntity(set);
        }
    }
}
