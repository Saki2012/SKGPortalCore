using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SKGPortalCore.Data;
using SKGPortalCore.Model.SourceData;

namespace SKGPortalCore.SeedDataInitial.SourceData
{
    public static class ReceiptInfoMarketSeedData
    {
        /// <summary>
        /// 資訊流-超商
        /// </summary>
        public static List<dynamic> ReceiptInfoMarketData(SysMessageLog Message)
        {
            List<dynamic> marts = new List<dynamic>();

            marts.AddRange(new List<ReceiptInfoBillMarketModel>() {
                new ReceiptInfoBillMarketModel() { CollectionType ="",Channel="",AccountingDay="",PayDate="",Barcode2=""},
                new ReceiptInfoBillMarketModel() { CollectionType ="",Channel="",AccountingDay="",PayDate="",Barcode2=""},
                new ReceiptInfoBillMarketModel() { CollectionType ="",Channel="",AccountingDay="",PayDate="",Barcode2=""},
                new ReceiptInfoBillMarketModel() { CollectionType ="",Channel="",AccountingDay="",PayDate="",Barcode2=""},
                new ReceiptInfoBillMarketModel() { CollectionType ="",Channel="",AccountingDay="",PayDate="",Barcode2=""},
            });

            marts.AddRange(new List<ReceiptInfoBillMarketSPIModel>()
            {
                new ReceiptInfoBillMarketSPIModel(){ Channel ="", TransDate="",PayDate="",Barcode2="",Barcode3_CompareCode="",Barcode3_Amount=""},
                new ReceiptInfoBillMarketSPIModel(){ Channel ="", TransDate="",PayDate="",Barcode2="",Barcode3_CompareCode="",Barcode3_Amount=""},
                new ReceiptInfoBillMarketSPIModel(){ Channel ="", TransDate="",PayDate="",Barcode2="",Barcode3_CompareCode="",Barcode3_Amount=""},
                new ReceiptInfoBillMarketSPIModel(){ Channel ="", TransDate="",PayDate="",Barcode2="",Barcode3_CompareCode="",Barcode3_Amount=""},
                new ReceiptInfoBillMarketSPIModel(){ Channel ="", TransDate="",PayDate="",Barcode2="",Barcode3_CompareCode="",Barcode3_Amount=""},
            });

            bool err = false;
            marts.ForEach(p =>
            {
                if (p is ReceiptInfoBillMarketModel)
                { if (p.Source != new ReceiptInfoBillMarketModel() { Source = p.Source }.Source) { err = true; return; } }
                else
                { if (p.Source != new ReceiptInfoBillMarketSPIModel() { Source = p.Source }.Source) { err = true; return; } }
            });
            if (err) { Message.AddCustErrorMessage(MessageCode.Code0000, "資訊流-超商Source拆分組合異常"); return null; }
            string path = $@"D:\ibankRoot\Ftp_SKGPortalCore\TransactionListDaily\"; Directory.CreateDirectory(path);
            using StreamWriter sw = new StreamWriter($@"{path}SKG_MART.{DateTime.Now.ToString("yyyyMMdd")}", false, Encoding.GetEncoding(950));
            marts.ForEach(p => sw.WriteLine(p.Source));
            sw.Close();
            return marts;
        }
    }
}
