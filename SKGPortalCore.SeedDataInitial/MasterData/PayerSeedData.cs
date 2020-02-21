using System;
using System.Collections.Generic;
using System.Text;
using SKGPortalCore.Data;
using SKGPortalCore.Model.Enum;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Repository.MasterData;

namespace SKGPortalCore.SeedDataInitial.MasterData
{
    public class PayerSeedData
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
                    new PayerSet(){ Payer=new PayerModel(){CustomerCode="992091",PayerId="000001", PayerName="測試繳款人1", PayerType= PayerType.Normal, PayerNo="000001" } },
                    new PayerSet(){ Payer=new PayerModel(){CustomerCode="992099",PayerId="000010", PayerName="測試繳款人2", PayerType= PayerType.Normal, PayerNo="000010" } },
                    new PayerSet(){ Payer=new PayerModel(){CustomerCode="992079",PayerId="000100", PayerName="測試繳款人3", PayerType= PayerType.Normal, PayerNo="000100" } },
                };
                foreach (PayerSet payer in payers)
                {
                    if (null == repo.QueryData(new[] { payer.Payer.CustomerCode, payer.Payer.PayerId }))
                    {
                        repo.Create(payer);
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
