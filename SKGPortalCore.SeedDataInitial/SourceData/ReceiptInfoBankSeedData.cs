using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SKGPortalCore.Data;
using SKGPortalCore.Model.SourceData;

namespace SKGPortalCore.SeedDataInitial.SourceData
{
    public static class ReceiptInfoBankSeedData
    {
        /// <summary>
        /// 資訊流-銀行
        /// </summary>
        public static List<ReceiptInfoBillBankModel> ReceiptInfoBankData(SysMessageLog Message)
        {
            List<ReceiptInfoBillBankModel> banks = new List<ReceiptInfoBillBankModel>() {
                new ReceiptInfoBillBankModel() { CompareCode = "0000992086100001", PN = "+", Amount = "10",Channel = "00" },
                new ReceiptInfoBillBankModel() { CompareCode = "0000992086100001", PN = "+", Amount = "10",Channel = "00" },
                new ReceiptInfoBillBankModel() { CompareCode = "0000992086100001", PN = "+", Amount = "10",Channel = "00" },
                new ReceiptInfoBillBankModel() { CompareCode = "0000992086100001", PN = "+", Amount = "10",Channel = "00" },
                new ReceiptInfoBillBankModel() { CompareCode = "0000992086100001", PN = "+", Amount = "10",Channel = "00" },
                new ReceiptInfoBillBankModel() { CompareCode = "0000992086100001", PN = "+", Amount = "10",Channel = "00" },
            };

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
