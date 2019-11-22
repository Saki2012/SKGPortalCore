using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SKGPortalCore.Lib;

namespace SKGPortalCore.Model.SourceData
{
    /// <summary>
    /// 服務申請書主檔
    /// </summary>
    [Description("服務申請書主檔")]
    public class ACCFTT : IImportSource
    {
        #region Public
        public int Id { get; set; }
        /// <summary>
        /// 01.全方位帳號(13位:990343;14位:998;16位:1024;)
        /// X(06)
        /// </summary>
        [Description("全方位帳號"), MaxLength(6)]
        public string KEYNO { get => _KEYNO.PadRight(6, ' ').ByteSubString(0, 6); set => _KEYNO = value.PadRight(6, ' ').ByteSubString(0, 6); }
        /// <summary>
        /// 02.實體帳號
        /// 9(13)
        /// </summary>
        [Description("實體帳號"), MaxLength(13)]
        public string ACCIDNO { get => _ACCIDNO.PadLeft(13, '0').ByteSubString(0, 13); set => _ACCIDNO = value.PadLeft(13, '0').ByteSubString(0, 13); }
        /// <summary>
        /// 03.戶名
        /// X(40)
        /// </summary>
        [Description("戶名"), MaxLength(40)]
        public string CUSTNAME { get => _CUSTNAME.PadRight(40, ' ').ByteSubString(0, 40); set => _CUSTNAME = value.PadRight(40, ' ').ByteSubString(0, 40); }
        /// <summary>
        /// 04.申請分行
        /// X(04)
        /// </summary>
        [Description("申請分行"), MaxLength(4)]
        public string APPBECODE { get => _APPBECODE.PadLeft(4, '0').ByteSubString(0, 4); set => _APPBECODE = value.PadLeft(4, '0').ByteSubString(0, 4); }
        /// <summary>
        /// 05.所屬分行(帳務分行)
        /// X(04)
        /// </summary>
        [Description("所屬分行"), MaxLength(4)]
        public string BRCODE { get => _BRCODE.PadLeft(4, '0').ByteSubString(0, 4); set => _BRCODE = value.PadLeft(4, '0').ByteSubString(0, 4); }
        /// <summary>
        /// 06.統一編號(第十一碼有值時為重號)
        /// X(11)
        /// </summary>
        [Description("統一編號"), MaxLength(11)]
        public string IDCODE { get => _IDCODE.PadLeft(11, '0').ByteSubString(0, 11); set => _IDCODE = value.PadLeft(11, '0').ByteSubString(0, 11); }
        /// <summary>
        /// 07.申請日期
        /// X(8)
        /// </summary>
        [Description("申請日期"), MaxLength(8)]
        public string APPLYDATE { get => _APPLYDATE.PadLeft(8, '0').ByteSubString(0, 8); set => _APPLYDATE = value.PadLeft(8, '0').ByteSubString(0, 8); }
        /// <summary>
        /// 08.最後異動日期
        /// X(8)
        /// </summary>
        [Description("最後異動日期"), MaxLength(8)]
        public string CHGDATE { get => _CHGDATE.PadLeft(8, ' ').ByteSubString(0, 8); set => _CHGDATE = value.PadLeft(8, ' ').ByteSubString(0, 8); }
        /// <summary>
        /// 09.申請狀態(0:申請 1:中止 9:終止)
        /// 9(01)
        /// </summary>
        [Description("申請狀態"), MaxLength(1)]
        public string APPLYSTAT { get => _APPLYSTAT.PadLeft(1, '0').ByteSubString(0, 1); set => _APPLYSTAT = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 10.採用檢碼(Y:是 N:否)針對14碼、(2:二位檢碼 1:一位檢碼 0:否) 針對16位
        /// X(1)
        /// </summary>
        [Description("採用檢碼"), MaxLength(1)]
        public string CHKNUMFLAG { get => _CHKNUMFLAG.PadLeft(1, '0').ByteSubString(0, 1); set => _CHKNUMFLAG = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 11.是否檢核金額(Y:是 N:否)
        /// X(01)
        /// </summary>
        [Description("是否檢核金額"), MaxLength(1)]
        public string CHKAMTFLAG { get => _CHKAMTFLAG.PadLeft(1, '0').ByteSubString(0, 1); set => _CHKAMTFLAG = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 12.是否檢核繳款期限(1:是 0:否)
        /// 9(01)
        /// </summary>
        [Description("是否檢核繳款期限"), MaxLength(1)]
        public string DUETERM { get => _DUETERM.PadLeft(1, '0').ByteSubString(0, 1); set => _DUETERM = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 13.繳款通路(9:全部 0:除自動化 1:除臨櫃 2:僅臨櫃超商 3:僅匯款 4:僅超商郵局) 
        /// X(01)
        /// </summary>
        [Description("繳款通路"), MaxLength(1)]
        public string CHANNEL { get => _CHANNEL.PadLeft(1, '0').ByteSubString(0, 1); set => _CHANNEL = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 14.全方位手續費
        /// 9(03)
        /// </summary>
        [Description("全方位手續費"), MaxLength(3)]
        public string FEE { get => _FEE.PadLeft(3, '0').ByteSubString(0, 3); set => _FEE = value.PadLeft(3, '0').ByteSubString(0, 3); }
        /// <summary>
        /// 15.代收超商(7-11)(1:是 0:否)
        /// 9(01)
        /// </summary>
        [Description("代收超商(7-11)"), MaxLength(1)]
        public string RSTORE1 { get => _RSTORE1.PadLeft(1, '0').ByteSubString(0, 1); set => _RSTORE1 = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 16.代收超商(全家)(1:是 0:否)
        /// 9(01)
        /// </summary>
        [Description("代收超商(全家)"), MaxLength(1)]
        public string RSTORE2 { get => _RSTORE2.PadLeft(1, '0').ByteSubString(0, 1); set => _RSTORE2 = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 17.代收超商(OK)(1:是 0:否)
        /// 9(01)
        /// </summary>
        [Description("代收超商(OK)"), MaxLength(1)]
        public string RSTORE3 { get => _RSTORE3.PadLeft(1, '0').ByteSubString(0, 1); set => _RSTORE3 = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 18.代收超商(萊爾富)(1:是 0:否)
        /// 9(01)
        /// </summary>
        [Description("代收超商(萊爾富)"), MaxLength(1)]
        public string RSTORE4 { get => _RSTORE4.PadLeft(1, '0').ByteSubString(0, 1); set => _RSTORE4 = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 19.超商代收項目1
        /// X(03)
        /// </summary>
        [Description("超商代收項目1"), MaxLength(3)]
        public string RECVITEM1 { get => _RECVITEM1.PadLeft(3, ' ').ByteSubString(0, 3); set => _RECVITEM1 = value.PadLeft(3, ' ').ByteSubString(0, 3); }
        /// <summary>
        /// 20.超商代收項目2
        /// X(03)
        /// </summary>
        [Description("超商代收項目2"), MaxLength(3)]
        public string RECVITEM2 { get => _RECVITEM2.PadLeft(3, ' ').ByteSubString(0, 3); set => _RECVITEM2 = value.PadLeft(3, ' ').ByteSubString(0, 3); }
        /// <summary>
        /// 21.超商代收項目3
        /// X(03)
        /// </summary>
        [Description("超商代收項目3"), MaxLength(3)]
        public string RECVITEM3 { get => _RECVITEM3.PadLeft(3, ' ').ByteSubString(0, 3); set => _RECVITEM3 = value.PadLeft(3, ' ').ByteSubString(0, 3); }
        /// <summary>
        /// 22.超商代收項目4
        /// X(03)
        /// </summary>
        [Description("超商代收項目4"), MaxLength(3)]
        public string RECVITEM4 { get => _RECVITEM4.PadLeft(3, ' ').ByteSubString(0, 3); set => _RECVITEM4 = value.PadLeft(3, ' ').ByteSubString(0, 3); }
        /// <summary>
        /// 23.超商代收項目5
        /// X(03)
        /// </summary>
        [Description("超商代收項目5"), MaxLength(3)]
        public string RECVITEM5 { get => _RECVITEM5.PadLeft(3, ' ').ByteSubString(0, 3); set => _RECVITEM5 = value.PadLeft(3, ' ').ByteSubString(0, 3); }
        /// <summary>
        /// 24.超商清算手續費
        /// 9(02)
        /// </summary>
        [Description("超商清算手續費"), MaxLength(2)]
        public string ACTFEE { get => _ACTFEE.PadLeft(2, '0').ByteSubString(0, 2); set => _ACTFEE = value.PadLeft(2, '0').ByteSubString(0, 2); }
        /// <summary>
        /// 25.分行分擔超商代收手續費(20000元以下)
        /// 9(02)
        /// </summary>
        [Description("分行分擔超商代收手續費(20000元以下)"), MaxLength(2)]
        public string MARTFEE1 { get => _MARTFEE1.PadLeft(2, '0').ByteSubString(0, 2); set => _MARTFEE1 = value.PadLeft(2, '0').ByteSubString(0, 2); }
        /// <summary>
        /// 26.分行分擔超商代收手續費(20001~40000元)
        /// 9(02)
        /// </summary>
        [Description("分行分擔超商代收手續費(20001~40000元)"), MaxLength(2)]
        public string MARTFEE2 { get => _MARTFEE2.PadLeft(2, '0').ByteSubString(0, 2); set => _MARTFEE2 = value.PadLeft(2, '0').ByteSubString(0, 2); }
        /// <summary>
        /// 27.分行分擔超商代收手續費(40001元以上)
        /// 9(02)
        /// </summary>
        [Description("分行分擔超商代收手續費(40001元以上)"), MaxLength(2)]
        public string MARTFEE3 { get => _MARTFEE3.PadLeft(2, '0').ByteSubString(0, 2); set => _MARTFEE3 = value.PadLeft(2, '0').ByteSubString(0, 2); }
        /// <summary>
        /// 28.郵局申請狀態(1:是 0:否)
        /// 9(01)
        /// </summary>
        [Description("郵局申請狀態"), MaxLength(1)]
        public string POSTFLAG { get => _POSTFLAG.PadLeft(1, '0').ByteSubString(0, 1); set => _POSTFLAG = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 29.郵局清算手續費
        /// 9(02)
        /// </summary>
        [Description("郵局清算手續費"), MaxLength(2)]
        public string ACTFEEPT { get => _ACTFEEPT.PadLeft(2, '0').ByteSubString(0, 2); set => _ACTFEEPT = value.PadLeft(2, '0').ByteSubString(0, 2); }
        /// <summary>
        /// 30.分行分擔郵局代收手續費
        /// 9(02)
        /// </summary>
        [Description("分行分擔郵局代收手續費"), MaxLength(2)]
        public string POSTFEE { get => _POSTFEE.PadLeft(2, '0').ByteSubString(0, 2); set => _POSTFEE = value.PadLeft(2, '0').ByteSubString(0, 2); }
        /// <summary>
        /// 31.網路平台申請(0:未申請 1:單機版 2:網路版-分潤 3:網路版-長期)
        /// X(01)
        /// </summary>
        [Description("網路平台申請"), MaxLength(1)]
        public string HIFLAG { get => _HIFLAG.PadLeft(1, '0').ByteSubString(0, 1); set => _HIFLAG = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 32.網路平台使用手續費
        /// 9(03)
        /// </summary>
        [Description("網路平台使用手續費"), MaxLength(3)]
        public string HIFARE { get => _HIFARE.PadLeft(3, '0').ByteSubString(0, 3); set => _HIFARE = value.PadLeft(3, '0').ByteSubString(0, 3); }
        /// <summary>
        /// 33.代收網啟用日期
        /// X(08)
        /// </summary>
        [Description("代收網啟用日期"), MaxLength(8)]
        public string NETDATE { get => _NETDATE.PadLeft(8, '0').ByteSubString(0, 8); set => _NETDATE = value.PadLeft(8, '0').ByteSubString(0, 8); }
        /// <summary>
        /// 34.自動約定扣款(1:是 0:否)
        /// 9(01)
        /// </summary>
        [Description("自動約定扣款"), MaxLength(1)]
        public string AUTOFLAG { get => _AUTOFLAG.PadLeft(1, '0').ByteSubString(0, 1); set => _AUTOFLAG = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 35.全國性e繳費(1:是 0:否)
        /// 9(01)
        /// </summary>
        [Description("全國性e繳費"), MaxLength(1)]
        public string EBFLAG { get => _EBFLAG.PadLeft(1, '0').ByteSubString(0, 1); set => _EBFLAG = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 36.全國性e繳費日期(西元年月日)
        /// X(08)
        /// </summary>
        [Description("全國性e繳費日期"), MaxLength(8)]
        public string EBDATE { get => _EBDATE.PadLeft(8, '0').ByteSubString(0, 8); set => _EBDATE = value.PadLeft(8, '0').ByteSubString(0, 8); }
        /// <summary>
        /// 37.e手續費負擔方式(1:使用者 2:企業者)
        /// 9(01)
        /// </summary>
        [Description("e手續費負擔方式"), MaxLength(1)]
        public string EBFEEFLAG { get => _EBFEEFLAG.PadLeft(1, '0').ByteSubString(0, 1); set => _EBFEEFLAG = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 38.e手續費負擔
        /// 9(03)
        /// </summary>
        [Description("e手續費負擔"), MaxLength(3)]
        public string EBFEE { get => _EBFEE.PadLeft(3, '0').ByteSubString(0, 3); set => _EBFEE = value.PadLeft(3, '0').ByteSubString(0, 3); }
        /// <summary>
        /// 39.e手續費結算方式(1:直接月結 2:人工月結 3:每筆結)
        /// 9(01)
        /// </summary>
        [Description("e手續費結算方式"), MaxLength(1)]
        public string EBACTTYPE { get => _EBACTTYPE.PadLeft(1, '0').ByteSubString(0, 1); set => _EBACTTYPE = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 40.檢核重複繳款
        /// 9(01)
        /// </summary>
        [Description("檢核重複繳款"), MaxLength(1)]
        public string CHKDUPPAY { get => _CHKDUPPAY.PadLeft(1, '0').ByteSubString(0, 1); set => _CHKDUPPAY = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 41.委託單位代號(代理)
        /// 9(07)
        /// </summary>
        [Description("委託單位代號"), MaxLength(7)]
        public string CUSTID { get => _CUSTID.PadLeft(7, '0').ByteSubString(0, 7); set => _CUSTID = value.PadLeft(7, '0').ByteSubString(0, 7); }
        /// <summary>
        /// 42.用途(0:一般 1:期貨或信託 2:代收網 3:發送Mail 5:代收網及發送Mail)
        /// 9(01)
        /// </summary>
        [Description("用途"), MaxLength(1)]
        public string FUNC { get => _FUNC.PadLeft(1, '0').ByteSubString(0, 1); set => _FUNC = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 43.郵局通路手續費
        /// 9(03)
        /// </summary>
        [Description("郵局通路手續費"), MaxLength(3)]
        public string MAFARE { get => _MAFARE.PadLeft(3, '0').ByteSubString(0, 3); set => _MAFARE = value.PadLeft(3, '0').ByteSubString(0, 3); }
        /// <summary>
        /// 44.臨櫃通路手續費
        /// 9(03)
        /// </summary>
        [Description("臨櫃通路手續費"), MaxLength(3)]
        public string NOFARE { get => _NOFARE.PadLeft(3, '0').ByteSubString(0, 3); set => _NOFARE = value.PadLeft(3, '0').ByteSubString(0, 3); }
        /// <summary>
        /// 45.申請中信平台繳學費(1:是 0:否)
        /// X(01)
        /// </summary>
        [Description("申請中信平台繳學費"), MaxLength(1)]
        public string CTBCFLAG { get => _CTBCFLAG.PadLeft(1, '0').ByteSubString(0, 1); set => _CTBCFLAG = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 46.全方位帳號-是否分潤
        /// X(01)
        /// </summary>
        [Description("全方位帳號-是否分潤"), MaxLength(1)]
        public string SHAREBNFTFLG { get => _SHAREBNFTFLG.PadLeft(1, '0').ByteSubString(0, 1); set => _SHAREBNFTFLG = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 47.全方位帳號-分潤%
        /// 9(02)
        /// </summary>
        [Description("全方位帳號-分潤%"), MaxLength(2)]
        public string SHAREBEFTPERCENT { get => _SHAREBEFTPERCENT.PadLeft(2, '0').ByteSubString(0, 2); set => _SHAREBEFTPERCENT = value.PadLeft(2, '0').ByteSubString(0, 2); }
        /// <summary>
        /// 48.全方位帳號-每筆手續費
        /// 9(02)
        /// </summary>
        [Description("全方位帳號-每筆手續費"), MaxLength(2)]
        public string ACTFEEBEFT { get => _ACTFEEBEFT.PadLeft(2, '0').ByteSubString(0, 2); set => _ACTFEEBEFT = value.PadLeft(2, '0').ByteSubString(0, 2); }
        /// <summary>
        /// 49.超商通路每筆總手續費
        /// 9(02)
        /// </summary>
        [Description("超商通路每筆總手續費"), MaxLength(2)]
        public string ACTFEEMART { get => _ACTFEEMART.PadLeft(2, '0').ByteSubString(0, 2); set => _ACTFEEMART = value.PadLeft(2, '0').ByteSubString(0, 2); }
        /// <summary>
        /// 50.超商通路-是否分潤
        /// X(01)
        /// </summary>
        [Description("超商通路-是否分潤"), MaxLength(1)]
        public string SHAREACTFLG { get => _SHAREACTFLG.PadLeft(1, '0').ByteSubString(0, 1); set => _SHAREACTFLG = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 51.超商通路-分潤%
        /// 9(02)
        /// </summary>
        [Description("超商通路-分潤%"), MaxLength(2)]
        public string ACTPERCENT { get => _ACTPERCENT.PadLeft(2, '0').ByteSubString(0, 2); set => _ACTPERCENT = value.PadLeft(2, '0').ByteSubString(0, 2); }
        /// <summary>
        /// 52.超商通路-清算手續費1
        /// 9(02)
        /// </summary>
        [Description("超商通路-清算手續費1"), MaxLength(2)]
        public string CLEARFEEMART1 { get => _CLEARFEEMART1.PadLeft(2, '0').ByteSubString(0, 2); set => _CLEARFEEMART1 = value.PadLeft(2, '0').ByteSubString(0, 2); }
        /// <summary>
        /// 53.超商通路-清算手續費2
        /// 9(02)
        /// </summary>
        [Description("超商通路-清算手續費2"), MaxLength(2)]
        public string CLEARFEEMART2 { get => _CLEARFEEMART2.PadLeft(2, '0').ByteSubString(0, 2); set => _CLEARFEEMART2 = value.PadLeft(2, '0').ByteSubString(0, 2); }
        /// <summary>
        /// 54.超商通路-清算手續費3
        /// 9(02)
        /// </summary>
        [Description("超商通路-清算手續費3"), MaxLength(2)]
        public string CLEARFEEMART3 { get => _CLEARFEEMART3.PadLeft(2, '0').ByteSubString(0, 2); set => _CLEARFEEMART3 = value.PadLeft(2, '0').ByteSubString(0, 2); }
        /// <summary>
        /// 55.超商通路-清算手續費4
        /// 9(02)
        /// </summary>
        [Description("超商通路-清算手續費4"), MaxLength(2)]
        public string CLEARFEEMART4 { get => _CLEARFEEMART4.PadLeft(2, '0').ByteSubString(0, 2); set => _CLEARFEEMART4 = value.PadLeft(2, '0').ByteSubString(0, 2); }
        /// <summary>
        /// 56.超商通路-清算手續費5
        /// 9(02)
        /// </summary>
        [Description("超商通路-清算手續費5"), MaxLength(2)]
        public string CLEARFEEMART5 { get => _CLEARFEEMART5.PadLeft(2, '0').ByteSubString(0, 2); set => _CLEARFEEMART5 = value.PadLeft(2, '0').ByteSubString(0, 2); }
        /// <summary>
        /// 57.郵局手續費收費方式
        /// X(01)
        /// </summary>
        [Description("郵局手續費收費方式"), MaxLength(1)]
        public string PAYKINDPOST { get => _PAYKINDPOST.PadLeft(1, '0').ByteSubString(0, 1); set => _PAYKINDPOST = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 58.郵局通路每筆總手續費
        /// 9(02)
        /// </summary>
        [Description("郵局通路每筆總手續費"), MaxLength(2)]
        public string ACTFEEPOST { get => _ACTFEEPOST.PadLeft(2, '0').ByteSubString(0, 2); set => _ACTFEEPOST = value.PadLeft(2, '0').ByteSubString(0, 2); }
        /// <summary>
        /// 59.郵局通路-是否分潤
        /// X(01)
        /// </summary>
        [Description("郵局通路-是否分潤"), MaxLength(1)]
        public string SHAREPOSTFLG { get => _SHAREPOSTFLG.PadLeft(1, '0').ByteSubString(0, 1); set => _SHAREPOSTFLG = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 60.郵局通路-分潤%
        /// 9(02)
        /// </summary>
        [Description("郵局通路-分潤%"), MaxLength(2)]
        public string POSTPERCENT { get => _POSTPERCENT.PadLeft(2, '0').ByteSubString(0, 2); set => _POSTPERCENT = value.PadLeft(2, '0').ByteSubString(0, 2); }
        /// <summary>
        /// 61.農金平台申請
        /// X(01)
        /// </summary>
        [Description("農金平台申請"), MaxLength(1)]
        public string AGRIFLAG { get => _AGRIFLAG.PadLeft(1, '0').ByteSubString(0, 1); set => _AGRIFLAG = value.PadLeft(1, '0').ByteSubString(0, 1); }
        /// <summary>
        /// 62.農金平台手續費
        /// 9(02)
        /// </summary>
        [Description("農金平台手續費"), MaxLength(2)]
        public string AGRIFEE { get => _AGRIFEE.PadLeft(2, '0').ByteSubString(0, 2); set => _AGRIFEE = value.PadLeft(2, '0').ByteSubString(0, 2); }
        /// <summary>
        /// 63.保留
        /// X(50)
        /// </summary>
        [Description("保留"), MaxLength(50)]
        public string FILLER { get => _FILLER.PadLeft(50, ' ').ByteSubString(0, 50); set => _FILLER = value.PadLeft(50, ' ').ByteSubString(0, 50); }
        /// <summary>
        /// 導入批號
        /// </summary>
        [Description("導入批號")]
        public string ImportBatchNo { get; set; }
        /// <summary>
        /// Source
        /// </summary>
        [Description("Source")]
        public string Source
        {
            get => $"{_KEYNO}{_ACCIDNO}{_CUSTNAME}{_APPBECODE}{_BRCODE}{_IDCODE}{_APPLYDATE}{_CHGDATE}{_APPLYSTAT}{_CHKNUMFLAG}{_CHKAMTFLAG}{_DUETERM}{_CHANNEL}{_FEE}{_RSTORE1}{_RSTORE2}{_RSTORE3}{_RSTORE4}{_RECVITEM1}{_RECVITEM2}{_RECVITEM3}{_RECVITEM4}{_RECVITEM5}{_ACTFEE}{_MARTFEE1}{_MARTFEE2}{_MARTFEE3}{_POSTFLAG}{_ACTFEEPT}{_POSTFEE}{_HIFLAG}{_HIFARE}{_NETDATE}{_AUTOFLAG}{_EBFLAG}{_EBDATE}{_EBFEEFLAG}{_EBFEE}{_EBACTTYPE}{_CHKDUPPAY}{_CUSTID}{_FUNC}{_MAFARE}{_NOFARE}{_CTBCFLAG}{_SHAREBNFTFLG}{_SHAREBEFTPERCENT}{_ACTFEEBEFT}{_ACTFEEMART}{_SHAREACTFLG}{_ACTPERCENT}{_CLEARFEEMART1}{_CLEARFEEMART2}{_CLEARFEEMART3}{_CLEARFEEMART4}{_CLEARFEEMART5}{_PAYKINDPOST}{_ACTFEEPOST}{_SHAREPOSTFLG}{_POSTPERCENT}{_AGRIFLAG}{_AGRIFEE}{_FILLER}";
            set
            {
                _KEYNO = value.ByteSubString(0, 6);
                _ACCIDNO = value.ByteSubString(6, 13);
                _CUSTNAME = value.ByteSubString(19, 40);
                _APPBECODE = value.ByteSubString(59, 4);
                _BRCODE = value.ByteSubString(63, 4);
                _IDCODE = value.ByteSubString(67, 11);
                _APPLYDATE = value.ByteSubString(78, 8);
                _CHGDATE = value.ByteSubString(86, 8);
                _APPLYSTAT = value.ByteSubString(94, 1);
                _CHKNUMFLAG = value.ByteSubString(95, 1);
                _CHKAMTFLAG = value.ByteSubString(96, 1);
                _DUETERM = value.ByteSubString(97, 1);
                _CHANNEL = value.ByteSubString(98, 1);
                _FEE = value.ByteSubString(99, 3);
                _RSTORE1 = value.ByteSubString(102, 1);
                _RSTORE2 = value.ByteSubString(103, 1);
                _RSTORE3 = value.ByteSubString(104, 1);
                _RSTORE4 = value.ByteSubString(105, 1);
                _RECVITEM1 = value.ByteSubString(106, 3);
                _RECVITEM2 = value.ByteSubString(109, 3);
                _RECVITEM3 = value.ByteSubString(112, 3);
                _RECVITEM4 = value.ByteSubString(115, 3);
                _RECVITEM5 = value.ByteSubString(118, 3);
                _ACTFEE = value.ByteSubString(121, 2);
                _MARTFEE1 = value.ByteSubString(123, 2);
                _MARTFEE2 = value.ByteSubString(125, 2);
                _MARTFEE3 = value.ByteSubString(127, 2);
                _POSTFLAG = value.ByteSubString(129, 1);
                _ACTFEEPT = value.ByteSubString(130, 2);
                _POSTFEE = value.ByteSubString(132, 2);
                _HIFLAG = value.ByteSubString(134, 1);
                _HIFARE = value.ByteSubString(135, 3);
                _NETDATE = value.ByteSubString(138, 8);
                _AUTOFLAG = value.ByteSubString(146, 1);
                _EBFLAG = value.ByteSubString(147, 1);
                _EBDATE = value.ByteSubString(148, 8);
                _EBFEEFLAG = value.ByteSubString(156, 1);
                _EBFEE = value.ByteSubString(157, 3);
                _EBACTTYPE = value.ByteSubString(160, 1);
                _CHKDUPPAY = value.ByteSubString(161, 1);
                _CUSTID = value.ByteSubString(162, 7);
                _FUNC = value.ByteSubString(169, 1);
                _MAFARE = value.ByteSubString(170, 3);
                _NOFARE = value.ByteSubString(173, 3);
                _CTBCFLAG = value.ByteSubString(176, 1);
                _SHAREBNFTFLG = value.ByteSubString(177, 1);
                _SHAREBEFTPERCENT = value.ByteSubString(178, 2);
                _ACTFEEBEFT = value.ByteSubString(180, 2);
                _ACTFEEMART = value.ByteSubString(182, 2);
                _SHAREACTFLG = value.ByteSubString(184, 1);
                _ACTPERCENT = value.ByteSubString(185, 2);
                _CLEARFEEMART1 = value.ByteSubString(187, 2);
                _CLEARFEEMART2 = value.ByteSubString(189, 2);
                _CLEARFEEMART3 = value.ByteSubString(191, 2);
                _CLEARFEEMART4 = value.ByteSubString(193, 2);
                _CLEARFEEMART5 = value.ByteSubString(195, 2);
                _PAYKINDPOST = value.ByteSubString(197, 1);
                _ACTFEEPOST = value.ByteSubString(198, 2);
                _SHAREPOSTFLG = value.ByteSubString(200, 1);
                _POSTPERCENT = value.ByteSubString(201, 2);
                _AGRIFLAG = value.ByteSubString(203, 1);
                _AGRIFEE = value.ByteSubString(204, 2);
                _FILLER = value.ByteSubString(206, 50);
                Src = value;
            }
        }
        /// <summary>
        /// 原Source
        /// Get：原導入資訊流
        /// Set：源自Source Set
        /// </summary>
        public string Src { get; private set; }
        #endregion
        #region Private
        private string _KEYNO;
        private string _ACCIDNO;
        private string _CUSTNAME;
        private string _APPBECODE;
        private string _BRCODE;
        private string _IDCODE;
        private string _APPLYDATE;
        private string _CHGDATE;
        private string _APPLYSTAT;
        private string _CHKNUMFLAG;
        private string _CHKAMTFLAG;
        private string _DUETERM;
        private string _CHANNEL;
        private string _FEE;
        private string _RSTORE1;
        private string _RSTORE2;
        private string _RSTORE3;
        private string _RSTORE4;
        private string _RECVITEM1;
        private string _RECVITEM2;
        private string _RECVITEM3;
        private string _RECVITEM4;
        private string _RECVITEM5;
        private string _ACTFEE;
        private string _MARTFEE1;
        private string _MARTFEE2;
        private string _MARTFEE3;
        private string _POSTFLAG;
        private string _ACTFEEPT;
        private string _POSTFEE;
        private string _HIFLAG;
        private string _HIFARE;
        private string _NETDATE;
        private string _AUTOFLAG;
        private string _EBFLAG;
        private string _EBDATE;
        private string _EBFEEFLAG;
        private string _EBFEE;
        private string _EBACTTYPE;
        private string _CHKDUPPAY;
        private string _CUSTID;
        private string _FUNC;
        private string _MAFARE;
        private string _NOFARE;
        private string _CTBCFLAG;
        private string _SHAREBNFTFLG;
        private string _SHAREBEFTPERCENT;
        private string _ACTFEEBEFT;
        private string _ACTFEEMART;
        private string _SHAREACTFLG;
        private string _ACTPERCENT;
        private string _CLEARFEEMART1;
        private string _CLEARFEEMART2;
        private string _CLEARFEEMART3;
        private string _CLEARFEEMART4;
        private string _CLEARFEEMART5;
        private string _PAYKINDPOST;
        private string _ACTFEEPOST;
        private string _SHAREPOSTFLG;
        private string _POSTPERCENT;
        private string _AGRIFLAG;
        private string _AGRIFEE;
        private string _FILLER;
        #endregion
    }
}
