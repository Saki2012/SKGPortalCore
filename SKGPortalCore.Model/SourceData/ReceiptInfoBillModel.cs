using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SKGPortalCore.Lib;

namespace SKGPortalCore.Model.SourceData
{
    /// <summary>
    /// 繳款資訊單-銀行
    /// </summary>
    [Description("繳款資訊單-銀行")]
    public class ReceiptInfoBillBankModel : IImportSource
    {
        #region Public
        /// <summary>
        /// 檔案中的第N行
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 1. 實體帳號(13)
        /// </summary>
        [Description("實體帳號"), MaxLength(13)]
        public string RealAccount { get { return _RealAccount; } set { _RealAccount = value.ByteSubString(0, 13); } }
        /// <summary>
        /// 2. 交易日期(8)
        /// </summary>
        [Description("交易日期"), MaxLength(8)]
        public string TradeDate { get { return _TradeDate; } set { _TradeDate = value.ByteSubString(0, 8); } }
        /// <summary>
        /// 3. 交易時間(6)
        /// </summary>
        [Description("交易時間"), MaxLength(6)]
        public string TradeTime { get { return _TradeTime; } set { _TradeTime = value.ByteSubString(0, 6); } }
        /// <summary>
        /// 4. 全方位銷帳編號(16)
        /// </summary>
        [Description("全方位銷帳編號"), MaxLength(16)]
        public string CompareCode { get { return _CompareCode; } set { _CompareCode = value.ByteSubString(0, 16); } }
        /// <summary>
        /// 5. 金額符號±(1)
        /// </summary>
        [Description("金額符號±"), MaxLength(1)]
        public string PN { get { return _PN; } set { _PN = value.ByteSubString(0, 1); } }
        /// <summary>
        /// 6. 交易金額(10)
        /// </summary>
        [Description("交易金額"), MaxLength(10)]
        public string Amount { get { return _Amount; } set { _Amount = value.ByteSubString(0, 10); } }
        /// <summary>
        /// 7. 摘要(10)
        /// </summary>
        [Description("摘要"), MaxLength(10)]
        public string Summary { get { return _Summary; } set { _Summary = value.ByteSubString(0, 10); } }
        /// <summary>
        /// 8. 代理行(4)
        /// </summary>
        [Description("代理行"), MaxLength(4)]
        public string Branch { get { return _Branch; } set { _Branch = value.ByteSubString(0, 4); } }
        /// <summary>
        /// 9. 交易通路(2)
        /// SA 存款; QA 支存; AG 約定扣繳;
        /// </summary>
        [Description("交易通路"), MaxLength(2)]
        public string TradeChannel { get { return _TradeChannel; } set { _TradeChannel = value.ByteSubString(0, 2); } }
        /// <summary>
        /// 10. 超商代號(2)
        /// 00 臨櫃; 06 自動化交易; 07 約定扣繳;
        /// </summary>
        [Description("超商代號"), MaxLength(2)]
        public string Channel { get { return _Channel; } set { _Channel = value.ByteSubString(0, 2); } }
        /// <summary>
        /// 11. 異動日期(8)
        /// </summary>
        [Description("異動日期"), MaxLength(8)]
        public string ChangeDate { get { return _ChangeDate; } set { _ChangeDate = value.ByteSubString(0, 8); } }
        /// <summary>
        /// 12. 營業日(8)
        /// </summary>
        [Description("營業日"), MaxLength(8)]
        public string BizDate { get { return _BizDate; } set { _BizDate = value.ByteSubString(0, 8); } }
        /// <summary>
        /// 13. 往來序號(6)
        /// </summary>
        [Description("往來序號"), MaxLength(6)]
        public string Serial { get { return _Serial; } set { _Serial = value.ByteSubString(0, 6); } }
        /// <summary>
        /// 14. 全方位帳號(6)
        /// </summary>
        [Description("全方位帳號"), MaxLength(6)]
        public string CustomerCode { get { return _CustomerCode; } set { _CustomerCode = value.ByteSubString(0, 6); } }
        /// <summary>
        /// 15. 手續費(3)
        /// </summary>
        [Description("手續費"), MaxLength(3)]
        public string Fee { get { return _Fee; } set { _Fee = value.ByteSubString(0, 3); } }
        /// <summary>
        /// 16. 保留(25)
        /// </summary>
        [Description("保留"), MaxLength(25)]
        public string Empty { get { return _Empty; } set { _Empty = value.ByteSubString(0, 25); } }
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
            get { return $"{_RealAccount}{_TradeDate}{_TradeTime}{_CompareCode}{_PN}{_Amount}{_Summary}{_Branch}{_TradeChannel}{_Channel}{_Channel}{_ChangeDate}{_BizDate}{_Serial}{_CustomerCode}{_Fee}{_Empty}"; }
            set
            {
                _RealAccount = value.ByteSubString(0, 13);
                _TradeDate = value.ByteSubString(13, 8);
                _TradeTime = value.ByteSubString(21, 6);
                _CompareCode = value.ByteSubString(27, 16);
                _PN = value.ByteSubString(43, 1);
                _Amount = value.ByteSubString(44, 10);
                _Summary = value.ByteSubString(54, 10);
                _Branch = value.ByteSubString(64, 4);
                _TradeChannel = value.ByteSubString(68, 2);
                _Channel = value.ByteSubString(70, 2);
                _ChangeDate = value.ByteSubString(72, 8);
                _BizDate = value.ByteSubString(80, 8);
                _Serial = value.ByteSubString(88, 6);
                _CustomerCode = value.ByteSubString(94, 6);
                _Fee = value.ByteSubString(100, 3);
                _Empty = value.ByteSubString(103, 25);
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
        private string _RealAccount;
        private string _TradeDate;
        private string _TradeTime;
        private string _CompareCode;
        private string _PN;
        private string _Amount;
        private string _Summary;
        private string _Branch;
        private string _TradeChannel;
        private string _Channel;
        private string _ChangeDate;
        private string _BizDate;
        private string _Serial;
        private string _CustomerCode;
        private string _Fee;
        private string _Empty;
        #endregion
    }
    /// <summary>
    /// 繳款資訊單-超商
    /// </summary>
    [Description("繳款資訊單-超商")]
    public class ReceiptInfoBillMarketModel : IImportSource
    {
        #region Public
        /// <summary>
        /// 檔案中的第N行
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 1. 錄別(1)
        /// </summary>
        [Description("錄別"), MaxLength(1)]
        public string Idx { get { return _Idx; } set { _Idx = value.ByteSubString(0, 1); } }
        /// <summary>
        /// 2. 公司代號(8)
        /// </summary>
        [Description("公司代號"), MaxLength(8)]
        public string CollectionType { get { return _CollectionType; } set { _CollectionType = value.ByteSubString(0, 8); } }
        /// <summary>
        /// 3. 代收機構代號(8)
        /// </summary>
        [Description("代收機構代號"), MaxLength(8)]
        public string Channel { get { return _Channel; } set { _Channel = value.ByteSubString(0, 8); } }
        /// <summary>
        /// 4. 代收門市店號(8)
        /// </summary>
        [Description("代收門市店號"), MaxLength(8)]
        public string Store { get { return _Store; } set { _Store = value.ByteSubString(0, 8); } }
        /// <summary>
        /// 5. 轉帳代繳帳號(14)
        /// </summary>
        [Description("轉帳代繳帳號"), MaxLength(14)]
        public string TransAccount { get { return _TransAccount; } set { _TransAccount = value.ByteSubString(0, 14); } }
        /// <summary>
        /// 6. 轉帳類別(3)
        /// </summary>
        [Description("轉帳類別"), MaxLength(3)]
        public string TransType { get { return _TransType; } set { _TransType = value.ByteSubString(0, 3); } }
        /// <summary>
        /// 7. 扣繳狀況(2)
        /// </summary>
        [Description("扣繳狀況"), MaxLength(2)]
        public string PayStatus { get { return _PayStatus; } set { _PayStatus = value.ByteSubString(0, 2); } }
        /// <summary>
        /// 8. 門市會計日(8)
        /// </summary>
        [Description("門市會計日"), MaxLength(8)]
        public string AccountingDay { get { return _AccountingDay; } set { _AccountingDay = value.ByteSubString(0, 8); } }
        /// <summary>
        /// 9. 顧客繳費日(8)
        /// </summary>
        [Description("顧客繳費日"), MaxLength(8)]
        public string PayDate { get { return _PayDate; } set { _PayDate = value.ByteSubString(0, 8); } }
        /// <summary>
        /// 10. Barcode1(9)
        /// </summary>
        [Description("Barcode1"), MaxLength(9)]
        public string Barcode1 { get { return _Barcode1; } set { _Barcode1 = value.ByteSubString(0, 9); } }
        /// <summary>
        /// 11. Barcode2(20)
        /// </summary>
        [Description("Barcode2"), MaxLength(20)]
        public string Barcode2 { get { return _Barcode2; } set { _Barcode2 = value.ByteSubString(0, 20); } }
        /// <summary>
        /// 12. Barcode3(15)
        /// </summary>
        [Description("Barcode3"), MaxLength(15)]
        public string Barcode3 { get { return _Barcode3; } set { _Barcode3 = value.ByteSubString(0, 15); } }
        /// <summary>
        /// 13. 保留(16)
        /// </summary>
        [Description("保留"), MaxLength(16)]
        public string Empty { get { return _Empty; } set { _Empty = value.ByteSubString(0, 16); } }
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
            get { return $"{_Idx}{_CollectionType}{_Channel}{_Store}{_TransAccount}{_TransType}{_PayStatus}{_AccountingDay}{_PayDate}{_Barcode1}{_Barcode2}{_Barcode3}{_Empty}"; }
            set
            {
                _Idx = value.ByteSubString(0, 1);
                _CollectionType = value.ByteSubString(1, 8);
                _Channel = value.ByteSubString(9, 8);
                _Store = value.ByteSubString(17, 8);
                _TransAccount = value.ByteSubString(25, 14);
                _TransType = value.ByteSubString(39, 3);
                _PayStatus = value.ByteSubString(42, 2);
                _AccountingDay = value.ByteSubString(44, 8);
                _PayDate = value.ByteSubString(52, 8);
                _Barcode1 = value.ByteSubString(60, 9);
                _Barcode2 = value.ByteSubString(69, 20);
                _Barcode3 = value.ByteSubString(89, 15);
                _Empty = value.ByteSubString(104, 16);
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
        private string _Idx;
        private string _CollectionType;
        private string _Channel;
        private string _Store;
        private string _TransAccount;
        private string _TransType;
        private string _PayStatus;
        private string _AccountingDay;
        private string _PayDate;
        private string _Barcode1;
        private string _Barcode2;
        private string _Barcode3;
        private string _Empty;
        #endregion
    }
    /// <summary>
    /// 繳款資訊單-超商產險
    /// </summary>
    [Description("繳款資訊單-超商產險")]
    public class ReceiptInfoBillMarketSPIModel : IImportSource
    {
        #region Public
        /// <summary>
        /// 檔案中的第N行
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 錄別(1)
        /// </summary>
        [Description("錄別"), MaxLength(1)]
        public string Idx { get { return _Idx; } set { _Idx = value.ByteSubString(0, 1); } }
        /// <summary>
        /// 代收機構代號(8)
        /// </summary>
        [Description("代收機構代號"), MaxLength(8)]
        public string Channel { get { return _Channel; } set { _Channel = value.ByteSubString(0, 8); } }
        /// <summary>
        /// 收件單位(8)
        /// </summary>
        [Description("收件單位"), MaxLength(8)]
        public string ISC { get { return _ISC; } set { _ISC = value.ByteSubString(0, 8); } }
        /// <summary>
        /// 入/扣帳日期(8)
        /// </summary>
        [Description("入/扣帳日期"), MaxLength(8)]
        public string TransDate { get { return _TransDate; } set { _TransDate = value.ByteSubString(0, 8); } }
        /// <summary>
        /// 繳費日期(8)
        /// </summary>
        [Description("繳費日期"), MaxLength(8)]
        public string PayDate { get { return _PayDate; } set { _PayDate = value.ByteSubString(0, 8); } }
        /// <summary>
        /// 交易序號(16)
        /// </summary>
        [Description("交易序號"), MaxLength(16)]
        public string Barcode2 { get { return _Barcode2; } set { _Barcode2 = value.ByteSubString(0, 16); } }
        /// <summary>
        /// 應繳日期[Barcode3:1-4](4)
        /// </summary>
        [Description("應繳日期"), MaxLength(4)]
        public string Barcode3_Date { get { return _Barcode3_Date; } set { _Barcode3_Date = value.ByteSubString(0, 4); } }
        /// <summary>
        /// 校對碼[Barcode3:5-6](2)
        /// </summary>
        [Description("校對碼"), MaxLength(2)]
        public string Barcode3_CompareCode { get { return _Barcode3_CompareCode; } set { _Barcode3_CompareCode = value.ByteSubString(0, 2); } }
        /// <summary>
        /// 代收金額[Barcode3:5-6](9)
        /// </summary>
        [Description("代收金額"), MaxLength(9)]
        public string Barcode3_Amount { get { return _Barcode3_Amount; } set { _Barcode3_Amount = value.ByteSubString(0, 9); } }
        /// <summary>
        /// 保留欄位1(18)
        /// </summary>
        [Description("保留欄位1"), MaxLength(18)]
        public string Empty1 { get { return _Empty1; } set { _Empty1 = value.ByteSubString(0, 18); } }
        /// <summary>
        /// 代收門市
        /// </summary>
        [Description("代收門市"), MaxLength(6)]
        public string Store { get { return _Store; } set { _Store = value.ByteSubString(0, 6); } }
        /// <summary>
        /// 保留欄位2(32)
        /// </summary>
        [Description("保留欄位2"), MaxLength(32)]
        public string Empty2 { get { return _Empty2; } set { _Empty2 = value.ByteSubString(0, 32); } }
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
            get { return $"{_Idx}{_Channel}{_ISC}{_TransDate}{_PayDate}{_Barcode2}{_Barcode3_Date}{_Barcode3_CompareCode}{_Barcode3_Amount}{_Empty1}{_Store}{_Empty2}"; }
            set
            {
                _Idx = value.ByteSubString(0, 1);
                _Channel = value.ByteSubString(1, 8);
                _ISC = value.ByteSubString(9, 8);
                _TransDate = value.ByteSubString(17, 8);
                _PayDate = value.ByteSubString(25, 8);
                _Barcode2 = value.ByteSubString(33, 16);
                _Barcode3_Date = value.ByteSubString(49, 4);
                _Barcode3_CompareCode = value.ByteSubString(53, 2);
                _Barcode3_Amount = value.ByteSubString(55, 9);
                _Empty1 = value.ByteSubString(64, 18);
                _Store = value.ByteSubString(82, 6);
                _Empty2 = value.ByteSubString(88, 32);
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
        private string _Idx;
        private string _Channel;
        private string _ISC;
        private string _TransDate;
        private string _PayDate;
        private string _Barcode2;
        private string _Barcode3_Date;
        private string _Barcode3_CompareCode;
        private string _Barcode3_Amount;
        private string _Empty1;
        private string _Store;
        private string _Empty2;
        #endregion
    }
    /// <summary>
    /// 繳款資訊單-郵局
    /// </summary>
    [Description("繳款資訊單-郵局")]
    public class ReceiptInfoBillPostModel : IImportSource
    {
        #region Public
        /// <summary>
        /// 檔案中的第N行
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 1. 劃撥帳號(8)
        /// </summary>
        [Description("劃撥帳號"), MaxLength(8)]
        public string CollectionType { get { return _CollectionType; } set { _CollectionType = value.ByteSubString(0, 8); } }
        /// <summary>
        /// 2. 交易日期(7)
        /// </summary>
        [Description("交易日期"), MaxLength(7)]
        public string TradeDate { get { return _TradeDate; } set { _TradeDate = value.ByteSubString(0, 7); } }
        /// <summary>
        /// 3. 經辦局號(6)
        /// </summary>
        [Description("經辦局號"), MaxLength(6)]
        public string Branch { get { return _Branch; } set { _Branch = value.ByteSubString(0, 6); } }
        /// <summary>
        /// 4. 交易代號(4)
        /// </summary>
        [Description("交易代號"), MaxLength(4)]
        public string Channel { get { return _Channel; } set { _Channel = value.ByteSubString(0, 4); } }
        /// <summary>
        /// 5. 交易序號(7) 
        /// </summary>
        [Description("交易序號"), MaxLength(7)]
        public string TradeSer { get { return _TradeSer; } set { _TradeSer = value.ByteSubString(0, 7); } }
        /// <summary>
        /// 6. 存提別(±)(1)
        /// </summary>
        [Description("存提別(±)"), MaxLength(1)]
        public string PN { get { return _PN; } set { _PN = value.ByteSubString(0, 1); } }
        /// <summary>
        /// 7. 交易金額(11)
        /// </summary>
        [Description("交易金額"), MaxLength(11)]
        public string Amount { get { return _Amount; } set { _Amount = value.ByteSubString(0, 11); } }
        /// <summary>
        /// 8. 用戶編號(24)
        /// </summary>
        [Description("用戶編號"), MaxLength(24)]
        public string CompareCode { get { return _CompareCode; } set { _CompareCode = value.ByteSubString(0, 24); } }
        /// <summary>
        /// 9. 保留(42)
        /// </summary>
        [Description("保留"), MaxLength(42)]
        public string Empty { get { return _Empty; } set { _Empty = value.ByteSubString(0, 42); } }
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
            get { return $"{_CollectionType}{_TradeDate}{_Branch}{_Channel}{_TradeSer}{_PN}{_Amount}{_CompareCode}{_Empty}"; }
            set
            {
                _CollectionType = value.ByteSubString(0, 8);
                _TradeDate = value.ByteSubString(8, 7);
                _Branch = value.ByteSubString(15, 6);
                _Channel = value.ByteSubString(21, 4);
                _TradeSer = value.ByteSubString(25, 7);
                _PN = value.ByteSubString(32, 1);
                _Amount = value.ByteSubString(33, 11);
                _CompareCode = value.ByteSubString(44, 24);
                _Empty = value.ByteSubString(68, 42);
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
        private string _CollectionType;
        private string _TradeDate;
        private string _Branch;
        private string _Channel;
        private string _TradeSer;
        private string _PN;
        private string _Amount;
        private string _CompareCode;
        private string _Empty;
        #endregion
    }
    /// <summary>
    /// 繳款資訊單-農金
    /// </summary>
    [Description("繳款資訊單-農金")]
    public class ReceiptInfoBillFarmModel : IImportSource
    {
        #region Public
        /// <summary>
        /// 檔案中的第N行
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 錄別(1)
        /// </summary>
        [Description("錄別"), MaxLength(1)]
        public string Idx { get { return _Idx; } set { _Idx = value.ByteSubString(0, 1); } }
        /// <summary>
        /// 公司代號(8)
        /// </summary>
        [Description("公司代號"), MaxLength(8)]
        public string CollectionType { get { return _CollectionType; } set { _CollectionType = value.ByteSubString(0, 8); } }
        /// <summary>
        /// 代收機構代號(8)
        /// </summary>
        [Description("代收機構代號"), MaxLength(8)]
        public string Channel { get { return _Channel; } set { _Channel = value.ByteSubString(0, 8); } }
        /// <summary>
        /// 代收門市店號(8)
        /// </summary>
        [Description("代收門市店號"), MaxLength(8)]
        public string Store { get { return _Store; } set { _Store = value.ByteSubString(0, 8); } }
        /// <summary>
        /// 轉帳代繳帳號(14)
        /// </summary>
        [Description("轉帳代繳帳號"), MaxLength(14)]
        public string TransAccount { get { return _TransAccount; } set { _TransAccount = value.ByteSubString(0, 14); } }
        /// <summary>
        /// 轉帳類別(3)
        /// </summary>
        [Description("轉帳類別"), MaxLength(3)]
        public string TransType { get { return _TransType; } set { _TransType = value.ByteSubString(0, 3); } }
        /// <summary>
        /// 扣繳狀況(2)
        /// </summary>
        [Description("扣繳狀況"), MaxLength(2)]
        public string PayStatus { get { return _PayStatus; } set { _PayStatus = value.ByteSubString(0, 2); } }
        /// <summary>
        /// 門市會計日(8)
        /// </summary>
        [Description("門市會計日"), MaxLength(8)]
        public string AccountingDay { get { return _AccountingDay; } set { _AccountingDay = value.ByteSubString(0, 8); } }
        /// <summary>
        /// 顧客繳費日(8)
        /// </summary>
        [Description("顧客繳費日"), MaxLength(8)]
        public string PayDate { get { return _PayDate; } set { _PayDate = value.ByteSubString(0, 8); } }
        /// <summary>
        /// Barcode1(9)
        /// </summary>
        [Description("Barcode1"), MaxLength(9)]
        public string Barcode1 { get { return _Barcode1; } set { _Barcode1 = value.ByteSubString(0, 9); } }
        /// <summary>
        /// Barcode2(20)
        /// </summary>
        [Description("Barcode2"), MaxLength(20)]
        public string Barcode2 { get { return _Barcode2; } set { _Barcode2 = value.ByteSubString(0, 20); } }
        /// <summary>
        /// Barcode3(15)
        /// </summary>
        [Description("Barcode3"), MaxLength(15)]
        public string Barcode3 { get { return _Barcode3; } set { _Barcode3 = value.ByteSubString(0, 15); } }
        /// <summary>
        /// 保留(16)
        /// </summary>
        [Description("保留"), MaxLength(16)]
        public string Empty { get { return _Empty; } set { _Empty = value.ByteSubString(0, 16); } }
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
            get { return $"{_Idx}{_CollectionType}{_Channel}{_Store}{_TransAccount}{_TransType}{_PayStatus}{_AccountingDay}{_PayDate}{_Barcode1}{_Barcode2}{_Barcode3}{_Empty}"; }
            set
            {
                _Idx = value.ByteSubString(0, 1);
                _CollectionType = value.ByteSubString(1, 8);
                _Channel = value.ByteSubString(9, 8);
                _Store = value.ByteSubString(17, 8);
                _TransAccount = value.ByteSubString(25, 14);
                _TransType = value.ByteSubString(39, 3);
                _PayStatus = value.ByteSubString(42, 2);
                _AccountingDay = value.ByteSubString(44, 8);
                _PayDate = value.ByteSubString(52, 8);
                _Barcode1 = value.ByteSubString(60, 9);
                _Barcode2 = value.ByteSubString(69, 20);
                _Barcode3 = value.ByteSubString(89, 15);
                _Empty = value.ByteSubString(104, 16);
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
        private string _Idx;
        private string _CollectionType;
        private string _Channel;
        private string _Store;
        private string _TransAccount;
        private string _TransType;
        private string _PayStatus;
        private string _AccountingDay;
        private string _PayDate;
        private string _Barcode1;
        private string _Barcode2;
        private string _Barcode3;
        private string _Empty;
        #endregion
    }
}
