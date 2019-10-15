using System;
using System.Collections.Generic;
using System.Text;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Repository.MasterData;

namespace SKGPortalCore.SeedDataInitial.MasterData
{
    public class ChannelVerifyPeriodSeedData
    {
        public static void CreateChannelVerifyPeriod(MessageLog Message, ApplicationDbContext dataAccess)
        {
            try
            {
                Message.Prefix = "新增「通路核銷週期」-初始資料：";
                using ChannelVerifyPeriodRepository repo = new ChannelVerifyPeriodRepository(dataAccess) { User = SystemOperator.SysOperator, Message = Message };
                List<ChannelVerifyPeriodSet> periods = new List<ChannelVerifyPeriodSet>()
                                                                  {
                                                                  new ChannelVerifyPeriodSet(){ ChannelVerifyPeriod=new ChannelVerifyPeriodModel(){ChannelId="00" , CollectionTypeId="Bank999" } },
                                                                  };
                foreach (ChannelVerifyPeriodSet period in periods)
                {
                    if (null == repo.QueryData(new[] { period.ChannelVerifyPeriod.ChannelId, period.ChannelVerifyPeriod.CollectionTypeId }))
                    {
                        repo.Create(period);
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
