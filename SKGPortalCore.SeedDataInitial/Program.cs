using System;
using System.Collections.Generic;
using System.IO;
using SKGPortalCore.Data;
using SKGPortalCore.Model;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Repository.BillData;
using SKGPortalCore.Repository.MasterData;

namespace SKGPortalCore.SeedDataInitial
{
    class Program
    {
        static readonly MessageLog Message = new MessageLog(SystemOperator.SysOperator, logFileName: "SKGPortalCore.SeedDataInitial");

        static void Main()
        {
            CreateSeedData();
            CreateImportData();
        }

        public static void CreateSeedData()
        {
            try
            {
                using ApplicationDbContext dataAccess = LibDataAccess.CreateDataAccess();
                if (dataAccess.Set<BackendUserModel>().Find("SysOperator") == null)
                    dataAccess.Add(SystemOperator.SysOperator);
                //資料
                CreateCustUser(dataAccess);
                CreateRole(dataAccess);
                CreateChannel(dataAccess);
                CreateCollectionType(dataAccess);
                CreateCustomer(dataAccess);
                CreateBizCustomer(dataAccess);
                CreatePayer(dataAccess);
                CreateBillTerm(dataAccess);
                //單據
                CreateBill(dataAccess);
                CreateReceiptBill(dataAccess);

                dataAccess.BulkSaveChanges();
            }
            catch (Exception e)
            {
                Message.AddExceptionError(e);
            }
            Message.WriteLogTxt();
        }
        public static void CreateImportData()
        {
            try
            {
                if (!Directory.Exists($@".\ImportData")) { Directory.CreateDirectory($@".\ImportData"); }
                ACCFTTData();
                ReceiptInfoBankData();
                ReceiptInfoPostData();
                ReceiptInfoMarketData();
                ReceiptInfoMarketSPIData();
                ReceiptInfoFarmData();
                RemitInfoData();
            }
            catch (Exception e)
            {
                Message.AddExceptionError(e);
            }
            Message.WriteLogTxt();
        }
        #region MasterData
        /// <summary>
        /// 新增「前台用戶使用者」-初始資料
        /// </summary>
        /// <param name="dataAccess"></param>
        private static void CreateCustUser(ApplicationDbContext dataAccess)
        {
            try
            {
                Message.Prefix = "新增「前台用戶使用者」-初始資料：";
                Message.AddErrorMessage(MessageCode.Code1008, "Test");
                using CustUserRepository repo = new CustUserRepository(dataAccess) { User = SystemOperator.SysOperator, Message = Message }; ;
                var users = new List<CustUserSet>() { new CustUserSet() { User = new CustUserModel() { KeyId = "80425514,admin", CustomerId = "80425514", UserId = "admin", UserName = "管理員", Pasuwado = "123456", AccountStatus = AccountStatus.Enable }, UserRoles = new List<CustUserRoleModel>() { new CustUserRoleModel() { KeyId = "80425514,admin", RoleId = "admin" } } } };

                foreach (var user in users)
                {
                    if (null == repo.QueryData(new[] { user.User.KeyId }))
                        repo.Create(user);
                }
            }
            finally
            {
                Message.Prefix = string.Empty;
            }
        }
        /// <summary>
        /// 新增「角色權限」-初始資料
        /// </summary>
        /// <param name="dataAccess"></param>
        private static void CreateRole(ApplicationDbContext dataAccess)
        {
            try
            {
                Message.Prefix = "新增「角色權限」-初始資料：";
                using RoleRepository repo = new RoleRepository(dataAccess) { User = SystemOperator.SysOperator, Message = Message }; ;
                var roles = new List<RoleSet>() { new RoleSet() { Role = new RoleModel() { RoleId = "admin", RoleName = "管理員", EndType = EndType.Frontend, IsAdmin = true }, RolePermission = new List<RolePermissionModel>() { new RolePermissionModel() { RoleId = "admin", RowId = 1, FuncName = "Bill", FuncAction = 255 } } } };
                foreach (var role in roles)
                {
                    if (null == repo.QueryData(new[] { role.Role.RoleId }))
                        repo.Create(role);
                }
            }
            finally
            {
                Message.Prefix = string.Empty;
            }
        }
        /// <summary>
        /// 新增「代收通路」-初始資料
        /// </summary>
        /// <param name="db"></param>
        private static void CreateChannel(ApplicationDbContext dataAccess)
        {
            try
            {
                Message.Prefix = "新增「代收通路」-初始資料：";
                using ChannelRepository repo = new ChannelRepository(dataAccess) { User = SystemOperator.SysOperator, Message = Message };
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
            finally
            {
                Message.Prefix = string.Empty;
            }
        }
        /// <summary>
        /// 新增「代收類別」-初始資料
        /// </summary>
        /// <param name="db"></param>
        private static void CreateCollectionType(ApplicationDbContext dataAccess)
        {
            try
            {
                Message.Prefix = "新增「代收類別」-初始資料：";
                using CollectionTypeRepository repo = new CollectionTypeRepository(dataAccess) { User = SystemOperator.SysOperator, Message = Message };
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
            finally
            {
                Message.Prefix = string.Empty;
            }
        }
        /// <summary>
        /// 新增「客戶基本資料」-初始資料
        /// </summary>
        /// <param name="db"></param>
        private static void CreateCustomer(ApplicationDbContext dataAccess)
        {
            try
            {
                Message.Prefix = "新增「客戶基本資料」-初始資料：";
                using CustomerRepository repo = new CustomerRepository(dataAccess) { User = SystemOperator.SysOperator, Message = Message };
                var customers = new List<CustomerSet>() { new CustomerSet() { Customer = new CustomerModel() { CustomerId = "80425514", CustomerName = "測試客戶A", Address = "桃園市", Tel = "03-43123456", Fax = "03-4123123", ZipCode = "320", ZipUnit = "320-05", ZipNum = "05", BillTermLen = 3,PayerNoLen=6,DeptId="",PayerAuthorize=false,IsSysCust=false } },
                                                      //new CustomerSet(){ },
                                                    };
                foreach (var customer in customers)
                {
                    if (null == repo.QueryData(new[] { customer.Customer.CustomerId }))
                        repo.Create(customer);
                }
            }
            finally
            {
                Message.Prefix = string.Empty;
            }
        }
        /// <summary>
        /// 新增「商戶資料」-初始資料
        /// </summary>
        /// <param name="db"></param>
        private static void CreateBizCustomer(ApplicationDbContext dataAccess)
        {
            try
            {
                Message.Prefix = "新增「商戶資料」-初始資料：";
                using BizCustomerRepository repo = new BizCustomerRepository(dataAccess) { User = SystemOperator.SysOperator, Message = Message };
                var customers = new List<BizCustomerSet>() { new BizCustomerSet() { BizCustomer = new BizCustomerModel() { CustomerId = "80425514", CustomerCode = "990521", AccountDeptId = "", RealAccount = "0505100015307", VirtualAccountLen = 13, VirtualAccount1 = VirtualAccount1.Empty, VirtualAccount2 = VirtualAccount2.Empty, VirtualAccount3 = VirtualAccount3.NoverifyCode, ChannelIds = "00,01,02,03,04,05,06,A3,A1", CollectionTypeIds = "6V5,6V6", HiTrustFlag = HiTrustFlag.NoApplication, EntrustCustId = "8551414", AccountStatus = AccountStatus.Enable,Source="" }, BizCustomerFeeDetail=new List<BizCustomerFeeDetailModel >(){ new BizCustomerFeeDetailModel () {  CustomerCode= "990521", ChannelType= CanalisType.Bank , FeeType= FeeType.ClearFee, Fee=10, Percent=0} } },
                                                         //new BizCustomerSet(){ },
                                                       };
                foreach (var customer in customers)
                {
                    if (null == repo.QueryData(new[] { customer.BizCustomer.CustomerCode }))
                        repo.Create(customer);
                }
            }
            finally
            {
                Message.Prefix = string.Empty;
            }
        }
        /// <summary>
        /// 新增「繳款人」-初始資料
        /// </summary>
        /// <param name="db"></param>
        private static void CreatePayer(ApplicationDbContext dataAccess)
        {
            try
            {
                Message.Prefix = "新增「繳款人」-初始資料：";
                using PayerRepository repo = new PayerRepository(dataAccess) { User = SystemOperator.SysOperator, Message = Message };
                var payers = new List<PayerSet>() {new PayerSet(){ Payer=new PayerModel(){CustomerId="80425514", PayerId="0001", PayerName="測試繳款人1", PayerType= Model.PayerType.Normal, PayerNo="1007", IDCard="F1233151847",Tel="0921447116",Address="平鎮",Memo="",CardNum="4478-1181-5547-9631", CardValidateMonth=12,CardValidateYear=23,CVV="225" } },
                                                       };
                foreach (var payer in payers)
                {
                    if (null == repo.QueryData(new[] { payer.Payer.CustomerId, payer.Payer.PayerId }))
                        repo.Create(payer);
                }
            }
            finally
            {
                Message.Prefix = string.Empty;
            }
        }
        /// <summary>
        /// 新增「期別」-初始資料
        /// </summary>
        /// <param name="db"></param>
        private static void CreateBillTerm(ApplicationDbContext dataAccess)
        {
            try
            {
                Message.Prefix = "新增「期別」-初始資料：";
                using BillTermRepository repo = new BillTermRepository(dataAccess) { User = SystemOperator.SysOperator, Message = Message };
                var billTerms = new List<BillTermSet>() {new BillTermSet(){ BillTerm=new BillTermModel(){CustomerCode="990521", BillTermId="0001", BillTermName="測試期別1",BillTermNo="01",  }, BillTermDetail=new List<BillTermDetailModel>(){ new BillTermDetailModel() {CustomerCode="990521", BillTermId = "0001", FeeName = "費用01", IsDeduction = false }, new BillTermDetailModel() { CustomerCode = "990521", BillTermId = "0001", FeeName = "費用02", IsDeduction = true } } }
                                                       };
                foreach (var billTerm in billTerms)
                {
                    if (null == repo.QueryData(new[] { billTerm.BillTerm.CustomerCode, billTerm.BillTerm.BillTermId }))
                        repo.Create(billTerm);
                }
            }
            finally
            {
                Message.Prefix = string.Empty;
            }
        }
        #endregion

        #region BillData
        /// <summary>
        /// 新增「帳單」-初始資料
        /// </summary>
        /// <param name="db"></param>
        private static void CreateBill(ApplicationDbContext dataAccess)
        {
            try
            {
                Message.Prefix = "新增「帳單」-初始資料：";
                using BillRepository repo = new BillRepository(dataAccess) { User = SystemOperator.SysOperator, Message = Message };
                var bills = new List<BillSet>() { new BillSet() { Bill = new BillModel() { BillNo = "0001", BillTermId = "0001", CustomerId = "80425514", CustomerCode = "990521", PayerId = "0001", PayerType = Model.PayerType.Normal, ImportBatchNo = string.Empty, PayEndDate = DateTime.Parse("2019-09-01"), PayStatus = PayStatus.Unpaid, Memo1 = string.Empty, Memo2 = string.Empty }, BillDetail = new List<BillDetailModel>() { new BillDetailModel() { BillNo = "0001", BillTermRowId = 3, PayAmount = 20 }, new BillDetailModel() { BillNo = "0001", BillTermRowId = 4, PayAmount = 5 } }, BillReceiptDetail = new List<BillReceiptDetailModel>() } };
                foreach (var bill in bills)
                {
                    if (null == repo.QueryData(new[] { bill.Bill.BillNo }))
                        repo.Create(bill);
                }
            }
            finally
            {
                Message.Prefix = string.Empty;
            }
        }
        /// <summary>
        /// 新增「收款單(自收款)」-初始資料
        /// </summary>
        /// <param name="db"></param>
        private static void CreateReceiptBill(ApplicationDbContext dataAccess)
        {
            using BillRepository repo = new BillRepository(dataAccess) { User = SystemOperator.SysOperator, Message = Message };

        }
        #endregion

        #region ImportData
        /// <summary>
        /// 服務申請書
        /// </summary>
        private static void ACCFTTData()
        {
            List<ACCFTT> accftts = new List<ACCFTT>() {
                //每筆總手續費-有分潤
                new ACCFTT() { KEYNO="912", ACCIDNO="7745251524762", CUSTNAME="每筆總手續費-有分潤", APPBECODE="0499", BRCODE="0499", IDCODE="03458902", APPLYDATE=DateTime.Now.ToString("yyyyMMdd"), CHGDATE=DateTime.Now.ToString("yyyyMMdd"), APPLYSTAT="0", CHKNUMFLAG="2", CHKAMTFLAG="N", DUETERM="0", CHANNEL="4", FEE="0", RSTORE1="1", RSTORE2="1", RSTORE3="1", RSTORE4="1", RECVITEM1="6V1", RECVITEM2="6V2", RECVITEM3="", RECVITEM4="", RECVITEM5="", ACTFEE="0", MARTFEE1="00", MARTFEE2="00", MARTFEE3="00", POSTFLAG="1", ACTFEEPT="0", POSTFEE="0", HIFLAG="0", HIFARE="000", NETDATE="00000000", AUTOFLAG="0", EBFLAG="0", EBDATE="00000000", EBFEEFLAG="1", EBFEE="0", EBACTTYPE="2", CHKDUPPAY="0", CUSTID="1234567", FUNC="0", MAFARE="0", NOFARE="0", CTBCFLAG="0", SHAREBNFTFLG="1", SHAREBEFTPERCENT="20", ACTFEEBEFT="50", ACTFEEMART="15", SHAREACTFLG="1", ACTPERCENT="50", CLEARFEEMART1="0", CLEARFEEMART2="0", CLEARFEEMART3="0", CLEARFEEMART4="0", CLEARFEEMART5="0", PAYKINDPOST="0", ACTFEEPOST="15", SHAREPOSTFLG="1", POSTPERCENT="50", AGRIFLAG="1", AGRIFEE="25", FILLER=""},
                new ACCFTT() { KEYNO="", ACCIDNO="", CUSTNAME="", APPBECODE="", BRCODE="", IDCODE="", APPLYDATE="", CHGDATE="", APPLYSTAT="", CHKNUMFLAG="", CHKAMTFLAG="", DUETERM="", CHANNEL="", FEE="", RSTORE1="", RSTORE2="", RSTORE3="", RSTORE4="", RECVITEM1="", RECVITEM2="", RECVITEM3="", RECVITEM4="", RECVITEM5="", ACTFEE="", MARTFEE1="", MARTFEE2="", MARTFEE3="", POSTFLAG="", ACTFEEPT="", POSTFEE="", HIFLAG="", HIFARE="", NETDATE="", AUTOFLAG="", EBFLAG="", EBDATE="", EBFEEFLAG="", EBFEE="", EBACTTYPE="", CHKDUPPAY="", CUSTID="", FUNC="", MAFARE="", NOFARE="", CTBCFLAG="", SHAREBNFTFLG="", SHAREBEFTPERCENT="", ACTFEEBEFT="", ACTFEEMART="", SHAREACTFLG="", ACTPERCENT="", CLEARFEEMART1="", CLEARFEEMART2="", CLEARFEEMART3="", CLEARFEEMART4="", CLEARFEEMART5="", PAYKINDPOST="", ACTFEEPOST="", SHAREPOSTFLG="", POSTPERCENT="", AGRIFLAG="", AGRIFEE="", FILLER=""},
                new ACCFTT() { KEYNO="", ACCIDNO="", CUSTNAME="", APPBECODE="", BRCODE="", IDCODE="", APPLYDATE="", CHGDATE="", APPLYSTAT="", CHKNUMFLAG="", CHKAMTFLAG="", DUETERM="", CHANNEL="", FEE="", RSTORE1="", RSTORE2="", RSTORE3="", RSTORE4="", RECVITEM1="", RECVITEM2="", RECVITEM3="", RECVITEM4="", RECVITEM5="", ACTFEE="", MARTFEE1="", MARTFEE2="", MARTFEE3="", POSTFLAG="", ACTFEEPT="", POSTFEE="", HIFLAG="", HIFARE="", NETDATE="", AUTOFLAG="", EBFLAG="", EBDATE="", EBFEEFLAG="", EBFEE="", EBACTTYPE="", CHKDUPPAY="", CUSTID="", FUNC="", MAFARE="", NOFARE="", CTBCFLAG="", SHAREBNFTFLG="", SHAREBEFTPERCENT="", ACTFEEBEFT="", ACTFEEMART="", SHAREACTFLG="", ACTPERCENT="", CLEARFEEMART1="", CLEARFEEMART2="", CLEARFEEMART3="", CLEARFEEMART4="", CLEARFEEMART5="", PAYKINDPOST="", ACTFEEPOST="", SHAREPOSTFLG="", POSTPERCENT="", AGRIFLAG="", AGRIFEE="", FILLER=""},
            };
            bool err = false;
            accftts.ForEach(p => { if (p.Source != new ACCFTT() { Source = p.Source }.Source) { err = true; return; } });
            if (err) { Message.AddErrorMessage(MessageCode.Code0000, "服務申請書Source拆分組合異常"); return; }
            using StreamWriter sw = new StreamWriter($@"D:\ibankRoot\Ftp_SKGPortalCore\ACCFTT\ACCFTT.{DateTime.Now.ToString("yyyyMMdd")}", false);
            accftts.ForEach(p => sw.WriteLine(p.Source));
            sw.Close();
        }
        /// <summary>
        /// 資訊流-銀行
        /// </summary>
        private static void ReceiptInfoBankData()
        {
            List<ReceiptInfoBillBankModel> banks = new List<ReceiptInfoBillBankModel>() {
                new ReceiptInfoBillBankModel() { RealAccount="",  TradeDate="",  TradeTime="",  CompareCode="",  PN="",  Amount="",  Summary="",  Branch="",  TradeChannel="",  Channel="",  ChangeDate="",  BizDate="",  Serial="",  CustomerCode="",  Fee="",  Empty="" },
                new ReceiptInfoBillBankModel() { RealAccount="",  TradeDate="",  TradeTime="",  CompareCode="",  PN="",  Amount="",  Summary="",  Branch="",  TradeChannel="",  Channel="",  ChangeDate="",  BizDate="",  Serial="",  CustomerCode="",  Fee="",  Empty="" },
                new ReceiptInfoBillBankModel() { RealAccount="",  TradeDate="",  TradeTime="",  CompareCode="",  PN="",  Amount="",  Summary="",  Branch="",  TradeChannel="",  Channel="",  ChangeDate="",  BizDate="",  Serial="",  CustomerCode="",  Fee="",  Empty="" },
            };
            bool err = false;
            banks.ForEach(p => { if (p.Source != new ReceiptInfoBillBankModel() { Source = p.Source }.Source) { err = true; return; } });
            if (err) { Message.AddErrorMessage(MessageCode.Code0000, "資訊流-銀行Source拆分組合異常"); return; }
            using StreamWriter sw = new StreamWriter($@"D:\ibankRoot\Ftp_SKGPortalCore\TransactionListDaily\SKG_BANK.{DateTime.Now.ToString("yyyyMMdd")}", false);
            banks.ForEach(p => sw.WriteLine(p.Source));
            sw.Close();
        }
        /// <summary>
        /// 資訊流-郵局
        /// </summary>
        private static void ReceiptInfoPostData()
        {
            List<ReceiptInfoBillPostModel> posts = new List<ReceiptInfoBillPostModel>() {
                new ReceiptInfoBillPostModel() { CollectionType="", TradeDate="", Branch="", Channel="", TradeSer="", PN="", Amount="", CompareCode="", Empty=""},
                new ReceiptInfoBillPostModel() { CollectionType="", TradeDate="", Branch="", Channel="", TradeSer="", PN="", Amount="", CompareCode="", Empty="" },
                new ReceiptInfoBillPostModel() { CollectionType="", TradeDate="", Branch="", Channel="", TradeSer="", PN="", Amount="", CompareCode="", Empty="" },
            };
            bool err = false;
            posts.ForEach(p => { if (p.Source != new ReceiptInfoBillPostModel() { Source = p.Source }.Source) { err = true; return; } });
            if (err) { Message.AddErrorMessage(MessageCode.Code0000, "資訊流-郵局Source拆分組合異常"); return; }
            using StreamWriter sw = new StreamWriter($@"D:\ibankRoot\Ftp_SKGPortalCore\TransactionListDaily\SKG_POST.{DateTime.Now.ToString("yyyyMMdd")}", false);
            posts.ForEach(p => sw.WriteLine(p.Source));
            sw.Close();
        }
        /// <summary>
        /// 資訊流-超商
        /// </summary>
        private static void ReceiptInfoMarketData()
        {
            List<ReceiptInfoBillMarketModel> marts = new List<ReceiptInfoBillMarketModel>() {
                new ReceiptInfoBillMarketModel() { Idx = "", CollectionType = "", Channel = "", Store = "", TransAccount = "", TransType = "", PayStatus = "", AccountingDay = "", PayDate = "", Barcode1 = "", Barcode2 = "", Barcode3 = "", Empty="" },
                new ReceiptInfoBillMarketModel() { Idx = "", CollectionType = "", Channel = "", Store = "", TransAccount = "", TransType = "", PayStatus = "", AccountingDay = "", PayDate = "", Barcode1 = "", Barcode2 = "", Barcode3 = "", Empty="" },
                new ReceiptInfoBillMarketModel() { Idx = "", CollectionType = "", Channel = "", Store = "", TransAccount = "", TransType = "", PayStatus = "", AccountingDay = "", PayDate = "", Barcode1 = "", Barcode2 = "", Barcode3 = "", Empty="" },
            };
            bool err = false;
            marts.ForEach(p => { if (p.Source != new ReceiptInfoBillMarketModel() { Source = p.Source }.Source) { err = true; return; } });
            if (err) { Message.AddErrorMessage(MessageCode.Code0000, "資訊流-超商Source拆分組合異常"); return; }
            using StreamWriter sw = new StreamWriter($@"D:\ibankRoot\Ftp_SKGPortalCore\TransactionListDaily\SKG_MART.{DateTime.Now.ToString("yyyyMMdd")}", false);
            marts.ForEach(p => sw.WriteLine(p.Source));
            sw.Close();
        }
        /// <summary>
        /// 資訊流-超商產險
        /// </summary>
        private static void ReceiptInfoMarketSPIData()
        {
            List<ReceiptInfoBillMarketSPIModel> martSPIs = new List<ReceiptInfoBillMarketSPIModel>() {
                new ReceiptInfoBillMarketSPIModel() { Idx="", Channel="", ISC="", TransDate="", PayDate="", Barcode2="", Barcode3_Date="", Barcode3_CompareCode="", Barcode3_Amount="", Empty1="", Store="", Empty2=""},
                new ReceiptInfoBillMarketSPIModel() { Idx="", Channel="", ISC="", TransDate="", PayDate="", Barcode2="", Barcode3_Date="", Barcode3_CompareCode="", Barcode3_Amount="", Empty1="", Store="", Empty2=""},
                new ReceiptInfoBillMarketSPIModel() { Idx="", Channel="", ISC="", TransDate="", PayDate="", Barcode2="", Barcode3_Date="", Barcode3_CompareCode="", Barcode3_Amount="", Empty1="", Store="", Empty2=""},
            };
            bool err = false;
            martSPIs.ForEach(p => { if (p.Source != new ReceiptInfoBillMarketSPIModel() { Source = p.Source }.Source) { err = true; return; } });
            if (err) { Message.AddErrorMessage(MessageCode.Code0000, "資訊流-超商產險Source拆分組合異常"); return; }
            using StreamWriter sw = new StreamWriter($@"D:\ibankRoot\Ftp_SKGPortalCore\TransactionListDaily\SKG_MARTSPI.{DateTime.Now.ToString("yyyyMMdd")}", false);
            martSPIs.ForEach(p => sw.WriteLine(p.Source));
            sw.Close();
        }
        /// <summary>
        /// 資訊流-農金
        /// </summary>
        private static void ReceiptInfoFarmData()
        {
            List<ReceiptInfoBillFarmModel> farms = new List<ReceiptInfoBillFarmModel>() {
                new ReceiptInfoBillFarmModel() { Idx="", CollectionType="", Channel="", Store="", TransAccount="", TransType="", PayStatus="", AccountingDay="", PayDate="", Barcode1="", Barcode2="", Barcode3="", Empty=""},
                new ReceiptInfoBillFarmModel() { Idx="", CollectionType="", Channel="", Store="", TransAccount="", TransType="", PayStatus="", AccountingDay="", PayDate="", Barcode1="", Barcode2="", Barcode3="", Empty=""},
                new ReceiptInfoBillFarmModel() { Idx="", CollectionType="", Channel="", Store="", TransAccount="", TransType="", PayStatus="", AccountingDay="", PayDate="", Barcode1="", Barcode2="", Barcode3="", Empty=""},
            };
            bool err = false;
            farms.ForEach(p => { if (p.Source != new ReceiptInfoBillFarmModel() { Source = p.Source }.Source) { err = true; return; } });
            if (err) { Message.AddErrorMessage(MessageCode.Code0000, "資訊流-農金Source拆分組合異常"); return; }
            using StreamWriter sw = new StreamWriter($@"D:\ibankRoot\Ftp_SKGPortalCore\TransactionListDaily\SKG_FARM.{DateTime.Now.ToString("yyyyMMdd")}", false);
            farms.ForEach(p => sw.WriteLine(p.Source));
            sw.Close();
        }
        /// <summary>
        /// 匯款檔
        /// </summary>
        private static void RemitInfoData()
        {
            List<RemitInfoModel> rts = new List<RemitInfoModel>() {
                new RemitInfoModel() { RemitDate="", RemitTime="", Channel="", CollectionType="", Amount="", BatchNo="", Empty="" },
                new RemitInfoModel() { RemitDate="", RemitTime="", Channel="", CollectionType="", Amount="", BatchNo="", Empty="" },
                new RemitInfoModel() { RemitDate="", RemitTime="", Channel="", CollectionType="", Amount="", BatchNo="", Empty="" },
            };
            bool err = false;
            rts.ForEach(p => { if (p.Source != new RemitInfoModel() { Source = p.Source }.Source) { err = true; return; } });
            if (err) { Message.AddErrorMessage(MessageCode.Code0000, "匯款檔Source拆分組合異常"); return; }
            using StreamWriter sw = new StreamWriter($@"D:\ibankRoot\Ftp_SKGPortalCore\TransactionListDaily\SKG_RT.{DateTime.Now.ToString("yyyyMMdd")}", false);
            rts.ForEach(p => sw.WriteLine(p.Source));
            sw.Close();
        }
        #endregion
    }
}
