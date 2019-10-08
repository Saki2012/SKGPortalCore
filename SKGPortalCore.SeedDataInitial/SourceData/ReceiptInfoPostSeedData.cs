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
        public static List<ReceiptInfoBillPostModel> ReceiptInfoPostData(MessageLog Message)
        {
            List<ReceiptInfoBillPostModel> posts = new List<ReceiptInfoBillPostModel>() {
                new ReceiptInfoBillPostModel() { CollectionType="50084884", TradeDate="1080910", Branch="004596", Channel="05", TradeSer="0000001", PN="+", Amount="6000", CompareCode="500848848879",  Empty="" },
                //new ReceiptInfoBillPostModel() { CollectionType="", TradeDate="", Branch="", Channel="", TradeSer="", PN="", Amount="", CompareCode="", Empty="" },
                //new ReceiptInfoBillPostModel() { CollectionType="", TradeDate="", Branch="", Channel="", TradeSer="", PN="", Amount="", CompareCode="", Empty="" },
            };
            bool err = false;
            posts.ForEach(p => { if (p.Source != new ReceiptInfoBillPostModel() { Source = p.Source }.Source) { err = true; return; } });
            if (err) { Message.AddErrorMessage(MessageCode.Code0000, "資訊流-郵局Source拆分組合異常"); return null; }
            using StreamWriter sw = new StreamWriter($@"D:\ibankRoot\Ftp_SKGPortalCore\TransactionListDaily\SKG_POST.{DateTime.Now.ToString("yyyyMMdd")}", false);
            posts.ForEach(p => sw.WriteLine(p.Source));
            sw.Close();
            return posts;
        }
    }
}
