using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SKGPortalCore.Model.BillData
{
    interface IReceiptInfoBill
    {
        int Id { get; set; }
        string Source { get; set; }
    }
    /// <summary>
    /// 繳款資訊單-銀行
    /// </summary>
    [Description("繳款資訊單-銀行")]
    public class ReceiptInfoBillBankModel : BasicDataModel, IReceiptInfoBill
    {
        public int Id { get; set; }
        /// <summary>
        /// 通路
        /// </summary>
        [Description("通路")]
        public string Channel { get; set; }
        /// <summary>
        /// 代收類別
        /// </summary>
        [Description("代收類別")]
        public string CollectionType { get; set; }
        /// <summary>
        /// 入/扣帳日期
        /// </summary>
        [Description("入/扣帳日期")]
        public string TransDate { get; set; }
        /// <summary>
        /// 繳費日期
        /// </summary>
        [Description("繳費日期")]
        public string TradeDate { get; set; }
        /// <summary>
        /// 交易序號
        /// </summary>
        [Description("交易序號")]
        public string CompareCode { get; set; }
        /// <summary>
        /// 代收金額
        /// </summary>
        [Description("代收金額")]
        public decimal Amount { get; set; }
        /// <summary>
        /// 代收門市
        /// </summary>
        [Description("代收門市")]
        public string Branch { get; set; }
        /// <summary>
        /// 交易代號
        /// </summary>
        [Description("交易代號")]
        public string TradeCode { get; set; }
        public string Source { get; set; }
    }
    /// <summary>
    /// 繳款資訊單-農金
    /// </summary>
    [Description("繳款資訊單-農金")]
    public class ReceiptInfoBillFarmModel : BasicDataModel, IReceiptInfoBill
    {
        public int Id { get; set; }
        /// <summary>
        /// 通路
        /// </summary>
        [Description("通路")]
        public string Channel { get; set; }
        /// <summary>
        /// 代收類別
        /// </summary>
        [Description("代收類別")]
        public string CollectionType { get; set; }
        /// <summary>
        /// 入/扣帳日期
        /// </summary>
        [Description("入/扣帳日期")]
        public string TransDate { get; set; }
        /// <summary>
        /// 繳費日期
        /// </summary>
        [Description("繳費日期")]
        public string TradeDate { get; set; }
        /// <summary>
        /// 交易序號
        /// </summary>
        [Description("交易序號")]
        public string CompareCode { get; set; }
        /// <summary>
        /// 代收金額
        /// </summary>
        [Description("代收金額")]
        public string Amount { get; set; }
        /// <summary>
        /// 代收門市
        /// </summary>
        [Description("代收門市")]
        public string Branch { get; set; }
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
    public class ReceiptInfoBillMarketModel : BasicDataModel, IReceiptInfoBill
    {
        public int Id { get; set; }
        /// <summary>
        /// 通路
        /// </summary>
        [Description("通路")]
        public string Channel { get; set; }
        /// <summary>
        /// 代收類別
        /// </summary>
        [Description("代收類別")]
        public string CollectionType { get; set; }
        /// <summary>
        /// 入/扣帳日期
        /// </summary>
        [Description("入/扣帳日期")]
        public string TransDate { get; set; }
        /// <summary>
        /// 繳費日期
        /// </summary>
        [Description("繳費日期")]
        public string TradeDate { get; set; }
        /// <summary>
        /// 交易序號
        /// </summary>
        [Description("交易序號")]
        public string CompareCode { get; set; }
        /// <summary>
        /// 代收金額
        /// </summary>
        [Description("代收金額")]
        public string Amount { get; set; }
        /// <summary>
        /// 代收門市
        /// </summary>
        [Description("代收門市")]
        public string Branch { get; set; }
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
    public class ReceiptInfoBillPostModel : BasicDataModel, IReceiptInfoBill
    {
        public int Id { get; set; }
        /// <summary>
        /// 通路
        /// </summary>
        [Description("通路")]
        public string Channel { get; set; }
        /// <summary>
        /// 代收類別
        /// </summary>
        [Description("代收類別")]
        public string CollectionType { get; set; }
        /// <summary>
        /// 入/扣帳日期
        /// </summary>
        [Description("入/扣帳日期")]
        public string TransDate { get; set; }
        /// <summary>
        /// 繳費日期
        /// </summary>
        [Description("繳費日期")]
        public string TradeDate { get; set; }
        /// <summary>
        /// 交易序號
        /// </summary>
        [Description("交易序號")]
        public string CompareCode { get; set; }
        /// <summary>
        /// 代收金額
        /// </summary>
        [Description("代收金額")]
        public string Amount { get; set; }
        /// <summary>
        /// 代收門市
        /// </summary>
        [Description("代收門市")]
        public string Branch { get; set; }
        /// <summary>
        /// 存提別(±)
        /// </summary>
        [Description("存提別(±)")]
        public char PN { get; set; }
        public string Source { get; set; }
    }
}
