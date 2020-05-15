using System;
using System.Collections.Generic;
using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Model.User;
using SKGPortalCore.Interface.IRepository.BillData;
using SKGPortalCore.Model.BillData;

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
                using IBillRepository repo = new BillRepository(DataAccess) { User = SystemOperator.SysOperator, Message = Message };
                List<BillSet> bills = new List<BillSet>()
                {
                    new BillSet() { Bill = new BillModel() { BillTermId = "100",  CustomerCode = "992086", PayerId = "1",  ImportBatchNo = "BeginData", PayEndDate = DateTime.Now, CollectionTypeId="6V1"},
                        BillDetail = new List<BillDetailModel>() { new BillDetailModel() {  FeeName= "停車費",  PayAmount = 2000 }, new BillDetailModel() { FeeName = "管理費", PayAmount = 75 } } },
                    new BillSet() { Bill = new BillModel() { BillTermId = "101",  CustomerCode = "2143", PayerId = "1",  ImportBatchNo = "BeginData", PayEndDate = DateTime.Now, CollectionTypeId="6V1"},
                        BillDetail = new List<BillDetailModel>() { new BillDetailModel() {  FeeName= "停車費",  PayAmount = 2000 }, new BillDetailModel() { FeeName = "管理費", PayAmount = 75 } } },
                    new BillSet() { Bill = new BillModel() { BillTermId = "102",  CustomerCode = "805", PayerId = "1",  ImportBatchNo = "BeginData", PayEndDate = DateTime.Now , CollectionTypeId="6V1"},
                        BillDetail = new List<BillDetailModel>() { new BillDetailModel() {  FeeName= "停車費",  PayAmount = 2000 }, new BillDetailModel() { FeeName = "管理費", PayAmount = 75 } } },
                    new BillSet() { Bill = new BillModel() { BillTermId = "103",  CustomerCode = "993586", PayerId = "1",  ImportBatchNo = "BeginData", PayEndDate = DateTime.Now , CollectionTypeId="6V1"},
                        BillDetail = new List<BillDetailModel>() { new BillDetailModel() {  FeeName= "停車費",  PayAmount = 2000 }, new BillDetailModel() { FeeName = "管理費", PayAmount = 75 } } },

                };

                bills.ForEach(bill =>
                {
                    if (null == repo.QueryData(new[] { bill.Bill.BillNo })) repo.Create(bill);
                });
                repo.CommitData(FuncAction.Create);
            }
            finally
            {
                Message.Prefix = string.Empty;
            }
        }
    }
}
