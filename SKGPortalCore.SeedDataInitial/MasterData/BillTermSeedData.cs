using System;
using System.Collections.Generic;
using System.Text;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Repository.MasterData;

namespace SKGPortalCore.SeedDataInitial.MasterData
{
    public static class BillTermSeedData
    {
        /// <summary>
        /// 新增「期別」-初始資料
        /// </summary>
        /// <param name="db"></param>
        public static void CreateBillTerm(SysMessageLog Message, ApplicationDbContext dataAccess)
        {
            try
            {
                Message.Prefix = "新增「期別」-初始資料：";
                using BillTermRepository repo = new BillTermRepository(dataAccess) { User = SystemOperator.SysOperator, Message = Message };
                List<BillTermSet> billTerms = new List<BillTermSet>()
                {
                    new BillTermSet(){ BillTerm=new BillTermModel(){ CustomerCode="992086", BillTermId="100", BillTermName="2020年3月管理費",BillTermNo="853",  },
                        BillTermDetail=new List<BillTermDetailModel>(){
                        new BillTermDetailModel() { CustomerCode = "992086", BillTermId = "100", FeeName = "停車費", IsDeduction = false },
                        new BillTermDetailModel() { CustomerCode = "992086", BillTermId = "100", FeeName = "管理費", IsDeduction = true }
                     }
                    },
                    new BillTermSet(){ BillTerm=new BillTermModel(){ CustomerCode="2143", BillTermId="101", BillTermName="2020年4月管理費",BillTermNo="751953",  },
                        BillTermDetail=new List<BillTermDetailModel>(){
                        new BillTermDetailModel() { CustomerCode = "2143", BillTermId = "101", FeeName = "停車費", IsDeduction = false },
                        new BillTermDetailModel() { CustomerCode = "2143", BillTermId = "101", FeeName = "管理費", IsDeduction = true }
                     }
                    },
                    new BillTermSet(){ BillTerm=new BillTermModel(){ CustomerCode="805", BillTermId="102", BillTermName="2020年5月管理費",BillTermNo="87749",  },
                        BillTermDetail=new List<BillTermDetailModel>(){
                        new BillTermDetailModel() { CustomerCode = "805", BillTermId = "102", FeeName = "停車費", IsDeduction = false },
                        new BillTermDetailModel() { CustomerCode = "805", BillTermId = "102", FeeName = "管理費", IsDeduction = true }
                     }
                    },
                    new BillTermSet(){ BillTerm=new BillTermModel(){ CustomerCode="993586", BillTermId="103", BillTermName="2020年6月管理費",BillTermNo="25",  },
                        BillTermDetail=new List<BillTermDetailModel>(){
                        new BillTermDetailModel() { CustomerCode = "993586", BillTermId = "103", FeeName = "停車費", IsDeduction = false },
                        new BillTermDetailModel() { CustomerCode = "993586", BillTermId = "103", FeeName = "管理費", IsDeduction = true }
                     }
                    },
                };

                billTerms.ForEach(set =>
                {
                    if (null == repo.QueryData(new[] { set.BillTerm.CustomerCode, set.BillTerm.BillTermId }))
                    {
                        repo.Create(set);
                    }
                });
            }
            finally
            {
                Message.Prefix = string.Empty;
            }
        }
    }
}
