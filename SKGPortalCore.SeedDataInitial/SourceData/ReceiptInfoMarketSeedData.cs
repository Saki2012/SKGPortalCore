using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SKGPortalCore.Data;
using SKGPortalCore.Model.SourceData;

namespace SKGPortalCore.SeedDataInitial.SourceData
{
    public class ReceiptInfoMarketSeedData
    {
        /// <summary>
        /// 資訊流-超商
        /// </summary>
        public static List<ReceiptInfoBillMarketModel> ReceiptInfoMarketData(SysMessageLog Message)
        {
            List<ReceiptInfoBillMarketModel> marts = new List<ReceiptInfoBillMarketModel>() {
                new ReceiptInfoBillMarketModel() { Idx = "2", CollectionType = "6V1", Channel = "01", Store = "02048888", TransAccount = "77855941", TransType = "336", PayStatus = "21", AccountingDay = "20190910", PayDate = "20190910", Barcode1 = "89858", Barcode2 = "78494", Barcode3 = "963852",  Empty="" },
                //new ReceiptInfoBillMarketModel() { Idx = "", CollectionType = "", Channel = "", Store = "", TransAccount = "", TransType = "", PayStatus = "", AccountingDay = "", PayDate = "", Barcode1 = "", Barcode2 = "", Barcode3 = "", Empty="" },
                //new ReceiptInfoBillMarketModel() { Idx = "", CollectionType = "", Channel = "", Store = "", TransAccount = "", TransType = "", PayStatus = "", AccountingDay = "", PayDate = "", Barcode1 = "", Barcode2 = "", Barcode3 = "", Empty="" },
            };
            bool err = false;
            marts.ForEach(p => { if (p.Source != new ReceiptInfoBillMarketModel() { Source = p.Source }.Source) { err = true; return; } });
            if (err) { Message.AddErrorMessage(MessageCode.Code0000, "資訊流-超商Source拆分組合異常"); return null; }
            string path = $@"D:\ibankRoot\Ftp_SKGPortalCore\TransactionListDaily\"; Directory.CreateDirectory(path);
            using StreamWriter sw = new StreamWriter($@"{path}SKG_MART.{DateTime.Now.ToString("yyyyMMdd")}", false, Encoding.GetEncoding(950));
            marts.ForEach(p => sw.WriteLine(p.Source));
            sw.Close();
            return marts;
        }
    }
}
