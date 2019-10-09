using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SKGPortalCore.Data;
using SKGPortalCore.Model.SourceData;

namespace SKGPortalCore.SeedDataInitial.SourceData
{
    public class ACCFTTSeedData
    {
        /// <summary>
        /// 服務申請書
        /// </summary>
        public static void ACCFTTData(MessageLog Message)
        {
            List<ACCFTT> accftts = new List<ACCFTT>() {
                //每筆總手續費-有分潤
                new ACCFTT() { KEYNO="912", ACCIDNO="7745251524762", CUSTNAME="每筆總手續費-有分潤", APPBECODE="0499", BRCODE="0499", IDCODE="33458902", APPLYDATE=DateTime.Now.ToString("yyyyMMdd"), CHGDATE=DateTime.Now.ToString("yyyyMMdd"), APPLYSTAT="0", CHKNUMFLAG="2", CHKAMTFLAG="N", DUETERM="0", CHANNEL="4", FEE="0", RSTORE1="1", RSTORE2="1", RSTORE3="1", RSTORE4="1", RECVITEM1="6V1", RECVITEM2="6V2", RECVITEM3="", RECVITEM4="", RECVITEM5="", ACTFEE="0", MARTFEE1="00", MARTFEE2="00", MARTFEE3="00", POSTFLAG="1", ACTFEEPT="0", POSTFEE="0", HIFLAG="0", HIFARE="000", NETDATE="00000000", AUTOFLAG="0", EBFLAG="0", EBDATE="00000000", EBFEEFLAG="1", EBFEE="0", EBACTTYPE="2", CHKDUPPAY="0", CUSTID="1234567", FUNC="0", MAFARE="0", NOFARE="0", CTBCFLAG="0", SHAREBNFTFLG="1", SHAREBEFTPERCENT="20", ACTFEEBEFT="50", ACTFEEMART="15", SHAREACTFLG="1", ACTPERCENT="50", CLEARFEEMART1="0", CLEARFEEMART2="0", CLEARFEEMART3="0", CLEARFEEMART4="0", CLEARFEEMART5="0", PAYKINDPOST="0", ACTFEEPOST="15", SHAREPOSTFLG="1", POSTPERCENT="50", AGRIFLAG="1", AGRIFEE="25",FILLER=""},
                new ACCFTT() { KEYNO="1024", ACCIDNO="4245251524762", CUSTNAME="每筆總手續費-無分潤", APPBECODE="0499", BRCODE="0499", IDCODE="80425514", APPLYDATE=DateTime.Now.ToString("yyyyMMdd"), CHGDATE=DateTime.Now.ToString("yyyyMMdd"), APPLYSTAT="0", CHKNUMFLAG="2", CHKAMTFLAG="N", DUETERM="0", CHANNEL="9", FEE="0", RSTORE1="1", RSTORE2="1", RSTORE3="1", RSTORE4="1", RECVITEM1="6V1", RECVITEM2="6V2", RECVITEM3="", RECVITEM4="", RECVITEM5="", ACTFEE="0", MARTFEE1="0", MARTFEE2="0", MARTFEE3="0", POSTFLAG="1", ACTFEEPT="0", POSTFEE="0", HIFLAG="1", HIFARE="8", NETDATE="00000000", AUTOFLAG="0", EBFLAG="0", EBDATE="00000000", EBFEEFLAG="1", EBFEE="0", EBACTTYPE="2", CHKDUPPAY="0", CUSTID="7654321", FUNC="0", MAFARE="0", NOFARE="0", CTBCFLAG="0", SHAREBNFTFLG="0", SHAREBEFTPERCENT="0", ACTFEEBEFT="35", ACTFEEMART="45", SHAREACTFLG="0", ACTPERCENT="0", CLEARFEEMART1="", CLEARFEEMART2="", CLEARFEEMART3="", CLEARFEEMART4="", CLEARFEEMART5="", PAYKINDPOST="0", ACTFEEPOST="40", SHAREPOSTFLG="0", POSTPERCENT="", AGRIFLAG="1", AGRIFEE="22",FILLER=""},
                new ACCFTT() { KEYNO="990128", ACCIDNO="9945251524762", CUSTNAME="清算手續費", APPBECODE="0499", BRCODE="0499", IDCODE="91009206", APPLYDATE=DateTime.Now.ToString("yyyyMMdd"), CHGDATE=DateTime.Now.ToString("yyyyMMdd"), APPLYSTAT="0", CHKNUMFLAG="2", CHKAMTFLAG="N", DUETERM="0", CHANNEL="9", FEE="20", RSTORE1="1", RSTORE2="1", RSTORE3="1", RSTORE4="1", RECVITEM1="6V1", RECVITEM2="6V2", RECVITEM3="", RECVITEM4="", RECVITEM5="", ACTFEE="21", MARTFEE1="0", MARTFEE2="0", MARTFEE3="0", POSTFLAG="1", ACTFEEPT="22", POSTFEE="0", HIFLAG="1", HIFARE="8", NETDATE="00000000", AUTOFLAG="0", EBFLAG="0", EBDATE="00000000", EBFEEFLAG="1", EBFEE="0", EBACTTYPE="2", CHKDUPPAY="0", CUSTID="7419635", FUNC="0", MAFARE="0", NOFARE="0", CTBCFLAG="0", SHAREBNFTFLG="1", SHAREBEFTPERCENT="0", ACTFEEBEFT="0", ACTFEEMART="0", SHAREACTFLG="0", ACTPERCENT="0", CLEARFEEMART1="", CLEARFEEMART2="", CLEARFEEMART3="", CLEARFEEMART4="", CLEARFEEMART5="", PAYKINDPOST="0", ACTFEEPOST="0", SHAREPOSTFLG="0", POSTPERCENT="", AGRIFLAG="1", AGRIFEE="11",FILLER=""},
            };
            bool err = false;
            accftts.ForEach(p => { if (p.Source != new ACCFTT() { Source = p.Source }.Source) { err = true; return; } });
            if (err) { Message.AddErrorMessage(MessageCode.Code0000, "服務申請書Source拆分組合異常"); return; }
            string path = $@"D:\ibankRoot\Ftp_SKGPortalCore\ACCFTT\"; Directory.CreateDirectory(path);
            using StreamWriter sw = new StreamWriter($@"{path}ACCFTT.{ DateTime.Now.ToString("yyyyMMdd")}", false, Encoding.GetEncoding(950));
            accftts.ForEach(p => sw.WriteLine(p.Source));
            sw.Close();
        }
    }
}
