using System;
using System.Collections.Generic;
using SKGPortalCore.Data;
using SKGPortalCore.Model;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Repository.BillData;

namespace SKGPortalCore.SeedDataInitial.BillData
{
    public static class BillSeedData
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
                    new BillSet() { Bill = new BillModel() { BillTermId = "100",  CustomerCode = "992086", PayerId = "000100",  ImportBatchNo = "BeginData", PayEndDate = DateTime.Now},
                        BillDetail = new List<BillDetailModel>() { new BillDetailModel() {  FeeName= "停車費",  PayAmount = 20000 }, new BillDetailModel() { FeeName = "管理費", PayAmount = 75 } } },
                    new BillSet() { Bill = new BillModel() { BillTermId = "101",  CustomerCode = "2143", PayerId = "000100",  ImportBatchNo = "BeginData", PayEndDate = DateTime.Now},
                        BillDetail = new List<BillDetailModel>() { new BillDetailModel() {  FeeName= "停車費",  PayAmount = 20000 }, new BillDetailModel() { FeeName = "管理費", PayAmount = 75 } } },
                    new BillSet() { Bill = new BillModel() { BillTermId = "102",  CustomerCode = "805", PayerId = "000100",  ImportBatchNo = "BeginData", PayEndDate = DateTime.Now },
                        BillDetail = new List<BillDetailModel>() { new BillDetailModel() {  FeeName= "停車費",  PayAmount = 20000 }, new BillDetailModel() { FeeName = "管理費", PayAmount = 75 } } },
                    new BillSet() { Bill = new BillModel() { BillTermId = "103",  CustomerCode = "993586", PayerId = "000100",  ImportBatchNo = "BeginData", PayEndDate = DateTime.Now },
                        BillDetail = new List<BillDetailModel>() { new BillDetailModel() {  FeeName= "停車費",  PayAmount = 20000 }, new BillDetailModel() { FeeName = "管理費", PayAmount = 75 } } },

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
