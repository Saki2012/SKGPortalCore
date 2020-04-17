using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Repository.BillData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SKGPortalCore.NUnit.BillData.A_UnitTest
{
    public class BillUnitTest : BaseUnitTest
    {
        #region Property
        BillRepository Repo;
        [SetUp]
        public void Setup()
        {
            Repo = new BillRepository(DataAccess) { User = SystemOperator.SysOperator };
        }
        #endregion
        #region Test

        #region Create
        private class Create : BillUnitTest
        {
            /// <summary>
            /// 
            /// </summary>
            [Test]
            public void C_CorrectData_A()
            {
                for (int i = 0; i < 1000; i++)
                    Repo.QueryData(new[] { "Bill2020031700001" });
                //var a = DataAccess.Set<BillModel>().Where(p => p.BillNo == "Bill2020031700001")
                //    .Include("BillTerm").Select(p =>
                //    new BillModel()
                //    {
                //        PayAmount = p.PayAmount,
                //        BillNo = p.BillNo,
                //        BillTerm = new BillTermModel() { BillTermNo = p.BillTerm.BillTermNo },
                //        BillTermName = p.BillTerm.BillTermName,
                //    }
                //    );

                //var b = a.Expression;
                //var c = a.ToList();
                //var d = a.Cast<BillModel>().ToList();

            }
            [Test]
            public void C_CorrectData_B()
            {
                List<BillSet> sets = new List<BillSet>();
                for (int i = 0; i < 30000; i++)
                    sets.Add(new BillSet()
                    {
                        Bill = new BillModel() { BillTermId = "100", CustomerCode = "992086", PayerId = "1", ImportBatchNo = "BeginData", PayEndDate = DateTime.Now, CollectionTypeId = "6V1" },
                        BillDetail = new List<BillDetailModel>() { new BillDetailModel() { FeeName = "停車費", PayAmount = 2000 }, new BillDetailModel() { FeeName = "管理費", PayAmount = 75 } }
                    }
                   );
                Stopwatch sw = new Stopwatch();
                foreach (var set in sets)
                {
                    sw.Start();
                    Repo.Create(set);
                    sw.Stop();
                    Console.WriteLine($"Time={sw.ElapsedMilliseconds:n0}ms");
                }
                Repo.CommitData(Model.System.FuncAction.Create);
                //AssertCorrectData(expectData, actualData);
            }
            [Test]
            public void Test()
            {
                var repo = new ReceiptBillRepository(DataAccess) { User = SystemOperator.SysOperator };
                repo.ChannelTotalFeeRpt("30262944");
            }

        }
        #endregion

        #endregion
    }
}
