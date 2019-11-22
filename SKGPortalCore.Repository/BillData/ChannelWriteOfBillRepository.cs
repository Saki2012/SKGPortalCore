using SKGPortalCore.Data;
using SKGPortalCore.Model;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Repository.SKGPortalCore.Business.BillData;

namespace SKGPortalCore.Repository.BillData
{
    /// <summary>
    /// 通路帳款核銷單庫
    /// </summary>
    public class ChannelWriteOfBillRepository : BasicRepository<ChannelWriteOfBillSet>
    {
        #region Construct
        public ChannelWriteOfBillRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
        #endregion
        #region Protected
        protected override void AfterSetEntity(ChannelWriteOfBillSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            BizChannelWriteOfBill.CheckData(set);
        }

        protected override void AfterApprove(ChannelWriteOfBillSet set, bool status)
        {
            base.AfterApprove(set, status);
            if (status)
            {
            }
        }
        #endregion

    }
}
