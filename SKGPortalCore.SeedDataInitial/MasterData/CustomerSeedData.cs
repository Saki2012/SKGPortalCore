using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.Libary;
using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Model.User;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Repository.MasterData;
using System.Collections.Generic;

namespace SKGPortalCore.SeedDataInitial.MasterData
{
    public static class CustomerSeedData
    {
        /// <summary>
        /// 新增「商戶」-初始資料
        /// </summary>
        /// <param name="db"></param>
        public static void CreateCustomer(SysMessageLog Message, ApplicationDbContext DataAccess)
        {
            Customer(Message, DataAccess);
            BizCustomer(Message, DataAccess);
        }

        private static void Customer(SysMessageLog Message, ApplicationDbContext DataAccess)
        {
            try
            {
                Message.Prefix = "新增「客戶」-初始資料：";
                using CustomerRepository repo = new CustomerRepository(DataAccess) { User = SystemOperator.SysOperator, Message = Message };
                List<CustomerSet> custs = new List<CustomerSet>()
                {
                    new CustomerSet() { Customer = new CustomerModel() {  CustomerId="64487252", CustomerName="單元測試用客戶",         Address="台北市",Tel="02-23659193",Fax="02-23659193",ZipCode="740",ZipUnit="294",ZipNum="852"} } ,
                    new CustomerSet() { Customer = new CustomerModel() {  CustomerId="30262944", CustomerName="新榮天然氣股份有限公司", Address="台北市",Tel="02-23659193",Fax="02-23659193",ZipCode="740",ZipUnit="294",ZipNum="852"} } ,
                    new CustomerSet() { Customer = new CustomerModel() {  CustomerId="00146877", CustomerName="人壽保險",               Address="台中市",Tel="05-88422913",Fax="05-88422913",ZipCode="310",ZipUnit="848",ZipNum="145"} } ,
                    new CustomerSet() { Customer = new CustomerModel() {  CustomerId="01031142", CustomerName="台北市女子第一高級中學", Address="台北市",Tel="02-77428894",Fax="02-77428894",ZipCode="361",ZipUnit="221",ZipNum="543"} } ,
                    new CustomerSet() { Customer = new CustomerModel() {  CustomerId="00973171", CustomerName="美喜劇場",               Address="桃園市",Tel="03-12577633",Fax="03-12341124",ZipCode="056",ZipUnit="357",ZipNum="221"} } ,
                    new CustomerSet() { Customer = new CustomerModel() {  CustomerId="48297921", CustomerName="中正管理委員會",         Address="台南市",Tel="07-55712513",Fax="07-55712513",ZipCode="561",ZipUnit="885",ZipNum="631"} } ,
                    new CustomerSet() { Customer = new CustomerModel() {  CustomerId="48577921", CustomerName="玉明科技",               Address="台南市",Tel="07-55712513",Fax="07-55712513",ZipCode="561",ZipUnit="885",ZipNum="631"} } ,
                    new CustomerSet() { Customer = new CustomerModel() {  CustomerId="77494941", CustomerName="Hitrust",                Address="台南市",Tel="07-55712513",Fax="07-55712513",ZipCode="561",ZipUnit="885",ZipNum="631"} } ,
                };

                custs.ForEach(cust =>
                {
                    if (null == repo.QueryData(new[] { cust.Customer.CustomerId })) repo.Create(cust);
                });
            }
            finally
            {
                Message.Prefix = string.Empty;
            }
        }
        private static void BizCustomer(SysMessageLog Message, ApplicationDbContext DataAccess)
        {
            try
            {
                Message.Prefix = "新增「商戶」-初始資料：";
                using BizCustomerRepository repo = new BizCustomerRepository(DataAccess) { User = SystemOperator.SysOperator, Message = Message };
                List<BizCustomerSet> custs = new List<BizCustomerSet>()
                {
                    new BizCustomerSet() {
                        BizCustomer = new BizCustomerModel(){ CustomerCode="445235",CustomerId="48577921",AccountDeptId="0019",RealAccount="7714815486840", VirtualAccountLen= VirtualAccountLen.Len13,
                            BillTermLen=2,PayerNoLen=5,VirtualAccount1= VirtualAccount1.BillTerm,VirtualAccount2= VirtualAccount2.PayerNo,VirtualAccount3= VirtualAccount3.NoverifyCode, BizCustType= BizCustType.Introducer,
                            ChannelIds="00,01,02,03,04,05,12,13,14",CollectionTypeIds= LibData.Merge(",",false,SystemCP.BankCollectionTypeId,SystemCP.PostCollectionTypeId,"6V1,6V2,6V3"),AccountStatus= AccountStatus.Enable,ImportBatchNo="BeginData" },
                        BizCustomerFeeDetail =new List<BizCustomerFeeDetailModel>(){
                            new BizCustomerFeeDetailModel(){ CustomerCode= "445235", ChannelGroupType= ChannelGroupType.Bank, BankFeeType= BankFeeType.ClearFee_CurMonth, Fee=15, IntroPercent=0 } ,
                            new BizCustomerFeeDetailModel(){ CustomerCode= "445235", ChannelGroupType= ChannelGroupType.Market, BankFeeType= BankFeeType.ClearFee_CurMonth, Fee=15, IntroPercent=0 } ,
                            new BizCustomerFeeDetailModel(){ CustomerCode= "445235", ChannelGroupType= ChannelGroupType.Post, BankFeeType= BankFeeType.ClearFee_CurMonth, Fee=15, IntroPercent=0 } ,
                        }
                    },
                    new BizCustomerSet() {
                        BizCustomer = new BizCustomerModel(){ CustomerCode="774336",CustomerId="77494941",AccountDeptId="0019",RealAccount="7714815486840", VirtualAccountLen= VirtualAccountLen.Len13,
                            BillTermLen=2,PayerNoLen=5,VirtualAccount1= VirtualAccount1.BillTerm,VirtualAccount2= VirtualAccount2.PayerNo,VirtualAccount3= VirtualAccount3.NoverifyCode, BizCustType= BizCustType.Hitrust,
                            ChannelIds="00,01,02,03,04,05,12,13,14",CollectionTypeIds= LibData.Merge(",",false,SystemCP.BankCollectionTypeId,SystemCP.PostCollectionTypeId,"6V1,6V2,6V3"),AccountStatus= AccountStatus.Enable,ImportBatchNo="BeginData" },
                        BizCustomerFeeDetail =new List<BizCustomerFeeDetailModel>(){
                            new BizCustomerFeeDetailModel(){ CustomerCode= "774336", ChannelGroupType= ChannelGroupType.Bank, BankFeeType= BankFeeType.ClearFee_CurMonth, Fee=15, IntroPercent=0 } ,
                            new BizCustomerFeeDetailModel(){ CustomerCode= "774336", ChannelGroupType= ChannelGroupType.Market, BankFeeType= BankFeeType.ClearFee_CurMonth, Fee=15, IntroPercent=0 } ,
                            new BizCustomerFeeDetailModel(){ CustomerCode= "774336", ChannelGroupType= ChannelGroupType.Post, BankFeeType= BankFeeType.ClearFee_CurMonth, Fee=15, IntroPercent=0 } ,
                        }
                    },
                    new BizCustomerSet() {
                        BizCustomer = new BizCustomerModel(){ CustomerCode="992086",CustomerId="30262944",AccountDeptId="0019",RealAccount="0514101002570", VirtualAccountLen= VirtualAccountLen.Len13,
                            BillTermLen=3,PayerNoLen=4,VirtualAccount1 = VirtualAccount1.BillTerm,VirtualAccount2 = VirtualAccount2.PayerNo,VirtualAccount3 = VirtualAccount3.NoverifyCode,
                            ChannelIds="00,01,02,03,04,05,12,13,14",CollectionTypeIds= LibData.Merge(",",false,SystemCP.BankCollectionTypeId,SystemCP.PostCollectionTypeId,"6V1,6V2,6V3"),BizCustType= BizCustType.Cust,AccountStatus= AccountStatus.Enable,ImportBatchNo="BeginData" },
                        BizCustomerFeeDetail =new List<BizCustomerFeeDetailModel>(){
                            new BizCustomerFeeDetailModel(){ CustomerCode= "992086", ChannelGroupType= ChannelGroupType.Bank, BankFeeType= BankFeeType.ClearFee_NextMonth, Fee=15, IntroPercent=0 } ,
                            new BizCustomerFeeDetailModel(){ CustomerCode= "992086", ChannelGroupType= ChannelGroupType.Market, BankFeeType= BankFeeType.ClearFee_NextMonth, Fee=15, IntroPercent=0 } ,
                            new BizCustomerFeeDetailModel(){ CustomerCode= "992086", ChannelGroupType= ChannelGroupType.Post, BankFeeType= BankFeeType.ClearFee_NextMonth, Fee=15, IntroPercent=0 } ,
                        }
                    },
                    new BizCustomerSet() {
                        BizCustomer = new BizCustomerModel(){ CustomerCode="2143"  ,CustomerId="00146877",AccountDeptId="0019",RealAccount="0518425007840", VirtualAccountLen= VirtualAccountLen.Len16,
                            BillTermLen=6,PayerNoLen=6,VirtualAccount1= VirtualAccount1.BillTerm,VirtualAccount2= VirtualAccount2.PayerNo,VirtualAccount3= VirtualAccount3.NoverifyCode, IntroCustomerCode="445235",
                            ChannelIds="00,01,02,03,04,05,12,13,14",CollectionTypeIds= LibData.Merge(",",false,SystemCP.BankCollectionTypeId,SystemCP.PostCollectionTypeId,"6V1,6V2,6V3"),BizCustType= BizCustType.Cust,AccountStatus= AccountStatus.Enable,ImportBatchNo="BeginData" },
                        BizCustomerFeeDetail =new List<BizCustomerFeeDetailModel>(){
                            new BizCustomerFeeDetailModel(){ CustomerCode= "2143", ChannelGroupType= ChannelGroupType.Bank, BankFeeType= BankFeeType.TotalFee, Fee=35, IntroPercent=0 } ,
                            new BizCustomerFeeDetailModel(){ CustomerCode= "2143", ChannelGroupType= ChannelGroupType.Market, BankFeeType= BankFeeType.TotalFee, Fee=35, IntroPercent=0 } ,
                            new BizCustomerFeeDetailModel(){ CustomerCode= "2143", ChannelGroupType= ChannelGroupType.Post, BankFeeType= BankFeeType.TotalFee, Fee=35, IntroPercent=0 } ,
                        }
                    },
                    new BizCustomerSet() {
                        BizCustomer = new BizCustomerModel(){ CustomerCode="805",CustomerId="01031142",AccountDeptId="0019",RealAccount="7714815486840",VirtualAccountLen= VirtualAccountLen.Len14,
                            BillTermLen=5,PayerNoLen=6,VirtualAccount1= VirtualAccount1.BillTerm,VirtualAccount2= VirtualAccount2.PayerNo,VirtualAccount3= VirtualAccount3.NoverifyCode,BizCustType= BizCustType.Cust,
                            IntroCustomerCode="445235",
                            ChannelIds="00,01,02,03,04,05,12,13,14",CollectionTypeIds= LibData.Merge(",",false,SystemCP.BankCollectionTypeId,SystemCP.PostCollectionTypeId,"6V1,6V2,6V3"),AccountStatus= AccountStatus.Enable,ImportBatchNo="BeginData" },
                        BizCustomerFeeDetail =new List<BizCustomerFeeDetailModel>(){
                            new BizCustomerFeeDetailModel(){ CustomerCode= "805", ChannelGroupType= ChannelGroupType.Bank, BankFeeType= BankFeeType.TotalFee, Fee=28, IntroPercent=50 } ,
                            new BizCustomerFeeDetailModel(){ CustomerCode= "805", ChannelGroupType= ChannelGroupType.Market, BankFeeType= BankFeeType.TotalFee, Fee=28, IntroPercent=50 } ,
                            new BizCustomerFeeDetailModel(){ CustomerCode= "805", ChannelGroupType= ChannelGroupType.Post, BankFeeType= BankFeeType.TotalFee, Fee=28, IntroPercent=50 } ,
                        }
                    },
                    new BizCustomerSet() {
                        BizCustomer = new BizCustomerModel(){ CustomerCode="993586",CustomerId="00973171",AccountDeptId="0019",RealAccount="7714815486840", VirtualAccountLen= VirtualAccountLen.Len13,
                            BillTermLen=2,PayerNoLen=5,VirtualAccount1= VirtualAccount1.BillTerm,VirtualAccount2= VirtualAccount2.PayerNo,VirtualAccount3= VirtualAccount3.NoverifyCode,BizCustType= BizCustType.Cust,
                            ChannelIds="00,01,02,03,04,05,12,13,14",CollectionTypeIds= LibData.Merge(",",false,SystemCP.BankCollectionTypeId,SystemCP.PostCollectionTypeId,"6V1,6V2,6V3"),AccountStatus= AccountStatus.Enable,ImportBatchNo="BeginData" },
                        BizCustomerFeeDetail =new List<BizCustomerFeeDetailModel>(){
                            new BizCustomerFeeDetailModel(){ CustomerCode= "993586", ChannelGroupType= ChannelGroupType.Bank, BankFeeType= BankFeeType.ClearFee_CurMonth, Fee=15, IntroPercent=0 } ,
                            new BizCustomerFeeDetailModel(){ CustomerCode= "993586", ChannelGroupType= ChannelGroupType.Market, BankFeeType= BankFeeType.ClearFee_CurMonth, Fee=15, IntroPercent=0 } ,
                            new BizCustomerFeeDetailModel(){ CustomerCode= "993586", ChannelGroupType= ChannelGroupType.Post, BankFeeType= BankFeeType.ClearFee_CurMonth, Fee=15, IntroPercent=0 } ,
                        }
                    },
                };

                custs.ForEach(cust =>
                {
                    if (null == repo.QueryData(new[] { cust.BizCustomer.CustomerCode })) repo.Create(cust);
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
