using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SKGPortalCore.Data;
using SKGPortalCore.Model.SourceData;

namespace SKGPortalCore.SeedDataInitial.SourceData
{
    public class ReceiptInfoPostSeedData
    {
        /// <summary>
        /// 資訊流-郵局
        /// </summary>
        public static List<ReceiptInfoBillPostModel> ReceiptInfoPostData(SysMessageLog Message)
        {
            List<ReceiptInfoBillPostModel> posts = new List<ReceiptInfoBillPostModel>() {
                new ReceiptInfoBillPostModel() {TradeDate="" ,Channel="",Amount="",CompareCode=""},
                new ReceiptInfoBillPostModel() {TradeDate="" ,Channel="",Amount="",CompareCode=""},
                new ReceiptInfoBillPostModel() {TradeDate="" ,Channel="",Amount="",CompareCode=""},
                new ReceiptInfoBillPostModel() {TradeDate="" ,Channel="",Amount="",CompareCode=""},
                new ReceiptInfoBillPostModel() {TradeDate="" ,Channel="",Amount="",CompareCode=""},
                new ReceiptInfoBillPostModel() {TradeDate="" ,Channel="",Amount="",CompareCode=""},
                new ReceiptInfoBillPostModel() {TradeDate="" ,Channel="",Amount="",CompareCode=""},
            };
            bool err = false;
            posts.ForEach(p => { if (p.Source != new ReceiptInfoBillPostModel() { Source = p.Source }.Source) { err = true; return; } });
            if (err) { Message.AddCustErrorMessage(MessageCode.Code0000, "資訊流-郵局Source拆分組合異常"); return null; }
            string path = $@"D:\ibankRoot\Ftp_SKGPortalCore\TransactionListDaily\"; Directory.CreateDirectory(path);
            using StreamWriter sw = new StreamWriter($@"{path}SKG_POST.{DateTime.Now.ToString("yyyyMMdd")}", false, Encoding.GetEncoding(950));
            posts.ForEach(p => sw.WriteLine(p.Source));
            sw.Close();
            return posts;
        }
    }
}
