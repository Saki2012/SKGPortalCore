using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SKGPortalCore.Data;
using SKGPortalCore.Model.SourceData;

namespace SKGPortalCore.SeedDataInitial.SourceData
{
    public class ReceiptInfoFarmSeedData
    {
        /// <summary>
        /// 資訊流-農金
        /// </summary>
        public static List<ReceiptInfoBillFarmModel> ReceiptInfoFarmData(SysMessageLog Message)
        {
            List<ReceiptInfoBillFarmModel> farms = new List<ReceiptInfoBillFarmModel>() {
                new ReceiptInfoBillFarmModel() { Idx="2", CollectionType="6V1", Channel="05", Store="3", TransAccount="884212", TransType="1", PayStatus="1", AccountingDay="20190910", PayDate="20190910", Barcode1="85274", Barcode2="9639", Barcode3="789456",  Empty="" },
                //new ReceiptInfoBillFarmModel() { Idx="", CollectionType="", Channel="", Store="", TransAccount="", TransType="", PayStatus="", AccountingDay="", PayDate="", Barcode1="", Barcode2="", Barcode3="", Empty=""},
                //new ReceiptInfoBillFarmModel() { Idx="", CollectionType="", Channel="", Store="", TransAccount="", TransType="", PayStatus="", AccountingDay="", PayDate="", Barcode1="", Barcode2="", Barcode3="", Empty=""},
            };
            bool err = false;
            farms.ForEach(p => { if (p.Source != new ReceiptInfoBillFarmModel() { Source = p.Source }.Source) { err = true; return; } });
            if (err) { Message.AddCustErrorMessage(MessageCode.Code0000, "資訊流-農金Source拆分組合異常"); return null; }
            string path = $@"D:\ibankRoot\Ftp_SKGPortalCore\TransactionListDaily\"; Directory.CreateDirectory(path);
            using StreamWriter sw = new StreamWriter($@"{path}SKG_FARM.{DateTime.Now.ToString("yyyyMMdd")}", false, Encoding.GetEncoding(950));
            farms.ForEach(p => sw.WriteLine(p.Source));
            sw.Close();
            return farms;
        }
    }
}
