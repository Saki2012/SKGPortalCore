using System;
using System.Collections.Generic;
using System.Text;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Repository.MasterData;

namespace SKGPortalCore.SeedDataInitial.MasterData
{
    public class BillTermSeedData
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
                    new BillTermSet(){ BillTerm=new BillTermModel(){CustomerCode="992086",BillTermId="100", BillTermName="測試期別C",BillTermNo="100",  }, BillTermDetail=new List<BillTermDetailModel>(){ 
                        new BillTermDetailModel() { CustomerCode = "992086", BillTermId = "100", FeeName = "費用01", IsDeduction = false }, 
                        new BillTermDetailModel() { CustomerCode = "992086", BillTermId = "100", FeeName = "費用02", IsDeduction = true } 
                     }
                    },
                };
                foreach (BillTermSet billTerm in billTerms)
                {
                    if (null == repo.QueryData(new[] { billTerm.BillTerm.CustomerCode, billTerm.BillTerm.BillTermId }))
                    {
                        repo.Create(billTerm);
                    }
                }
            }
            finally
            {
                Message.Prefix = string.Empty;
            }
        }
    }
}
