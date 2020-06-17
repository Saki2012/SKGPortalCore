using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SKGPortalCore.Core
{
    /// <summary>
    /// 固定參數
    /// </summary>
    public static class SystemCP
    {
        #region System
        /// <summary>
        /// 組態路徑
        /// </summary>
        public const string AppSettingsJsonPath = @"D:\Proj\SKGPortalCore\SKGPortalCore";
        /// <summary>
        /// 組態檔名
        /// </summary>
        public const string AppSettingsJson = "appsettings.json";
        /// <summary>
        /// SQL連線組態名
        /// </summary>
        public const string SqlConnection = "SqlConnection";
        /// <summary>
        /// Redis連線組態名
        /// </summary>
        public const string RedisConnection = "RedisConnection";
        /// <summary>
        /// 
        /// </summary>
        public const string CookieName = "YouKnowDaWaeOfDevil";
        /// <summary>
        /// 系統操作
        /// </summary>
        public const string SysOperator = "SysOperator";
        /// <summary>
        /// 系統操作
        /// </summary>
        public const string SysOperatorName = "系統操作";
        /// <summary>
        /// 銀行-代收類別代號
        /// </summary>
        public const string BankCollectionTypeId = "Bank999";
        /// <summary>
        /// 郵局-代收類別代號
        /// </summary>
        public const string PostCollectionTypeId = "50084884";
        /// <summary>
        /// 
        /// </summary>
        public const string UserKey = "session.user";
        /// <summary>
        /// Log默認路徑
        /// </summary>
        public const string LogDefaultPath = @"./Log/";
        /// <summary>
        /// 文件路徑
        /// </summary>
        public const string DocPath = @"D:\ibankRoot\Ftp_SKGPortalCore\Doc\";
        /// <summary>
        /// Log默認檔名
        /// </summary>
        public const string LogDefaultFileName = "SKGPortalCoreLog";
        /// <summary>
        /// 自定義訊息辨別碼
        /// </summary>
        public const string CustomerMessageCode = "CustomerMessageCode";
        /// <summary>
        /// 異常發生訊息
        /// </summary>
        public const string ExpectionMessage = "異常發生，請洽客服人員。";
        /// <summary>
        /// 
        /// </summary>
        public const string DbSet = "Set";
        /// <summary>
        /// 
        /// </summary>
        public const string RowId = "RowId";
        /// <summary>
        /// 主鍵
        /// </summary>
        public const string KeyVal = "keyVal";
        /// <summary>
        /// Json Web Token
        /// </summary>
        public const string JWT = "jwt";
        /// <summary>
        /// 過濾條件
        /// </summary>
        public const string Condition = "condition";
        /// <summary>
        /// 表單資料
        /// </summary>
        public const string Set = "set";
        /// <summary>
        /// 狀態
        /// </summary>
        public const string Status = "status";
        /// <summary>
        /// 
        /// </summary>
        public const string RepositoryDLL = "RepositoryDLL";
        /// <summary>
        /// 
        /// </summary>
        public const string GraphDLL = "GraphDLL";

        #endregion

        #region Model
        /// <summary>
        /// 
        /// </summary>
        public const string Model = "Model";
        /// <summary>
        /// 
        /// </summary>
        public const string Schema = "Schema";
        /// <summary>
        /// 
        /// </summary>
        public const string Type = "Type";
        /// <summary>
        /// 
        /// </summary>
        public const string InputType = "InputType";
        /// <summary>
        /// 單據編號長度
        /// </summary>
        public const int BillNoLen = 30;
        /// <summary>
        /// 資料主鍵長度
        /// </summary>
        public const int DataIdLen = 15;
        /// <summary>
        /// 一般資料長度
        /// </summary>
        public const int NormalLen = 20;
        /// <summary>
        /// 大筆資料長度
        /// </summary>
        public const int LongLen = 100;
        #endregion

        #region ProgId
        /// <summary>
        /// 約定扣款單 ProgId
        /// </summary>
        public const string ProgId_AutoDebitBill = "AutoDebitBill";
        /// <summary>
        /// 帳單 ProgId
        /// </summary>
        public const string ProgId_Bill = "Bill";
        /// <summary>
        /// 金流帳簿 ProgId
        /// </summary>
        public const string ProgId_CashFlowBill = "CashFlowBill";
        /// <summary>
        /// 通路帳簿庫 ProgId
        /// </summary>
        public const string ProgId_ChannelEAccountBill = "ChannelEAccountBill";
        /// <summary>
        /// 通路帳款核銷單 ProgId
        /// </summary>
        public const string ProgId_ChannelWriteOfBill = "ChannelWriteOfBill";
        /// <summary>
        /// 入金機 ProgId
        /// </summary>
        public const string ProgId_DepositBill = "DepositBill";
        /// <summary>
        /// 撥款單 ProgId
        /// </summary>
        public const string ProgId_DisbursementBill = "DisbursementBill";
        /// <summary>
        /// 收款單 ProgId
        /// </summary>
        public const string ProgId_ReceiptBill = "ReceiptBill";
        /// <summary>
        /// 後臺用戶 ProgId
        /// </summary>
        public const string ProgId_BackendUser = "BackendUser";
        /// <summary>
        /// 前臺用戶 ProgId
        /// </summary>
        public const string ProgId_CustUser = "CustUser";
        /// <summary>
        /// 角色權限 ProgId
        /// </summary>
        public const string ProgId_Role = "Role";
        /// <summary>
        /// 期別 ProgId
        /// </summary>
        public const string ProgId_BillTerm = "BillTerm";
        /// <summary>
        /// 商戶資料 ProgId
        /// </summary>
        public const string ProgId_BizCustomer = "BizCustomer";
        /// <summary>
        /// 代收通路 ProgId
        /// </summary>
        public const string ProgId_Channel = "Channel";
        /// <summary>
        /// 代收類別 ProgId
        /// </summary>
        public const string ProgId_CollectionType = "CollectionType";
        /// <summary>
        /// 客戶 ProgId
        /// </summary>
        public const string ProgId_Customer = "Customer";
        /// <summary>
        /// 部門 ProgId
        /// </summary>
        public const string ProgId_Dept = "Dept";
        /// <summary>
        /// 繳款人 ProgId
        /// </summary>
        public const string ProgId_Payer = "Payer";
        /// <summary>
        /// 帳單相關報表 ProgId
        /// </summary>
        public const string ProgId_BillRpt = "BillRpt";
        /// <summary>
        /// 收款單相關報表 ProgId
        /// </summary>
        public const string ProgId_ReceiptBillRpt = "ReceiptBillRpt";
        /// <summary>
        /// 系統使用相關報表
        /// </summary>
        public const string ProgId_SystemRpt = "SystemRpt";
        #endregion

        #region Description
        /// <summary>
        /// 約定扣款單
        /// </summary>
        public const string DESC_AutoDebitBill = "約定扣款單";
        /// <summary>
        /// 約定扣款單收款明細
        /// </summary>
        public const string DESC_AutoDebitDt = "約定扣款單收款明細";
        /// <summary>
        /// 單據編號
        /// </summary>
        public const string DESC_BillNo = "單據編號";
        /// <summary>
        /// 企業編號
        /// </summary>
        public const string DESC_CustomerCode = "企業編號";
        /// <summary>
        /// 繳款人代號
        /// </summary>
        public const string DESC_PayerId = "繳款人代號";
        /// <summary>
        /// 繳款人名稱
        /// </summary>
        public const string DESC_PayerName = "繳款人名稱";
        /// <summary>
        /// 繳款人編碼
        /// </summary>
        public const string DESC_PayerNo = "繳款人編碼";
        /// <summary>
        /// 繳款人類別
        /// </summary>
        public const string DESC_PayerType = "繳款人類別";
        /// <summary>
        /// 應繳金額
        /// </summary>
        public const string DESC_ShouldPayAmount = "應繳金額";
        /// <summary>
        /// 虛擬帳號
        /// </summary>
        public const string DESC_VirtualAccountCode = "虛擬帳號";
        /// <summary>
        /// 匯入批號
        /// </summary>
        public const string DESC_ImportBatchNo = "匯入批號";
        /// <summary>
        /// 收款單號
        /// </summary>
        public const string DESC_ReceiptBillNo = "收款單號";
        /// <summary>
        /// 帳單
        /// </summary>
        public const string DESC_Bill = "帳單";
        /// <summary>
        /// 帳單明細
        /// </summary>
        public const string DESC_BillDt = "帳單明細";
        /// <summary>
        /// 帳單收款明細
        /// </summary>
        public const string DESC_BillReceiptDt = "帳單收款明細";
        /// <summary>
        /// 期別代號
        /// </summary>
        public const string DESC_BillTermId = "期別代號";
        /// <summary>
        /// 繳款截止日
        /// </summary>
        public const string DESC_PayEndDate = "繳款截止日";
        /// <summary>
        /// 代收項目
        /// </summary>
        public const string DESC_CollectionType = "代收項目";
        /// <summary>
        /// 代收項目費率明細
        /// </summary>
        public const string DESC_CollectionTypeDt = "代收項目費率明細";
        /// <summary>
        /// 代收項目代號
        /// </summary>
        public const string DESC_CollectionTypeId = "代收項目代號";
        /// <summary>
        /// 代收項目名稱
        /// </summary>
        public const string DESC_CollectionTypeName = "代收項目名稱";
        /// <summary>
        /// 已繳金額
        /// </summary>
        public const string DESC_HadPayAmount = "已繳金額";
        /// <summary>
        /// 繳款狀態
        /// </summary>
        public const string DESC_PayStatus = "繳款狀態";
        /// <summary>
        /// 序號
        /// </summary>
        public const string DESC_Id = "序號";
        /// <summary>
        /// 行序號
        /// </summary>
        public const string DESC_RowId = "行序號";
        /// <summary>
        /// 費用
        /// </summary>
        public const string DESC_FeeName = "費用";
        /// <summary>
        /// 匯款日期
        /// </summary>
        public const string DESC_RemitTime = "匯款日期";
        /// <summary>
        /// 代收通路代號
        /// </summary>
        public const string DESC_ChannelId = "代收通路代號";
        /// <summary>
        /// 代收通路名稱
        /// </summary>
        public const string DESC_ChannelName = "代收通路名稱";
        /// <summary>
        /// 交易金額
        /// </summary>
        public const string DESC_Amount = "交易金額";
        /// <summary>
        /// 來源
        /// </summary>
        public const string DESC_Source = "來源";
        /// <summary>
        /// 通路帳簿
        /// </summary>
        public const string DESC_ChannelEAccountBill = "通路帳簿";
        /// <summary>
        /// 通路收款明細帳簿
        /// </summary>
        public const string DESC_ChannelEAccountBillDt = "通路收款明細帳簿";
        /// <summary>
        /// 預計匯款日期
        /// </summary>
        public const string DESC_ExpectRemitDate = "預計匯款日期";
        /// <summary>
        /// 遞延天數
        /// </summary>
        public const string DESC_PostponeDays = "遞延天數";
        /// <summary>
        /// 通路手續費
        /// </summary>
        public const string DESC_ChannelFee = "通路手續費";
        /// <summary>
        /// 預計匯款金額
        /// </summary>
        public const string DESC_ExpectRemitAmount = "預計匯款金額";
        /// <summary>
        /// 筆數
        /// </summary>
        public const string DESC_TotalCount = "筆數";
        /// <summary>
        /// 金流帳簿
        /// </summary>
        public const string DESC_CashFlowBill = "金流帳簿";
        /// <summary>
        /// 通路帳款核銷單
        /// </summary>
        public const string DESC_ChannelWriteOfBill = "通路帳款核銷單";
        /// <summary>
        /// 核銷狀態
        /// </summary>
        public const string DESC_WriteOfStatus = "核銷狀態";
        /// <summary>
        /// 預計撥款金額
        /// </summary>
        public const string DESC_PrePayAmount = "預計撥款金額";
        /// <summary>
        /// 撥款單號
        /// </summary>
        public const string DESC_DisBillNo = "撥款單號";
        /// <summary>
        /// 通路帳目明細
        /// </summary>
        public const string DESC_ChannelWriteOfDt = "通路帳目明細";
        /// <summary>
        /// 通路帳簿單號
        /// </summary>
        public const string DESC_ChannelEAccountBillNo = "通路帳簿單號";
        /// <summary>
        /// 金流帳目明細
        /// </summary>
        public const string DESC_CashFlowWriteOfDt = "金流帳目明細";
        /// <summary>
        /// 金流帳簿單號
        /// </summary>
        public const string DESC_CashFlowBillNo = "金流帳簿單號";
        /// <summary>
        /// 入金機
        /// </summary>
        public const string DESC_DepositBill = "入金機";
        /// <summary>
        /// 入金機收款明細
        /// </summary>
        public const string DESC_DepositBillReceiptDt = "入金機收款明細";
        /// <summary>
        /// 櫃位名稱
        /// </summary>
        public const string DESC_StoreName = "櫃位名稱";
        /// <summary>
        /// 撥款單
        /// </summary>
        public const string DESC_DisbursementBill = "撥款單";
        /// <summary>
        /// 通路帳款核銷單號
        /// </summary>
        public const string DESC_ChannelWriteOfBillNo = "通路帳款核銷單號";
        /// <summary>
        /// 收款單
        /// </summary>
        public const string DESC_ReceiptBill = "收款單";
        /// <summary>
        /// 收款單異動說明表
        /// </summary>
        public const string DESC_ReceiptBillChange = "收款單異動說明表";
        /// <summary>
        /// 交易日期
        /// </summary>
        public const string DESC_TradeDate = "交易日期";
        /// <summary>
        /// 傳輸日期
        /// </summary>
        public const string DESC_TransDate = "傳輸日期";
        /// <summary>
        /// 實繳金額
        /// </summary>
        public const string DESC_PaidAmount = "實繳金額";
        /// <summary>
        /// 通路手續費清算方式
        /// </summary>
        public const string DESC_ChargePayType = "通路手續費清算方式";
        /// <summary>
        /// 銀行手續費類型
        /// </summary>
        public const string DESC_BankFeeType = "銀行手續費類型";
        /// <summary>
        /// 銀行手續費
        /// </summary>
        public const string DESC_BankFee = "銀行手續費";
        /// <summary>
        /// 介紹商手續費
        /// </summary>
        public const string DESC_IntroFee = "介紹商手續費";
        /// <summary>
        /// 通路回饋手續費
        /// </summary>
        public const string DESC_ChannelFeedBackFee = "通路回饋手續費";
        /// <summary>
        /// 通路回扣手續費
        /// </summary>
        public const string DESC_ChannelRebateFee = "通路回扣手續費";
        /// <summary>
        /// 通路總手續費
        /// </summary>
        public const string DESC_ChannelTotalFee = "通路總手續費";
        /// <summary>
        /// 總手續費
        /// </summary>
        public const string DESC_TotalFee = "總手續費";
        /// <summary>
        /// ProgId
        /// </summary>
        public const string DESC_ProgId = "ProgId";
        /// <summary>
        /// 來源單據編號
        /// </summary>
        public const string DESC_SrcBillNo = "來源單據編號";
        /// <summary>
        /// 異常資料
        /// </summary>
        public const string DESC_ErrData = "異常資料";
        /// <summary>
        /// 異常訊息
        /// </summary>
        public const string DESC_ErrMessage = "異常訊息";
        /// <summary>
        /// 異動時間
        /// </summary>
        public const string DESC_ChangeTime = "異動時間";
        /// <summary>
        /// 單據狀態
        /// </summary>
        public const string DESC_FormStatus = "單據狀態";
        /// <summary>
        /// 異動原因
        /// </summary>
        public const string DESC_Reason = "異動原因";
        /// <summary>
        /// 期別
        /// </summary>
        public const string DESC_BillTerm = "期別";
        /// <summary>
        /// 期別費用明細
        /// </summary>
        public const string DESC_BillTermDt = "期別費用明細";
        /// <summary>
        /// 期別名稱
        /// </summary>
        public const string DESC_BillTermName = "期別名稱";
        /// <summary>
        /// 期別編號
        /// </summary>
        public const string DESC_BillTermNo = "期別編號";
        /// <summary>
        /// 商戶資料
        /// </summary>
        public const string DESC_BizCustomer = "商戶資料";
        /// <summary>
        /// 商戶手續費管理明細
        /// </summary>
        public const string DESC_BizCustomerFeeDt = "商戶手續費管理明細";
        /// <summary>
        /// 客戶統編
        /// </summary>
        public const string DESC_CustomerId = "客戶統編";
        /// <summary>
        /// 帳務分行
        /// </summary>
        public const string DESC_AccountDeptId = "帳務分行";
        /// <summary>
        /// 實體帳號
        /// </summary>
        public const string DESC_RealAccount = "實體帳號";
        /// <summary>
        /// 虛擬帳號長度
        /// </summary>
        public const string DESC_VirtualAccountLen = "虛擬帳號長度";
        /// <summary>
        /// 期別編號長度
        /// </summary>
        public const string DESC_BillTermLen = "期別編號長度";
        /// <summary>
        /// 繳款人編號長度
        /// </summary>
        public const string DESC_PayerNoLen = "繳款人編號長度";
        /// <summary>
        /// 自組銷帳編號
        /// </summary>
        public const string DESC_VirtualAccount = "自組銷帳編號";
        /// <summary>
        /// 啟用通路
        /// </summary>
        public const string DESC_ChannelIds = "啟用通路";
        /// <summary>
        /// 啟用代收項目
        /// </summary>
        public const string DESC_CollectionTypeIds = "啟用代收項目";
        /// <summary>
        /// 啟用超商通路
        /// </summary>
        public const string DESC_MarketEnable = "啟用超商通路";
        /// <summary>
        /// 啟用郵局通路
        /// </summary>
        public const string DESC_PostEnable = "啟用郵局通路";
        /// <summary>
        /// 商戶類型
        /// </summary>
        public const string DESC_BizCustType = "商戶類型";
        /// <summary>
        /// 介紹商企業代號
        /// </summary>
        public const string DESC_IntroCustomerCode = "介紹商企業代號";
        /// <summary>
        /// 帳戶狀態
        /// </summary>
        public const string DESC_AccountStatus = "帳戶狀態";
        /// <summary>
        /// 導入時間
        /// </summary>
        public const string DESC_SyncDateTime = "導入時間";
        /// <summary>
        /// 通路類別
        /// </summary>
        public const string DESC_ChannelGroupType = "通路類別";
        /// <summary>
        /// 手續費
        /// </summary>
        public const string DESC_Fee = "手續費";
        /// <summary>
        /// 介紹商手續費/分潤%
        /// </summary>
        public const string DESC_IntroPercent = "介紹商手續費/分潤%";
        /// <summary>
        /// 代收通路
        /// </summary>
        public const string DESC_Channel = "代收通路";
        /// <summary>
        /// 交易代號與平台通路代號關聯表
        /// </summary>
        public const string DESC_ChannelMap = "交易代號與平台通路代號關聯表";
        /// <summary>
        /// 交易代號
        /// </summary>
        public const string DESC_TransCode = "交易代號";
        /// <summary>
        /// 收款區間(起)
        /// </summary>
        public const string DESC_SRange = "收款區間(起)";
        /// <summary>
        /// 收款區間(迄)
        /// </summary>
        public const string DESC_ERange = "收款區間(迄)";
        /// <summary>
        /// 通路核銷週期明細
        /// </summary>
        public const string DESC_CollectionTypeVerifyPeriod = "通路核銷週期明細";
        /// <summary>
        /// 通路帳務核銷週期
        /// </summary>
        public const string DESC_PayPeriodType = "通路帳務核銷週期";
        /// <summary>
        /// 客戶資料
        /// </summary>
        public const string DESC_Customer = "客戶資料";
        /// <summary>
        /// 客戶名稱
        /// </summary>
        public const string DESC_CustomerName = "客戶名稱";
        /// <summary>
        /// 地址
        /// </summary>
        public const string DESC_Address = "地址";
        /// <summary>
        /// 電話
        /// </summary>
        public const string DESC_Tel = "電話";
        /// <summary>
        /// 傳真
        /// </summary>
        public const string DESC_Fax = "傳真";
        /// <summary>
        /// 郵遞區號
        /// </summary>
        public const string DESC_ZipCode = "郵遞區號";
        /// <summary>
        /// 郵簡許可單位
        /// </summary>
        public const string DESC_ZipUnit = "郵簡許可單位";
        /// <summary>
        /// 郵簡字號
        /// </summary>
        public const string DESC_ZipNum = "郵簡字號";
        /// <summary>
        /// 部門
        /// </summary>
        public const string DESC_Dept = "部門";
        /// <summary>
        /// 部門代號
        /// </summary>
        public const string DESC_DeptId = "部門代號";
        /// <summary>
        /// 部門名稱
        /// </summary>
        public const string DESC_DeptName = "部門名稱";
        /// <summary>
        /// 是否分行
        /// </summary>
        public const string DESC_IsBranch = "是否分行";
        /// <summary>
        /// 繳款人
        /// </summary>
        public const string DESC_Payer = "繳款人";
        /// <summary>
        /// 營業日
        /// </summary>
        public const string DESC_WorkDate = "營業日";
        /// <summary>
        /// 日期
        /// </summary>
        public const string DESC_Date = "日期";
        /// <summary>
        /// 是否營業日
        /// </summary>
        public const string DESC_IsWorkDate = "是否營業日";
        /// <summary>
        /// 假別名
        /// </summary>
        public const string DESC_HolidayName = "假別名";
        /// <summary>
        /// 假類別
        /// </summary>
        public const string DESC_HolidayCategory = "假類別";
        /// <summary>
        /// 說明
        /// </summary>
        public const string DESC_Description = "說明";
        /// <summary>
        /// 創建人員
        /// </summary>
        public const string DESC_CreateStaff = "創建人員";
        /// <summary>
        /// 創建時間
        /// </summary>
        public const string DESC_CreateTime = "創建時間";
        /// <summary>
        /// 修改人員
        /// </summary>
        public const string DESC_ModifyStaff = "修改人員";
        /// <summary>
        /// 修改時間
        /// </summary>
        public const string DESC_ModifyTime = "修改時間";
        /// <summary>
        /// 審核人員
        /// </summary>
        public const string DESC_ApproveStaff = "審核人員";
        /// <summary>
        /// 審核時間
        /// </summary>
        public const string DESC_ApproveTime = "審核時間";
        /// <summary>
        /// 結案人員
        /// </summary>
        public const string DESC_EndCaseStaff = "結案人員";
        /// <summary>
        /// 結案時間
        /// </summary>
        public const string DESC_EndCaseTime = "結案時間";
        /// <summary>
        /// 作廢人員
        /// </summary>
        public const string DESC_InvalidStaff = "作廢人員";
        /// <summary>
        /// 作廢時間
        /// </summary>
        public const string DESC_InvalidTime = "作廢時間";
        /// <summary>
        /// 內部唯一標識號
        /// </summary>
        public const string DESC_InternalId = "內部唯一標識號";
        /// <summary>
        /// 行狀態
        /// </summary>
        public const string DESC_RowState = "行狀態";
        /// <summary>
        /// 變更日誌
        /// </summary>
        public const string DESC_DataChangeLog = "變更日誌";
        /// <summary>
        /// 變更日誌明細
        /// </summary>
        public const string DESC_DataChangeLogDt = "變更日誌明細";
        /// <summary>
        /// 使用者ID
        /// </summary>
        public const string DESC_UserId = "使用者ID";
        /// <summary>
        /// 變更時間
        /// </summary>
        public const string DESC_DataChangeTime = "變更時間";
        /// <summary>
        /// 表單索引
        /// </summary>
        public const string DESC_TableIndex = "表單索引";
        /// <summary>
        /// 變更前後資料
        /// </summary>
        public const string DESC_ChangeData = "變更前後資料";
        /// <summary>
        /// 編碼規則表
        /// </summary>
        public const string DESC_DataFlowNo = "編碼規則表";
        /// <summary>
        /// 流水號日期
        /// </summary>
        public const string DESC_FlowDate = "流水號日期";
        /// <summary>
        /// 流水號
        /// </summary>
        public const string DESC_FlowNo = "流水號";
        /// <summary>
        /// 有效虛擬帳號表
        /// </summary>
        public const string DESC_VirtualAccountCodeModel = "有效虛擬帳號表";
        /// <summary>
        /// 來源ProgId
        /// </summary>
        public const string DESC_SrcProgId = "來源ProgId";
        /// <summary>
        /// 操作日誌
        /// </summary>
        public const string DESC_OperateLog = "操作日誌";
        /// <summary>
        /// 登入IP位置
        /// </summary>
        public const string DESC_IP = "登入IP位置";
        /// <summary>
        /// 瀏覽器資訊
        /// </summary>
        public const string DESC_Browser = "瀏覽器資訊";
        /// <summary>
        /// 資料主鍵
        /// </summary>
        public const string DESC_PK = "資料主鍵";
        /// <summary>
        /// 操作時間
        /// </summary>
        public const string DESC_OperateTime = "操作時間";
        /// <summary>
        /// 動作
        /// </summary>
        public const string DESC_Action = "動作";
        /// <summary>
        /// 備註
        /// </summary>
        public const string DESC_Memo = "備註";
        /// <summary>
        /// 後臺使用者
        /// </summary>
        public const string DESC_BackendUser = "後臺使用者";
        /// <summary>
        /// 後臺使用者角色權限清單
        /// </summary>
        public const string DESC_BackendUserRoleList = "後臺使用者角色權限清單";
        /// <summary>
        /// 員工編號
        /// </summary>
        public const string DESC_MemberId = "員工編號";
        /// <summary>
        /// 使用者名稱
        /// </summary>
        public const string DESC_UserName = "使用者名稱";
        /// <summary>
        /// Email
        /// </summary>
        public const string DESC_Email = "Email";
        /// <summary>
        /// 角色權限代號
        /// </summary>
        public const string DESC_RoleId = "角色權限代號";
        /// <summary>
        /// 角色權限名稱
        /// </summary>
        public const string DESC_RoleName = "角色權限名稱";
        /// <summary>
        /// 前/後台
        /// </summary>
        public const string DESC_EndType = "前/後台";
        /// <summary>
        /// 前臺使用者
        /// </summary>
        public const string DESC_CustUser = "前臺使用者";
        /// <summary>
        /// 前臺使用者角色權限清單
        /// </summary>
        public const string DESC_CustUserRoleList = "前臺使用者角色權限清單";
        /// <summary>
        /// 登入失敗次數
        /// </summary>
        public const string DESC_LoginErrorCount = "登入失敗次數";
        /// <summary>
        /// 密碼過期時間
        /// </summary>
        public const string DESC_PasuwadoExpiredDate = "密碼過期時間";
        /// <summary>
        /// 角色權限
        /// </summary>
        public const string DESC_Role = "角色權限";
        /// <summary>
        /// 角色權限設置
        /// </summary>
        public const string DESC_RolePermission = "角色權限設置";
        /// <summary>
        /// 是否為管理者
        /// </summary>
        public const string DESC_IsAdmin = "是否為管理者";
        /// <summary>
        /// 權限列表
        /// </summary>
        public const string DESC_Permissions = "權限列表";
        /// <summary>
        /// 功能名稱
        /// </summary>
        public const string DESC_FuncName = "功能名稱";
        /// <summary>
        /// 權限
        /// </summary>
        public const string DESC_FuncAction = "權限";
        /// <summary>
        /// 帳單繳費進度報表
        /// </summary>
        public const string DESC_BillPayProgressRpt = "帳單繳費進度報表";
        /// <summary>
        /// 客戶手續費報表
        /// </summary>
        public const string DESC_ChannelTotalFeeRpt = "客戶手續費報表";
        /// <summary>
        /// 收款明細報表
        /// </summary>
        public const string DESC_ReceiptRpt = "收款明細報表";
        /// <summary>
        /// 入帳金額
        /// </summary>
        public const string DESC_IncomeAmount = "入帳金額";
        /// <summary>
        /// 總收款報表
        /// </summary>
        public const string DESC_TotalReceiptRpt = "總收款報表";
        /// <summary>
        /// 查看表單
        /// </summary>
        public const string DESC_QueryData = "查看表單";
        /// <summary>
        /// 清單列表查詢
        /// </summary>
        public const string DESC_QueryList = "清單列表查詢";
        /// <summary>
        /// 新增表單
        /// </summary>
        public const string DESC_Create = "新增表單";
        /// <summary>
        /// 修改表單
        /// </summary>
        public const string DESC_Update = "修改表單";
        /// <summary>
        /// 刪除表單
        /// </summary>
        public const string DESC_Delete = "刪除表單";
        /// <summary>
        /// 審核表單
        /// </summary>
        public const string DESC_Approve = "審核表單";
        /// <summary>
        /// 作廢表單
        /// </summary>
        public const string DESC_Invalid = "作廢表單";
        /// <summary>
        /// 結案表單
        /// </summary>
        public const string DESC_EndCase = "結案表單";
        /// <summary>
        /// 表單資料
        /// </summary>
        public const string DESC_Set = "表單資料";
        /// <summary>
        /// 主鍵
        /// </summary>
        public const string DESC_KeyVal = "主鍵";
        /// <summary>
        /// Json Web Token
        /// </summary>
        public const string DESC_JWT = "Json Web Token";
        /// <summary>
        /// 過濾條件
        /// </summary>
        public const string DESC_Condition = "過濾條件";
        /// <summary>
        /// 狀態
        /// </summary>
        public const string DESC_Status = "狀態";
        /// <summary>
        /// 帳號
        /// </summary>
        public const string DESC_Account = "帳號";
        /// <summary>
        /// 密碼
        /// </summary>
        public const string DESC_Pasuwado = "密碼";
        /// <summary>
        /// 登入帳號
        /// </summary>
        public const string DESC_Login = "登入帳號";
        /// <summary>
        /// 登出帳號
        /// </summary>
        public const string DESC_Logout = "登出帳號";
        /// <summary>
        /// 上傳檔案
        /// </summary>
        public const string DESC_UploadFile = "上傳檔案";
        /// <summary>
        /// 檔案資訊
        /// </summary>
        public const string DESC_FileInfo = "檔案資訊";
        #endregion

        #region Index
        /// <summary>
        /// 索引-通路+代收項目+預計匯款日
        /// </summary>
        public const string IX_ChannelId_CollectionTypeId_ExpectRemitDate = "IX_ChannelId_CollectionTypeId_ExpectRemitDate";
        #endregion
    }
}
