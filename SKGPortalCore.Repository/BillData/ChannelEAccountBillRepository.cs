﻿using SKGPortalCore.Data;
using SKGPortalCore.Model;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Repository.SKGPortalCore.Business.BillData;

namespace SKGPortalCore.Repository.BillData
{
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
        protected override void AfterRemoveEntity(ChannelEAccountBillSet set)
        {
            base.AfterRemoveEntity(set);

        }
        #endregion

        #region Private



        #endregion
    }
}
