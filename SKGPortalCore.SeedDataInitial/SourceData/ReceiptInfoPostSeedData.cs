using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SKGPortalCore.Core;
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
                new ReceiptInfoBillPostModel() { Channel="0587",Amount="1331",CompareCode="0009920868538462" },
                new ReceiptInfoBillPostModel() { Channel="0587",Amount="2222",CompareCode="0009935862576194" },
                new ReceiptInfoBillPostModel() { Channel="0587",Amount="3313",CompareCode="0080587749729183" },
                new ReceiptInfoBillPostModel() { Channel="0587",Amount="4124",CompareCode="2143751953183729" },
                new ReceiptInfoBillPostModel() { Channel="0587",Amount="5565",CompareCode="0000992086100001" },
                new ReceiptInfoBillPostModel() { Channel="0587",Amount="6236",CompareCode="0009935862576194" },
                new ReceiptInfoBillPostModel() { Channel="0587",Amount="767",CompareCode="2143751953183729" },
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
