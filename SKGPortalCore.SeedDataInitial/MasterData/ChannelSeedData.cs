using System;
using System.Collections.Generic;
using System.Text;
using SKGPortalCore.Data;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Repository.MasterData;

namespace SKGPortalCore.SeedDataInitial.MasterData
{
    public class ChannelSeedData
    {
        /// <summary>
        /// 新增「代收通路」-初始資料
        /// </summary>
        /// <param name="db"></param>
        public static void CreateChannel(MessageLog Message, ApplicationDbContext dataAccess)
        {
            try
            {
                Message.Prefix = "新增「代收通路」-初始資料：";
                using ChannelRepository repo = new ChannelRepository(dataAccess) { User = SystemOperator.SysOperator, Message = Message };
                List<ChannelSet> channels = new List<ChannelSet>() { new ChannelSet() { Channel = new ChannelModel(){ ChannelId="00", ChannelName="銀行臨櫃", ChannelType= CanalisType.Bank} },
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="01", ChannelName="7-11", ChannelType= CanalisType.Market}, ChannelMap = new List<ChannelMapModel>(){ new ChannelMapModel() { ChannelId = "01", TransCode = "7111111" } } },
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="02", ChannelName="全家", ChannelType= CanalisType.Market}, ChannelMap = new List<ChannelMapModel>(){new ChannelMapModel() { ChannelId = "02", TransCode = "TFM" } } },
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="03", ChannelName="OK", ChannelType= CanalisType.Market}, ChannelMap = new List<ChannelMapModel>(){new ChannelMapModel() { ChannelId = "03", TransCode = "OKM" } } },
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="04", ChannelName="萊爾富", ChannelType= CanalisType.Market}, ChannelMap = new List<ChannelMapModel>(){new ChannelMapModel() { ChannelId = "04", TransCode = "HILIFE" } } },
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="05", ChannelName="郵局臨櫃", ChannelType= CanalisType.Post}, ChannelMap = new List<ChannelMapModel>(){ new ChannelMapModel() { ChannelId = "05", TransCode = "0587" }, new ChannelMapModel() { ChannelId = "05", TransCode = "0588" }, new ChannelMapModel() { ChannelId = "05", TransCode = "0589" }, new ChannelMapModel() { ChannelId = "05", TransCode = "058A" }, new ChannelMapModel() { ChannelId = "05", TransCode = "0215" }, new ChannelMapModel() { ChannelId = "05", TransCode = "0216" }, new ChannelMapModel() { ChannelId = "05", TransCode = "0509" }, new ChannelMapModel() { ChannelId = "05", TransCode = "050A" }, new ChannelMapModel() { ChannelId = "05", TransCode = "050C" }, new ChannelMapModel() { ChannelId = "05", TransCode = "0516" }, new ChannelMapModel() { ChannelId = "05", TransCode = "0559" }, } },
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="06", ChannelName="自動化交易(ATM)", ChannelType= CanalisType.Bank}},
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="07", ChannelName="約定扣款(主機端)", ChannelType= CanalisType.Bank}},
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="09", ChannelName="票交所", ChannelType= CanalisType.Bank}},
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="10", ChannelName="中信平台繳學費", ChannelType= CanalisType.Bank}},
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="12", ChannelName="農業金庫", ChannelType= CanalisType.Farm}, ChannelMap = new List<ChannelMapModel>(){new ChannelMapModel() { ChannelId = "12", TransCode = "AGRI" } } },
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="A0", ChannelName="企業戶自收款", ChannelType= CanalisType.Self}},
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="A1", ChannelName="郵局網路平台", ChannelType= CanalisType.Post}, ChannelMap = new List<ChannelMapModel>(){ new ChannelMapModel() { ChannelId = "A1", TransCode = "A421" },new ChannelMapModel() { ChannelId = "A1", TransCode = "057W" } } },
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="A2", ChannelName="自動化交易(匯款)", ChannelType= CanalisType.Bank}},
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="A3", ChannelName="信用卡", ChannelType= CanalisType.Credit}},
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="A4", ChannelName="ACH扣款", ChannelType= CanalisType.Credit}},
                                                    new ChannelSet() { Channel = new ChannelModel(){ ChannelId="A5", ChannelName="約定扣款(平台端)", ChannelType= CanalisType.Credit}},
                                                    };
                foreach (ChannelSet channel in channels)
                {
                    if (null == repo.QueryData(new[] { channel.Channel.ChannelId }))
                    {
                        repo.Create(channel);
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
