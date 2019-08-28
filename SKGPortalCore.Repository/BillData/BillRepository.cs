using SKGPortalCore.Business.BillData;
using SKGPortalCore.Data;
using SKGPortalCore.Model;
using SKGPortalCore.Model.BillData;

namespace SKGPortalCore.Repository.BillData
{
    /// <summary>
    /// 帳單庫
    /// </summary>
    public class BillRepository : BasicRepository<BillSet>
    {
        #region Construct
        public BillRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
        #endregion
        #region Protected
        protected override void AfterSetEntity(BillSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            using BizBill biz = new BizBill(Message, DataAccess, User);
            biz.SetData(set, action);
            biz.CheckData(set);
        }
        #endregion
    }
}
