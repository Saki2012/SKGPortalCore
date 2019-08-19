using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Business.BillData;
using SKGPortalCore.Data;
using SKGPortalCore.Models.BillData;

namespace SKGPortalCore.Repository.BillData
{
    public class BillRepository : BasicRepository<BillSet>
    {
        #region Construct
        public BillRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
        #endregion
        #region Protected
        protected override void BeforeSetEntity(BillSet set)
        {
            base.BeforeSetEntity(set);
            return;
            using BizBill biz = new BizBill(Message, DataAccess);
            biz.CheckData(set);
            biz.SetData(set);
        }
        protected override void AfterSetEntity(BillSet set)
        {
            base.AfterSetEntity(set);
        }
        #endregion
    }
}
