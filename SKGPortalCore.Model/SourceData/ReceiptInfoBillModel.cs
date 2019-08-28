using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SKGPortalCore.Model.SourceData
{
    public interface IReceiptInfoBill
    {
        int Id { get; set; }
        string ImportBatchNo { get; set; }
        string Source { get; set; }
    }
    /// <summary>
    /// 繳款資訊單-銀行
    /// </summary>
    [Description("繳款資訊單-銀行")]
    public class ReceiptInfoBillBankModel : IReceiptInfoBill
    {
        /// <summary>
        /// 檔案中的第N行
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 1. 實體帳號(13)
        /// </summary>
        [Description("實體帳號"), StringLength(13)]
        public string RealAccount { get; set; }
        /// <summary>
        /// 2. 交易日期(8)
        /// </summary>
        [Description("交易日期"), StringLength(8)]
        public string TradeDate { get; set; }
        /// <summary>
        /// 3. 交易時間(6)
        /// </summary>
        [Description("交易時間"), StringLength(6)]
        public string TradeTime { get; set; }
        /// <summary>
        /// 4. 全方位銷帳編號(16)
        /// </summary>
        [Description("全方位銷帳編號"), StringLength(16)]
        public string CompareCode { get; set; }
        /// <summary>
        /// 5. 金額符號±(1)
        /// </summary>
        [Description("金額符號±"), StringLength(1)]
        public string PN { get; set; }
        /// <summary>
        /// 6. 交易金額(10)
        /// </summary>
        [Description("交易金額"), StringLength(10)]
        public string Amount { get; set; }
        /// <summary>
        /// 7. 摘要(10)
        /// </summary>
        [Description("摘要"), StringLength(10)]
        public string Summary { get; set; }
        /// <summary>
        /// 8. 代理行(4)
        /// </summary>
        [Description("代理行"), StringLength(4)]
        public string Branch { get; set; }
        /// <summary>
        /// 9. 交易通路(2)
        /// SA 存款; QA 支存; AG 約定扣繳;
        /// </summary>
        [Description("交易通路"), StringLength(2)]
        public string TradeChannel { get; set; }
        /// <summary>
        /// 10. 超商代號(2)
        /// 00 臨櫃; 06 自動化交易; 07 約定扣繳;
        /// </summary>
        [Description("超商代號"), StringLength(2)]
        public string Channel { get; set; }
        /// <summary>
        /// 11. 異動日期(8)
        /// </summary>
        [Description("異動日期"), StringLength(8)]
        public string ChangeDate { get; set; }
        /// <summary>
        /// 12. 營業日(8)
        /// </summary>
        [Description("營業日"), StringLength(8)]
        public string BizDate { get; set; }
        /// <summary>
        /// 13. 往來序號(6)
        /// </summary>
        [Description("往來序號"), StringLength(6)]
        public string Serial { get; set; }
        /// <summary>
        /// 14. 全方位帳號(6)
        /// </summary>
        [Description("全方位帳號"), StringLength(6)]
        public string CustomerCode { get; set; }
        /// <summary>
        /// 15. 手續費(3)
        /// </summary>
        [Description("手續費"), StringLength(3)]
        public string Fee { get; set; }
        /// <summary>
        /// 16. 保留(25)
        /// </summary>
        [Description("保留"), StringLength(25)]
        public string Empty { get; set; }
        /// <summary>
        /// 導入批號
        /// </summary>
        [Description("導入批號")]
        public string ImportBatchNo { get; set; }
        /// <summary>
        /// 原Source
        /// </summary>
        [Description("原Source")]
        public string Source { get; set; }
    }
    /// <summary>
    /// 繳款資訊單-超商
    /// </summary>
    [Description("繳款資訊單-超商")]
    public class ReceiptInfoBillMarketModel : IReceiptInfoBill
    {
        /// <summary>
        /// 檔案中的第N行
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 1. 錄別(1)
        /// </summary>
        [Description("錄別"), StringLength(1)]
        public string Idx { get; set; }
        /// <summary>
        /// 2. 公司代號(8)
        /// </summary>
        [Description("公司代號"), StringLength(8)]
        public string CollectionType { get; set; }
        /// <summary>
        /// 3. 代收機構代號(8)
        /// </summary>
        [Description("代收機構代號"), StringLength(8)]
        public string Channel { get; set; }
        /// <summary>
        /// 4. 代收門市店號(8)
        /// </summary>
        [Description("代收門市店號"), StringLength(8)]
        public string Store { get; set; }
        /// <summary>
        /// 5. 轉帳代繳帳號(14)
        /// </summary>
        [Description("轉帳代繳帳號"), StringLength(14)]
        public string TransAccount { get; set; }
        /// <summary>
        /// 6. 轉帳類別(3)
        /// </summary>
        [Description("轉帳類別"), StringLength(3)]
        public string TransType { get; set; }
        /// <summary>
        /// 7. 扣繳狀況(2)
        /// </summary>
        [Description("扣繳狀況"), StringLength(2)]
        public string PayStatus { get; set; }
        /// <summary>
        /// 8. 門市會計日(8)
        /// </summary>
        [Description("門市會計日"), StringLength(8)]
        public string AccountingDay { get; set; }
        /// <summary>
        /// 9. 顧客繳費日(8)
        /// </summary>
        [Description("顧客繳費日"), StringLength(8)]
        public string PayDate { get; set; }
        /// <summary>
        /// 10. Barcode1(9)
        /// </summary>
        [Description("Barcode1"), StringLength(9)]
        public string Barcode1 { get; set; }
        /// <summary>
        /// 11. Barcode2(20)
        /// </summary>
        [Description("Barcode2"), StringLength(20)]
        public string Barcode2 { get; set; }
        /// <summary>
        /// 12. Barcode3(15)
        /// </summary>
        [Description("Barcode3"), StringLength(15)]
        public string Barcode3 { get; set; }
        /// <summary>
        /// 13. 保留(16)
        /// </summary>
        [Description("保留"), StringLength(16)]
        public string Empty { get; set; }
        /// <summary>
        /// 導入批號
        /// </summary>
        [Description("導入批號")]
        public string ImportBatchNo { get; set; }
        /// <summary>
        /// 原Source
        /// </summary>
        [Description("原Source")]
        public string Source { get; set; }
    }
    /// <summary>
    /// 繳款資訊單-超商產險
    /// </summary>
    [Description("繳款資訊單-超商產險")]
    public class ReceiptInfoBillMarketSPIModel : IReceiptInfoBill
    {
        /// <summary>
        /// 檔案中的第N行
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 錄別(1)
        /// </summary>
        [Description("錄別"), StringLength(1)]
        public string Idx { get; set; }
        /// <summary>
        /// 代收機構代號(8)
        /// </summary>
        [Description("代收機構代號"), StringLength(8)]
        public string Channel { get; set; }
        /// <summary>
        /// 收件單位(8)
        /// </summary>
        [Description("收件單位"), StringLength(8)]
        public string ISC { get; set; }
        /// <summary>
        /// 入/扣帳日期(8)
        /// </summary>
        [Description("入/扣帳日期"), StringLength(8)]
        public string TransDate { get; set; }
        /// <summary>
        /// 繳費日期(8)
        /// </summary>
        [Description("繳費日期"), StringLength(8)]
        public string PayDate { get; set; }
        /// <summary>
        /// 交易序號(16)
        /// </summary>
        [Description("交易序號"), StringLength(16)]
        public string Barcode2 { get; set; }
        /// <summary>
        /// 應繳日期[Barcode3:1-4](4)
        /// </summary>
        [Description("應繳日期"), StringLength(4)]
        public string Barcode3_Date { get; set; }
        /// <summary>
        /// 校對碼[Barcode3:5-6](2)
        /// </summary>
        [Description("校對碼"), StringLength(2)]
        public string Barcode3_CompareCode { get; set; }
        /// <summary>
        /// 代收金額[Barcode3:5-6](9)
        /// </summary>
        [Description("代收金額"), StringLength(9)]
        public string Barcode3_Amount { get; set; }
        /// <summary>
        /// 保留欄位1(18)
        /// </summary>
        [Description("保留欄位1"), StringLength(18)]
        public string Empty1 { get; set; }
        /// <summary>
        /// 代收門市
        /// </summary>
        [Description("代收門市"), StringLength(6)]
        public string Store { get; set; }
        /// <summary>
        /// 保留欄位2(32)
        /// </summary>
        [Description("保留欄位2"), StringLength(32)]
        public string Empty2 { get; set; }
        /// <summary>
        /// 導入批號
        /// </summary>
        [Description("導入批號")]
        public string ImportBatchNo { get; set; }
        /// <summary>
        /// 原Source
        /// </summary>
        [Description("原Source")]
        public string Source { get; set; }
    }
    /// <summary>
    /// 繳款資訊單-郵局
    /// </summary>
    [Description("繳款資訊單-郵局")]
    public class ReceiptInfoBillPostModel : IReceiptInfoBill
    {
        /// <summary>
        /// 檔案中的第N行
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 1. 劃撥帳號(8)
        /// </summary>
        [Description("劃撥帳號"), StringLength(8)]
        public string CollectionType { get; set; }
        /// <summary>
        /// 2. 交易日期(7)
        /// </summary>
        [Description("交易日期"), StringLength(7)]
        public string TradeDate { get; set; }
        /// <summary>
        /// 3. 經辦局號(6)
        /// </summary>
        [Description("經辦局號"), StringLength(6)]
        public string Branch { get; set; }
        /// <summary>
        /// 4. 交易代號(4)
        /// </summary>
        [Description("交易代號"), StringLength(4)]
        public string Channel { get; set; }
        /// <summary>
        /// 5. 交易序號(7) 
        /// </summary>
        [Description("交易序號"), StringLength(7)]
        public string TradeSer { get; set; }
        /// <summary>
        /// 6. 存提別(±)(1)
        /// </summary>
        [Description("存提別(±)"), StringLength(1)]
        public string PN { get; set; }
        /// <summary>
        /// 7. 交易金額(11)
        /// </summary>
        [Description("交易金額"), StringLength(11)]
        public string Amount { get; set; }
        /// <summary>
        /// 8. 用戶編號(24)
        /// </summary>
        [Description("用戶編號"), StringLength(24)]
        public string CompareCode { get; set; }
        /// <summary>
        /// 9. 保留(42)
        /// </summary>
        [Description("保留"), StringLength(42)]
        public string Empty { get; set; }
        /// <summary>
        /// 導入批號
        /// </summary>
        [Description("導入批號")]
        public string ImportBatchNo { get; set; }
        /// <summary>
        /// 原Source
        /// </summary>
        [Description("原Source")]
        public string Source { get; set; }
    }
    /// <summary>
    /// 繳款資訊單-農金
    /// </summary>
    [Description("繳款資訊單-農金")]
    public class ReceiptInfoBillFarmModel : IReceiptInfoBill
    {
        /// <summary>
        /// 檔案中的第N行
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 錄別(1)
        /// </summary>
        [Description("錄別"), StringLength(1)]
        public string Idx { get; set; }
        /// <summary>
        /// 公司代號(8)
        /// </summary>
        [Description("公司代號"), StringLength(8)]
        public string CollectionType { get; set; }
        /// <summary>
        /// 代收機構代號(8)
        /// </summary>
        [Description("代收機構代號"), StringLength(8)]
        public string Channel { get; set; }
        /// <summary>
        /// 代收門市店號(8)
        /// </summary>
        [Description("代收門市店號"), StringLength(8)]
        public string Store { get; set; }
        /// <summary>
        /// 轉帳代繳帳號(14)
        /// </summary>
        [Description("轉帳代繳帳號"), StringLength(14)]
        public string TransAccount { get; set; }
        /// <summary>
        /// 轉帳類別(3)
        /// </summary>
        [Description("轉帳類別"), StringLength(3)]
        public string TransType { get; set; }
        /// <summary>
        /// 扣繳狀況(2)
        /// </summary>
        [Description("扣繳狀況"), StringLength(2)]
        public string PayStatus { get; set; }
        /// <summary>
        /// 門市會計日(8)
        /// </summary>
        [Description("門市會計日"), StringLength(8)]
        public string AccountingDay { get; set; }
        /// <summary>
        /// 顧客繳費日(8)
        /// </summary>
        [Description("顧客繳費日"), StringLength(8)]
        public string PayDate { get; set; }
        /// <summary>
        /// Barcode1(9)
        /// </summary>
        [Description("Barcode1"), StringLength(9)]
        public string Barcode1 { get; set; }
        /// <summary>
        /// Barcode2(20)
        /// </summary>
        [Description("Barcode2"), StringLength(20)]
        public string Barcode2 { get; set; }
        /// <summary>
        /// Barcode3(15)
        /// </summary>
        [Description("Barcode3"), StringLength(15)]
        public string Barcode3 { get; set; }
        /// <summary>
        /// 保留(16)
        /// </summary>
        [Description("保留"), StringLength(16)]
        public string Empty { get; set; }
        /// <summary>
        /// 導入批號
        /// </summary>
        [Description("導入批號")]
        public string ImportBatchNo { get; set; }
        /// <summary>
        /// 原Source
        /// </summary>
        [Description("原Source")]
        public string Source { get; set; }
    }
}
