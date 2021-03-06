﻿using NUnit.Framework;
using SKGPortalCore.Core;
using SKGPortalCore.Core.Libary;
using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Model.User;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Repository.MasterData;
using System.Collections.Generic;

namespace SKGPortalCore.NUnit.MasterData.A_UnitTest
{
    public class BizCustomerUnitTest : BaseUnitTest
    {
        #region Property
        BizCustomerRepository Repo;
        [SetUp]
        public void Setup()
        {
            Repo = new BizCustomerRepository(DataAccess) { User = SystemOperator.SysOperator };
        }
        #endregion

        #region Test

        #region Create
        private class Create : BizCustomerUnitTest
        {
            /// <summary>
            /// 正常資料
            /// 一般用戶：企業代號：6碼、虛擬帳號長度：13碼、未啟用檢碼、啟用「銀行、超商、郵局」通路／代收類別
            /// 清算手續費(次月扣款)
            /// </summary>
            [Test]
            public void C_CorrectData_6_1()
            {
                string key = "990660";
                BizCustomerSet expectData = new BizCustomerSet()
                {
                    BizCustomer = new BizCustomerModel() { MarketEnable = true, PostEnable = true, },
                    BizCustomerFeeDetail = new List<BizCustomerFeeDetailModel>() {
                        new BizCustomerFeeDetailModel(){ IntroPercent=0 },
                        new BizCustomerFeeDetailModel(){ IntroPercent=0 },
                        new BizCustomerFeeDetailModel(){ IntroPercent=0 },
                        },
                },
                    actualData = Repo.Create(new BizCustomerSet()
                    {
                        BizCustomer = new BizCustomerModel()
                        {
                            CustomerId = "64487252",
                            CustomerCode = key,
                            VirtualAccountLen = VirtualAccountLen.Len13,
                            BillTermLen = 3,
                            PayerNoLen = 4,
                            BizCustType = BizCustType.Cust,
                            VirtualAccount1 = VirtualAccount1.BillTerm,
                            VirtualAccount2 = VirtualAccount2.PayerNo,
                            VirtualAccount3 = VirtualAccount3.NoverifyCode,
                            ChannelIds = "00,01,02,03,04,05",
                            CollectionTypeIds = $"{SystemCP.BankCollectionTypeId},{SystemCP.PostCollectionTypeId},6V1,6V2,6V3",
                            AccountStatus = AccountStatus.Enable,
                        },
                        BizCustomerFeeDetail = new List<BizCustomerFeeDetailModel>(){
                            new BizCustomerFeeDetailModel(){ CustomerCode=key, ChannelGroupType= ChannelGroupType.Bank, BankFeeType= BankFeeType.ClearFee_NextMonth,Fee=10, IntroPercent=0 },
                            new BizCustomerFeeDetailModel(){ CustomerCode=key, ChannelGroupType= ChannelGroupType.Market, BankFeeType= BankFeeType.ClearFee_NextMonth,Fee=10, IntroPercent=0 },
                            new BizCustomerFeeDetailModel(){ CustomerCode=key, ChannelGroupType= ChannelGroupType.Post, BankFeeType= BankFeeType.ClearFee_NextMonth,Fee=10, IntroPercent=0 },
                        }
                    });
                AssertCorrectData(expectData, actualData);
            }
            /// <summary>
            /// 正常資料
            /// 一般用戶：企業代號：6碼、虛擬帳號長度：13碼、未啟用檢碼、啟用「銀行、郵局」通路／代收類別
            /// 清算手續費(次月扣款)
            /// </summary>
            [Test]
            public void C_CorrectData_6_2()
            {
                string key = "990661";
                BizCustomerSet expectData = new BizCustomerSet()
                {
                    BizCustomer = new BizCustomerModel() { MarketEnable = false, PostEnable = true, },
                    BizCustomerFeeDetail = new List<BizCustomerFeeDetailModel>() {
                        new BizCustomerFeeDetailModel(){ IntroPercent=0 },
                        new BizCustomerFeeDetailModel(){ IntroPercent=0 },
                        },
                },
                actualData = Repo.Create(new BizCustomerSet()
                {
                    BizCustomer = new BizCustomerModel()
                    {
                        CustomerId = "64487252",
                        CustomerCode = key,
                        VirtualAccountLen = VirtualAccountLen.Len13,
                        BillTermLen = 3,
                        PayerNoLen = 4,
                        BizCustType = BizCustType.Cust,
                        VirtualAccount1 = VirtualAccount1.BillTerm,
                        VirtualAccount2 = VirtualAccount2.PayerNo,
                        VirtualAccount3 = VirtualAccount3.NoverifyCode,
                        ChannelIds = "00,05",
                        CollectionTypeIds = $"{SystemCP.BankCollectionTypeId},{SystemCP.PostCollectionTypeId}",
                        AccountStatus = AccountStatus.Enable,
                    },
                    BizCustomerFeeDetail = new List<BizCustomerFeeDetailModel>(){
                            new BizCustomerFeeDetailModel(){ CustomerCode=key, ChannelGroupType= ChannelGroupType.Bank, BankFeeType= BankFeeType.ClearFee_NextMonth,Fee=10, IntroPercent=0 },
                            new BizCustomerFeeDetailModel(){ CustomerCode=key, ChannelGroupType= ChannelGroupType.Post, BankFeeType= BankFeeType.ClearFee_NextMonth,Fee=10, IntroPercent=0 },
                        }
                });
                AssertCorrectData(expectData, actualData);
            }
            /// <summary>
            /// 正常資料：
            /// 一般用戶：企業代號：4碼、銷帳編號長度：16碼、未啟用檢碼、啟用「銀行、超商、郵局」通路／代收類別
            /// 清算手續費(次月扣款)
            /// </summary>
            [Test]
            public void C_CorrectData_4_1()
            {
                BizCustomerSet expectData = new BizCustomerSet()
                {
                    BizCustomer = new BizCustomerModel() { MarketEnable = true, PostEnable = true, },
                    BizCustomerFeeDetail = new List<BizCustomerFeeDetailModel>() {
                        new BizCustomerFeeDetailModel(){ IntroPercent=0 },
                        new BizCustomerFeeDetailModel(){ IntroPercent=0 },
                        new BizCustomerFeeDetailModel(){ IntroPercent=0 },
                        },
                },
                    actualData = Repo.Create(new BizCustomerSet()
                    {
                        BizCustomer = new BizCustomerModel()
                        {
                            CustomerId = "64487252",
                            CustomerCode = "2665",
                            VirtualAccountLen = VirtualAccountLen.Len16,
                            BillTermLen = 6,
                            PayerNoLen = 6,
                            BizCustType = BizCustType.Cust,
                            VirtualAccount1 = VirtualAccount1.BillTerm,
                            VirtualAccount2 = VirtualAccount2.PayerNo,
                            VirtualAccount3 = VirtualAccount3.NoverifyCode,
                            ChannelIds = "00,01,02,03,04,05",
                            CollectionTypeIds = $"{SystemCP.BankCollectionTypeId},{SystemCP.PostCollectionTypeId},6V1,6V2,6V3",
                            AccountStatus = AccountStatus.Enable,
                        },
                        BizCustomerFeeDetail = new List<BizCustomerFeeDetailModel>(){
                            new BizCustomerFeeDetailModel(){ CustomerCode="2665", ChannelGroupType= ChannelGroupType.Bank, BankFeeType= BankFeeType.ClearFee_NextMonth,Fee=10, IntroPercent=0 },
                            new BizCustomerFeeDetailModel(){ CustomerCode="2665", ChannelGroupType= ChannelGroupType.Market, BankFeeType= BankFeeType.ClearFee_NextMonth,Fee=10, IntroPercent=0 },
                            new BizCustomerFeeDetailModel(){ CustomerCode="2665", ChannelGroupType= ChannelGroupType.Post, BankFeeType= BankFeeType.ClearFee_NextMonth,Fee=10, IntroPercent=0 },
                        }
                    });
                AssertCorrectData(expectData, actualData);
            }
            /// <summary>
            /// 正常資料：
            /// 企業代號：3碼、銷帳編號長度：14碼、未啟用檢碼、啟用「銀行、超商、郵局」通路／代收類別
            /// 清算手續費(次月扣款)
            /// </summary>
            [Test]
            public void C_CorrectData_3_1()
            {
                BizCustomerSet expectData = new BizCustomerSet()
                {
                    BizCustomer = new BizCustomerModel() { MarketEnable = true, PostEnable = true, },
                    BizCustomerFeeDetail = new List<BizCustomerFeeDetailModel>() {
                        new BizCustomerFeeDetailModel(){ IntroPercent=0 },
                        new BizCustomerFeeDetailModel(){ IntroPercent=0 },
                        new BizCustomerFeeDetailModel(){ IntroPercent=0 },
                        },
                },
                    actualData = Repo.Create(new BizCustomerSet()
                    {
                        BizCustomer = new BizCustomerModel()
                        {
                            CustomerId = "64487252",
                            CustomerCode = "805",
                            VirtualAccountLen = VirtualAccountLen.Len14,
                            BillTermLen = 5,
                            PayerNoLen = 6,
                            BizCustType = BizCustType.Cust,
                            VirtualAccount1 = VirtualAccount1.BillTerm,
                            VirtualAccount2 = VirtualAccount2.PayerNo,
                            VirtualAccount3 = VirtualAccount3.NoverifyCode,
                            ChannelIds = "00,01,02,03,04,05",
                            CollectionTypeIds = $"{SystemCP.BankCollectionTypeId},{SystemCP.PostCollectionTypeId},6V1,6V2,6V3",
                            AccountStatus = AccountStatus.Enable,
                        },
                        BizCustomerFeeDetail = new List<BizCustomerFeeDetailModel>(){
                            new BizCustomerFeeDetailModel(){ CustomerCode="805", ChannelGroupType= ChannelGroupType.Bank, BankFeeType= BankFeeType.ClearFee_NextMonth,Fee=10, IntroPercent=0 },
                            new BizCustomerFeeDetailModel(){ CustomerCode="805", ChannelGroupType= ChannelGroupType.Market, BankFeeType= BankFeeType.ClearFee_NextMonth,Fee=10, IntroPercent=0 },
                            new BizCustomerFeeDetailModel(){ CustomerCode="805", ChannelGroupType= ChannelGroupType.Post, BankFeeType= BankFeeType.ClearFee_NextMonth,Fee=10, IntroPercent=0 },
                        }
                    });
                AssertCorrectData(expectData, actualData);
            }
            /// <summary>
            /// 
            /// </summary>
            [Test]
            public void E_CheckVirtualAccountLength_A()
            {
                string key = "930660";
                Repo.Create(new BizCustomerSet()
                {
                    BizCustomer = new BizCustomerModel()
                    {
                        CustomerId = "64487252",
                        CustomerCode = key,
                        VirtualAccountLen = VirtualAccountLen.Len13,
                        BillTermLen = 9,
                        PayerNoLen = 6,
                        BizCustType = BizCustType.Cust,
                        VirtualAccount1 = VirtualAccount1.BillTerm,
                        VirtualAccount2 = VirtualAccount2.PayerNo,
                        VirtualAccount3 = VirtualAccount3.NoverifyCode,
                        ChannelIds = "00,01,02,03,04,05",
                        CollectionTypeIds = $"{SystemCP.BankCollectionTypeId},{SystemCP.PostCollectionTypeId},6V1,6V2,6V3",
                        AccountStatus = AccountStatus.Enable,
                    },
                    BizCustomerFeeDetail = new List<BizCustomerFeeDetailModel>(){
                            new BizCustomerFeeDetailModel(){ CustomerCode=key, ChannelGroupType= ChannelGroupType.Bank, BankFeeType= BankFeeType.ClearFee_NextMonth,Fee=10, IntroPercent=0 },
                            new BizCustomerFeeDetailModel(){ CustomerCode=key, ChannelGroupType= ChannelGroupType.Market, BankFeeType= BankFeeType.ClearFee_NextMonth,Fee=10, IntroPercent=0 },
                            new BizCustomerFeeDetailModel(){ CustomerCode=key, ChannelGroupType= ChannelGroupType.Post, BankFeeType= BankFeeType.ClearFee_NextMonth,Fee=10, IntroPercent=0 },
                        }
                });
                CheckVirtualAccountLength();
            }
            /// <summary>
            /// 
            /// </summary>
            [Test]
            public void E_CheckBizCustType_A()
            {
                string key = "99607";
                Repo.Create(new BizCustomerSet()
                {
                    BizCustomer = new BizCustomerModel()
                    {
                        CustomerId = "64487252",
                        CustomerCode = key,
                        VirtualAccountLen = VirtualAccountLen.Len14,
                        BillTermLen = 5,
                        PayerNoLen = 6,
                        BizCustType = BizCustType.Cust,
                        VirtualAccount1 = VirtualAccount1.BillTerm,
                        VirtualAccount2 = VirtualAccount2.PayerNo,
                        VirtualAccount3 = VirtualAccount3.NoverifyCode,
                        ChannelIds = "00,01,02,03,04,05",
                        CollectionTypeIds = $"{SystemCP.BankCollectionTypeId},{SystemCP.PostCollectionTypeId},6V1,6V2,6V3",
                        AccountStatus = AccountStatus.Enable,
                    },
                    BizCustomerFeeDetail = new List<BizCustomerFeeDetailModel>(){
                            new BizCustomerFeeDetailModel(){ CustomerCode=key, ChannelGroupType= ChannelGroupType.Bank, BankFeeType= BankFeeType.ClearFee_NextMonth,Fee=10, IntroPercent=0 },
                            new BizCustomerFeeDetailModel(){ CustomerCode=key, ChannelGroupType= ChannelGroupType.Market, BankFeeType= BankFeeType.Hitrust_ClearFee_NextMonth,Fee=10, IntroPercent=0 },
                            new BizCustomerFeeDetailModel(){ CustomerCode=key, ChannelGroupType= ChannelGroupType.Post, BankFeeType= BankFeeType.Hitrust_ClearFee_NextMonth,Fee=10, IntroPercent=0 },
                        }
                });
                CheckBizCustType();
            }
            /// <summary>
            /// 
            /// </summary>
            [Test]
            public void E_CheckIntroduceType_A()
            {
                string key = "99608";
                Repo.Create(new BizCustomerSet()
                {
                    BizCustomer = new BizCustomerModel()
                    {
                        CustomerId = "64487252",
                        CustomerCode = key,
                        VirtualAccountLen = VirtualAccountLen.Len14,
                        BillTermLen = 5,
                        PayerNoLen = 6,
                        BizCustType = BizCustType.Cust,
                        VirtualAccount1 = VirtualAccount1.BillTerm,
                        VirtualAccount2 = VirtualAccount2.PayerNo,
                        VirtualAccount3 = VirtualAccount3.NoverifyCode,
                        ChannelIds = "00,01,02,03,04,05",
                        CollectionTypeIds = $"{SystemCP.BankCollectionTypeId},{SystemCP.PostCollectionTypeId},6V1,6V2,6V3",
                        AccountStatus = AccountStatus.Enable,
                    },
                    BizCustomerFeeDetail = new List<BizCustomerFeeDetailModel>(){
                            new BizCustomerFeeDetailModel(){ CustomerCode=key, ChannelGroupType= ChannelGroupType.Bank, BankFeeType= BankFeeType.ClearFee_NextMonth,Fee=10, IntroPercent=0 },
                            new BizCustomerFeeDetailModel(){ CustomerCode=key, ChannelGroupType= ChannelGroupType.Market, BankFeeType= BankFeeType.Hitrust_ClearFee_NextMonth,Fee=10, IntroPercent=0 },
                            new BizCustomerFeeDetailModel(){ CustomerCode=key, ChannelGroupType= ChannelGroupType.Post, BankFeeType= BankFeeType.TotalFee,Fee=10, IntroPercent=0 },
                        }
                });
                CheckIntroduceType();
            }
        }
        #endregion

