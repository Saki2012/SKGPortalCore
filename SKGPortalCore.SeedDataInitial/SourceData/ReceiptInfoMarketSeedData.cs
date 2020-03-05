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
                new ReceiptInfoBillMarketModel() { CollectionType ="6V1",Channel="7111111", Barcode2="0009920868538462", Barcode3="125" },
                new ReceiptInfoBillMarketModel() { CollectionType ="6V1",Channel="TFM", Barcode2="0009935862576194", Barcode3="2130" },
                new ReceiptInfoBillMarketModel() { CollectionType ="6V1",Channel="OKM", Barcode2="0080587749729183", Barcode3="255" },
                new ReceiptInfoBillMarketModel() { CollectionType ="6V1",Channel="HILIFE", Barcode2="2143751953183729", Barcode3="320" },
                new ReceiptInfoBillMarketModel() { CollectionType ="6V1",Channel="AGRI", Barcode2="0009920868538462", Barcode3="351" },
                new ReceiptInfoBillMarketModel() { CollectionType ="6V1",Channel="GT", Barcode2="0009935862576194", Barcode3="405" },
                new ReceiptInfoBillMarketModel() { CollectionType ="62N",Channel="OKM", Barcode2="0000992086100001", Barcode3="457" },
            });

            marts.AddRange(new List<ReceiptInfoBillMarketSPIModel>()
            {
                new ReceiptInfoBillMarketSPIModel(){ Channel = "7111111", Barcode2="0009920868538462", Barcode3_Amount="15" },
                new ReceiptInfoBillMarketSPIModel(){ Channel = "TFM", Barcode2="0009935862576194", Barcode3_Amount="20" },
                new ReceiptInfoBillMarketSPIModel(){ Channel = "OKM", Barcode2="0080587749729183", Barcode3_Amount="25" },
                new ReceiptInfoBillMarketSPIModel(){ Channel = "HILIFE", Barcode2="2143751953183729", Barcode3_Amount="30" },
                new ReceiptInfoBillMarketSPIModel(){ Channel = "AGRI", Barcode2="0009920868538462", Barcode3_Amount="35" },
                new ReceiptInfoBillMarketSPIModel(){ Channel = "GT", Barcode2="0009935862576194", Barcode3_Amount="40" },
                new ReceiptInfoBillMarketSPIModel(){ Channel = "SIM", Barcode2="0000992086100001", Barcode3_Amount="45" },
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
