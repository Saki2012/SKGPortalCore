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
        public static void CreateBill(SysMessageLog Message, ApplicationDbContext DataAccess)
        {
            try
            {
                Message.Prefix = "新增「帳單」-初始資料：";
                using BillRepository repo = new BillRepository(DataAccess) { User = SystemOperator.SysOperator, Message = Message };
                List<BillSet> bills = new List<BillSet>()
                {
                    new BillSet() { Bill = new BillModel() { BillTermId = "001",  CustomerCode = "912", PayerId = "000001", ImportBatchNo = "BeginData", PayEndDate = DateTime.Parse("2021-09-01") }, 
                        BillDetail = new List<BillDetailModel>() { new BillDetailModel() { PayAmount = 20 }, new BillDetailModel() {  PayAmount = 5 } }, BillReceiptDetail = new List<BillReceiptDetailModel>() },
                    new BillSet() { Bill = new BillModel() { BillTermId = "010",  CustomerCode = "1024", PayerId = "000010",  ImportBatchNo = "BeginData", PayEndDate = DateTime.Parse("2021-09-01") }, 
                        BillDetail = new List<BillDetailModel>() { new BillDetailModel() {  PayAmount = 10000 }, new BillDetailModel() { PayAmount = 53 } }, BillReceiptDetail = new List<BillReceiptDetailModel>() },
                    new BillSet() { Bill = new BillModel() { BillTermId = "100",  CustomerCode = "990128", PayerId = "000100",  ImportBatchNo = "BeginData", PayEndDate = DateTime.Parse("2021-09-01") }, 
                        BillDetail = new List<BillDetailModel>() { new BillDetailModel() {  PayAmount = 20000 }, new BillDetailModel() {PayAmount = 75 } }, BillReceiptDetail = new List<BillReceiptDetailModel>() }
                };
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
