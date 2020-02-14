using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SKGPortalCore.Data;
using SKGPortalCore.Model.SourceData;

namespace SKGPortalCore.SeedDataInitial.SourceData
{
    public class ReceiptInfoBankSeedData
    {
        /// <summary>
        /// 資訊流-銀行
        /// </summary>
        public static List<ReceiptInfoBillBankModel> ReceiptInfoBankData(SysMessageLog Message)
        {
            List<ReceiptInfoBillBankModel> banks = new List<ReceiptInfoBillBankModel>() {
                new ReceiptInfoBillBankModel() { RealAccount="770259918032",  TradeDate="20190910",  TradeTime="000000",  CompareCode="990128100000100",  PN="+",  Amount="5000",  Summary="銀行通路",  Branch="0499",  TradeChannel="SA",  Channel="00",  ChangeDate="20190910",  BizDate="20190910",  Serial="000001",  CustomerCode="990128",  Fee="000",  Empty=""  },
                new ReceiptInfoBillBankModel() { RealAccount="770259918032",  TradeDate="20190910",  TradeTime="000000",  CompareCode="1024010000010",  PN="+",  Amount="7000",  Summary="銀行通路",  Branch="0499",  TradeChannel="SA",  Channel="00",  ChangeDate="20190910",  BizDate="20190910",  Serial="000001",  CustomerCode="1024",  Fee="000",  Empty=""  },
                new ReceiptInfoBillBankModel() { RealAccount="770259918032",  TradeDate="20190910",  TradeTime="000000",  CompareCode="912001000001",  PN="+",  Amount="9000",  Summary="銀行通路",  Branch="0499",  TradeChannel="SA",  Channel="00",  ChangeDate="20190910",  BizDate="20190910",  Serial="000001",  CustomerCode="912",  Fee="000",  Empty=""  },
            };

            for (int i = 0; i < 10000; i++)
            {
                banks.Add(new ReceiptInfoBillBankModel()
                {
                    RealAccount = "770259918032",
                    TradeDate = "20190910",
                    TradeTime = "000000",
                    CompareCode = "912001000001",
                    PN = "+",
                    Amount = i.ToString(),
                    Summary = "銀行通路",
                    Branch = "0499",
                    TradeChannel = "SA",
                    Channel = "00",
                    ChangeDate = "20190910",
                    BizDate = "20190910",
                    Serial = "000001",
                    CustomerCode = "912",
                    Fee = "000",
                    Empty = ""
                });
            }

            bool err = false;
            banks.ForEach(p => { if (p.Source != new ReceiptInfoBillBankModel() { Source = p.Source }.Src) { err = true; return; } });
            if (err) { Message.AddCustErrorMessage(MessageCode.Code0000, "資訊流-銀行Source拆分組合異常"); return null; }
            string path = $@"D:\ibankRoot\Ftp_SKGPortalCore\TransactionListDaily\"; Directory.CreateDirectory(path);
            using StreamWriter sw = new StreamWriter($@"{path}SKG_BANK.{DateTime.Now.ToString("yyyyMMdd")}", false, Encoding.GetEncoding(950));
            banks.ForEach(p => sw.WriteLine(p.Source));
            sw.Close();
            return banks;
        }
    }
}
