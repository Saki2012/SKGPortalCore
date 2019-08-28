using SKGPortalCore.Business.BillData;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SKGPortalCore.Repository.BillData
{
    /// <summary>
    /// 收款單庫
    /// </summary>
    public class ReceiptBillRepository : BasicRepository<ReceiptBillSet>
    {
        #region Construct
        public ReceiptBillRepository(ApplicationDbContext dataAccess) : base(dataAccess)
        {
            User = new SystemOperator().SysOperator;
        }
        #endregion

        #region Protected
        protected override void AfterSetEntity(ReceiptBillSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            using BizReceiptBill biz = new BizReceiptBill(Message);
            set.ReceiptBill.ToBillNo = GetBillNo(set.ReceiptBill.CompareCodeForCheck);
            InsertBillReceiptDetail(set.ReceiptBill.BillNo, set.ReceiptBill.ToBillNo);
            if (action == FuncAction.Create)//未來若有修改RemitDate的情況，需進行差異調整
                set.ReceiptBill.RemitDate = biz.GetRemitDate(set.ReceiptBill, null);
            InsertChannelEAccount(biz, set);
        }
        protected override void AfterRemoveEntity(ReceiptBillSet set)
        {
            base.AfterRemoveEntity(set);
            RemoveBillReceiptDetail(set.ReceiptBill.BillNo, set.ReceiptBill.ToBillNo);
            RemoveChannelEAccount();
        }
        #endregion

        #region Private
        /// <summary>
        /// 獲取對應的帳單編號
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        private string GetBillNo(string compareCodeForCheck)
        {
            List<string> bills = DataAccess.Set<BillModel>().Where(p => p.CompareCodeForCheck == compareCodeForCheck &&
             (p.FormStatus == FormStatus.Saved || p.FormStatus == FormStatus.Approved)).OrderByDescending(p => p.CreateTime).Select(p => p.BillNo).ToList();
            return bills.HasData() ? bills[0] : string.Empty;
        }
        /// <summary>
        /// 插入帳單收款明細
        /// </summary>
        /// <param name="receiptBillNo"></param>
        /// <param name="billNo"></param>
        private void InsertBillReceiptDetail(string receiptBillNo, string billNo)
        {
            if (billNo.IsNullOrEmpty()) return;
            using BillRepository rep = new BillRepository(DataAccess);
            BillSet billSet = rep.QueryData(new object[] { billNo });
            if (null == billSet) { /*add Message:查無帳單*/ return; }
            billSet.BillReceiptDetail.Add(new BillReceiptDetailModel() { BillNo = billNo, ReceiptBillNo = receiptBillNo, RowState = RowState.Insert });
            rep.Update(billSet);
        }
        /// <summary>
        /// 移除帳單收款明細
        /// </summary>
        /// <param name="receiptBillNo"></param>
        /// <param name="billNo"></param>
        private void RemoveBillReceiptDetail(string receiptBillNo, string billNo)
        {
            if (billNo.IsNullOrEmpty()) return;
            using BillRepository rep = new BillRepository(DataAccess);
            BillSet billSet = rep.QueryData(new object[] { billNo });
            if (null == billSet) { /*add Message:查無帳單*/return; }
            BillReceiptDetailModel receiptDetail = billSet.BillReceiptDetail.FirstOrDefault(p => p.BillNo == billNo && p.ReceiptBillNo == receiptBillNo);
            if (null == receiptDetail) { /*add Message 已無存在*/return; }
            receiptDetail.RowState = RowState.Delete;
            rep.Update(billSet);
        }
        /// <summary>
        /// 插入通路電子帳簿
        /// </summary>
        private void InsertChannelEAccount(BizReceiptBill biz, ReceiptBillSet set)
        {
            if (set.ReceiptBill.RemitDate == DateTime.MinValue) return;
            using ChannelEAccountBillRepository repo = new ChannelEAccountBillRepository(DataAccess);
            if (DataAccess.Set<ChannelEAccountBillModel>().Where(p => p.CollectionTypeId == set.ReceiptBill.CollectionTypeId && p.ExpectRemitDate == set.ReceiptBill.RemitDate).Count() == 0)
            {
                var accountSet = biz.CreateChannelEAccountBill(set.ReceiptBill);
                repo.Create(accountSet);
            }
            else
            {
                var accountSet = repo.QueryData(new object[] { "" });
                if (DataAccess.Set<ChannelEAccountBillDetailModel>().Where(p => p.ReceiptBillNo == set.ReceiptBill.BillNo).Count() == 0)
                    accountSet.ChannelEAccountBillDetail.Add(new ChannelEAccountBillDetailModel() { BillNo = accountSet.ChannelEAccountBill.BillNo, ReceiptBillNo = set.ReceiptBill.BillNo, RowState = RowState.Insert });
                repo.Update(accountSet);
            }
        }
        /// <summary>
        /// 移除通路電子帳簿
        /// </summary>
        private void RemoveChannelEAccount()
        {
            using ChannelEAccountBillRepository repo = new ChannelEAccountBillRepository(DataAccess);

        }
        #endregion
    }
}
