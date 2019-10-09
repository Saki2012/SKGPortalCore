using System;
using System.Collections.Generic;
using System.Text;
using SKGPortalCore.Data;
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
        public static void CreatePayer(MessageLog Message,ApplicationDbContext dataAccess)
        {
            try
            {
                Message.Prefix = "新增「繳款人」-初始資料：";
                using PayerRepository repo = new PayerRepository(dataAccess) { User = SystemOperator.SysOperator, Message = Message };
                List<PayerSet> payers = new List<PayerSet>()
                {
                    new PayerSet(){ Payer=new PayerModel(){CustomerId="33458902", PayerId="0001", PayerName="測試繳款人1", PayerType= Model.PayerType.Normal, PayerNo="1007", IDCard="F1233151847",Tel="0921447116",Address="平鎮",Memo="",CardNum="4478-1181-5547-9631", CardValidateMonth=12,CardValidateYear=23,CVV="225" } },
                    new PayerSet(){ Payer=new PayerModel(){CustomerId="80425514", PayerId="0001", PayerName="測試繳款人1", PayerType= Model.PayerType.Normal, PayerNo="1007", IDCard="F1233151847",Tel="0921447116",Address="平鎮",Memo="",CardNum="4478-1181-5547-9631", CardValidateMonth=12,CardValidateYear=23,CVV="225" } },
                    new PayerSet(){ Payer=new PayerModel(){CustomerId="91009206", PayerId="0001", PayerName="測試繳款人1", PayerType= Model.PayerType.Normal, PayerNo="1007", IDCard="F1233151847",Tel="0921447116",Address="平鎮",Memo="",CardNum="4478-1181-5547-9631", CardValidateMonth=12,CardValidateYear=23,CVV="225" } },
                };
                foreach (PayerSet payer in payers)
                {
                    if (null == repo.QueryData(new[] { payer.Payer.CustomerId, payer.Payer.PayerId }))
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
