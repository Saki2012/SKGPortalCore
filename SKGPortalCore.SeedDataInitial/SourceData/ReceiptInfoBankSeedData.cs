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
        public static List<ReceiptInfoBillBankModel> ReceiptInfoBankData(MessageLog Message)
        {
            List<ReceiptInfoBillBankModel> banks = new List<ReceiptInfoBillBankModel>() {
                new ReceiptInfoBillBankModel() { RealAccount="770259918032",  TradeDate="20190910",  TradeTime="000000",  CompareCode="7891237419638525",  PN="+",  Amount="5000",  Summary="銀行通路",  Branch="0499",  TradeChannel="SA",  Channel="00",  ChangeDate="20190910",  BizDate="20190910",  Serial="000001",  CustomerCode="33458902",  Fee="000",  Empty=""  },
                //new ReceiptInfoBillBankModel() { RealAccount="",  TradeDate="",  TradeTime="",  CompareCode="",  PN="",  Amount="",  Summary="",  Branch="",  TradeChannel="",  Channel="",  ChangeDate="",  BizDate="",  Serial="",  CustomerCode="",  Fee="",  Empty="" },
                //new ReceiptInfoBillBankModel() { RealAccount="",  TradeDate="",  TradeTime="",  CompareCode="",  PN="",  Amount="",  Summary="",  Branch="",  TradeChannel="",  Channel="",  ChangeDate="",  BizDate="",  Serial="",  CustomerCode="",  Fee="",  Empty="" },
            };
            bool err = false;
            banks.ForEach(p => { if (p.Source != new ReceiptInfoBillBankModel() { Source = p.Source }.Src) { err = true; return; } });
            if (err) { Message.AddErrorMessage(MessageCode.Code0000, "資訊流-銀行Source拆分組合異常"); return null; }
            using StreamWriter sw = new StreamWriter($@"D:\ibankRoot\Ftp_SKGPortalCore\TransactionListDaily\SKG_BANK.{DateTime.Now.ToString("yyyyMMdd")}", false);
            banks.ForEach(p => sw.WriteLine(p.Source));
            sw.Close();
            return banks;
        }
    }
}
