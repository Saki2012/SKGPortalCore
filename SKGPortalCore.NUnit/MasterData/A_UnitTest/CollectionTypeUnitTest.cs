using NUnit.Framework;
using SKGPortalCore.Core;
using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Model.User;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Repository.MasterData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SKGPortalCore.NUnit.MasterData.A_UnitTest
{
    public class CollectionTypeUnitTest : BaseUnitTest
    {
        #region Property
        CollectionTypeRepository Repo;
        [SetUp]
        public void Setup()
        {
            Repo = new CollectionTypeRepository(DataAccess) { User = SystemOperator.SysOperator };
        }
        #endregion

        #region Test

        #region Create
        private class Create : CollectionTypeUnitTest
        {
            /// <summary>
            /// 超商代收類別-正常資料
            /// </summary>
            [Test]
            public void C_CorrectData_MartA()
            {
                CollectionTypeSet expectData = new CollectionTypeSet()
                {
                    CollectionType = new CollectionTypeModel(),
                    CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                    },
                    CollectionTypeVerifyPeriod = new List<CollectionTypeVerifyPeriodModel>() {
                        new CollectionTypeVerifyPeriodModel() { ChannelId="01" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="02" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="03" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="04" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="12" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="13" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="14" },
                    },
                },
                                  actualData = Repo.Create(new CollectionTypeSet()
                                  {
                                      CollectionType = new CollectionTypeModel() { CollectionTypeId = "6P1", CollectionTypeName = "超商內扣", ChargePayType = ChargePayType.Deduction, },
                                      CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P1", ChannelId = "01", SRange = 1, ERange = 20000, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, },
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P1", ChannelId = "02", SRange = 1, ERange = 20000, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, },
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P1", ChannelId = "03", SRange = 1, ERange = 20000, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, },
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P1", ChannelId = "04", SRange = 1, ERange = 20000, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, },
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P1", ChannelId = "12", SRange = 1, ERange = 20000, ChannelFee = 6, ChannelFeedBackFee = 4, ChannelRebateFee = 0, },
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P1", ChannelId = "13", SRange = 1, ERange = 20000, ChannelFee = 8, ChannelFeedBackFee = 2, ChannelRebateFee = 0, },
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P1", ChannelId = "14", SRange = 1, ERange = 20000, ChannelFee = 0, ChannelFeedBackFee = 0, ChannelRebateFee = 10, },
                                      },
                                      CollectionTypeVerifyPeriod = new List<CollectionTypeVerifyPeriodModel>() {
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6P1", ChannelId="01", PayPeriodType= PayPeriodType.NDay_A },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6P1", ChannelId="02", PayPeriodType= PayPeriodType.NDay_B },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6P1", ChannelId="03", PayPeriodType= PayPeriodType.NDay_C },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6P1", ChannelId="04", PayPeriodType= PayPeriodType.TenDay },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6P1", ChannelId="12", PayPeriodType= PayPeriodType.Week },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6P1", ChannelId="13", PayPeriodType= PayPeriodType.NDay_A },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6P1", ChannelId="14", PayPeriodType= PayPeriodType.NDay_B },
                                      },
                                  });
                AssertCorrectData(expectData, actualData);
            }
            /// <summary>
            /// 超商代收類別-核銷週期明細有多的代收通路
            /// </summary>
            [Test]
            public void C_CorrectData_MartB()
            {
                CollectionTypeSet expectData = new CollectionTypeSet()
                {
                    CollectionType = new CollectionTypeModel(),
                    CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                    },
                    CollectionTypeVerifyPeriod = new List<CollectionTypeVerifyPeriodModel>() {
                        new CollectionTypeVerifyPeriodModel() { ChannelId="01" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="02" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="03" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="04" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="12" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="13" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="14" },
                    },
                },
                                  actualData = Repo.Create(new CollectionTypeSet()
                                  {
                                      CollectionType = new CollectionTypeModel() { CollectionTypeId = "6P2", CollectionTypeName = "超商內扣", ChargePayType = ChargePayType.Deduction, },
                                      CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P2", ChannelId = "01", SRange = 1, ERange = 20000, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, },
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P2", ChannelId = "02", SRange = 1, ERange = 20000, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, },
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P2", ChannelId = "03", SRange = 1, ERange = 20000, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, },
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P2", ChannelId = "04", SRange = 1, ERange = 20000, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, },
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P2", ChannelId = "12", SRange = 1, ERange = 20000, ChannelFee = 6, ChannelFeedBackFee = 4, ChannelRebateFee = 0, },
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P2", ChannelId = "13", SRange = 1, ERange = 20000, ChannelFee = 8, ChannelFeedBackFee = 2, ChannelRebateFee = 0, },
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P2", ChannelId = "14", SRange = 1, ERange = 20000, ChannelFee = 0, ChannelFeedBackFee = 0, ChannelRebateFee = 10, },
                                      },
                                      CollectionTypeVerifyPeriod = new List<CollectionTypeVerifyPeriodModel>() {
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6P2", ChannelId="01", PayPeriodType= PayPeriodType.NDay_A },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6P2", ChannelId="02", PayPeriodType= PayPeriodType.NDay_B },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6P2", ChannelId="03", PayPeriodType= PayPeriodType.NDay_C },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6P2", ChannelId="04", PayPeriodType= PayPeriodType.TenDay },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6P2", ChannelId="12", PayPeriodType= PayPeriodType.Week },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6P2", ChannelId="13", PayPeriodType= PayPeriodType.NDay_A },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6P2", ChannelId="14", PayPeriodType= PayPeriodType.NDay_B },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6P2", ChannelId="05", PayPeriodType= PayPeriodType.NDay_B },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6P2", ChannelId="06", PayPeriodType= PayPeriodType.NDay_B },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6P2", ChannelId="07", PayPeriodType= PayPeriodType.NDay_B },
                                      },
                                  });
                AssertCorrectData(expectData, actualData);
            }
            /// <summary>
            /// 收款區間重疊時-完全重疊
            /// </summary>
            [Test]
            public void E_CheckIsOverlap_A()
            {
                Repo.Create(new CollectionTypeSet()
                {
                    CollectionType = new CollectionTypeModel() { CollectionTypeId = "6P3", CollectionTypeName = "超商內扣", ChargePayType = ChargePayType.Deduction, },
                    CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P3", ChannelId = "01", SRange = 1, ERange = 20000, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, },
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P3", ChannelId = "01", SRange = 1, ERange = 20000, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, },
                                      },
                    CollectionTypeVerifyPeriod = new List<CollectionTypeVerifyPeriodModel>() {
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6P3", ChannelId="01", PayPeriodType= PayPeriodType.NDay_A },
                                      },
                });
                CheckIsOverlap();
            }
            /// <summary>
            /// 收款區間重疊時-內疊
            /// </summary>
            [Test]
            public void E_CheckIsOverlap_B()
            {
                Repo.Create(new CollectionTypeSet()
                {
                    CollectionType = new CollectionTypeModel() { CollectionTypeId = "6P4", CollectionTypeName = "超商內扣", ChargePayType = ChargePayType.Deduction, },
                    CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P4", ChannelId = "01", SRange = 1, ERange = 20000, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, },
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P4", ChannelId = "01", SRange = 2, ERange = 19999, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, },
                                      },
                    CollectionTypeVerifyPeriod = new List<CollectionTypeVerifyPeriodModel>() {
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6P4", ChannelId="01", PayPeriodType= PayPeriodType.NDay_A },
                                      },
                });
                CheckIsOverlap();
            }
            /// <summary>
            /// 收款區間重疊時-起外疊
            /// </summary>
            [Test]
            public void E_CheckIsOverlap_C()
            {
                Repo.Create(new CollectionTypeSet()
                {
                    CollectionType = new CollectionTypeModel() { CollectionTypeId = "6P5", CollectionTypeName = "超商內扣", ChargePayType = ChargePayType.Deduction, },
                    CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P5", ChannelId = "01", SRange = 1, ERange = 20000, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, },
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P5", ChannelId = "01", SRange = 0, ERange = 19999, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, },
                                      },
                    CollectionTypeVerifyPeriod = new List<CollectionTypeVerifyPeriodModel>() {
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6P5", ChannelId="01", PayPeriodType= PayPeriodType.NDay_A },
                                      },
                });
                CheckIsOverlap();
            }
            /// <summary>
            /// 收款區間重疊時-迄外疊
            /// </summary>
            [Test]
            public void E_CheckIsOverlap_D()
            {
                Repo.Create(new CollectionTypeSet()
                {
                    CollectionType = new CollectionTypeModel() { CollectionTypeId = "6P6", CollectionTypeName = "超商內扣", ChargePayType = ChargePayType.Deduction, },
                    CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P6", ChannelId = "01", SRange = 1, ERange = 20000, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, },
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P6", ChannelId = "01", SRange = 2, ERange = 20001, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, },
                                      },
                    CollectionTypeVerifyPeriod = new List<CollectionTypeVerifyPeriodModel>() {
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6P6", ChannelId="01", PayPeriodType= PayPeriodType.NDay_A },
                                      },
                });
                CheckIsOverlap();
            }
            /// <summary>
            /// 通路尚未填寫核銷規則時
            /// </summary>
            [Test]
            public void E_CheckChannelVerifyPeriod_A()
            {
                Repo.Create(new CollectionTypeSet()
                {
                    CollectionType = new CollectionTypeModel() { CollectionTypeId = "6P7", CollectionTypeName = "超商內扣", ChargePayType = ChargePayType.Deduction, },
                    CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6P7", ChannelId = "01", SRange = 1, ERange = 20000, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, },
                                      },
                });
                CheckChannelVerifyPeriod();
            }
        }
        #endregion

        #region Update
        private class Update : CollectionTypeUnitTest
        {
            /// <summary>
            /// 超商代收類別-正常資料
            /// </summary>
            [Test]
            public void C_CorrectData_MartA()
            {
                CollectionTypeSet expectData = new CollectionTypeSet()
                {
                    CollectionType = new CollectionTypeModel(),
                    CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                    },
                    CollectionTypeVerifyPeriod = new List<CollectionTypeVerifyPeriodModel>() {
                        new CollectionTypeVerifyPeriodModel() { ChannelId="01" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="02" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="03" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="04" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="05" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="12" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="13" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="14" },
                    },
                },
                                  actualData = Repo.Update(new object[] { "6V1" }, new CollectionTypeSet()
                                  {
                                      CollectionType = new CollectionTypeModel() { CollectionTypeId = "6V1", CollectionTypeName = "超商內扣", ChargePayType = ChargePayType.Deduction, },
                                      CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6V1", ChannelId = "05", SRange = 1, ERange = 20000, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, RowState= RowState.Insert },
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6V1", ChannelId = "02", SRange = 1, ERange = 20000, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, RowId=2,RowState= RowState.Update },
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6V1", ChannelId = "03", SRange = 1, ERange = 20000, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, RowId=3,RowState= RowState.Delete },
                                      },
                                      CollectionTypeVerifyPeriod = new List<CollectionTypeVerifyPeriodModel>() {
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6V1", ChannelId="05", PayPeriodType= PayPeriodType.NDay_A, RowState= RowState.Insert },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6V1", ChannelId="01", PayPeriodType= PayPeriodType.NDay_A },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6V1", ChannelId="02", PayPeriodType= PayPeriodType.NDay_B },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6V1", ChannelId="03", PayPeriodType= PayPeriodType.NDay_C },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6V1", ChannelId="04", PayPeriodType= PayPeriodType.TenDay },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6V1", ChannelId="12", PayPeriodType= PayPeriodType.Week },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6V1", ChannelId="13", PayPeriodType= PayPeriodType.NDay_A },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6V1", ChannelId="14", PayPeriodType= PayPeriodType.NDay_B },
                                      },
                                  });
                AssertCorrectData(expectData, actualData);
            }

            /// <summary>
            /// 超商代收類別-正常資料
            /// </summary>
            [Test]
            public void C_CorrectData_MartA_100Time()
            {
                CollectionTypeSet expectData = new CollectionTypeSet()
                {
                    CollectionType = new CollectionTypeModel(),
                    CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                        new CollectionTypeDetailModel() { ChannelTotalFee=10 },
                    },
                    CollectionTypeVerifyPeriod = new List<CollectionTypeVerifyPeriodModel>() {
                        new CollectionTypeVerifyPeriodModel() { ChannelId="01" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="02" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="03" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="04" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="05" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="12" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="13" },
                        new CollectionTypeVerifyPeriodModel() { ChannelId="14" },
                    },
                };
                Stopwatch sw = new Stopwatch();
                for (int i = 0; i < 5000; i++)
                {
                    var c = new CollectionTypeSet()
                    {
                        CollectionType = new CollectionTypeModel() { CollectionTypeId = "6V1", CollectionTypeName = "超商內扣", ChargePayType = ChargePayType.Deduction, },
                        CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                          //new CollectionTypeDetailModel() { CollectionTypeId = "6V1", ChannelId = "05", SRange = 1, ERange = 20000, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, RowState= RowState.Insert },
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6V1", ChannelId = "02", SRange = 1, ERange = 20000, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, RowId=2,RowState= RowState.Update },
                                          new CollectionTypeDetailModel() { CollectionTypeId = "6V1", ChannelId = "03", SRange = 1, ERange = 20000, ChannelFee = 10, ChannelFeedBackFee = 0, ChannelRebateFee = 0, RowId=3,RowState= RowState.Delete },
                                          },
                        CollectionTypeVerifyPeriod = new List<CollectionTypeVerifyPeriodModel>() {
                                      //new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6V1", ChannelId="05", PayPeriodType= PayPeriodType.NDay_A, RowState= RowState.Insert },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6V1", ChannelId="01", PayPeriodType= PayPeriodType.NDay_A },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6V1", ChannelId="02", PayPeriodType= PayPeriodType.NDay_B },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6V1", ChannelId="03", PayPeriodType= PayPeriodType.NDay_C },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6V1", ChannelId="04", PayPeriodType= PayPeriodType.TenDay },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6V1", ChannelId="12", PayPeriodType= PayPeriodType.Week },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6V1", ChannelId="13", PayPeriodType= PayPeriodType.NDay_A },
                                      new CollectionTypeVerifyPeriodModel(){ CollectionTypeId="6V1", ChannelId="14", PayPeriodType= PayPeriodType.NDay_B },
                                          },
                    };
                    if (i != 0) sw.Start();
                    Repo.Update(new object[] { "6V1" }, c);
                    if (i != 0) sw.Stop();
                    //AssertCorrectData(expectData, actualData);
                    Console.WriteLine($"Time={sw.ElapsedMilliseconds:n0}ms");
                }
            }
        }
        #endregion

        #region Delete
        private class Delete : CollectionTypeUnitTest
        {

        }
        #endregion

        #region Invalid
        private class Invalid : CollectionTypeUnitTest
        {

        }
        #endregion

        #region EndCase
        private class EndCase : CollectionTypeUnitTest
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
        private void AssertCorrectData(CollectionTypeSet expectData, CollectionTypeSet actualData, bool isCheckInput = true)
        {
            Comm.CheckInputValueIsNull(actualData);
            AssertSet(expectData, actualData);
            AssertMessage();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectData"></param>
        /// <param name="actualData"></param>
        private void AssertSet(CollectionTypeSet expectData, CollectionTypeSet actualData)
        {
            //檢查通路總手續費是否計算錯誤
            for (int i = 0; i < expectData.CollectionTypeDetail.Count; i++)
                Assert.AreEqual(expectData.CollectionTypeDetail[i].ChannelTotalFee, actualData.CollectionTypeDetail[i].ChannelTotalFee);
            //檢查是否有多餘的代收通路
            foreach (var data in actualData.CollectionTypeVerifyPeriod)
                Assert.Contains(data.ChannelId, expectData.CollectionTypeVerifyPeriod.Select(p => p.ChannelId).ToList());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageLog"></param>
        private void AssertMessage()
        {
            Comm.SpecAssertMessage(Repo.Message.MsgCodeList, MessageCode.Code1013, false);
            Comm.SpecAssertMessage(Repo.Message.MsgCodeList, MessageCode.Code1014, false);
        }
        #endregion

        #region ErrorData
        private void CheckIsOverlap()
        {
            Comm.SpecAssertMessage(Repo.Message.MsgCodeList, MessageCode.Code1013, true);
        }
        private void CheckChannelVerifyPeriod()
        {
            Comm.SpecAssertMessage(Repo.Message.MsgCodeList, MessageCode.Code1014, true);
        }
        #endregion

        #endregion
    }
}
