using System;
using System.Collections.Generic;
using System.Text;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Model.System;
using SKGPortalCore.Repository.MasterData;

namespace SKGPortalCore.SeedDataInitial.MasterData
{
    public static class PayerSeedData
    {
        /// <summary>
        /// 新增「繳款人」-初始資料
        /// </summary>
        /// <param name="db"></param>
        public static void CreatePayer(SysMessageLog Message, ApplicationDbContext dataAccess)
        {
            try
            {
                Message.Prefix = "新增「繳款人」-初始資料：";
                using PayerRepository repo = new PayerRepository(dataAccess) { User = SystemOperator.SysOperator, Message = Message };
                List<PayerSet> payers = new List<PayerSet>()
                {
                    new PayerSet(){ Payer=new PayerModel(){CustomerCode="992086",PayerId="1", PayerName="測試繳款人1", PayerType= PayerType.Normal, PayerNo="8462" } },
                    new PayerSet(){ Payer=new PayerModel(){CustomerCode="2143",PayerId="1", PayerName="測試繳款人2", PayerType= PayerType.Normal, PayerNo="183729" } },
                    new PayerSet(){ Payer=new PayerModel(){CustomerCode="805",PayerId="1", PayerName="測試繳款人3", PayerType= PayerType.Normal, PayerNo="729183" } },
                    new PayerSet(){ Payer=new PayerModel(){CustomerCode="993586",PayerId="1", PayerName="測試繳款人4", PayerType= PayerType.Normal, PayerNo="76194" } },
                };
                payers.ForEach(payer =>
                {
                    if (null == repo.QueryData(new[] { payer.Payer.CustomerCode, payer.Payer.PayerId })) repo.Create(payer);
                });
                repo.CommitData(FuncAction.Create);
            }
            finally
            {
                Message.Prefix = string.Empty;
            }
        }
    }
}
