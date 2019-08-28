using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKGPortalCore.Model.SourceData
{

    [Description("服務申請書主檔")]
    public class ACCFTT
    {
        /// <summary>
        /// 01.全方位帳號(13位:990343;14位:998;16位:1024;)
        /// X(06)
        /// </summary>
        public string KEYNO { get; set; }
        /// <summary>
        /// 02.實體帳號
        /// 9(13)
        /// </summary>
        public string ACCIDNO { get; set; }
        /// <summary>
        /// 03.戶名
        /// X(40)
        /// </summary>
        public string CUSTNAME { get; set; }
        /// <summary>
        /// 04.申請分行
        /// X(04)
        /// </summary>
        public string APPBECODE { get; set; }
        /// <summary>
        /// 05.所屬分行(帳務分行)
        /// X(04)
        /// </summary>
        public string BRCODE { get; set; }
        /// <summary>
        /// 06.統一編號(第十一碼有值時為重號)
        /// X(11)
        /// </summary>
        public string IDCODE { get; set; }
        /// <summary>
        /// 07.申請日期
        /// X(8)
        /// </summary>
        public string APPLYDATE { get; set; }
        /// <summary>
        /// 08.最後異動日期
        /// X(8)
        /// </summary>
        public string CHGDATE { get; set; }
        /// <summary>
        /// 09.申請狀態(0:申請 1:中止 9:終止)
        /// 9(01)
        /// </summary>
        public string APPLYSTAT { get; set; }
        /// <summary>
        /// 10.採用檢碼(Y:是 N:否)針對14碼、(2:二位檢碼 1:一位檢碼 0:否) 針對16位
        /// X(1)
        /// </summary>
        public string CHKNUMFLAG { get; set; }
        /// <summary>
        /// 11.是否檢核金額(Y:是 N:否)
        /// X(01)
        /// </summary>
        public string CHKAMTFLAG { get; set; }
        /// <summary>
        /// 12.是否檢核繳款期限(1:是 0:否)
        /// 9(01)
        /// </summary>
        public string DUETERM { get; set; }
        /// <summary>
        /// 13.繳款通路(9:全部 0:除自動化 1:除臨櫃 2:僅臨櫃超商 3:僅匯款 4:僅超商郵局) 
        /// X(01)
        /// </summary>
        public string CHANNEL { get; set; }
        /// <summary>
        /// 14.全方位手續費
        /// 9(03)
        /// </summary>
        public string FEE { get; set; }
        /// <summary>
        /// 15.代收超商(7-11)(1:是 0:否)
        /// 9(01)
        /// </summary>
        public string RSTORE1 { get; set; }
        /// <summary>
        /// 16.代收超商(全家)(1:是 0:否)
        /// 9(01)
        /// </summary>
        public string RSTORE2 { get; set; }
        /// <summary>
        /// 17.代收超商(OK)(1:是 0:否)
        /// 9(01)
        /// </summary>
        public string RSTORE3 { get; set; }
        /// <summary>
        /// 18.代收超商(萊爾富)(1:是 0:否)
        /// 9(01)
        /// </summary>
        public string RSTORE4 { get; set; }
        /// <summary>
        /// 19.超商代收項目1
        /// X(03)
        /// </summary>
        public string RECVITEM1 { get; set; }
        /// <summary>
        /// 20.超商代收項目2
        /// X(03)
        /// </summary>
        public string RECVITEM2 { get; set; }
        /// <summary>
        /// 21.超商代收項目3
        /// X(03)
        /// </summary>
        public string RECVITEM3 { get; set; }
        /// <summary>
        /// 22.超商代收項目4
        /// X(03)
        /// </summary>
        public string RECVITEM4 { get; set; }
        /// <summary>
        /// 23.超商代收項目5
        /// X(03)
        /// </summary>
        public string RECVITEM5 { get; set; }
        /// <summary>
        /// 24.超商清算手續費
        /// 9(02)
        /// </summary>
        public string ACTFEE { get; set; }
        /// <summary>
        /// 25.分行分擔超商代收手續費(20000元以下)
        /// 9(02)
        /// </summary>
        public string MARTFEE1 { get; set; }
        /// <summary>
        /// 26.分行分擔超商代收手續費(20001~40000元
        /// 9(02)
        /// </summary>
        public string MARTFEE2 { get; set; }
        /// <summary>
        /// 27.分行分擔超商代收手續費(40001元以上)
        /// 9(02)
        /// </summary>
        public string MARTFEE3 { get; set; }
        /// <summary>
        /// 28.郵局申請狀態(1:是 0:否)
        /// 9(01)
        /// </summary>
        public string POSTFLAG { get; set; }
        /// <summary>
        /// 29.郵局清算手續費
        /// 9(02)
        /// </summary>
        public string ACTFEEPT { get; set; }
        /// <summary>
        /// 30.分行分擔郵局代收手續費
        /// 9(02)
        /// </summary>
        public string POSTFEE { get; set; }
        /// <summary>
        /// 31.網路平台申請(0:未申請 1:單機版 2:網路版-分潤 3:網路版-長期)
        /// X(01)
        /// </summary>
        public string HIFLAG { get; set; }
        /// <summary>
        /// 32.網路平台使用手續費
        /// 9(02)
        /// </summary>
        public string HIFARE { get; set; }
        /// <summary>
        /// 33.代收網啟用日期
        /// X(08)
        /// </summary>
        public string NETDATE { get; set; }
        /// <summary>
        /// 34.自動約定扣款(1:是 0:否)
        /// 9(01)
        /// </summary>
        public string AUTOFLAG { get; set; }
        /// <summary>
        /// 35.全國性e繳費(1:是 0:否)
        /// 9(01)
        /// </summary>
        public string EBFLAG { get; set; }
        /// <summary>
        /// 36.全國性e繳費日期(西元年月日)
        /// X(08)
        /// </summary>
        public string EBDATE { get; set; }
        /// <summary>
        /// 37.e手續費負擔方式(1:使用者 2:企業者)
        /// 9(01)
        /// </summary>
        public string EBFEEFLAG { get; set; }
        /// <summary>
        /// 38.e手續費負擔
        /// 9(02)
        /// </summary>
        public string EBFEE { get; set; }
        /// <summary>
        /// 39.e手續費結算方式(1:直接月結 2:人工月結 3:每筆結)
        /// 9(01)
        /// </summary>
        public string EBACTTYPE { get; set; }
        /// <summary>
        /// 40.檢核重複繳款
        /// 9(01)
        /// </summary>
        public string CHKDUPPAY { get; set; }
        /// <summary>
        /// 41.委託單位代號(代理)
        /// 9(07)
        /// </summary>
        public string CUSTID { get; set; }
        /// <summary>
        /// 42.用途(0:一般 1:期貨或信託 2:代收網 3:發送Mail 5:代收網及發送Mail)
        /// 9(01)
        /// </summary>
        public string FUNC { get; set; }
        /// <summary>
        /// 43.郵局通路手續費
        /// 9(02)
        /// </summary>
        public string MAFARE { get; set; }
        /// <summary>
        /// 44.臨櫃通路手續費
        /// 9(02)
        /// </summary>
        public string NOFARE { get; set; }
        /// <summary>
        /// 45.申請中信平台繳學費(1:是 0:否)
        /// X(01)
        /// </summary>
        public string CTBCFLAG { get; set; }
        /// <summary>
        /// 46.全方位帳號-是否分潤
        /// X(01)
        /// </summary>
        public string SHAREBNFTFLG { get; set; }
        /// <summary>
        /// 47.全方位帳號-分潤%
        /// 9(02)
        /// </summary>
        public string SHAREBEFTPERCENT { get; set; }
        /// <summary>
        /// 48.全方位帳號-清算手續費
        /// 9(02)
        /// </summary>
        public string ACTFEEBEFT { get; set; }
        /// <summary>
        /// 49.超商通路每筆總手續費
        /// 9(02)
        /// </summary>
        public string ACTFEEMART { get; set; }
        /// <summary>
        /// 50.超商通路-是否分潤
        /// X(01)
        /// </summary>
        public string SHAREACTFLG { get; set; }
        /// <summary>
        /// 51.超商通路-分潤%
        /// 9(02)
        /// </summary>
        public string ACTPERCENT { get; set; }
        /// <summary>
        /// 52.超商通路-清算手續費1
        /// 9(02)
        /// </summary>
        public string CLEARFEEMART1 { get; set; }
        /// <summary>
        /// 53.超商通路-清算手續費2
        /// 9(02)
        /// </summary>
        public string CLEARFEEMART2 { get; set; }
        /// <summary>
        /// 54.超商通路-清算手續費3
        /// 9(02)
        /// </summary>
        public string CLEARFEEMART3 { get; set; }
        /// <summary>
        /// 55.超商通路-清算手續費4
        /// 9(02)
        /// </summary>
        public string CLEARFEEMART4 { get; set; }
        /// <summary>
        /// 56.超商通路-清算手續費5
        /// 9(02)
        /// </summary>
        public string CLEARFEEMART5 { get; set; }
        /// <summary>
        /// 57.郵局手續費收費方式
        /// X(01)
        /// </summary>
        public string PAYKINDPOST { get; set; }
        /// <summary>
        /// 58.郵局通路每筆總手續費
        /// 9(02)
        /// </summary>
        public string ACTFEEPOST { get; set; }
        /// <summary>
        /// 59.郵局通路-是否分潤
        /// X(01)
        /// </summary>
        public string SHAREPOSTFLG { get; set; }
        /// <summary>
        /// 60.郵局通路-分潤%
        /// 9(02)
        /// </summary>
        public string POSTPERCENT { get; set; }
        /// <summary>
        /// 61.農金平台申請
        /// X(01)
        /// </summary>
        public string AGRIFLAG { get; set; }
        /// <summary>
        /// 62.農金平台手續費
        /// 9(02)
        /// </summary>
        public string AGRIFEE { get; set; }
        /// <summary>
        /// 63.保留
        /// X(50)
        /// </summary>
        public string FILLER { get; set; }

        public void SetValue(int idx, string value)
        {
            idx++;
            switch (idx)
            {
                case 1: KEYNO = value; break;
                case 2: ACCIDNO = value; break;
                case 3: CUSTNAME = value; break;
                case 4: APPBECODE = value; break;
                case 5: BRCODE = value; break;
                case 6: IDCODE = value; break;
                case 7: APPLYDATE = value; break;
                case 8: CHGDATE = value; break;
                case 9: APPLYSTAT = value; break;
                case 10: CHKNUMFLAG = value; break;
                case 11: CHKAMTFLAG = value; break;
                case 12: DUETERM = value; break;
                case 13: CHANNEL = value; break;
                case 14: FEE = value; break;
                case 15: RSTORE1 = value; break;
                case 16: RSTORE2 = value; break;
                case 17: RSTORE3 = value; break;
                case 18: RSTORE4 = value; break;
                case 19: RECVITEM1 = value; break;
                case 20: RECVITEM2 = value; break;
                case 21: RECVITEM3 = value; break;
                case 22: RECVITEM4 = value; break;
                case 23: RECVITEM5 = value; break;
                case 24: ACTFEE = value; break;
                case 25: MARTFEE1 = value; break;
                case 26: MARTFEE2 = value; break;
                case 27: MARTFEE3 = value; break;
                case 28: POSTFLAG = value; break;
                case 29: ACTFEEPT = value; break;
                case 30: POSTFEE = value; break;
                case 31: HIFLAG = value; break;
                case 32: HIFARE = value; break;
                case 33: NETDATE = value; break;
                case 34: AUTOFLAG = value; break;
                case 35: EBFLAG = value; break;
                case 36: EBDATE = value; break;
                case 37: EBFEEFLAG = value; break;
                case 38: EBFEE = value; break;
                case 39: EBACTTYPE = value; break;
                case 40: CHKDUPPAY = value; break;
                case 41: CUSTID = value; break;
                case 42: FUNC = value; break;
                case 43: MAFARE = value; break;
                case 44: NOFARE = value; break;
                case 45: CTBCFLAG = value; break;
                case 46: SHAREBNFTFLG = value; break;
                case 47: SHAREBEFTPERCENT = value; break;
                case 48: ACTFEEBEFT = value; break;
                case 49: ACTFEEMART = value; break;
                case 50: SHAREACTFLG = value; break;
                case 51: ACTPERCENT = value; break;
                case 52: CLEARFEEMART1 = value; break;
                case 53: CLEARFEEMART2 = value; break;
                case 54: CLEARFEEMART3 = value; break;
                case 55: CLEARFEEMART4 = value; break;
                case 56: CLEARFEEMART5 = value; break;
                case 57: PAYKINDPOST = value; break;
                case 58: ACTFEEPOST = value; break;
                case 59: SHAREPOSTFLG = value; break;
                case 60: POSTPERCENT = value; break;
                case 61: AGRIFLAG = value; break;
                case 62: AGRIFEE = value; break;
                case 63: FILLER = value; break;
                default: throw new NotImplementedException();
            }
        }
    }
}
