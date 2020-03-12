using NUnit.Framework;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Repository.MasterData;
using System.Collections.Generic;

namespace SKGPortalCore.NUnit.MasterData
{
    //[SetUpFixture]
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

        #region Public

        #region Create
        public class Create : BillTermUnitTest
        {
            /// <summary>
            /// 
            /// </summary>
            [Test]
            public void CorrectData_A()
            {
                AssertCorrectData(new BillTermSet(), new BillTermSet());
            }
            [Test]
            public void CheckTermNo_A()
            {
                CheckTermNo();
            }
            [Test]
            public void CheckTermNoLen_A()
            {
                CheckTermNoLen();
            }
            [Test]
            public void CheckTermNoExist_A()
            {
                CheckTermNoExist();
            }
        }
        #endregion

        #region Update
        public class Update : BillTermUnitTest
        {

        }
        #endregion

        #region Delete
        public class Delete : BillTermUnitTest
        {

        }
        #endregion

        #region Invalid
        public class Invalid : BillTermUnitTest
        {

        }
        #endregion

        #region EndCase
        public class EndCase : BillTermUnitTest
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
        /// <param name="testData"></param>
        /// <param name="result"></param>
        private void AssertSet(BillTermSet testData, BillTermSet result)
        {
            //Assert.AreEqual(testData.BillTerm.BillTermId, result.BillTerm.BillTermId, $"{ResxManage.GetDescription<BillTermSet>(p => p.BillTerm)} 結果不一致");
            Assert.AreEqual(testData.BillTermDetail, result.BillTermDetail, $"{ResxManage.GetDescription<BillTermSet>(p => p.BillTermDetail)} 結果不一致");
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