using System.Linq;
using System.Runtime.InteropServices;
using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Repository.Entity;
using SKGPortalCore.Interface.IRepository.BillData;
using SKGPortalCore.Model.BillData;

namespace SKGPortalCore.Repository.BillData
{
    [ProgId(SystemCP.ProgId_DisbursementBill)]
    public class DisbursementBillRepository : BasicRepository<DisbursementBillSet>, IDisbursementBillRepository
    {
        #region Construct
        public DisbursementBillRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
        #endregion

        #region Protect
        protected override void AfterSetEntity(DisbursementBillSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            if (action == FuncAction.Create)
            {
                WriteBackBillNo(set.DisbursementBill.BillNo, set.DisbursementBill.ChannelWriteOfBillNo);
            }
        }
        protected override void AfterRemoveEntity(DisbursementBillSet set)
        {
            base.AfterRemoveEntity(set);
            WriteBackBillNo(set.DisbursementBill.BillNo, null);
        }

        protected override void AfterApprove(DisbursementBillSet set, bool status)
        {
            base.AfterApprove(set, status);
            /*產生撥款檔，並拋給核心*/
        }
        protected override void AfterInvalid(DisbursementBillSet set, bool status)
        {
            base.AfterInvalid(set, status);
            if (status)
            {
                WriteBackBillNo(set.DisbursementBill.BillNo, null);
            }
            else
            {
                WriteBackBillNo(set.DisbursementBill.BillNo, set.DisbursementBill.ChannelWriteOfBillNo);
            }
        }
        #endregion

        #region Private
        private void WriteBackBillNo(string billNo, string channelWriteOfBillNo)
        {
            ChannelWriteOfBillModel b = DataAccess.Set<ChannelWriteOfBillModel>().FirstOrDefault(p => p.BillNo.Equals(channelWriteOfBillNo));
            b.DisBillNo = billNo;
            DataAccess.Set<ChannelWriteOfBillModel>().Update(b);
        }
        #endregion
    }
}
