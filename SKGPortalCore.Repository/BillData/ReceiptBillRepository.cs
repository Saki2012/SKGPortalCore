using SKGPortalCore.Business.BillData;
using SKGPortalCore.Data;
using SKGPortalCore.Model;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Models.BillData;
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
        }
        protected override void AfterRemoveEntity(ReceiptBillSet set)
        {
            base.AfterRemoveEntity(set);
            RemoveBillReceiptDetail(set.ReceiptBill.BillNo, set.ReceiptBill.ToBillNo);
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
            List<BillModel> bills = DataAccess.Bill.Where(p => p.CompareCodeForCheck == compareCodeForCheck &&
             (p.FormStatus == FormStatus.Saved || p.FormStatus == FormStatus.Approved)).OrderBy(p=>p.CreateTime).ToList();
            return bills[0].BillNo;
        }
        /// <summary>
        /// 插入帳單收款明細
        /// </summary>
        /// <param name="receiptBillNo"></param>
        /// <param name="billNo"></param>
        private void InsertBillReceiptDetail(string receiptBillNo, string billNo)
        {
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
            using BillRepository rep = new BillRepository(DataAccess);
            BillSet billSet = rep.QueryData(new object[] { billNo });
            if (null == billSet) { /*add Message:查無帳單*/return; }
            BillReceiptDetailModel receiptDetail = billSet.BillReceiptDetail.FirstOrDefault(p => p.BillNo == billNo && p.ReceiptBillNo == receiptBillNo);
            if (null == receiptDetail) { /*add Message 已無存在*/return; }
            receiptDetail.RowState = RowState.Delete;
            rep.Update(billSet);
        }
        #endregion
    }
}
