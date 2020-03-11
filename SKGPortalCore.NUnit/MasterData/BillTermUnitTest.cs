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
        #endregion
        #region Public
        [OneTimeSetUp]
        public void CreateDataAccess()
        {
            DataAccess = LibDataAccess.CreateDataAccess();
        }
        [SetUp]
        public void Setup()
        {
            //DataAccess = LibDataAccess.CreateDataAccess();
            Repo = new BillTermRepository(DataAccess) { User = SystemOperator.SysOperator };
        }

        [Test]
        public void Create()
        {
            BillTermSet testData = new BillTermSet() { BillTerm = new BillTermModel() { }, BillTermDetail = new List<BillTermDetailModel>() { new BillTermDetailModel() { } } };
            BillTermSet testData2 = new BillTermSet() { BillTerm = new BillTermModel() { }, BillTermDetail = new List<BillTermDetailModel>() { new BillTermDetailModel() { } } };
            //BillTermSet result = repo.Create(new BillTermSet());

            //Assert.Contains(repo.Message.AddCustErrorMessage)
            //Assert.AreEqual()
            AssertTable(testData, testData2);
        }
        [Test]
        public void Update()
        {
            BillTermSet testData = new BillTermSet() { BillTerm = new BillTermModel() { }, BillTermDetail = new List<BillTermDetailModel>() { new BillTermDetailModel() { } } };
            BillTermSet testData2 = new BillTermSet() { BillTerm = new BillTermModel() { }, BillTermDetail = new List<BillTermDetailModel>() { new BillTermDetailModel() { } } };
            //BillTermSet result = repo.Create(new BillTermSet());

            //Assert.Contains(repo.Message.AddCustErrorMessage)
            //Assert.AreEqual()
            AssertTable(testData, testData2);
        }
        [Test]
        public void Delete()
        {
            BillTermSet testData = new BillTermSet() { BillTerm = new BillTermModel() { }, BillTermDetail = new List<BillTermDetailModel>() { new BillTermDetailModel() { } } };
            BillTermSet testData2 = new BillTermSet() { BillTerm = new BillTermModel() { }, BillTermDetail = new List<BillTermDetailModel>() { new BillTermDetailModel() { } } };
            //BillTermSet result = repo.Create(new BillTermSet());

            //Assert.Contains(repo.Message.AddCustErrorMessage)
            //Assert.AreEqual()
            AssertTable(testData, testData2);
        }
        [Test]
        public void Invalid()
        {
            BillTermSet testData = new BillTermSet() { BillTerm = new BillTermModel() { }, BillTermDetail = new List<BillTermDetailModel>() { new BillTermDetailModel() { } } };
            BillTermSet testData2 = new BillTermSet() { BillTerm = new BillTermModel() { }, BillTermDetail = new List<BillTermDetailModel>() { new BillTermDetailModel() { } } };
            //BillTermSet result = repo.Create(new BillTermSet());

            //Assert.Contains(repo.Message.AddCustErrorMessage)
            //Assert.AreEqual()
            AssertTable(testData, testData2);
        }
        [Test]
        public void EndCase()
        {
            BillTermSet testData = new BillTermSet() { BillTerm = new BillTermModel() { }, BillTermDetail = new List<BillTermDetailModel>() { new BillTermDetailModel() { } } };
            BillTermSet testData2 = new BillTermSet() { BillTerm = new BillTermModel() { }, BillTermDetail = new List<BillTermDetailModel>() { new BillTermDetailModel() { } } };
            //BillTermSet result = repo.Create(new BillTermSet());

            //Assert.Contains(repo.Message.AddCustErrorMessage)
            //Assert.AreEqual()
            AssertTable(testData, testData2);
        }
        #endregion
        #region Private
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testData"></param>
        /// <param name="result"></param>
        private void AssertTable(BillTermSet testData, BillTermSet result)
        {
            //Assert.AreEqual(testData.BillTerm, result.BillTerm, $"{ResxManage.GetDescription<BillTermSet>(p => p.BillTerm)} 結果不一致");
            //Assert.AreEqual(testData.BillTermDetail, result.BillTermDetail, $"{ResxManage.GetDescription<BillTermSet>(p => p.BillTermDetail)} 結果不一致");
        }
        #endregion
    }
}