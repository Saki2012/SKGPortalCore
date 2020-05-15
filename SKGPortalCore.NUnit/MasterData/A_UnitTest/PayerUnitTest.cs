using NUnit.Framework;
using SKGPortalCore.Core;
using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Model.User;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Repository.MasterData;

namespace SKGPortalCore.NUnit.MasterData.A_UnitTest
{
    public class PayerUnitTest : BaseUnitTest
    {
        #region Property
        PayerRepository Repo;
        [SetUp]
        public void Setup()
        {
            Repo = new PayerRepository(DataAccess) { User = SystemOperator.SysOperator };
        }
        #endregion

        #region Test

        #region Create
        private class Create : PayerUnitTest
        {
            /// <summary>
            /// 
            /// </summary>
            [Test]
            public void C_CorrectData_A()
            {
                PayerSet expectData = new PayerSet() { },
                            actualData = Repo.Create(new PayerSet()
                            {
                                Payer = new PayerModel()
                                {
                                    CustomerCode = "992086",
                                    PayerName = "測試人員01",
                                    PayerNo = "8463",
                                    PayerType = PayerType.Normal,
                                }
                            });
                AssertCorrectData(expectData, actualData);
            }
            [Test]
            public void E_CheckPayerNoIsNotNum_A()
            {
                Repo.Create(new PayerSet()
                {
                    Payer = new PayerModel()
                    {
                        CustomerCode = "992086",
                        PayerName = "測試人員01",
                        PayerNo = "8cs3",
                        PayerType = PayerType.Normal,
                    }
                });
                CheckPayerNoIsNotNum();
            }
            [Test]
            public void E_CheckPayerNoLen_A()
            {
                Repo.Create(new PayerSet()
                {
                    Payer = new PayerModel()
                    {
                        CustomerCode = "992086",
                        PayerName = "測試人員01",
                        PayerNo = "3",
                        PayerType = PayerType.Normal,
                    }
                });
                CheckPayerNoLen();
            }
            [Test]
            public void E_CheckTermNoExist_A()
            {
                Repo.Create(new PayerSet()
                {
                    Payer = new PayerModel()
                    {
                        CustomerCode = "992086",
                        PayerName = "測試人員01",
                        PayerNo = "8462",
                        PayerType = PayerType.Normal,
                    }
                });
                CheckTermNoExist();
            }
        }
        #endregion

        #region Update
        private class Update : PayerUnitTest
        {

        }
        #endregion

        #region Delete
        private class Delete : PayerUnitTest
        {

        }
        #endregion

        #region Invalid
        private class Invalid : PayerUnitTest
        {

        }
        #endregion

        #region EndCase
        private class EndCase : PayerUnitTest
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
        private void AssertCorrectData(PayerSet expectData, PayerSet actualData)
        {
            AssertSet(expectData, actualData);
            AssertMessage();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectData"></param>
        /// <param name="actualData"></param>
        private void AssertSet(PayerSet expectData, PayerSet actualData)
        {
            //Assert.AreEqual(testData.BillTerm.BillTermId, result.BillTerm.BillTermId, $"{ResxManage.GetDescription<BillTermSet>(p => p.BillTerm)} 結果不一致");
            //Assert.AreEqual(expectData.BillTermDetail, actualData.BillTermDetail, $"{ResxManage.GetDescription<BillTermSet>(p => p.BillTermDetail)} 結果不一致");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageLog"></param>
        private void AssertMessage()
        {
            Comm.SpecAssertMessage(Repo.Message.MsgCodeList, MessageCode.Code1005, false);
            Comm.SpecAssertMessage(Repo.Message.MsgCodeList, MessageCode.Code1006, false);
            Comm.SpecAssertMessage(Repo.Message.MsgCodeList, MessageCode.Code1008, false);
        }
        #endregion

        #region ErrorData
        private void CheckPayerNoIsNotNum()
        {
            Comm.SpecAssertMessage(Repo.Message.MsgCodeList, MessageCode.Code1006, true);
        }
        private void CheckPayerNoLen()
        {
            Comm.SpecAssertMessage(Repo.Message.MsgCodeList, MessageCode.Code1005, true);
        }
        private void CheckTermNoExist()
        {
            Comm.SpecAssertMessage(Repo.Message.MsgCodeList, MessageCode.Code1008, true);
        }
        #endregion

        #endregion
    }
}
