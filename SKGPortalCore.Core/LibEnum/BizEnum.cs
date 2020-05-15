using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SKGPortalCore.Core.LibEnum
{
    /// <summary>
    /// 繳款狀態
    /// </summary>
    [Description("繳款狀態")]
    public enum PayStatus : byte
    {
        /// <summary>
        /// 未繳款
        /// </summary>
        [Description("未繳款")] Unpaid = 0,
        /// <summary>
        /// 繳款完成
        /// </summary>
        [Description("繳款完成")] PaidComplete = 1,
        /// <summary>
        /// 短繳
        /// </summary>
        [Description("短繳")] UnderPaid = 2,
        /// <summary>
        /// 溢繳
        /// </summary>
        [Description("溢繳")] OverPaid = 3,
    }
    /// <summary>
    /// 核銷狀態
    /// </summary>
    [Description("核銷狀態")]
    public enum WriteOfStatus : byte
    {
        /// <summary>
        /// 未核銷
        /// </summary>
        [Description("未核銷")] UnWrite = 0,
        /// <summary>
        /// 核銷中
        /// </summary>
        [Description("核銷中")] Writing = 1,
        /// <summary>
        /// 已核銷
        /// </summary>
        [Description("已核銷")] wrote = 2,
    }
    /// <summary>
    /// 撥款狀態
    /// </summary>
    [Description("撥款狀態")]
    public enum DisbursementStatus
    {
        /// <summary>
        /// 未撥款
        /// </summary>
        [Description("未撥款")] UnDisburse = 0,
        /// <summary>
        /// 撥款中
        /// </summary>
        [Description("撥款中")] Disbursing = 1,
        /// <summary>
        /// 已撥款
        /// </summary>
        [Description("已撥款")] Disbursed = 2,
    }
    /// <summary>
    /// 虛擬帳號長度
    /// </summary>
    [Description("虛擬帳號長度")]
    public enum VirtualAccountLen : byte
    {
        /// <summary>
        /// 13碼
        /// </summary>
        [Description("13碼")] Len13 = 13,
        /// <summary>
        /// 14碼
        /// </summary>
        [Description("14碼")] Len14 = 14,
        /// <summary>
        /// 16碼
        /// </summary>
        [Description("16碼")] Len16 = 16,
    }
    /// <summary>
    /// 自組銷帳編號1
    /// </summary>
    [Description("自組銷帳編號1")]
    public enum VirtualAccount1 : byte
    {
        /// <summary>
        /// 空白
        /// </summary>
        [Description] Empty = 0,
        /// <summary>
        /// 期別
        /// </summary>
        [Description("期別")] BillTerm = 1
    }
    /// <summary>
    /// 自組銷帳編號2
    /// </summary>
    [Description("自組銷帳編號2")]
    public enum VirtualAccount2 : byte
    {
        /// <summary>
        /// 空白
        /// </summary>
        [Description] Empty = 0,
        /// <summary>
        /// 繳款人編號
        /// </summary>
        [Description("繳款人編號")] PayerNo = 1,
        /// <summary>
        /// 流水號
        /// </summary>
        [Description("流水號")] Seq = 2,
    }
    /// <summary>
    /// 自組銷帳編號3
    /// </summary>
    [Description("自組銷帳編號3")]
    public enum VirtualAccount3 : byte
    {
        /// <summary>
        /// 無檢碼
        /// </summary>
        [Description("無檢碼")] NoverifyCode = 1,
        /// <summary>
        /// 檢碼
        /// </summary>
        [Description("檢碼")] Seq = 2,
        /// <summary>
        /// 檢碼(流水號+金額)
        /// </summary>
        [Description("檢碼(流水號+金額)")] SeqAmount = 3,
        /// <summary>
        /// 檢碼(流水號+截止日)
        /// </summary>
        [Description("檢碼(流水號+截止日)")] SeqPayEndDate = 4,
        /// <summary>
        /// 檢碼(流水號+金額+截止日)
        /// </summary>
        [Description("檢碼(流水號+金額+截止日)")] SeqAmountPayEndDate = 5,
    }
    /// <summary>
    /// 通路手續費清算方式
    /// </summary>
    [Description("通路手續費清算方式")]
    public enum ChargePayType : byte
    {
        /// <summary>
        /// 內扣
        /// </summary>
        [Description("內扣")] Deduction = 0,
        /// <summary>
        /// 外加
        /// </summary>
        [Description("外加")] Increase = 1,
    }
    /// <summary>
    /// 繳款人類別
    /// </summary>
    [Description("繳款人類別")]
    public enum PayerType : byte
    {
        /// <summary>
        /// 一般
        /// </summary>
        [Description("一般")] Normal = 0,
        /// <summary>
        /// 約定帳號扣款
        /// </summary>
        [Description("約定帳號扣款")] Account = 1,
        /// <summary>
        /// 信用卡授權扣款
        /// </summary>
        [Description("信用卡授權扣款")] CreditCard = 2,
        /// <summary>
        /// ACH扣款
        /// </summary>
        [Description("ACH扣款")] ACH = 3,
    }
    /// <summary>
    /// 通路類別
    /// </summary>
    [Description("通路類別")]
    public enum ChannelGroupType : byte
    {
        /// <summary>
        /// 銀行通路        
        /// </summary>
        [Description("銀行通路")] Bank = 0,
        /// <summary>
        /// 超商通路
        /// </summary>
        [Description("超商通路")] Market = 1,
        /// <summary>
        /// 郵局通路
        /// </summary>
        [Description("郵局通路")] Post = 2,
        /// <summary>
        /// 自收款
        /// </summary>
        [Description("自收款")] Self = 255,
    }
    /// <summary>
    /// 商戶類型
    /// </summary>
    [Description("商戶類型")]
    public enum BizCustType : byte
    {
        /// <summary>
        /// 商戶
        /// </summary>
        [Description("商戶")] Cust = 0,
        /// <summary>
        /// 介紹商
        /// </summary>
        [Description("介紹商")] Introducer = 1,
        /// <summary>
        /// Hitrust
        /// </summary>
        [Description("Hitrust")] Hitrust = 2,
    }
    /// <summary>
    /// 銀行手續費類型
    /// </summary>
    [Description("銀行手續費類型")]
    public enum BankFeeType : byte
    {
        /// <summary>
        /// 清算手續費(次月扣款)
        /// </summary>
        [Description("清算手續費(次月扣款)")] ClearFee_NextMonth = 1,
        /// <summary>
        /// 清算手續費(當月扣款)
        /// </summary>
        [Description("清算手續費(當月扣款)")] ClearFee_CurMonth = 2,
        /// <summary>
        /// 每筆總手續費
        /// </summary>
        [Description("每筆總手續費")] TotalFee = 3,
        /// <summary>
        /// Hitrust+清算手續費(次月扣款)
        /// </summary>
        [Description("Hitrust+清算手續費(次月扣款)")] Hitrust_ClearFee_NextMonth = 4,
        /// <summary>
        /// Hitrust+清算手續費(當月扣款)
        /// </summary>
        [Description("Hitrust+清算手續費(當月扣款)")] Hitrust_ClearFee_CurMonth = 5,

    }
    /// <summary>
    /// 通路帳務核銷週期
    /// </summary>
    [Description("通路帳務核銷週期")]
    public enum PayPeriodType : byte
    {
        /// <summary>
        /// 7-11、全家、郵局、農金、亞太
        /// 入帳日：入帳日為非營業日時，會與前一個傳輸日一同匯款
        /// Ex:T+3為例 
        /// -------------------------
        /// |資料傳輸日|入帳，撥款日|
        /// -------------------------
        /// |星期一　　|星期四　　　|
        /// -------------------------
        /// |星期二　　|(本週)星期五|
        /// -------------------------
        /// |星期三　　|(本週)星期五|
        /// -------------------------
        /// |星期四　　|(本週)星期五|
        /// -------------------------
        /// |星期五　　|星期一　　　|
        /// -------------------------
        /// |星期六　　|星期二　　　|
        /// -------------------------
        /// |星期日　　|星期三　　　|
        /// -------------------------
        /// </summary>
        /// <returns></returns>
        [Description(@"日結-
入帳日為非營業日時，會與前一個傳輸日一同匯款")] NDay_A = 1,
        /// <summary>
        /// OK
        /// 入帳日：入帳日為非營業日時，往後遞延下一個營業日
        /// Ex:T+3為例
        /// -------------------------
        /// |資料傳輸日|入帳，撥款日|
        /// -------------------------
        /// |星期一　　|星期四　　　|
        /// -------------------------
        /// |星期二　　|星期五　　　|
        /// -------------------------
        /// |星期三　　|(下週)星期一|
        /// -------------------------
        /// |星期四　　|(下週)星期一|
        /// -------------------------
        /// |星期五　　|(下週)星期一|
        /// -------------------------
        /// |星期六　　|星期二　　　|
        /// -------------------------
        /// |星期日　　|星期三　　　|
        /// -------------------------
        /// </summary>
        /// <returns></returns>
        [Description(@"日結-
入帳日為非營業日時，往後遞延下一個營業日")] NDay_B = 2,
        /// <summary>
        /// 萊爾富
        /// 入帳日：
        /// 1. 傳輸日為非營業日時，金流會併在後一個營業日的入帳日一起匯款
        /// 2. 入帳日為非營業日時，往後遞延下一個營業日
        /// Ex:T+3為例 
        /// -------------------------
        /// |資料傳輸日|入帳，撥款日|
        /// -------------------------
        /// |星期一　　|星期四　　　|
        /// -------------------------
        /// |星期二　　|星期五　　　|
        /// -------------------------
        /// |星期三　　|星期一　　　|
        /// -------------------------
        /// |星期四　　|星期二　　　|
        /// -------------------------
        /// |星期五　　|星期三　　　|
        /// -------------------------
        /// |星期六　　|(下週)星期四|
        /// -------------------------
        /// |星期日　　|(下週)星期四|
        /// -------------------------
        /// </summary>
        /// <returns></returns>
        [Description(@"日結-
1. 傳輸日為非營業日時，金流會併在後一個營業日的入帳日一起匯款
2. 入帳日為非營業日時，往後遞延下一個營業日")] NDay_C = 3,
        /// <summary>
        /// 週結
        /// Ex:週三結帳
        /// -------------------------------------------
        /// ｜               2020/02                  ｜          
        /// -------------------------------------------
        /// ｜日　｜一　｜二　｜三　｜四　｜五　｜六　｜
        /// -------------------------------------------
        /// ｜　　｜１７｜１８｜１９｜２０｜２１｜２２｜
        /// -------------------------------------------
        /// ｜２３｜　　｜　　｜匯款｜　　｜　　｜　　｜
        /// -------------------------------------------
        /// </summary>
        [Description("週結")] Week = 4,
        /// <summary>
        /// 旬結
        /// </summary>
        [Description("旬結")] TenDay = 5,
    }
}
