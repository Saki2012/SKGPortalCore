using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SKGPortalCore.Data;
using SKGPortalCore.Model.SourceData;

namespace SKGPortalCore.SeedDataInitial.SourceData
{
    public class RemitInfoSeedData
    {
        /// <summary>
        /// 匯款檔
        /// </summary>
        public static List<RemitInfoModel> RemitInfoData(SysMessageLog Message)
        {
            List<RemitInfoModel> rts = new List<RemitInfoModel>() {
                new RemitInfoModel() { RemitDate="20190910", RemitTime="145833", Channel="03", CollectionType="6V1", Amount="90000", BatchNo="03030",  Empty=""  },
                //new RemitInfoModel() { RemitDate="", RemitTime="", Channel="", CollectionType="", Amount="", BatchNo=""},
                //new RemitInfoModel() { RemitDate="", RemitTime="", Channel="", CollectionType="", Amount="", BatchNo=""},
            };
            bool err = false;
            rts.ForEach(p => { if (p.Source != new RemitInfoModel() { Source = p.Source }.Source) { err = true; return; } });
            if (err) { Message.AddCustErrorMessage(MessageCode.Code0000, "匯款檔Source拆分組合異常"); return null; }
            string path = $@"D:\ibankRoot\Ftp_SKGPortalCore\TransactionListDaily\"; Directory.CreateDirectory(path);
            using StreamWriter sw = new StreamWriter($@"{path}SKG_RT.{DateTime.Now.ToString("yyyyMMdd")}", false, Encoding.GetEncoding(950));
            rts.ForEach(p => sw.WriteLine(p.Source));
            sw.Close();
            return rts;
        }
    }
}
