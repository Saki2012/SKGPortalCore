using NUnit.Framework;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Repository.MasterData;
using System.Collections.Generic;

namespace SKGPortalCore.NUnit.MasterData.A_UnitTest
{
    public class BillTermUnitTest
    {
        #region Property
        ApplicationDbContext DataAccess;
        BillTermRepository Repo;
        [OneTimeSetUp]
        public void CreateDataAccess()
        {
            DataAccess = LibDataAccess.CreateDataAccess();
        }
        [SetUp]
        public void Setup()
        {
            Repo = new BillTermRepository(DataAccess) { User = SystemOperator.SysOperator };
        }
        #endregion

        #region Test

        #region Create
        private class Create : BillTermUnitTest
        {
            /// <summary>
            /// 
            /// </summary>
            [Test]
            public void C_CorrectData_A()
            {
                BillTermSet expectData = new BillTermSet() { },
                            actualData = Repo.Create(new BillTermSet() { });
                AssertCorrectData(expectData, actualData);
            }
            [Test]
            public void E_CheckTermNo_A()
            {
                Repo.Create(new BillTermSet() { });
                CheckTermNo();
            }
            [Test]
            public void E_CheckTermNoLen_A()
            {
                CheckTermNoLen();
            }
            [Test]
            public void E_CheckTermNoExist_A()
            {
                CheckTermNoExist();
            }
        }
        #endregion

        #region Update
        private class Update : BillTermUnitTest
        {

        }
        #endregion

        #region Delete
        private class Delete : BillTermUnitTest
        {

        }
        #endregion

        #region Invalid
        private class Invalid : BillTermUnitTest
        {

        }
        #endregion

        #region EndCase
        private class EndCase : BillTermUnitTest
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
        private void AssertCorrectData(BillTermSet expectData, BillTermSet actualData)
        {
            AssertSet(expectData, actualData);
            AssertMessage();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectData"></param>
        /// <param name="actualData"></param>
        private void AssertSet(BillTermSet expectData, BillTermSet actualData)
        {
            //Assert.AreEqual(testData.BillTerm.BillTermId, result.BillTerm.BillTermId, $"{ResxManage.GetDescription<BillTermSet>(p => p.BillTerm)} ���G���@�P");
            Assert.AreEqual(expectData.BillTermDetail, actualData.BillTermDetail, $"{ResxManage.GetDescription<BillTermSet>(p => p.BillTermDetail)} ���G���@�P");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageLog"></param>
        private void AssertMessage()
        {
            Comm.SpecAssertMessage(Repo.Message.MsgCodeList, MessageCode.Code1005, false);
            Comm.SpecAssertMessage(Repo.Message.MsgCodeList, MessageCode.Code1008, false);
            Comm.SpecAssertMessage(Repo.Message.MsgCodeList, MessageCode.Code1006, false);
        }
        #endregion

        #region ErrorData
        private void CheckTermNo()
        {
            Comm.SpecAssertMessage(Repo.Message.MsgCodeList, MessageCode.Code1006, true);
        }
        private void CheckTermNoLen()
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