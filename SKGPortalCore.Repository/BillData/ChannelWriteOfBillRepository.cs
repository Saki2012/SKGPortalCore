using System.Runtime.InteropServices;
using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Repository.Entity;
using SKGPortalCore.Interface.IRepository.BillData;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Repository.SKGPortalCore.Business.BillData;

namespace SKGPortalCore.Repository.BillData
{
    /// <summary>
    /// 通路帳款核銷單庫
    /// </summary>
    [ProgId(SystemCP.ProgId_ChannelWriteOfBill)]
    public class ChannelWriteOfBillRepository : BasicRepository<ChannelWriteOfBillSet>, IChannelWriteOfBillRepository
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
