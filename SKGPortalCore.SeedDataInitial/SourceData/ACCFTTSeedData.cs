﻿using System;
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
        public static void ACCFTTData(SysMessageLog Message)
        {
            List<ACCFTT> accftts = new List<ACCFTT>() {
                //每筆總手續費-有分潤
                new ACCFTT() { KEYNO="992091", ACCIDNO="0620101011288", CUSTNAME="每筆總手續費-有分潤", APPBECODE="0620", BRCODE="0620", IDCODE="53272487", APPLYDATE=DateTime.Now.ToString("yyyyMMdd"), CHGDATE="",                                APPLYSTAT="0", CHKNUMFLAG="Y", CHKAMTFLAG="N", DUETERM="0", CHANNEL="9", FEE="010", RSTORE1="0", RSTORE2="0", RSTORE3="0", RSTORE4="0", RECVITEM1="", RECVITEM2="", RECVITEM3="", RECVITEM4="", RECVITEM5="",          ACTFEE="00", MARTFEE1="00", MARTFEE2="00", MARTFEE3="00", POSTFLAG="0", ACTFEEPT="00", POSTFEE="00", HIFLAG="0", HIFARE="000", NETDATE="20190815", AUTOFLAG="0", EBFLAG="0", EBDATE="00000000", EBFEEFLAG="0", EBFEE="0", EBACTTYPE="2", CHKDUPPAY="0", CUSTID="1234567", FUNC="0", MAFARE="0", NOFARE="0", CTBCFLAG="0", SHAREBNFTFLG="1", SHAREBEFTPERCENT="20", ACTFEEBEFT="50", ACTFEEMART="15", SHAREACTFLG="1", ACTPERCENT="50", CLEARFEEMART1="0", CLEARFEEMART2="0", CLEARFEEMART3="0", CLEARFEEMART4="0", CLEARFEEMART5="0", PAYKINDPOST="0", ACTFEEPOST="15", SHAREPOSTFLG="1", POSTPERCENT="50", AGRIFLAG="1", AGRIFEE="25",FILLER=""},
                new ACCFTT() { KEYNO="992099", ACCIDNO="0204101004373", CUSTNAME="每筆總手續費-有分潤", APPBECODE="0204", BRCODE="0204", IDCODE="76336687", APPLYDATE=DateTime.Now.ToString("yyyyMMdd"), CHGDATE=DateTime.Now.ToString("yyyyMMdd"), APPLYSTAT="0", CHKNUMFLAG="Y", CHKAMTFLAG="N", DUETERM="0", CHANNEL="9", FEE="010", RSTORE1="1", RSTORE2="1", RSTORE3="1", RSTORE4="1", RECVITEM1="6V6", RECVITEM2="6V7", RECVITEM3="6RN", RECVITEM4="", RECVITEM5="", ACTFEE="10", MARTFEE1="00", MARTFEE2="00", MARTFEE3="00", POSTFLAG="1", ACTFEEPT="10", POSTFEE="00", HIFLAG="0", HIFARE="000", NETDATE="20190829", AUTOFLAG="1", EBFLAG="0", EBDATE="00000000", EBFEEFLAG="0", EBFEE="0", EBACTTYPE="2", CHKDUPPAY="0", CUSTID="1234567", FUNC="0", MAFARE="0", NOFARE="0", CTBCFLAG="0", SHAREBNFTFLG="1", SHAREBEFTPERCENT="20", ACTFEEBEFT="50", ACTFEEMART="15", SHAREACTFLG="1", ACTPERCENT="50", CLEARFEEMART1="0", CLEARFEEMART2="0", CLEARFEEMART3="0", CLEARFEEMART4="0", CLEARFEEMART5="0", PAYKINDPOST="0", ACTFEEPOST="15", SHAREPOSTFLG="1", POSTPERCENT="50", AGRIFLAG="1", AGRIFEE="25",FILLER=""},
                new ACCFTT() { KEYNO="992079", ACCIDNO="0833100024118", CUSTNAME="每筆總手續費-有分潤", APPBECODE="0921", BRCODE="0921", IDCODE="53976248", APPLYDATE=DateTime.Now.ToString("yyyyMMdd"), CHGDATE="",                                APPLYSTAT="0", CHKNUMFLAG="Y", CHKAMTFLAG="N", DUETERM="0", CHANNEL="9", FEE="010", RSTORE1="1", RSTORE2="1", RSTORE3="1", RSTORE4="1", RECVITEM1="6V1", RECVITEM2="6V2", RECVITEM3="6RM", RECVITEM4="", RECVITEM5="", ACTFEE="10", MARTFEE1="00", MARTFEE2="00", MARTFEE3="00", POSTFLAG="1", ACTFEEPT="10", POSTFEE="00", HIFLAG="0", HIFARE="000", NETDATE="20190819", AUTOFLAG="0", EBFLAG="0", EBDATE="00000000", EBFEEFLAG="0", EBFEE="0", EBACTTYPE="2", CHKDUPPAY="0", CUSTID="1234567", FUNC="0", MAFARE="0", NOFARE="0", CTBCFLAG="0", SHAREBNFTFLG="1", SHAREBEFTPERCENT="20", ACTFEEBEFT="50", ACTFEEMART="15", SHAREACTFLG="1", ACTPERCENT="50", CLEARFEEMART1="0", CLEARFEEMART2="0", CLEARFEEMART3="0", CLEARFEEMART4="0", CLEARFEEMART5="0", PAYKINDPOST="0", ACTFEEPOST="15", SHAREPOSTFLG="1", POSTPERCENT="50", AGRIFLAG="1", AGRIFEE="25",FILLER=""},
                new ACCFTT() { KEYNO="992080", ACCIDNO="0833100331983", CUSTNAME="每筆總手續費-有分潤", APPBECODE="0921", BRCODE="0921", IDCODE="05379410", APPLYDATE=DateTime.Now.ToString("yyyyMMdd"), CHGDATE="",                                APPLYSTAT="0", CHKNUMFLAG="Y", CHKAMTFLAG="N", DUETERM="0", CHANNEL="9", FEE="010", RSTORE1="1", RSTORE2="1", RSTORE3="1", RSTORE4="1", RECVITEM1="6V1", RECVITEM2="6V2", RECVITEM3="6RM", RECVITEM4="", RECVITEM5="", ACTFEE="10", MARTFEE1="00", MARTFEE2="00", MARTFEE3="00", POSTFLAG="1", ACTFEEPT="10", POSTFEE="00", HIFLAG="0", HIFARE="000", NETDATE="20190819", AUTOFLAG="0", EBFLAG="0", EBDATE="00000000", EBFEEFLAG="0", EBFEE="0", EBACTTYPE="2", CHKDUPPAY="0", CUSTID="1234567", FUNC="0", MAFARE="0", NOFARE="0", CTBCFLAG="0", SHAREBNFTFLG="1", SHAREBEFTPERCENT="20", ACTFEEBEFT="50", ACTFEEMART="15", SHAREACTFLG="1", ACTPERCENT="50", CLEARFEEMART1="0", CLEARFEEMART2="0", CLEARFEEMART3="0", CLEARFEEMART4="0", CLEARFEEMART5="0", PAYKINDPOST="0", ACTFEEPOST="15", SHAREPOSTFLG="1", POSTPERCENT="50", AGRIFLAG="1", AGRIFEE="25",FILLER=""},
                new ACCFTT() { KEYNO="992076", ACCIDNO="0310101006026", CUSTNAME="每筆總手續費-有分潤", APPBECODE="0310", BRCODE="0310", IDCODE="31723190", APPLYDATE=DateTime.Now.ToString("yyyyMMdd"), CHGDATE="",                                APPLYSTAT="0", CHKNUMFLAG="Y", CHKAMTFLAG="N", DUETERM="0", CHANNEL="9", FEE="010", RSTORE1="1", RSTORE2="1", RSTORE3="1", RSTORE4="1", RECVITEM1="6V6", RECVITEM2="6V7", RECVITEM3="6RN", RECVITEM4="", RECVITEM5="", ACTFEE="10", MARTFEE1="00", MARTFEE2="00", MARTFEE3="00", POSTFLAG="0", ACTFEEPT="00", POSTFEE="00", HIFLAG="0", HIFARE="000", NETDATE="20190902", AUTOFLAG="0", EBFLAG="0", EBDATE="00000000", EBFEEFLAG="0", EBFEE="0", EBACTTYPE="2", CHKDUPPAY="0", CUSTID="1234567", FUNC="0", MAFARE="0", NOFARE="0", CTBCFLAG="0", SHAREBNFTFLG="1", SHAREBEFTPERCENT="20", ACTFEEBEFT="50", ACTFEEMART="15", SHAREACTFLG="1", ACTPERCENT="50", CLEARFEEMART1="0", CLEARFEEMART2="0", CLEARFEEMART3="0", CLEARFEEMART4="0", CLEARFEEMART5="0", PAYKINDPOST="0", ACTFEEPOST="15", SHAREPOSTFLG="1", POSTPERCENT="50", AGRIFLAG="1", AGRIFEE="25",FILLER=""},
                new ACCFTT() { KEYNO="992074", ACCIDNO="0417101007830", CUSTNAME="每筆總手續費-有分潤", APPBECODE="1049", BRCODE="1049", IDCODE="99590035", APPLYDATE=DateTime.Now.ToString("yyyyMMdd"), CHGDATE=DateTime.Now.ToString("yyyyMMdd"), APPLYSTAT="0", CHKNUMFLAG="Y", CHKAMTFLAG="N", DUETERM="0", CHANNEL="9", FEE="010", RSTORE1="1", RSTORE2="1", RSTORE3="1", RSTORE4="1", RECVITEM1="6V6", RECVITEM2="6V7", RECVITEM3="", RECVITEM4="", RECVITEM5="",    ACTFEE="00", MARTFEE1="00", MARTFEE2="00", MARTFEE3="00", POSTFLAG="0", ACTFEEPT="00", POSTFEE="00", HIFLAG="0", HIFARE="000", NETDATE="20190627", AUTOFLAG="0", EBFLAG="0", EBDATE="00000000", EBFEEFLAG="0", EBFEE="0", EBACTTYPE="2", CHKDUPPAY="0", CUSTID="1234567", FUNC="0", MAFARE="0", NOFARE="0", CTBCFLAG="0", SHAREBNFTFLG="1", SHAREBEFTPERCENT="20", ACTFEEBEFT="50", ACTFEEMART="15", SHAREACTFLG="1", ACTPERCENT="50", CLEARFEEMART1="0", CLEARFEEMART2="0", CLEARFEEMART3="0", CLEARFEEMART4="0", CLEARFEEMART5="0", PAYKINDPOST="0", ACTFEEPOST="15", SHAREPOSTFLG="1", POSTPERCENT="50", AGRIFLAG="1", AGRIFEE="25",FILLER=""},
                new ACCFTT() { KEYNO="992108", ACCIDNO="0435101082576", CUSTNAME="每筆總手續費-有分潤", APPBECODE="0435", BRCODE="0435", IDCODE="72452571", APPLYDATE=DateTime.Now.ToString("yyyyMMdd"), CHGDATE="",                                APPLYSTAT="0", CHKNUMFLAG="Y", CHKAMTFLAG="N", DUETERM="0", CHANNEL="9", FEE="010", RSTORE1="1", RSTORE2="1", RSTORE3="1", RSTORE4="1", RECVITEM1="6V1", RECVITEM2="6V2", RECVITEM3="6RM", RECVITEM4="", RECVITEM5="", ACTFEE="10", MARTFEE1="00", MARTFEE2="00", MARTFEE3="00", POSTFLAG="0", ACTFEEPT="00", POSTFEE="00", HIFLAG="0", HIFARE="000", NETDATE="20190917", AUTOFLAG="0", EBFLAG="0", EBDATE="00000000", EBFEEFLAG="0", EBFEE="0", EBACTTYPE="2", CHKDUPPAY="0", CUSTID="1234567", FUNC="0", MAFARE="0", NOFARE="0", CTBCFLAG="0", SHAREBNFTFLG="1", SHAREBEFTPERCENT="20", ACTFEEBEFT="50", ACTFEEMART="15", SHAREACTFLG="1", ACTPERCENT="50", CLEARFEEMART1="0", CLEARFEEMART2="0", CLEARFEEMART3="0", CLEARFEEMART4="0", CLEARFEEMART5="0", PAYKINDPOST="0", ACTFEEPOST="15", SHAREPOSTFLG="1", POSTPERCENT="50", AGRIFLAG="1", AGRIFEE="25",FILLER=""},
                new ACCFTT() { KEYNO="992109", ACCIDNO="0462101005054", CUSTNAME="每筆總手續費-有分潤", APPBECODE="0462", BRCODE="0462", IDCODE="31753898", APPLYDATE=DateTime.Now.ToString("yyyyMMdd"), CHGDATE="",                                APPLYSTAT="0", CHKNUMFLAG="Y", CHKAMTFLAG="N", DUETERM="0", CHANNEL="9", FEE="010", RSTORE1="1", RSTORE2="1", RSTORE3="1", RSTORE4="1", RECVITEM1="6V1", RECVITEM2="6V2", RECVITEM3="", RECVITEM4="", RECVITEM5="",    ACTFEE="00", MARTFEE1="00", MARTFEE2="00", MARTFEE3="00", POSTFLAG="0", ACTFEEPT="00", POSTFEE="00", HIFLAG="0", HIFARE="000", NETDATE="20190919", AUTOFLAG="0", EBFLAG="0", EBDATE="00000000", EBFEEFLAG="0", EBFEE="0", EBACTTYPE="2", CHKDUPPAY="0", CUSTID="1234567", FUNC="0", MAFARE="0", NOFARE="0", CTBCFLAG="0", SHAREBNFTFLG="1", SHAREBEFTPERCENT="20", ACTFEEBEFT="50", ACTFEEMART="15", SHAREACTFLG="1", ACTPERCENT="50", CLEARFEEMART1="0", CLEARFEEMART2="0", CLEARFEEMART3="0", CLEARFEEMART4="0", CLEARFEEMART5="0", PAYKINDPOST="0", ACTFEEPOST="15", SHAREPOSTFLG="1", POSTPERCENT="50", AGRIFLAG="1", AGRIFEE="25",FILLER=""},
                new ACCFTT() { KEYNO="992106", ACCIDNO="1058108889999", CUSTNAME="每筆總手續費-有分潤", APPBECODE="1058", BRCODE="1058", IDCODE="85283063", APPLYDATE=DateTime.Now.ToString("yyyyMMdd"), CHGDATE="",                                APPLYSTAT="0", CHKNUMFLAG="Y", CHKAMTFLAG="N", DUETERM="0", CHANNEL="9", FEE="010", RSTORE1="1", RSTORE2="1", RSTORE3="1", RSTORE4="1", RECVITEM1="6V1", RECVITEM2="6V2", RECVITEM3="6RM", RECVITEM4="", RECVITEM5="", ACTFEE="10", MARTFEE1="00", MARTFEE2="00", MARTFEE3="00", POSTFLAG="1", ACTFEEPT="10", POSTFEE="00", HIFLAG="0", HIFARE="000", NETDATE="20190927", AUTOFLAG="0", EBFLAG="0", EBDATE="00000000", EBFEEFLAG="0", EBFEE="0", EBACTTYPE="2", CHKDUPPAY="0", CUSTID="1234567", FUNC="0", MAFARE="0", NOFARE="0", CTBCFLAG="0", SHAREBNFTFLG="1", SHAREBEFTPERCENT="20", ACTFEEBEFT="50", ACTFEEMART="15", SHAREACTFLG="1", ACTPERCENT="50", CLEARFEEMART1="0", CLEARFEEMART2="0", CLEARFEEMART3="0", CLEARFEEMART4="0", CLEARFEEMART5="0", PAYKINDPOST="0", ACTFEEPOST="15", SHAREPOSTFLG="1", POSTPERCENT="50", AGRIFLAG="1", AGRIFEE="25",FILLER=""},
            };
            bool err = false;
            accftts.ForEach(p => { if (p.Source != new ACCFTT() { Source = p.Source }.Source) { err = true; return; } });
            if (err) { Message.AddCustErrorMessage(MessageCode.Code0000, "服務申請書Source拆分組合異常"); return; }
            string path = $@"D:\ibankRoot\Ftp_SKGPortalCore\ACCFTT\"; Directory.CreateDirectory(path);
            using StreamWriter sw = new StreamWriter($@"{path}ACCFTT.{ DateTime.Now.ToString("yyyyMMdd")}", false, Encoding.GetEncoding(950));
            accftts.ForEach(p => sw.WriteLine(p.Source));
            sw.Close();
        }
    }
}
