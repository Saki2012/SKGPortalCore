using System;
using System.Collections.Generic;
using SKGPortalCore.Data;
using SKGPortalCore.Model;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Repository.BillData;

namespace SKGPortalCore.SeedDataInitial.BillData
{
    public class BillSeedData
    {
        /// <summary>
        /// 新增「帳單」-初始資料
        /// </summary>
        /// <param name="db"></param>
        public static void CreateBill(MessageLog Message, ApplicationDbContext DataAccess)
        {
            try
            {
                Message.Prefix = "新增「帳單」-初始資料：";
                using BillRepository repo = new BillRepository(DataAccess) { User = SystemOperator.SysOperator, Message = Message };
                List<BillSet> bills = new List<BillSet>() { new BillSet() { Bill = new BillModel() { BillNo = "0001", BillTermId = "0001", CustomerId = "33458902", CustomerCode = "912", PayerId = "0001", PayerType = Model.PayerType.Normal, ImportBatchNo = string.Empty, PayEndDate = DateTime.Parse("2019-09-01"), PayStatus = PayStatus.Unpaid, Memo1 = string.Empty, Memo2 = string.Empty }, BillDetail = new List<BillDetailModel>() { new BillDetailModel() { BillNo = "0001", BillTermRowId = 3, PayAmount = 20 }, new BillDetailModel() { BillNo = "0001", BillTermRowId = 4, PayAmount = 5 } }, BillReceiptDetail = new List<BillReceiptDetailModel>() } };
                foreach (BillSet bill in bills)
                {
                    if (null == repo.QueryData(new[] { bill.Bill.BillNo }))
                    {
                        repo.Create(bill);
                    }
                }
            }
            finally
            {
                Message.Prefix = string.Empty;
            }
        }
    }
}
