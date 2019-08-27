using System;
using System.Collections.Generic;
using System.Text;
using SKGPortalCore.Business.BillData;
using SKGPortalCore.Data;
using SKGPortalCore.Model;
using SKGPortalCore.Model.BillData;

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
            using BizChannelWriteOfBill biz = new BizChannelWriteOfBill(Message);
            biz.CheckData(set);
        }

        protected override void AfterApprove(ChannelWriteOfBillSet set, bool status)
        {
            base.AfterApprove(set, status);
            if (status)
            {
                CreateDisbursementBill(set.ChannelWriteOfBill.BillNo);
            }
        }
        #endregion
        /// <summary>
        /// 產生撥款單
        /// </summary>
        /// <param name="channelWriteOfBillNo"></param>
        private void CreateDisbursementBill(string channelWriteOfBillNo)
        {
            DisbursementBillSet disbursementBill = new DisbursementBillSet();
            disbursementBill.DisbursementBill.ChannelWriteOfBillNo = channelWriteOfBillNo;
            DisbursementBillRepository repo = new DisbursementBillRepository(DataAccess);
            repo.Create(disbursementBill);
        }
    }
}