        #region Update
        private class Update : BizCustomerUnitTest
        {

        }
        #endregion

        #region Delete
        private class Delete : BizCustomerUnitTest
        {

        }
        #endregion

        #region Invalid
        private class Invalid : BizCustomerUnitTest
        {

        }
        #endregion

        #region EndCase
        private class EndCase : BizCustomerUnitTest
        {

        }
        #endregion

        #endregion

        #region Private

        #region CorrectData
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectData"></param>
        /// <param name="actualData"></param>
        /// <param name="messageLog"></param>
        private void AssertCorrectData(BizCustomerSet expectData, BizCustomerSet actualData)
        {
            AssertSet(expectData, actualData);
            AssertMessage();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectData"></param>
        /// <param name="actualData"></param>
        private void AssertSet(BizCustomerSet expectData, BizCustomerSet actualData)
        {
            Assert.AreEqual(expectData.BizCustomer.MarketEnable, actualData.BizCustomer.MarketEnable, ResxManage.GetDescription<BizCustomerModel>(p => p.MarketEnable));
            Assert.AreEqual(expectData.BizCustomer.PostEnable, actualData.BizCustomer.PostEnable, ResxManage.GetDescription<BizCustomerModel>(p => p.PostEnable));
            for (int i = 0; i < expectData.BizCustomerFeeDetail.Count; i++)
            {
                Assert.AreEqual(expectData.BizCustomerFeeDetail[i].IntroPercent, actualData.BizCustomerFeeDetail[i].IntroPercent);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageLog"></param>
        private void AssertMessage()
        {
            Comm.SpecAssertMessage(Repo.Message.MsgCodeList, MessageCode.Code0001, false);
            Comm.SpecAssertMessage(Repo.Message.MsgCodeList, MessageCode.Code1007, false);
        }
        #endregion

        #region ErrorData
        private void CheckVirtualAccountLength()
        {
            Comm.SpecAssertMessage(Repo.Message.MsgCodeList, MessageCode.Code1007, true);
        }
        private void CheckBizCustType()
        {
            Comm.SpecAssertMessage(Repo.Message.MsgCodeList, MessageCode.Code1019, true);
        }
        private void CheckIntroduceType()
        {
            Comm.SpecAssertMessage(Repo.Message.MsgCodeList, MessageCode.Code1018, true);
        }
        #endregion

        #endregion
    }
}
