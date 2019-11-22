using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using SKGPortalCore.Business.BillData;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Repository.MasterData;

namespace SKGPortalCore.Repository.BillData
{
    /// <summary>
    /// 收款單庫
    /// </summary>
    [ProgId("ReceiptBill"), Description("收款單")]
    public class ReceiptBillRepository : BasicRepository<ReceiptBillSet>
    {
        #region Property
        public Dictionary<string, BizCustomerSet> BizCustSetDic = new Dictionary<string, BizCustomerSet>();//{ get; set; }
        public Dictionary<string, CollectionTypeSet> ColSetDic = new Dictionary<string, CollectionTypeSet>();// { get; set; }
        public readonly Dictionary<string, ChannelVerifyPeriodModel> PeriodDic = new Dictionary<string, ChannelVerifyPeriodModel>();
        #endregion

        #region Construct
        public ReceiptBillRepository(ApplicationDbContext dataAccess) : base(dataAccess)
        {
            SetFlowNo = new Action<ReceiptBillSet>(p =>
            {
                if (p.ReceiptBill.BillNo.IsNullOrEmpty())
                {
                    string billNo = $"Rec{DateTime.Today.ToString("yyyyMMdd")}{(++DataFlowNo.FlowNo).ToString().PadLeft(5, '0')}";
                    p.ReceiptBill.BillNo = billNo;
                }
            });
            IsSetRefModel = false;
        }
        #endregion

