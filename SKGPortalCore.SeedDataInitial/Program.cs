using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Controllers.BillData;
using SKGPortalCore.Controllers.MasterData;
using SKGPortalCore.Data;
using SKGPortalCore.Model;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Repository.BillData;
using SKGPortalCore.Repository.MasterData;
using System;
using System.Collections.Generic;

namespace SKGPortalCore.SeedDataInitial
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateSeedData();
        }

        public static void CreateSeedData()
        {
            DbContextOptionsBuilder<ApplicationDbContext> builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer("Server=.;Database=SKGPortalCore;Trusted_Connection=True;MultipleActiveResultSets=true");
            using ApplicationDbContext db = new ApplicationDbContext(builder.Options);
            using var transaction = db.Database.BeginTransaction();
            try
            {
                SystemOperator sys = new SystemOperator();
                if (db.Set<BackendUserModel>().Find("SysOperator") == null)
                    db.Add(sys.SysOperator);
                //資料
                CreateChannel(db, sys);
                CreateCollectionType(db, sys);
                //CreateChannelVerifyPeriod(db);
                CreateCustomer(db, sys);
                CreateBizCustomer(db, sys);
                CreatePayer(db, sys);
                CreateBillTerm(db, sys);
                //單據
                CreateBill(db, sys);
                CreateReceiptBill(db, sys);
                CreateCashFlowBill(db, sys);

                //db.SaveChanges();
                db.BulkSaveChanges();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Console.WriteLine(e);
                Console.ReadLine();
            }
        }

        #region MasterData
        /// <summary>
        /// 新增「代收通路」-初始資料
        /// </summary>
        /// <param name="db"></param>
        private static void CreateChannel(ApplicationDbContext db, SystemOperator sys)
        {
            ChannelRepository repo = new ChannelRepository(db);
            repo.User = sys.SysOperator;
            var channels = new List<ChannelSet>() { new ChannelSet() { Channel = new ChannelModel(){ ChannelId="00", ChannelName="銀行臨櫃", ChannelType= CanalisType.Bank} },
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="01", ChannelName="7-11", ChannelType= CanalisType.Market}, ChannelMap = new List<ChannelMapModel>(){ new ChannelMapModel() { ChannelId = "01", TransCode = "7111111" } } },
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="02", ChannelName="全家", ChannelType= CanalisType.Market}, ChannelMap = new List<ChannelMapModel>(){new ChannelMapModel() { ChannelId = "02", TransCode = "TFM" } } },
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="03", ChannelName="OK", ChannelType= CanalisType.Market}, ChannelMap = new List<ChannelMapModel>(){new ChannelMapModel() { ChannelId = "03", TransCode = "OKM" } } },
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="04", ChannelName="萊爾富", ChannelType= CanalisType.Market}, ChannelMap = new List<ChannelMapModel>(){new ChannelMapModel() { ChannelId = "04", TransCode = "HILIFE" } } },
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="05", ChannelName="郵局臨櫃", ChannelType= CanalisType.Post}, ChannelMap = new List<ChannelMapModel>(){ new ChannelMapModel() { ChannelId = "05", TransCode = "0587" }, new ChannelMapModel() { ChannelId = "05", TransCode = "0588" }, new ChannelMapModel() { ChannelId = "05", TransCode = "0589" }, new ChannelMapModel() { ChannelId = "05", TransCode = "058A" }, new ChannelMapModel() { ChannelId = "05", TransCode = "0215" }, new ChannelMapModel() { ChannelId = "05", TransCode = "0216" }, new ChannelMapModel() { ChannelId = "05", TransCode = "0509" }, new ChannelMapModel() { ChannelId = "05", TransCode = "050A" }, new ChannelMapModel() { ChannelId = "05", TransCode = "050C" }, new ChannelMapModel() { ChannelId = "05", TransCode = "0516" }, new ChannelMapModel() { ChannelId = "05", TransCode = "0559" }, } },
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="06", ChannelName="自動化交易(ATM)", ChannelType= CanalisType.Bank}},
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="07", ChannelName="約定扣款(主機端)", ChannelType= CanalisType.Bank}},
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="09", ChannelName="票交所", ChannelType= CanalisType.Bank}},
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="10", ChannelName="中信平台繳學費", ChannelType= CanalisType.Bank}},
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="12", ChannelName="農業金庫", ChannelType= CanalisType.Farm}, ChannelMap = new List<ChannelMapModel>(){new ChannelMapModel() { ChannelId = "12", TransCode = "AGRI" } } },
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="A0", ChannelName="企業戶自收款", ChannelType= CanalisType.Self}},
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="A1", ChannelName="郵局網路平台", ChannelType= CanalisType.Post}, ChannelMap = new List<ChannelMapModel>(){ new ChannelMapModel() { ChannelId = "A1", TransCode = "A421" },new ChannelMapModel() { ChannelId = "A1", TransCode = "057W" } } },
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="A2", ChannelName="自動化交易(匯款)", ChannelType= CanalisType.Bank}},
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="A3", ChannelName="信用卡", ChannelType= CanalisType.Credit}},
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="A4", ChannelName="ACH扣款", ChannelType= CanalisType.Credit}},
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="A5", ChannelName="約定扣款(平台端)", ChannelType= CanalisType.Credit}},
                                                    };
            foreach (var channel in channels)
            {
                if (null == repo.QueryData(new[] { channel.Channel.ChannelId }))
                    repo.Create(channel);
            }
        }
        /// <summary>
        /// 新增「代收類別」-初始資料
        /// </summary>
        /// <param name="db"></param>
        private static void CreateCollectionType(ApplicationDbContext db, SystemOperator sys)
        {
            CollectionTypeRepository repo = new CollectionTypeRepository(db)
            {
                User = sys.SysOperator
            };
            var collectionTypes = new List<CollectionTypeSet>() { new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="50084884",CollectionTypeName="郵局特戶", ChargePayType= ChargePayType.Deduction}, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() {RowId=1, CollectionTypeId= "50084884",ChannelId= "05", SRange=1,ERange=100,Fee=5 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "50084884", ChannelId = "05", SRange = 101, ERange = 1000, Fee = 10 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "50084884", ChannelId = "05", SRange = 1001, ERange = 9999999, Fee = 15 }, new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "50084884", ChannelId = "A1", SRange = 1, ERange = 100, Fee = 5 }, new CollectionTypeDetailModel() { RowId = 5, CollectionTypeId = "50084884", ChannelId = "A1", SRange = 101, ERange = 1000, Fee = 10 }, new CollectionTypeDetailModel() { RowId = 6, CollectionTypeId = "50084884", ChannelId = "A1", SRange = 1001, ERange = 9999999, Fee = 15 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="62H",CollectionTypeName="一般代收(2萬、內扣、日結)", ChargePayType= ChargePayType.Deduction }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "62H", ChannelId = "02", SRange = 1, ERange = 20000, Fee = 12 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "62H", ChannelId = "03", SRange = 1, ERange = 20000, Fee = 13 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "62H", ChannelId = "04", SRange = 1, ERange = 20000, Fee = 12 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="62I",CollectionTypeName="一般代收(4萬、內扣、日結)", ChargePayType= ChargePayType.Deduction }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "62I", ChannelId = "02", SRange = 20001, ERange = 40000, Fee = 16 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "62I", ChannelId = "03", SRange = 20001, ERange = 40000, Fee = 16 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "62I", ChannelId = "04", SRange = 20001, ERange = 40000, Fee = 16 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="62M",CollectionTypeName="一般代收(6萬、內扣、日結)", ChargePayType= ChargePayType.Deduction }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "62M", ChannelId = "02", SRange = 40001, ERange = 60000, Fee = 21 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "62M", ChannelId = "03", SRange = 40001, ERange = 60000, Fee = 21 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "62M", ChannelId = "04", SRange = 40001, ERange = 60000, Fee = 21 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="62N",CollectionTypeName="一般代收(2萬、外加、日結)", ChargePayType= ChargePayType.Increase }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "62N", ChannelId = "02", SRange = 1, ERange = 20000, Fee = 12 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "62N", ChannelId = "03", SRange = 1, ERange = 20000, Fee = 13 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "62N", ChannelId = "04", SRange = 1, ERange = 20000, Fee = 12 }  } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="62T",CollectionTypeName="一般代收(4萬、外加、日結)", ChargePayType= ChargePayType.Increase }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "62T", ChannelId = "02", SRange = 20001, ERange = 40000, Fee = 16 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "62T", ChannelId = "03", SRange = 20001, ERange = 40000, Fee = 16 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "62T", ChannelId = "04", SRange = 20001, ERange = 40000, Fee = 16 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="62U",CollectionTypeName="一般代收(6萬、外加、日結)", ChargePayType= ChargePayType.Increase }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "62U", ChannelId = "02", SRange = 40001, ERange = 60000, Fee = 21 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "62U", ChannelId = "03", SRange = 40001, ERange = 60000, Fee = 21 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "62U", ChannelId = "04", SRange = 40001, ERange = 60000, Fee = 21 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="62V",CollectionTypeName="政黨捐款", ChargePayType= ChargePayType.Deduction }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "62V", ChannelId = "02", SRange = 1, ERange = 20000, Fee = 6 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "62V", ChannelId = "03", SRange = 1, ERange = 20000, Fee = 6 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "62V", ChannelId = "04", SRange = 1, ERange = 20000, Fee = 6 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="62W",CollectionTypeName="慈善捐款", ChargePayType= ChargePayType.Deduction }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "62W", ChannelId = "02", SRange = 1, ERange = 20000, Fee = 6 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "62W", ChannelId = "03", SRange = 1, ERange = 20000, Fee = 6 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "62W", ChannelId = "04", SRange = 1, ERange = 20000, Fee = 6 }  } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="62X",CollectionTypeName="黨費", ChargePayType= ChargePayType.Deduction }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() {  new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "62X", ChannelId = "02", SRange = 1, ERange = 20000, Fee = 6 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "62X", ChannelId = "03", SRange = 1, ERange = 20000, Fee = 6 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "62X", ChannelId = "04", SRange = 1, ERange = 20000, Fee = 6 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="62Y",CollectionTypeName="有線電視", ChargePayType= ChargePayType.Deduction }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() {  new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "62Y", ChannelId = "02", SRange = 1, ERange = 20000, Fee = 6 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "62Y", ChannelId = "03", SRange = 1, ERange = 20000, Fee = 6 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "62Y", ChannelId = "04", SRange = 1, ERange = 20000, Fee = 6 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="62Z",CollectionTypeName="瓦斯費", ChargePayType= ChargePayType.Deduction }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() {  new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "62Z", ChannelId = "02", SRange = 1, ERange = 20000, Fee = 6 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "62Z", ChannelId = "03", SRange = 1, ERange = 20000, Fee = 6 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "62Z", ChannelId = "04", SRange = 1, ERange = 20000, Fee = 6 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6RK",CollectionTypeName="超商代收-學雜費", ChargePayType= ChargePayType.Deduction }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6RK", ChannelId = "01", SRange = 40001, ERange = 60000, Fee = 18 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6RK", ChannelId = "02", SRange = 40001, ERange = 60000, Fee = 18 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6RK", ChannelId = "03", SRange = 40001, ERange = 60000, Fee = 18 }, new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6RK", ChannelId = "04", SRange = 40001, ERange = 60000, Fee = 18 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6RL",CollectionTypeName="超商代收-學雜費", ChargePayType= ChargePayType.Increase }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6RL", ChannelId = "01", SRange = 40001, ERange = 60000, Fee = 18 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6RL", ChannelId = "02", SRange = 40001, ERange = 60000, Fee = 18 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6RL", ChannelId = "03", SRange = 40001, ERange = 60000, Fee = 18 }, new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6RL", ChannelId = "04", SRange = 40001, ERange = 60000, Fee = 18 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6RM",CollectionTypeName="超商代收-一般", ChargePayType= ChargePayType.Deduction }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6RM", ChannelId = "01", SRange = 40001, ERange = 60000, Fee = 13 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6RM", ChannelId = "02", SRange = 40001, ERange = 60000, Fee = 12 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6RM", ChannelId = "03", SRange = 40001, ERange = 60000, Fee = 12 }, new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6RM", ChannelId = "04", SRange = 40001, ERange = 60000, Fee = 13 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6RN",CollectionTypeName="超商代收-一般", ChargePayType= ChargePayType.Increase }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6RN", ChannelId = "01", SRange = 40001, ERange = 60000, Fee = 22 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6RN", ChannelId = "02", SRange = 40001, ERange = 60000, Fee = 22 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6RN", ChannelId = "03", SRange = 40001, ERange = 60000, Fee = 22 }, new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6RN", ChannelId = "04", SRange = 40001, ERange = 60000, Fee = 22 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6V0",CollectionTypeName="超商代收-學雜費", ChargePayType= ChargePayType.Increase }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6V0", ChannelId = "01", SRange = 20001, ERange = 40000, Fee = 15 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6V0", ChannelId = "02", SRange = 20001, ERange = 40000, Fee = 15 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6V0", ChannelId = "03", SRange = 20001, ERange = 40000, Fee = 15 }, new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6V0", ChannelId = "04", SRange = 20001, ERange = 40000, Fee = 15 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6V1",CollectionTypeName="超商代收-一般", ChargePayType= ChargePayType.Deduction }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6V1", ChannelId = "01", SRange = 1, ERange = 20000, Fee = 10 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6V1", ChannelId = "02", SRange = 1, ERange = 20000, Fee = 10 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6V1", ChannelId = "03", SRange = 1, ERange = 20000, Fee = 10 }, new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6V1", ChannelId = "04", SRange = 1, ERange = 20000, Fee = 10 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6V2",CollectionTypeName="超商代收-一般", ChargePayType= ChargePayType.Deduction }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6V2", ChannelId = "01", SRange = 20001, ERange = 40000, Fee = 15 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6V2", ChannelId = "02", SRange = 20001, ERange = 40000, Fee = 15 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6V2", ChannelId = "03", SRange = 20001, ERange = 40000, Fee = 15 }, new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6V2", ChannelId = "04", SRange = 20001, ERange = 40000, Fee = 15 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6V3",CollectionTypeName="超商代收-學雜費", ChargePayType= ChargePayType.Deduction }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6V3", ChannelId = "01", SRange = 1, ERange = 20000, Fee = 6 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6V3", ChannelId = "02", SRange = 1, ERange = 20000, Fee = 6 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6V3", ChannelId = "03", SRange = 1, ERange = 20000, Fee = 6 }, new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6V3", ChannelId = "04", SRange = 1, ERange = 20000, Fee = 6 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6V4",CollectionTypeName="超商代收-學雜費", ChargePayType= ChargePayType.Deduction }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6V4", ChannelId = "01", SRange = 1, ERange = 20000, Fee = 10 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6V4", ChannelId = "02", SRange = 1, ERange = 20000, Fee = 10 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6V4", ChannelId = "03", SRange = 1, ERange = 20000, Fee = 10 }, new CollectionTypeDetailModel() { CollectionTypeId = "6V4", ChannelId = "04", SRange = 1, ERange = 20000, Fee = 10 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6V5",CollectionTypeName="超商代收-學雜費", ChargePayType= ChargePayType.Deduction }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6V5", ChannelId = "01", SRange = 20001, ERange = 40000, Fee = 15 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6V5", ChannelId = "02", SRange = 20001, ERange = 40000, Fee = 15 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6V5", ChannelId = "03", SRange = 20001, ERange = 40000, Fee = 15 }, new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6V5", ChannelId = "04", SRange = 20001, ERange = 40000, Fee = 15 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6V6",CollectionTypeName="超商代收-一般", ChargePayType= ChargePayType.Increase }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6V6", ChannelId = "01", SRange = 1, ERange = 20000, Fee = 10 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6V6", ChannelId = "02", SRange = 1, ERange = 20000, Fee = 10 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6V6", ChannelId = "03", SRange = 1, ERange = 20000, Fee = 10 }, new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6V6", ChannelId = "04", SRange = 1, ERange = 20000, Fee = 10 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6V7",CollectionTypeName="超商代收-一般", ChargePayType= ChargePayType.Increase }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6V7", ChannelId = "01", SRange = 20001, ERange = 40000, Fee = 15 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6V7", ChannelId = "02", SRange = 20001, ERange = 40000, Fee = 15 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6V7", ChannelId = "03", SRange = 20001, ERange = 40000, Fee = 15 }, new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6V7", ChannelId = "04", SRange = 20001, ERange = 40000, Fee = 15 }} },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6V8",CollectionTypeName="超商代收-學雜費", ChargePayType= ChargePayType.Increase }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6V8", ChannelId = "01", SRange = 1, ERange = 20000, Fee = 6 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6V8", ChannelId = "02", SRange = 1, ERange = 20000, Fee = 6 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6V8", ChannelId = "03", SRange = 1, ERange = 20000, Fee = 6 }, new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6V8", ChannelId = "04", SRange = 1, ERange = 20000, Fee = 6 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6V9",CollectionTypeName="超商代收-學雜費", ChargePayType= ChargePayType.Increase}, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6V9", ChannelId = "01", SRange = 1, ERange = 20000, Fee = 10 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6V9", ChannelId = "02", SRange = 1, ERange = 20000, Fee = 10 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6V9", ChannelId = "03", SRange = 1, ERange = 20000, Fee = 10 }, new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6V9", ChannelId = "04", SRange = 1, ERange = 20000, Fee = 10 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="Bank999",CollectionTypeName="銀行通路", ChargePayType= ChargePayType.Increase }, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "Bank999", ChannelId = "00", SRange = 1, ERange = 9999999, Fee = 10 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "Bank999", ChannelId = "A2", SRange = 1, ERange = 9999999, Fee = 10 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "Bank999", ChannelId = "06", SRange = 1, ERange = 9999999, Fee = 10 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="FAR",CollectionTypeName="農金通路", ChargePayType= ChargePayType.Deduction}, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "FAR", ChannelId = "12", SRange = 1, ERange = 9999999, Fee = 10 } } },
                                                                  new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="I0O",CollectionTypeName="超商代收-保險費", ChargePayType= ChargePayType.Deduction}, CollectionTypeDetail = new List<CollectionTypeDetailModel>() { new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "I0O", ChannelId = "01", SRange = 1, ERange = 50000, Fee = 20 }, new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "I0O", ChannelId = "02", SRange = 1, ERange = 50000, Fee = 20 }, new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "I0O", ChannelId = "03", SRange = 1, ERange = 50000, Fee = 20 }, new CollectionTypeDetailModel() { CollectionTypeId = "I0O", ChannelId = "04", SRange = 1, ERange = 50000, Fee = 20 } } },
                                                                };
            foreach (var collectionType in collectionTypes)
            {
                if (null == repo.QueryData(new[] { collectionType.CollectionType.CollectionTypeId }))
                    repo.Create(collectionType);
            }
        }
        /// <summary>
        /// 新增「客戶基本資料」-初始資料
        /// </summary>
        /// <param name="db"></param>
        private static void CreateCustomer(ApplicationDbContext db, SystemOperator sys)
        {
            CustomerRepository repo = new CustomerRepository(db);
            repo.User = sys.SysOperator;
            var customers = new List<CustomerSet>() { new CustomerSet() { Customer = new CustomerModel() { CustomerId = "80425514", CustomerName = "測試客戶A", Address = "桃園市", Tel = "03-43123456", Fax = "03-4123123", ZipCode = "320", ZipUnit = "320-05", ZipNum = "05", BillTermLen = 3,PayerNoLen=6,DeptId="",PayerAuthorize=false,IsSysCust=false } },
                                                      //new CustomerSet(){ },
                                                    };
            foreach (var customer in customers)
            {
                if (null == repo.QueryData(new[] { customer.Customer.CustomerId }))
                    repo.Create(customer);
            }
        }
        /// <summary>
        /// 新增「商戶資料」-初始資料
        /// </summary>
        /// <param name="db"></param>
        private static void CreateBizCustomer(ApplicationDbContext db, SystemOperator sys)
        {
            BizCustomerRepository repo = new BizCustomerRepository(db);
            repo.User = sys.SysOperator;
            var customers = new List<BizCustomerSet>() { new BizCustomerSet() { BizCustomer = new BizCustomerModel() { CustomerId = "80425514", CustomerCode = "990521", AccountDeptId = "", RealAccount = "0505100015307", VirtualAccountLen = 13, VirtualAccount1 = VirtualAccount1.Empty, VirtualAccount2 = VirtualAccount2.Empty, VirtualAccount3 = VirtualAccount3.NoverifyCode, ChannelIds = "00,01,02,03,04,05,06,A3,A1", CollectionTypeIds = "6V5,6V6", HiTrustFlag = HiTrustFlag.NoApplication, EntrustCustId = "8551414", AccountStatus = AccountStatus.Enable,Source="" }, BizCustFeeDetail=new List<BizCustFeeDetailModel>(){ new BizCustFeeDetailModel() {  CustomerCode= "990521", ChannelType= CanalisType.Bank , FeeType= FeeType.ClearFee, Fee=10, Percent=0} } },
                                                         //new BizCustomerSet(){ },
                                                       };
            foreach (var customer in customers)
            {
                if (null == repo.QueryData(new[] { customer.BizCustomer.CustomerCode }))
                    repo.Create(customer);
            }
        }
        /// <summary>
        /// 新增「繳款人」-初始資料
        /// </summary>
        /// <param name="db"></param>
        private static void CreatePayer(ApplicationDbContext db, SystemOperator sys)
        {
            PayerRepository repo = new PayerRepository(db);
            repo.User = sys.SysOperator;
            var payers = new List<PayerSet>() {new PayerSet(){ Payer=new PayerModel(){CustomerId="80425514", PayerId="0001", PayerName="測試繳款人1", PayerType= Model.PayerType.Normal, PayerNo="1007", IDCard="F1233151847",Tel="0921447116",Address="平鎮",Memo="",CardNum="4478-1181-5547-9631", CardValidateMonth=12,CardValidateYear=23,CVV="225" } },
                                                       };
            foreach (var payer in payers)
            {
                if (null == repo.QueryData(new[] { payer.Payer.CustomerId, payer.Payer.PayerId }))
                    repo.Create(payer);
            }
        }
        /// <summary>
        /// 新增「期別」-初始資料
        /// </summary>
        /// <param name="db"></param>
        private static void CreateBillTerm(ApplicationDbContext db, SystemOperator sys)
        {
            BillTermRepository repo = new BillTermRepository(db);
            repo.User = sys.SysOperator;
            var billTerms = new List<BillTermSet>() {new BillTermSet(){ BillTerm=new BillTermModel(){CustomerCode="990521", BillTermId="0001", BillTermName="測試期別1",BillTermNo="01",  }, BillTermDetail=new List<BillTermDetailModel>(){ new BillTermDetailModel() {CustomerCode="990521", BillTermId = "0001", FeeName = "費用01", IsDeduction = false }, new BillTermDetailModel() { CustomerCode = "990521", BillTermId = "0001", FeeName = "費用02", IsDeduction = true } } }
                                                       };
            foreach (var billTerm in billTerms)
            {
                if (null == repo.QueryData(new[] { billTerm.BillTerm.CustomerCode, billTerm.BillTerm.BillTermId }))
                    repo.Create(billTerm);
            }
        }
        #endregion

        #region BillData
        /// <summary>
        /// 新增「帳單」-初始資料
        /// </summary>
        /// <param name="db"></param>
        private static void CreateBill(ApplicationDbContext db, SystemOperator sys)
        {
            BillRepository repo = new BillRepository(db);
            repo.User = sys.SysOperator;
            var bills = new List<BillSet>() { new BillSet() { Bill = new BillModel() { BillNo = "0001", BillTermId = "0001", CustomerId = "80425514", CustomerCode = "990521", PayerId = "0001", PayerType = Model.PayerType.Normal, ImportBatchNo = string.Empty, PayEndDate = DateTime.Parse("2019-09-01"), PayStatus = PayStatus.Unpaid, Memo1 = string.Empty, Memo2 = string.Empty }, BillDetail = new List<BillDetailModel>() { new BillDetailModel() { BillNo = "0001", BillTermRowId = 3, PayAmount = 20 }, new BillDetailModel() { BillNo = "0001", BillTermRowId = 4, PayAmount = 5 } }, BillReceiptDetail = new List<BillReceiptDetailModel>() } };
            foreach (var bill in bills)
            {
                if (null == repo.QueryData(new[] { bill.Bill.BillNo }))
                    repo.Create(bill);
            }
        }
        /// <summary>
        /// 新增「收款單(自收款)」-初始資料
        /// </summary>
        /// <param name="db"></param>
        private static void CreateReceiptBill(ApplicationDbContext db, SystemOperator sys)
        {


        }
        /// <summary>
        /// 新增「金流帳簿」-初始資料
        /// </summary>
        /// <param name="db"></param>
        private static void CreateCashFlowBill(ApplicationDbContext db, SystemOperator sys)
        { }
        #endregion
    }
}
