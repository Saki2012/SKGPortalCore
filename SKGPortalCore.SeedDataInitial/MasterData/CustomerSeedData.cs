using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Model.System;
using SKGPortalCore.Repository.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

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
                    new CustomerSet() { Customer = new CustomerModel() {  CustomerId="30262944", CustomerName="新榮天然氣股份有限公司", Address="台北市",Tel="02-23659193",Fax="02-23659193",ZipCode="740",ZipUnit="294",ZipNum="852"} } ,
                    new CustomerSet() { Customer = new CustomerModel() {  CustomerId="00146877", CustomerName="人壽保險",               Address="台中市",Tel="05-88422913",Fax="05-88422913",ZipCode="310",ZipUnit="848",ZipNum="145"} } ,
                    new CustomerSet() { Customer = new CustomerModel() {  CustomerId="01031142", CustomerName="台北市女子第一高級中學", Address="台北市",Tel="02-77428894",Fax="02-77428894",ZipCode="361",ZipUnit="221",ZipNum="543"} } ,
                    new CustomerSet() { Customer = new CustomerModel() {  CustomerId="00973171", CustomerName="美喜劇場",               Address="桃園市",Tel="03-12577633",Fax="03-12341124",ZipCode="056",ZipUnit="357",ZipNum="221"} } ,
                    new CustomerSet() { Customer = new CustomerModel() {  CustomerId="48297921", CustomerName="中正管理委員會",         Address="台南市",Tel="07-55712513",Fax="07-55712513",ZipCode="561",ZipUnit="885",ZipNum="631"} } ,
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
                        BizCustomer = new BizCustomerModel(){ CustomerCode="992086",CustomerId="30262944",AccountDeptId="0019",RealAccount="0514101002570",BillTermLen=3,PayerNoLen=3,VirtualAccount1= VirtualAccount1.Empty,VirtualAccount2= VirtualAccount2.Empty,VirtualAccount3= VirtualAccount3.NoverifyCode,AccountStatus= AccountStatus.Enable },
                        BizCustomerFeeDetail =new List<BizCustomerFeeDetailModel>(){
                            new BizCustomerFeeDetailModel(){ CustomerCode= "992086", ChannelType= ChannelGroupType.Bank, BankFeeType= BankFeeType.ClearFeeA, Fee=15, Percent=0 } ,
                            new BizCustomerFeeDetailModel(){ CustomerCode= "992086", ChannelType= ChannelGroupType.Market, BankFeeType= BankFeeType.ClearFeeA, Fee=15, Percent=0 } ,
                            new BizCustomerFeeDetailModel(){ CustomerCode= "992086", ChannelType= ChannelGroupType.Post, BankFeeType= BankFeeType.ClearFeeA, Fee=15, Percent=0 } ,
                        }
                    },
                    new BizCustomerSet() {
                        BizCustomer = new BizCustomerModel(){ CustomerCode="2143",CustomerId="00146877",AccountDeptId="0019",RealAccount="0518425007840",BillTermLen=3,PayerNoLen=3,VirtualAccount1= VirtualAccount1.Empty,VirtualAccount2= VirtualAccount2.Empty,VirtualAccount3= VirtualAccount3.NoverifyCode,AccountStatus= AccountStatus.Enable },
                        BizCustomerFeeDetail =new List<BizCustomerFeeDetailModel>(){
                            new BizCustomerFeeDetailModel(){ CustomerCode= "2143", ChannelType= ChannelGroupType.Bank, BankFeeType= BankFeeType.TotalFee, Fee=15, Percent=0 } ,
                            new BizCustomerFeeDetailModel(){ CustomerCode= "2143", ChannelType= ChannelGroupType.Market, BankFeeType= BankFeeType.TotalFee, Fee=15, Percent=0 } ,
                            new BizCustomerFeeDetailModel(){ CustomerCode= "2143", ChannelType= ChannelGroupType.Post, BankFeeType= BankFeeType.TotalFee, Fee=15, Percent=0 } ,
                        }
                    },
                    new BizCustomerSet() {
                        BizCustomer = new BizCustomerModel(){ CustomerCode="805",CustomerId="01031142",AccountDeptId="0019",RealAccount="7714815486840",BillTermLen=3,PayerNoLen=3,VirtualAccount1= VirtualAccount1.Empty,VirtualAccount2= VirtualAccount2.Empty,VirtualAccount3= VirtualAccount3.NoverifyCode,AccountStatus= AccountStatus.Enable },
                        BizCustomerFeeDetail =new List<BizCustomerFeeDetailModel>(){
                            new BizCustomerFeeDetailModel(){ CustomerCode= "805", ChannelType= ChannelGroupType.Bank, BankFeeType= BankFeeType.TotalFee, Fee=15, Percent=0 } ,
                            new BizCustomerFeeDetailModel(){ CustomerCode= "805", ChannelType= ChannelGroupType.Market, BankFeeType= BankFeeType.TotalFee, Fee=15, Percent=0 } ,
                            new BizCustomerFeeDetailModel(){ CustomerCode= "805", ChannelType= ChannelGroupType.Post, BankFeeType= BankFeeType.TotalFee, Fee=15, Percent=0 } ,
                        }
                    },
                };

                custs.ForEach(cust =>
                {
                    if (null == repo.QueryData(new[] { cust.BizCustomer.CustomerCode })) repo.Create(cust);
                });
            }
            finally
            {
                Message.Prefix = string.Empty;
            }
        }
    }
}