        #region Protected
        protected override void AfterSetEntity(ReceiptBillSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            BizCustomerSet bizCustSet = GetBizCustomerSet(set.ReceiptBill.CompareCode, out string compareCodeForCheck);
            GetCollectionTypeSet(set.ReceiptBill.CollectionTypeId, set.ReceiptBill.ChannelId, set.ReceiptBill.PayAmount, out ChargePayType chargePayType, out decimal channelFee);
            ChannelVerifyPeriodModel periodModel = GetChannelVerifyPeriod(set.ReceiptBill.CollectionTypeId, set.ReceiptBill.ChannelId);
            BizReceiptBill.SetData(set, bizCustSet.BizCustomerFeeDetail, compareCodeForCheck, chargePayType, channelFee);
            BizReceiptBill.SetData(DataAccess,set, periodModel, action);
            InsertBillReceiptDetail(set.ReceiptBill, set.ReceiptBill.ToBillNo);
            InsertChannelEAccount( set);
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
        /// 根據銷帳編號獲取商戶資料
        /// </summary>
        /// <param name="compareCode"></param>
        /// <param name="compareCodeForCheck"></param>
        /// <returns></returns>
        private BizCustomerSet GetBizCustomerSet(string compareCode, out string compareCodeForCheck)
        {
            compareCodeForCheck = string.Empty;
            BizCustomerSet bizCust = null;
            string custCode6 = compareCode.Substring(0, 6),
                   custCode4 = compareCode.Substring(0, 4),
                   custCode3 = compareCode.Substring(0, 3);

            if (null == bizCust && BizCustSetDic.ContainsKey(custCode6))
                bizCust = BizCustSetDic[custCode6];
            if (null == bizCust && BizCustSetDic.ContainsKey(custCode4))
                bizCust = BizCustSetDic[custCode4];
            if (null == bizCust && BizCustSetDic.ContainsKey(custCode3))
                bizCust = BizCustSetDic[custCode3];

            if (null == bizCust)
            {
                using BizCustomerRepository biz = new BizCustomerRepository(DataAccess) { Message = Message };
                bizCust = biz.QueryData(new object[] { custCode6 });
                if (null != bizCust)
                    BizCustSetDic.Add(custCode6, bizCust);
                if (null == bizCust)
                    bizCust = biz.QueryData(new object[] { custCode4 });
                if (null != bizCust)
                    BizCustSetDic.Add(custCode4, bizCust);
                if (null == bizCust)
                    bizCust = biz.QueryData(new object[] { custCode3 });
                if (null != bizCust)
                    BizCustSetDic.Add(custCode3, bizCust);
                if (null == bizCust)
                    return null;
            }
            if (bizCust.BizCustomer.AccountStatus == AccountStatus.Unable)
                compareCodeForCheck = compareCode;
            else
                compareCodeForCheck = bizCust.BizCustomer.VirtualAccount3 == VirtualAccount3.NoverifyCode ? compareCode : compareCode.Substring(0, compareCode.Length - 1);
            return bizCust;
        }
        /// <summary>
        /// 獲取代收類別
        /// </summary>
        /// <param name="DataAccess"></param>
        /// <param name="collectionTypeId"></param>
        /// <param name="channelId"></param>
        /// <param name="amount"></param>
        /// <param name="chargePayType"></param>
        /// <param name="channelFee"></param>
        private void GetCollectionTypeSet(string collectionTypeId, string channelId, decimal amount, out ChargePayType chargePayType, out decimal channelFee)
        {
            CollectionTypeSet colSet = null;
            channelFee = 0;
            chargePayType = ChargePayType.Deduction;
            if (!ColSetDic.ContainsKey(collectionTypeId))
            {
                using CollectionTypeRepository colRepo = new CollectionTypeRepository(DataAccess);
                colSet = colRepo.QueryData(new object[] { collectionTypeId });
                ColSetDic.Add(collectionTypeId, colSet);
            }
            colSet = ColSetDic[collectionTypeId];
            if (null == colSet) return;
            chargePayType = colSet.CollectionType.ChargePayType;
            CollectionTypeDetailModel c = colSet.CollectionTypeDetail.FirstOrDefault(p => p.CollectionTypeId == collectionTypeId && p.ChannelId == channelId && p.SRange <= amount && p.ERange >= amount);
            if (null != c) channelFee = c.Fee;
        }
        /// <summary>
        /// 獲取預計匯款
        /// </summary>
        /// <param name="collectionTypeId"></param>
        /// <param name="channelId"></param>
        private ChannelVerifyPeriodModel GetChannelVerifyPeriod(string collectionTypeId, string channelId)
        {
            string pk = $"{collectionTypeId},{channelId}";
            ChannelVerifyPeriodModel periodModel = null;
            if (!PeriodDic.ContainsKey(pk))
            {
                periodModel = DataAccess.Set<ChannelVerifyPeriodModel>().FirstOrDefault(p => p.ChannelId == channelId && p.CollectionTypeId == collectionTypeId);
                PeriodDic.Add(pk, periodModel);
            }
            periodModel = PeriodDic[pk];
            return periodModel;
        }

        /// <summary>
        /// 插入帳單收款明細
        /// </summary>
        /// <param name="receiptBillNo"></param>
        /// <param name="billNo"></param>
        private void InsertBillReceiptDetail(ReceiptBillModel receipt, string billNo)
        {
            if (billNo.IsNullOrEmpty()) return;
            //using BillRepository rep = new BillRepository(DataAccess) { User = User };
            //BillSet billSet = rep.QueryData(new object[] { billNo });
            //if (null == billSet) { /*add Message:查無帳單*/ return; }
            //billSet.BillReceiptDetail.Add(new BillReceiptDetailModel() { BillNo = billNo, ReceiptBill = receipt, ReceiptBillNo = receipt.BillNo, RowState = RowState.Insert });
            //rep.Update(billSet);
            BillModel bill = DataAccess.Find<BillModel>(billNo);
            BillReceiptDetailModel dt = new BillReceiptDetailModel() { BillNo = billNo, ReceiptBill = receipt, ReceiptBillNo = receipt.BillNo, RowState = RowState.Insert };
            DataAccess.Add(dt);
            bill.HasPayAmount += receipt.PayAmount;
            bill.PayStatus = BizBill.GetPayStatus(bill.PayAmount, bill.HasPayAmount);
            DataAccess.Update(bill);
        }
        /// <summary>
        /// 移除帳單收款明細
        /// </summary>
        /// <param name="receiptBillNo"></param>
        /// <param name="billNo"></param>
        private void RemoveBillReceiptDetail(string receiptBillNo, string billNo)
        {
            if (billNo.IsNullOrEmpty()) return;
            using BillRepository rep = new BillRepository(DataAccess) { User = User };
            BillSet billSet = rep.QueryData(new object[] { billNo });
            if (null == billSet) { return; }
            BillReceiptDetailModel receiptDetail = billSet.BillReceiptDetail.FirstOrDefault(p => p.BillNo == billNo && p.ReceiptBillNo == receiptBillNo);
            if (null == receiptDetail) { return; }
            receiptDetail.RowState = RowState.Delete;
            rep.Update(billSet);
        }
        /// <summary>
        /// 插入通路電子帳簿
        /// </summary>
        private void InsertChannelEAccount(ReceiptBillSet set)
        {
            if (set.ReceiptBill.RemitDate == DateTime.MinValue) return;

            using ChannelEAccountBillRepository repo = new ChannelEAccountBillRepository(DataAccess) { User = User };
            if (DataAccess.Set<ChannelEAccountBillModel>().Where(p => p.CollectionTypeId == set.ReceiptBill.CollectionTypeId && p.ExpectRemitDate == set.ReceiptBill.RemitDate).Count() == 0)
            {
                ChannelEAccountBillSet accountSet = BizReceiptBill.CreateChannelEAccountBill(set.ReceiptBill);
                repo.Create(accountSet);
            }
            else
            {
                ChannelEAccountBillSet accountSet = repo.QueryData(new object[] { "" });
                if (DataAccess.Set<ChannelEAccountBillDetailModel>().Where(p => p.ReceiptBillNo == set.ReceiptBill.BillNo).Count() == 0)
                {
                    accountSet.ChannelEAccountBillDetail.Add(new ChannelEAccountBillDetailModel() { BillNo = accountSet.ChannelEAccountBill.BillNo, ReceiptBillNo = set.ReceiptBill.BillNo, RowState = RowState.Insert });
                }

                repo.Update(accountSet);
            }
        }
        /// <summary>
        /// 移除通路電子帳簿
        /// </summary>
        private void RemoveChannelEAccount()
        {
            using ChannelEAccountBillRepository repo = new ChannelEAccountBillRepository(DataAccess) { User = User };

        }
        #endregion
    }
}
