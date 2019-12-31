using System.ComponentModel;

namespace SKGPortalCore.Model
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
        [Description("未繳款")]
        Unpaid = 0,
        /// <summary>
        /// 繳款完成
        /// </summary>
        [Description("繳款完成")]
        PaidComplete = 1,
        /// <summary>
        /// 短繳
        /// </summary>
        [Description("短繳")]
        UnderPaid = 2,
        /// <summary>
        /// 溢繳
        /// </summary>
        [Description("溢繳")]
        OverPaid = 3,
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
        [Description("未核銷")]
        UnWrite = 0,
        /// <summary>
        /// 核銷中
        /// </summary>
        [Description("核銷中")]
        Writing = 1,
        /// <summary>
        /// 已核銷
        /// </summary>
        [Description("已核銷")]
        wrote = 2,
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
        [Description("未撥款")]
        UnDisburse = 0,
        /// <summary>
        /// 撥款中
        /// </summary>
        [Description("撥款中")]
        Disbursing = 1,
        /// <summary>
        /// 已撥款
        /// </summary>
        [Description("已撥款")]
        Disbursed = 2,
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
        [Description]
        Empty = 0,
        /// <summary>
        /// 期別
        /// </summary>
        [Description("期別")]
        BillTerm = 1
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
        [Description]
        Empty = 0,
        /// <summary>
        /// 繳款人編號
        /// </summary>
        [Description("繳款人編號")]
        PayerNo = 1,
        /// <summary>
        /// 流水號
        /// </summary>
        [Description("流水號")]
        Seq = 2,
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
        [Description("無檢碼")]
        NoverifyCode = 1,
        /// <summary>
        /// 檢碼
        /// </summary>
        [Description("檢碼")]
        Seq = 2,
        /// <summary>
        /// 檢碼(流水號+金額)
        /// </summary>
        [Description("檢碼(流水號+金額)")]
        SeqAmount = 3,
        /// <summary>
        /// 檢碼(流水號+截止日)
        /// </summary>
        [Description("檢碼(流水號+截止日)")]
        SeqPayEndDate = 4,
        /// <summary>
        /// 檢碼(流水號+金額+截止日)
        /// </summary>
        [Description("檢碼(流水號+金額+截止日)")]
        SeqAmountPayEndDate = 5,
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
        [Description("內扣")]
        Deduction = 0,
        /// <summary>
        /// 外加
        /// </summary>
        [Description("外加")]
        Increase = 1,
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
        [Description("一般")]
        Normal = 0,
        /// <summary>
        /// 約定帳號扣款
        /// </summary>
        [Description("約定帳號扣款")]
        Account = 1,
        /// <summary>
        /// 信用卡授權扣款
        /// </summary>
        [Description("信用卡授權扣款")]
        CreditCard = 2,
        /// <summary>
        /// ACH扣款
        /// </summary>
        [Description("ACH扣款")]
        ACH = 3,
    }
    /// <summary>
    /// 帳單基本資料
    /// </summary>
    [Description("帳單基本資料")]
    public enum BillInfo : byte
    {
        /// <summary>
        /// 空
        /// </summary>
        [Description]
        Empty = 0,
        /// <summary>
        /// 繳款人編號
        /// </summary>
        [Description("繳款人編號")]
        PayerId = 1,
        /// <summary>
        /// 繳款人名稱
        /// </summary>
        [Description("繳款人名稱")]
        PayerName = 2,
        /// <summary>
        /// 繳款人類別
        /// </summary>
        [Description("繳款人類別")]
        PyaerType = 3,
        /// <summary>
        /// 身份證字號
        /// </summary>
        [Description("身份證字號")]
        IDCard = 4,
        /// <summary>
        /// 郵遞區號
        /// </summary>
        [Description("郵遞區號")]
        ZipCode = 5,
        /// <summary>
        /// 地址
        /// </summary>
        [Description("地址")]
        Address = 6,
        /// <summary>
        /// 電話
        /// </summary>
        [Description("電話")]
        Tel = 7,
        /// <summary>
        /// 銀行銷帳編號
        /// </summary>
        [Description("銀行銷帳編號")]
        BankBarCode = 8,
        /// <summary>
        /// 期別代號
        /// </summary>
        [Description("期別代號")]
        BillTermId = 9,
        /// <summary>
        /// 繳款截止日
        /// </summary>
        [Description("繳款截止日")]
        PayEndDate = 10,
        /// <summary>
        /// 帳單備註1
        /// </summary>
        [Description("帳單備註1")]
        BillMemo1 = 11,
        /// <summary>
        /// 帳單備註2
        /// </summary>
        [Description("帳單備註2")]
        BillMemo2 = 12,
        /// <summary>
        /// 備註01
        /// </summary>
        [Description("備註01")]
        PayerMemo1 = 13,
        /// <summary>
        /// 備註02
        /// </summary>
        [Description("備註02")]
        PayerMemo2 = 14,
        /// <summary>
        /// 備註03
        /// </summary>
        [Description("備註03")]
        PayerMemo3 = 15,
        /// <summary>
        /// 備註04
        /// </summary>
        [Description("備註04")]
        PayerMemo4 = 16,
    }
    /// <summary>
    /// 帳戶狀態
    /// </summary>
    [Description("帳戶狀態")]
    public enum AccountStatus : byte
    {
        /// <summary>
        /// 停用
        /// </summary>
        [Description("停用")]
        Unable = 0,
        /// <summary>
        /// 啟用
        /// </summary>
        [Description("啟用")]
        Enable = 1,
        /// <summary>
        /// 凍結
        /// </summary>
        [Description("凍結")]
        Freeze = 2,
        /// <summary>
        /// 密碼過期
        /// </summary>
        [Description("密碼過期")]
        PasuwadoExpired = 3,
        /// <summary>
        /// 主機預設密碼
        /// </summary>
        [Description("主機預設密碼")]
        HostDefault = 4,
    }
    /// <summary>
    /// 前/後台
    /// </summary>
    [Description("前/後台")]
    public enum EndType : byte
    {
        /// <summary>
        /// 後台
        /// </summary>
        [Description("後台")]
        Backend = 0,
        /// <summary>
        /// 前台
        /// </summary>
        [Description("前台")]
        Frontend = 1
    }
    /// <summary>
    /// 功能權限動作
    /// </summary>
    [Description("功能權限動作")]
    public enum FuncAction : int
    {
        /// <summary>
        /// 使用
        /// </summary>
        [Description("使用")]
        Use = 1,
        /// <summary>
        /// 查詢
        /// </summary>
        [Description("查詢")]
        Query = 2,
        /// <summary>
        /// 新增
        /// </summary>
        [Description("新增")]
        Create = 4,
        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        Update = 8,
        /// <summary>
        /// 刪除
        /// </summary>
        [Description("刪除")]
        Delete = 16,
        /// <summary>
        /// 審核
        /// </summary>
        [Description("審核")]
        Approve = 32,
        /// <summary>
        /// 結案
        /// </summary>
        [Description("結案")]
        EndCase = 64,
        /// <summary>
        /// 作廢
        /// </summary>
        [Description("作廢")]
        Invalid = 128,
        /// <summary>
        /// 全部
        /// </summary>
        All = 255,
    }
    /// <summary>
    /// 單據狀態
    /// </summary>
    [Description("單據狀態")]
    public enum FormStatus : byte
    {
        /// <summary>
        /// 保存
        /// </summary>
        [Description("保存")]
        Saved = 0,
        /// <summary>
        /// 審核
        /// </summary>
        [Description("審核")]
        Approved = 1,
        /// <summary>
        /// 結案
        /// </summary>
        [Description("結案")]
        EndCase = 2,
        /// <summary>
        /// 作廢
        /// </summary>
        [Description("作廢")]
        Obsoleted = 3,
    }
    /// <summary>
    /// 資料狀態
    /// </summary>
    [Description("資料狀態")]
    public enum DataStatus : byte
    {
        /// <summary>
        /// 未生效
        /// </summary>
        [Description("未生效")]
        NotEffective = 0,
        /// <summary>
        /// 生效
        /// </summary>
        [Description("生效")]
        Effective = 1
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
        [Description("銀行通路")]
        Bank = 0,
        /// <summary>
        /// 超商通路
        /// </summary>
        [Description("超商通路")]
        Market = 1,
        /// <summary>
        /// 郵局通路
        /// </summary>
        [Description("郵局通路")]
        Post = 2,
        /// <summary>
        /// 信用卡通路
        /// </summary>
        [Description("信用卡通路")]
        Credit = 3,
        /// <summary>
        /// 自收款
        /// </summary>
        [Description("自收款")]
        Self = 255,
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
        [Description("商戶")]
        Cust = 0,
        /// <summary>
        /// 介紹商
        /// </summary>
        [Description("介紹商")]
        Introducer = 1,

    }
    /// <summary>
    /// 單據動作
    /// </summary>
    [Description("單據動作")]
    public enum SetOpportunity : int
    {
        [Description("新增")]
        Create = 1,
        [Description("修改")]
        Edit = 2,
        [Description("審核")]
        Approve = 4,
        [Description("結案")]
        EndCase = 8
    }
    /// <summary>
    /// 手續費類型
    /// </summary>
    [Description("手續費類型")]
    public enum FeeType : byte
    {
        /// <summary>
        /// 清算手續費
        /// </summary>
        [Description("清算手續費")]
        ClearFee = 1,
        /// <summary>
        /// 通路手續費
        /// </summary>
        [Description("通路手續費")]
        ChannelFee = 2,
        /// <summary>
        /// 介紹商手續費
        /// </summary>
        [Description("介紹商手續費")]
        IntroducerFee = 3,
        /// <summary>
        /// 每筆總手續費
        /// </summary>
        [Description("每筆總手續費")]
        TotalFee = 4,
    }
    /// <summary>
    /// 資訊流資料異常狀態
    /// </summary>
    [Description("資訊流資料異常狀態")]
    public enum TransDataUnusual : int
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 0,
        /// <summary>
        /// 無法解出企業代號
        /// </summary>
        [Description("無法解出企業代號")]
        NoCustomerCode = 1,
        /// <summary>
        /// 無法解出代收類別代號
        /// </summary>
        [Description("無法解出代收類別代號")]
        NoCollectionTypeId = 2,
        /// <summary>
        /// 代收類別未開此通路
        /// </summary>
        [Description("代收類別未開此通路")]
        NoChannel = 4,
        /// <summary>
        /// 未開通該代收類別
        /// </summary>
        [Description("未開通該代收類別")]
        NoSetCollectionTyp = 8,
        /// <summary>
        /// 未開通該通路
        /// </summary>
        [Description("未開通該通路")]
        NoSetChannel = 16,
        /// <summary>
        /// 帳單金額與通路手續費設定級距不符
        /// </summary>
        [Description("帳單金額與通路手續費設定級距不符")]
        NotInRange = 32,
        /// <summary>
        /// 無手續費設定
        /// </summary>
        [Description("無手續費設定")]
        NoChargeFeeSet = 64,
        /// <summary>
        /// 服務申請書尚未設定完畢
        /// </summary>
        [Description("服務申請書尚未設定完畢")]
        ApplyFormSetError = 128,
    }
    /// <summary>
    /// 通路帳務核銷週期
    /// </summary>
    [Description("通路帳務核銷週期")]
    public enum PayPeriodType : byte
    {
        /// <summary>
        /// 日結
        /// </summary>
        [Description("日結")]
        NDay = 1,
        /// <summary>
        /// 週結
        /// </summary>
        [Description("週結")]
        Week = 2,
        /// <summary>
        /// 旬結
        /// </summary>
        [Description("旬結")]
        TenDay = 3,
        /// <summary>
        /// 月結
        /// </summary>
        [Description("月結")]
        Month = 4,
    }
    /// <summary>
    /// 網路平台申請
    /// </summary>
    [Description("網路平台申請")]
    public enum HiTrustFlag : byte
    {
        /// <summary>
        /// 未申請
        /// </summary>
        [Description("未申請")]
        NoApplication = 0,
        /// <summary>
        /// 單機版
        /// </summary>
        [Description("單機版")]
        Single = 1,
        /// <summary>
        /// 網路版-分潤
        /// </summary>
        [Description("網路版-分潤")]
        Net = 2,
        /// <summary>
        /// 網路版-長期
        /// </summary>
        [Description("網路版-長期")]
        NetLong = 3,
    }
    /// <summary>
    /// 行狀態
    /// </summary>
    [Description("行狀態")]
    public enum RowState : byte
    {
        /// <summary>
        /// 無異動
        /// </summary>
        [Description("無異動")]
        None = 0,
        /// <summary>
        /// 新增
        /// </summary>
        [Description("新增")]
        Insert = 1,
        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        Update = 2,
        /// <summary>
        /// 刪除
        /// </summary>
        [Description("刪除")]
        Delete = 3,
    }
}
