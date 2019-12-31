using System.ComponentModel;
using System.Runtime.InteropServices;
using SKGPortalCore.Data;
using SKGPortalCore.Model;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Repository.SKGPortalCore.Business.BillData;

namespace SKGPortalCore.Repository.BillData
{
    /// <summary>
    /// 通路帳簿庫
    /// </summary>
    [ProgId("ChannelEAccountBill"), Description("通路帳簿")]
    public class ChannelEAccountBillRepository : BasicRepository<ChannelEAccountBillSet>
    {
        #region Construct
        public ChannelEAccountBillRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
        #endregion

        #region Protected
        protected override void AfterSetEntity(ChannelEAccountBillSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            BizChannelEAccountBill.SetData(set);
        }
        #endregion
    }
}
