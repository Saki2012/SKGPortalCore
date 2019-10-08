using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SKGPortalCore.Data;
using SKGPortalCore.Model.SourceData;

namespace SKGPortalCore.SeedDataInitial.SourceData
{
    public class ReceiptInfoMarketSPISeedData
    {
        /// <summary>
        /// 資訊流-超商產險
        /// </summary>
        public static List<ReceiptInfoBillMarketSPIModel> ReceiptInfoMarketSPIData(MessageLog Message)
        {
            List<ReceiptInfoBillMarketSPIModel> martSPIs = new List<ReceiptInfoBillMarketSPIModel>() {
                new ReceiptInfoBillMarketSPIModel() { Idx="2", Channel="01", ISC="I0O", TransDate="20190910", PayDate="20190910", Barcode2="3000585", Barcode3_Date="1004", Barcode3_CompareCode="3", Barcode3_Amount="1420", Empty1="", Store="030", Empty2=""},
                //new ReceiptInfoBillMarketSPIModel() { Idx="", Channel="", ISC="", TransDate="", PayDate="", Barcode2="", Barcode3_Date="", Barcode3_CompareCode="", Barcode3_Amount="", Empty1="", Store="", Empty2=""},
                //new ReceiptInfoBillMarketSPIModel() { Idx="", Channel="", ISC="", TransDate="", PayDate="", Barcode2="", Barcode3_Date="", Barcode3_CompareCode="", Barcode3_Amount="", Empty1="", Store="", Empty2=""},
            };
            bool err = false;
            martSPIs.ForEach(p => { if (p.Source != new ReceiptInfoBillMarketSPIModel() { Source = p.Source }.Source) { err = true; return; } });
            if (err) { Message.AddErrorMessage(MessageCode.Code0000, "資訊流-超商產險Source拆分組合異常"); return null; }
            using StreamWriter sw = new StreamWriter($@"D:\ibankRoot\Ftp_SKGPortalCore\TransactionListDaily\SKG_MARTSPI.{DateTime.Now.ToString("yyyyMMdd")}", false);
            martSPIs.ForEach(p => sw.WriteLine(p.Source));
            sw.Close();
            return martSPIs;
        }
    }
}
