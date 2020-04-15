IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [CashFlowBill] (
    [BillNo] nvarchar(30) NOT NULL,
    [CreateStaff] nvarchar(15) NOT NULL,
    [CreateTime] datetime2 NOT NULL,
    [ModifyStaff] nvarchar(15) NOT NULL,
    [ModifyTime] datetime2 NOT NULL,
    [ApproveStaff] nvarchar(15) NOT NULL,
    [ApproveTime] datetime2 NOT NULL,
    [EndCaseStaff] nvarchar(15) NOT NULL,
    [EndCaseTime] datetime2 NOT NULL,
    [InvalidStaff] nvarchar(15) NOT NULL,
    [InvalidTime] datetime2 NOT NULL,
    [FormStatus] tinyint NOT NULL,
    [InternalId] nvarchar(max) NULL,
    [RemitTime] datetime2 NOT NULL,
    [ChannelId] nvarchar(max) NULL,
    [CollectionTypeId] nvarchar(max) NULL,
    [Amount] decimal(18,2) NOT NULL,
    [ImportBatchNo] nvarchar(20) NOT NULL,
    [Source] nvarchar(200) NULL,
    CONSTRAINT [PK_CashFlowBill] PRIMARY KEY ([BillNo])
);

GO

CREATE TABLE [Channel] (
    [ChannelId] nvarchar(15) NOT NULL,
    [CreateStaff] nvarchar(15) NOT NULL,
    [CreateTime] datetime2 NOT NULL,
    [ModifyStaff] nvarchar(15) NOT NULL,
    [ModifyTime] datetime2 NOT NULL,
    [ApproveStaff] nvarchar(15) NOT NULL,
    [ApproveTime] datetime2 NOT NULL,
    [EndCaseStaff] nvarchar(15) NOT NULL,
    [EndCaseTime] datetime2 NOT NULL,
    [InvalidStaff] nvarchar(15) NOT NULL,
    [InvalidTime] datetime2 NOT NULL,
    [FormStatus] tinyint NOT NULL,
    [InternalId] nvarchar(max) NULL,
    [ChannelName] nvarchar(20) NULL,
    [ChannelGroupType] tinyint NOT NULL,
    CONSTRAINT [PK_Channel] PRIMARY KEY ([ChannelId])
);

GO

CREATE TABLE [CollectionType] (
    [CollectionTypeId] nvarchar(15) NOT NULL,
    [CreateStaff] nvarchar(15) NOT NULL,
    [CreateTime] datetime2 NOT NULL,
    [ModifyStaff] nvarchar(15) NOT NULL,
    [ModifyTime] datetime2 NOT NULL,
    [ApproveStaff] nvarchar(15) NOT NULL,
    [ApproveTime] datetime2 NOT NULL,
    [EndCaseStaff] nvarchar(15) NOT NULL,
    [EndCaseTime] datetime2 NOT NULL,
    [InvalidStaff] nvarchar(15) NOT NULL,
    [InvalidTime] datetime2 NOT NULL,
    [FormStatus] tinyint NOT NULL,
    [InternalId] nvarchar(max) NULL,
    [CollectionTypeName] nvarchar(20) NOT NULL,
    [ChargePayType] tinyint NOT NULL,
    CONSTRAINT [PK_CollectionType] PRIMARY KEY ([CollectionTypeId])
);

GO

CREATE TABLE [Customer] (
    [CustomerId] nvarchar(15) NOT NULL,
    [CreateStaff] nvarchar(15) NOT NULL,
    [CreateTime] datetime2 NOT NULL,
    [ModifyStaff] nvarchar(15) NOT NULL,
    [ModifyTime] datetime2 NOT NULL,
    [ApproveStaff] nvarchar(15) NOT NULL,
    [ApproveTime] datetime2 NOT NULL,
    [EndCaseStaff] nvarchar(15) NOT NULL,
    [EndCaseTime] datetime2 NOT NULL,
    [InvalidStaff] nvarchar(15) NOT NULL,
    [InvalidTime] datetime2 NOT NULL,
    [FormStatus] tinyint NOT NULL,
    [InternalId] nvarchar(max) NULL,
    [CustomerName] nvarchar(20) NOT NULL,
    [Address] nvarchar(100) NOT NULL,
    [Tel] nvarchar(20) NOT NULL,
    [Fax] nvarchar(20) NOT NULL,
    [ZipCode] nvarchar(20) NOT NULL,
    [ZipUnit] nvarchar(20) NOT NULL,
    [ZipNum] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY ([CustomerId])
);

GO

CREATE TABLE [DataChangeLog] (
    [DataChangeId] bigint NOT NULL IDENTITY,
    [UserId] nvarchar(max) NULL,
    [ProgId] nvarchar(max) NULL,
    [InternalId] nvarchar(max) NULL,
    [DataChangeTime] datetime2 NOT NULL,
    CONSTRAINT [PK_DataChangeLog] PRIMARY KEY ([DataChangeId])
);

GO

CREATE TABLE [DataFlowNo] (
    [ProgId] nvarchar(30) NOT NULL,
    [FlowDate] datetime2 NOT NULL,
    [FlowNo] int NOT NULL,
    CONSTRAINT [PK_DataFlowNo] PRIMARY KEY ([ProgId])
);

GO

CREATE TABLE [Dept] (
    [DeptId] nvarchar(15) NOT NULL,
    [CreateStaff] nvarchar(15) NOT NULL,
    [CreateTime] datetime2 NOT NULL,
    [ModifyStaff] nvarchar(15) NOT NULL,
    [ModifyTime] datetime2 NOT NULL,
    [ApproveStaff] nvarchar(15) NOT NULL,
    [ApproveTime] datetime2 NOT NULL,
    [EndCaseStaff] nvarchar(15) NOT NULL,
    [EndCaseTime] datetime2 NOT NULL,
    [InvalidStaff] nvarchar(15) NOT NULL,
    [InvalidTime] datetime2 NOT NULL,
    [FormStatus] tinyint NOT NULL,
    [InternalId] nvarchar(max) NULL,
    [DeptName] nvarchar(20) NOT NULL,
    [IsBranch] bit NOT NULL,
    CONSTRAINT [PK_Dept] PRIMARY KEY ([DeptId])
);

GO

CREATE TABLE [OperateLog] (
    [Id] bigint NOT NULL IDENTITY,
    [UserId] nvarchar(max) NOT NULL,
    [IP] nvarchar(max) NOT NULL,
    [Browser] nvarchar(max) NOT NULL,
    [ProgId] nvarchar(max) NOT NULL,
    [PK] nvarchar(max) NOT NULL,
    [OperateTime] datetime2 NOT NULL,
    [Action] nvarchar(max) NOT NULL,
    [Memo] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_OperateLog] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Role] (
    [RoleId] nvarchar(450) NOT NULL,
    [CreateStaff] nvarchar(15) NOT NULL,
    [CreateTime] datetime2 NOT NULL,
    [ModifyStaff] nvarchar(15) NOT NULL,
    [ModifyTime] datetime2 NOT NULL,
    [ApproveStaff] nvarchar(15) NOT NULL,
    [ApproveTime] datetime2 NOT NULL,
    [EndCaseStaff] nvarchar(15) NOT NULL,
    [EndCaseTime] datetime2 NOT NULL,
    [InvalidStaff] nvarchar(15) NOT NULL,
    [InvalidTime] datetime2 NOT NULL,
    [FormStatus] tinyint NOT NULL,
    [InternalId] nvarchar(max) NULL,
    [RoleName] nvarchar(max) NULL,
    [EndType] tinyint NOT NULL,
    [IsAdmin] bit NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([RoleId])
);

GO

CREATE TABLE [VirtualAccountCode] (
    [VirtualAccountCode] nvarchar(20) NOT NULL,
    [SrcProgId] nvarchar(20) NOT NULL,
    [SrcBillNo] nvarchar(30) NOT NULL,
    CONSTRAINT [PK_VirtualAccountCode] PRIMARY KEY ([VirtualAccountCode])
);

GO

CREATE TABLE [WorkDate] (
    [Date] datetime2 NOT NULL,
    [IsWorkDate] bit NOT NULL,
    [HolidayName] nvarchar(max) NULL,
    [HolidayCategory] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_WorkDate] PRIMARY KEY ([Date])
);

GO

CREATE TABLE [ChannelMap] (
    [ChannelId] nvarchar(15) NOT NULL,
    [TransCode] nvarchar(15) NOT NULL,
    CONSTRAINT [PK_ChannelMap] PRIMARY KEY ([ChannelId], [TransCode]),
    CONSTRAINT [FK_ChannelMap_Channel_ChannelId] FOREIGN KEY ([ChannelId]) REFERENCES [Channel] ([ChannelId]) ON DELETE CASCADE
);

GO

CREATE TABLE [ChannelEAccountBill] (
    [BillNo] nvarchar(30) NOT NULL,
    [CreateStaff] nvarchar(15) NOT NULL,
    [CreateTime] datetime2 NOT NULL,
    [ModifyStaff] nvarchar(15) NOT NULL,
    [ModifyTime] datetime2 NOT NULL,
    [ApproveStaff] nvarchar(15) NOT NULL,
    [ApproveTime] datetime2 NOT NULL,
    [EndCaseStaff] nvarchar(15) NOT NULL,
    [EndCaseTime] datetime2 NOT NULL,
    [InvalidStaff] nvarchar(15) NOT NULL,
    [InvalidTime] datetime2 NOT NULL,
    [FormStatus] tinyint NOT NULL,
    [InternalId] nvarchar(max) NULL,
    [ChannelId] nvarchar(15) NULL,
    [CollectionTypeId] nvarchar(15) NULL,
    [ExpectRemitDate] datetime2 NOT NULL,
    [PostponeDays] int NOT NULL,
    [Amount] decimal(18,2) NOT NULL,
    [ChannelFee] decimal(18,2) NOT NULL,
    [ExpectRemitAmount] decimal(18,2) NOT NULL,
    [TotalCount] int NOT NULL,
    CONSTRAINT [PK_ChannelEAccountBill] PRIMARY KEY ([BillNo]),
    CONSTRAINT [FK_ChannelEAccountBill_Channel_ChannelId] FOREIGN KEY ([ChannelId]) REFERENCES [Channel] ([ChannelId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ChannelEAccountBill_CollectionType_CollectionTypeId] FOREIGN KEY ([CollectionTypeId]) REFERENCES [CollectionType] ([CollectionTypeId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [CollectionTypeDetail] (
    [CollectionTypeId] nvarchar(15) NOT NULL,
    [RowId] int NOT NULL,
    [ChannelId] nvarchar(15) NOT NULL,
    [SRange] decimal(18,2) NOT NULL,
    [ERange] decimal(18,2) NOT NULL,
    [ChannelFee] decimal(18,2) NOT NULL,
    [ChannelFeedBackFee] decimal(18,2) NOT NULL,
    [ChannelRebateFee] decimal(18,2) NOT NULL,
    [ChannelTotalFee] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_CollectionTypeDetail] PRIMARY KEY ([CollectionTypeId], [RowId]),
    CONSTRAINT [FK_CollectionTypeDetail_Channel_ChannelId] FOREIGN KEY ([ChannelId]) REFERENCES [Channel] ([ChannelId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CollectionTypeDetail_CollectionType_CollectionTypeId] FOREIGN KEY ([CollectionTypeId]) REFERENCES [CollectionType] ([CollectionTypeId]) ON DELETE CASCADE
);

GO

CREATE TABLE [CollectionTypeVerifyPeriod] (
    [CollectionTypeId] nvarchar(15) NOT NULL,
    [ChannelId] nvarchar(15) NOT NULL,
    [PayPeriodType] tinyint NOT NULL,
    CONSTRAINT [PK_CollectionTypeVerifyPeriod] PRIMARY KEY ([CollectionTypeId], [ChannelId]),
    CONSTRAINT [FK_CollectionTypeVerifyPeriod_Channel_ChannelId] FOREIGN KEY ([ChannelId]) REFERENCES [Channel] ([ChannelId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CollectionTypeVerifyPeriod_CollectionType_CollectionTypeId] FOREIGN KEY ([CollectionTypeId]) REFERENCES [CollectionType] ([CollectionTypeId]) ON DELETE CASCADE
);

GO

CREATE TABLE [CustUser] (
    [KeyId] nvarchar(450) NOT NULL,
    [CreateStaff] nvarchar(15) NOT NULL,
    [CreateTime] datetime2 NOT NULL,
    [ModifyStaff] nvarchar(15) NOT NULL,
    [ModifyTime] datetime2 NOT NULL,
    [ApproveStaff] nvarchar(15) NOT NULL,
    [ApproveTime] datetime2 NOT NULL,
    [EndCaseStaff] nvarchar(15) NOT NULL,
    [EndCaseTime] datetime2 NOT NULL,
    [InvalidStaff] nvarchar(15) NOT NULL,
    [InvalidTime] datetime2 NOT NULL,
    [FormStatus] tinyint NOT NULL,
    [InternalId] nvarchar(max) NULL,
    [CustomerId] nvarchar(15) NULL,
    [UserId] nvarchar(max) NULL,
    [UserName] nvarchar(max) NULL,
    [Pasuwado] nvarchar(max) NULL,
    [AccountStatus] tinyint NOT NULL,
    [LoginErrorCount] tinyint NOT NULL,
    [PasuwadoExpiredDate] datetime2 NOT NULL,
    CONSTRAINT [PK_CustUser] PRIMARY KEY ([KeyId]),
    CONSTRAINT [FK_CustUser_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([CustomerId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [DataChangeLogDetail] (
    [DataChangeId] bigint NOT NULL,
    [RowId] bigint NOT NULL,
    [TableIndex] int NOT NULL,
    [ChangeData] varbinary(max) NULL,
    [RowState] tinyint NOT NULL,
    CONSTRAINT [PK_DataChangeLogDetail] PRIMARY KEY ([DataChangeId], [RowId]),
    CONSTRAINT [FK_DataChangeLogDetail_DataChangeLog_DataChangeId] FOREIGN KEY ([DataChangeId]) REFERENCES [DataChangeLog] ([DataChangeId]) ON DELETE CASCADE
);

GO

CREATE TABLE [BackendUser] (
    [KeyId] nvarchar(450) NOT NULL,
    [CreateStaff] nvarchar(15) NOT NULL,
    [CreateTime] datetime2 NOT NULL,
    [ModifyStaff] nvarchar(15) NOT NULL,
    [ModifyTime] datetime2 NOT NULL,
    [ApproveStaff] nvarchar(15) NOT NULL,
    [ApproveTime] datetime2 NOT NULL,
    [EndCaseStaff] nvarchar(15) NOT NULL,
    [EndCaseTime] datetime2 NOT NULL,
    [InvalidStaff] nvarchar(15) NOT NULL,
    [InvalidTime] datetime2 NOT NULL,
    [FormStatus] tinyint NOT NULL,
    [InternalId] nvarchar(max) NULL,
    [UserName] nvarchar(max) NULL,
    [DeptId] nvarchar(15) NULL,
    [Email] nvarchar(max) NULL,
    [AccountStatus] tinyint NOT NULL,
    CONSTRAINT [PK_BackendUser] PRIMARY KEY ([KeyId]),
    CONSTRAINT [FK_BackendUser_Dept_DeptId] FOREIGN KEY ([DeptId]) REFERENCES [Dept] ([DeptId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [BizCustomer] (
    [CustomerCode] nvarchar(15) NOT NULL,
    [CreateStaff] nvarchar(15) NOT NULL,
    [CreateTime] datetime2 NOT NULL,
    [ModifyStaff] nvarchar(15) NOT NULL,
    [ModifyTime] datetime2 NOT NULL,
    [ApproveStaff] nvarchar(15) NOT NULL,
    [ApproveTime] datetime2 NOT NULL,
    [EndCaseStaff] nvarchar(15) NOT NULL,
    [EndCaseTime] datetime2 NOT NULL,
    [InvalidStaff] nvarchar(15) NOT NULL,
    [InvalidTime] datetime2 NOT NULL,
    [FormStatus] tinyint NOT NULL,
    [InternalId] nvarchar(max) NULL,
    [CustomerId] nvarchar(15) NOT NULL,
    [AccountDeptId] nvarchar(15) NULL,
    [RealAccount] nvarchar(20) NOT NULL,
    [VirtualAccountLen] tinyint NOT NULL,
    [BillTermLen] tinyint NOT NULL,
    [PayerNoLen] tinyint NOT NULL,
    [VirtualAccount1] tinyint NOT NULL,
    [VirtualAccount2] tinyint NOT NULL,
    [VirtualAccount3] tinyint NOT NULL,
    [ChannelIds] nvarchar(100) NOT NULL,
    [CollectionTypeIds] nvarchar(100) NOT NULL,
    [MarketEnable] bit NOT NULL,
    [PostEnable] bit NOT NULL,
    [BizCustType] tinyint NOT NULL,
    [IntroCustomerCode] nvarchar(15) NULL,
    [AccountStatus] tinyint NOT NULL,
    [SyncDateTime] datetime2 NOT NULL,
    [ImportBatchNo] nvarchar(20) NOT NULL,
    [Source] nvarchar(200) NULL,
    CONSTRAINT [PK_BizCustomer] PRIMARY KEY ([CustomerCode]),
    CONSTRAINT [FK_BizCustomer_Dept_AccountDeptId] FOREIGN KEY ([AccountDeptId]) REFERENCES [Dept] ([DeptId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_BizCustomer_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([CustomerId]) ON DELETE CASCADE,
    CONSTRAINT [FK_BizCustomer_BizCustomer_IntroCustomerCode] FOREIGN KEY ([IntroCustomerCode]) REFERENCES [BizCustomer] ([CustomerCode]) ON DELETE NO ACTION
);

GO

CREATE TABLE [RolePermission] (
    [RoleId] nvarchar(450) NOT NULL,
    [RowId] int NOT NULL,
    [FuncName] nvarchar(max) NULL,
    [FuncAction] int NOT NULL,
    CONSTRAINT [PK_RolePermission] PRIMARY KEY ([RoleId], [RowId]),
    CONSTRAINT [FK_RolePermission_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Role] ([RoleId]) ON DELETE CASCADE
);

GO

CREATE TABLE [CustUserRole] (
    [KeyId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_CustUserRole] PRIMARY KEY ([KeyId], [RoleId]),
    CONSTRAINT [FK_CustUserRole_CustUser_KeyId] FOREIGN KEY ([KeyId]) REFERENCES [CustUser] ([KeyId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CustUserRole_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Role] ([RoleId]) ON DELETE CASCADE
);

GO

CREATE TABLE [BackendUserRole] (
    [KeyId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_BackendUserRole] PRIMARY KEY ([KeyId], [RoleId]),
    CONSTRAINT [FK_BackendUserRole_BackendUser_KeyId] FOREIGN KEY ([KeyId]) REFERENCES [BackendUser] ([KeyId]) ON DELETE CASCADE,
    CONSTRAINT [FK_BackendUserRole_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Role] ([RoleId]) ON DELETE CASCADE
);

GO

CREATE TABLE [BillTerm] (
    [CustomerCode] nvarchar(15) NOT NULL,
    [BillTermId] nvarchar(15) NOT NULL,
    [CreateStaff] nvarchar(15) NOT NULL,
    [CreateTime] datetime2 NOT NULL,
    [ModifyStaff] nvarchar(15) NOT NULL,
    [ModifyTime] datetime2 NOT NULL,
    [ApproveStaff] nvarchar(15) NOT NULL,
    [ApproveTime] datetime2 NOT NULL,
    [EndCaseStaff] nvarchar(15) NOT NULL,
    [EndCaseTime] datetime2 NOT NULL,
    [InvalidStaff] nvarchar(15) NOT NULL,
    [InvalidTime] datetime2 NOT NULL,
    [FormStatus] tinyint NOT NULL,
    [InternalId] nvarchar(max) NULL,
    [BillTermName] nvarchar(20) NOT NULL,
    [BillTermNo] nvarchar(20) NOT NULL,
    [PayEndDate] datetime2 NOT NULL,
    CONSTRAINT [PK_BillTerm] PRIMARY KEY ([CustomerCode], [BillTermId]),
    CONSTRAINT [FK_BillTerm_BizCustomer_CustomerCode] FOREIGN KEY ([CustomerCode]) REFERENCES [BizCustomer] ([CustomerCode]) ON DELETE CASCADE
);

GO

CREATE TABLE [BizCustomerFeeDetail] (
    [CustomerCode] nvarchar(15) NOT NULL,
    [ChannelGroupType] tinyint NOT NULL,
    [BankFeeType] tinyint NOT NULL,
    [Fee] decimal(18,2) NOT NULL,
    [IntroPercent] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_BizCustomerFeeDetail] PRIMARY KEY ([CustomerCode], [ChannelGroupType]),
    CONSTRAINT [FK_BizCustomerFeeDetail_BizCustomer_CustomerCode] FOREIGN KEY ([CustomerCode]) REFERENCES [BizCustomer] ([CustomerCode]) ON DELETE CASCADE
);

GO

CREATE TABLE [DepositBillModel] (
    [BillNo] nvarchar(30) NOT NULL,
    [CustomerCode] nvarchar(15) NOT NULL,
    [StoreName] nvarchar(20) NULL,
    [VirtualAccountCode] nvarchar(20) NOT NULL,
    [ImportBatchNo] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_DepositBillModel] PRIMARY KEY ([BillNo]),
    CONSTRAINT [FK_DepositBillModel_BizCustomer_CustomerCode] FOREIGN KEY ([CustomerCode]) REFERENCES [BizCustomer] ([CustomerCode]) ON DELETE CASCADE
);

GO

CREATE TABLE [Payer] (
    [CustomerCode] nvarchar(15) NOT NULL,
    [PayerId] nvarchar(15) NOT NULL,
    [CreateStaff] nvarchar(15) NOT NULL,
    [CreateTime] datetime2 NOT NULL,
    [ModifyStaff] nvarchar(15) NOT NULL,
    [ModifyTime] datetime2 NOT NULL,
    [ApproveStaff] nvarchar(15) NOT NULL,
    [ApproveTime] datetime2 NOT NULL,
    [EndCaseStaff] nvarchar(15) NOT NULL,
    [EndCaseTime] datetime2 NOT NULL,
    [InvalidStaff] nvarchar(15) NOT NULL,
    [InvalidTime] datetime2 NOT NULL,
    [FormStatus] tinyint NOT NULL,
    [InternalId] nvarchar(max) NULL,
    [PayerName] nvarchar(20) NOT NULL,
    [PayerNo] nvarchar(20) NOT NULL,
    [PayerType] tinyint NOT NULL,
    CONSTRAINT [PK_Payer] PRIMARY KEY ([CustomerCode], [PayerId]),
    CONSTRAINT [FK_Payer_BizCustomer_CustomerCode] FOREIGN KEY ([CustomerCode]) REFERENCES [BizCustomer] ([CustomerCode]) ON DELETE CASCADE
);

GO

CREATE TABLE [ReceiptBill] (
    [BillNo] nvarchar(30) NOT NULL,
    [CreateStaff] nvarchar(15) NOT NULL,
    [CreateTime] datetime2 NOT NULL,
    [ModifyStaff] nvarchar(15) NOT NULL,
    [ModifyTime] datetime2 NOT NULL,
    [ApproveStaff] nvarchar(15) NOT NULL,
    [ApproveTime] datetime2 NOT NULL,
    [EndCaseStaff] nvarchar(15) NOT NULL,
    [EndCaseTime] datetime2 NOT NULL,
    [InvalidStaff] nvarchar(15) NOT NULL,
    [InvalidTime] datetime2 NOT NULL,
    [FormStatus] tinyint NOT NULL,
    [InternalId] nvarchar(max) NULL,
    [CustomerCode] nvarchar(15) NULL,
    [CollectionTypeId] nvarchar(15) NULL,
    [ChannelId] nvarchar(15) NULL,
    [TradeDate] datetime2 NOT NULL,
    [TransDate] datetime2 NOT NULL,
    [ExpectRemitDate] datetime2 NOT NULL,
    [PayAmount] decimal(18,2) NOT NULL,
    [VirtualAccountCode] nvarchar(20) NULL,
    [ChargePayType] tinyint NOT NULL,
    [BankFeeType] tinyint NOT NULL,
    [BankFee] decimal(18,2) NOT NULL,
    [ThirdFee] decimal(18,2) NOT NULL,
    [ChannelFeedBackFee] decimal(18,2) NOT NULL,
    [ChannelRebateFee] decimal(18,2) NOT NULL,
    [ChannelFee] decimal(18,2) NOT NULL,
    [ChannelTotalFee] decimal(18,2) NOT NULL,
    [TotalFee] decimal(18,2) NOT NULL,
    [BillProgId] nvarchar(20) NOT NULL,
    [ToBillNo] nvarchar(30) NOT NULL,
    [ImportBatchNo] nvarchar(20) NOT NULL,
    [Source] nvarchar(200) NULL,
    [IsErrData] bit NOT NULL,
    [ErrMessage] nvarchar(max) NULL,
    CONSTRAINT [PK_ReceiptBill] PRIMARY KEY ([BillNo]),
    CONSTRAINT [FK_ReceiptBill_Channel_ChannelId] FOREIGN KEY ([ChannelId]) REFERENCES [Channel] ([ChannelId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ReceiptBill_CollectionType_CollectionTypeId] FOREIGN KEY ([CollectionTypeId]) REFERENCES [CollectionType] ([CollectionTypeId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ReceiptBill_BizCustomer_CustomerCode] FOREIGN KEY ([CustomerCode]) REFERENCES [BizCustomer] ([CustomerCode]) ON DELETE NO ACTION
);

GO

CREATE TABLE [BillTermDetail] (
    [CustomerCode] nvarchar(15) NOT NULL,
    [BillTermId] nvarchar(15) NOT NULL,
    [RowId] int NOT NULL,
    [FeeName] nvarchar(20) NOT NULL,
    [PayAmount] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_BillTermDetail] PRIMARY KEY ([CustomerCode], [BillTermId], [RowId]),
    CONSTRAINT [FK_BillTermDetail_BillTerm_CustomerCode_BillTermId] FOREIGN KEY ([CustomerCode], [BillTermId]) REFERENCES [BillTerm] ([CustomerCode], [BillTermId]) ON DELETE CASCADE
);

GO

CREATE TABLE [AutoDebitBillModel] (
    [BillNo] nvarchar(30) NOT NULL,
    [CustomerCode] nvarchar(15) NOT NULL,
    [PayerId] nvarchar(15) NULL,
    [PayAmount] decimal(18,2) NOT NULL,
    [VirtualAccountCode] nvarchar(20) NOT NULL,
    [ImportBatchNo] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_AutoDebitBillModel] PRIMARY KEY ([BillNo]),
    CONSTRAINT [FK_AutoDebitBillModel_BizCustomer_CustomerCode] FOREIGN KEY ([CustomerCode]) REFERENCES [BizCustomer] ([CustomerCode]) ON DELETE CASCADE,
    CONSTRAINT [FK_AutoDebitBillModel_Payer_CustomerCode_PayerId] FOREIGN KEY ([CustomerCode], [PayerId]) REFERENCES [Payer] ([CustomerCode], [PayerId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Bill] (
    [BillNo] nvarchar(30) NOT NULL,
    [CreateStaff] nvarchar(15) NOT NULL,
    [CreateTime] datetime2 NOT NULL,
    [ModifyStaff] nvarchar(15) NOT NULL,
    [ModifyTime] datetime2 NOT NULL,
    [ApproveStaff] nvarchar(15) NOT NULL,
    [ApproveTime] datetime2 NOT NULL,
    [EndCaseStaff] nvarchar(15) NOT NULL,
    [EndCaseTime] datetime2 NOT NULL,
    [InvalidStaff] nvarchar(15) NOT NULL,
    [InvalidTime] datetime2 NOT NULL,
    [FormStatus] tinyint NOT NULL,
    [InternalId] nvarchar(max) NULL,
    [CustomerCode] nvarchar(15) NOT NULL,
    [BillTermId] nvarchar(15) NULL,
    [PayerId] nvarchar(15) NULL,
    [PayEndDate] datetime2 NOT NULL,
    [CollectionTypeId] nvarchar(15) NULL,
    [PayAmount] decimal(18,2) NOT NULL,
    [HadPayAmount] decimal(18,2) NOT NULL,
    [PayStatus] tinyint NOT NULL,
    [VirtualAccountCode] nvarchar(20) NOT NULL,
    [ImportBatchNo] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_Bill] PRIMARY KEY ([BillNo]),
    CONSTRAINT [FK_Bill_CollectionType_CollectionTypeId] FOREIGN KEY ([CollectionTypeId]) REFERENCES [CollectionType] ([CollectionTypeId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Bill_BizCustomer_CustomerCode] FOREIGN KEY ([CustomerCode]) REFERENCES [BizCustomer] ([CustomerCode]) ON DELETE CASCADE,
    CONSTRAINT [FK_Bill_BillTerm_CustomerCode_BillTermId] FOREIGN KEY ([CustomerCode], [BillTermId]) REFERENCES [BillTerm] ([CustomerCode], [BillTermId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Bill_Payer_CustomerCode_PayerId] FOREIGN KEY ([CustomerCode], [PayerId]) REFERENCES [Payer] ([CustomerCode], [PayerId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [ChannelEAccountBillDetail] (
    [BillNo] nvarchar(30) NOT NULL,
    [ReceiptBillNo] nvarchar(30) NOT NULL,
    CONSTRAINT [PK_ChannelEAccountBillDetail] PRIMARY KEY ([BillNo], [ReceiptBillNo]),
    CONSTRAINT [FK_ChannelEAccountBillDetail_ChannelEAccountBill_BillNo] FOREIGN KEY ([BillNo]) REFERENCES [ChannelEAccountBill] ([BillNo]) ON DELETE CASCADE,
    CONSTRAINT [FK_ChannelEAccountBillDetail_ReceiptBill_ReceiptBillNo] FOREIGN KEY ([ReceiptBillNo]) REFERENCES [ReceiptBill] ([BillNo]) ON DELETE CASCADE
);

GO

CREATE TABLE [DepositBillReceiptDetail] (
    [BillNo] nvarchar(30) NOT NULL,
    [ReceiptBillNo] nvarchar(30) NOT NULL,
    CONSTRAINT [PK_DepositBillReceiptDetail] PRIMARY KEY ([BillNo], [ReceiptBillNo]),
    CONSTRAINT [FK_DepositBillReceiptDetail_DepositBillModel_BillNo] FOREIGN KEY ([BillNo]) REFERENCES [DepositBillModel] ([BillNo]) ON DELETE CASCADE,
    CONSTRAINT [FK_DepositBillReceiptDetail_ReceiptBill_ReceiptBillNo] FOREIGN KEY ([ReceiptBillNo]) REFERENCES [ReceiptBill] ([BillNo]) ON DELETE CASCADE
);

GO

CREATE TABLE [ReceiptBillChange] (
    [BillNo] nvarchar(30) NOT NULL,
    [RowId] int NOT NULL,
    [ChangeTime] datetime2 NOT NULL,
    [FormStatus] tinyint NOT NULL,
    [Reason] nvarchar(100) NULL,
    CONSTRAINT [PK_ReceiptBillChange] PRIMARY KEY ([BillNo], [RowId]),
    CONSTRAINT [FK_ReceiptBillChange_ReceiptBill_BillNo] FOREIGN KEY ([BillNo]) REFERENCES [ReceiptBill] ([BillNo]) ON DELETE CASCADE
);

GO

CREATE TABLE [AutoDebitBillReceiptDetail] (
    [BillNo] nvarchar(30) NOT NULL,
    [ReceiptBillNo] nvarchar(30) NOT NULL,
    CONSTRAINT [PK_AutoDebitBillReceiptDetail] PRIMARY KEY ([BillNo], [ReceiptBillNo]),
    CONSTRAINT [FK_AutoDebitBillReceiptDetail_AutoDebitBillModel_BillNo] FOREIGN KEY ([BillNo]) REFERENCES [AutoDebitBillModel] ([BillNo]) ON DELETE CASCADE,
    CONSTRAINT [FK_AutoDebitBillReceiptDetail_ReceiptBill_ReceiptBillNo] FOREIGN KEY ([ReceiptBillNo]) REFERENCES [ReceiptBill] ([BillNo]) ON DELETE CASCADE
);

GO

CREATE TABLE [BillDetail] (
    [BillNo] nvarchar(30) NOT NULL,
    [RowId] int NOT NULL,
    [FeeName] nvarchar(20) NOT NULL,
    [PayAmount] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_BillDetail] PRIMARY KEY ([BillNo], [RowId]),
    CONSTRAINT [FK_BillDetail_Bill_BillNo] FOREIGN KEY ([BillNo]) REFERENCES [Bill] ([BillNo]) ON DELETE CASCADE
);

GO

CREATE TABLE [BillReceiptDetail] (
    [BillNo] nvarchar(30) NOT NULL,
    [ReceiptBillNo] nvarchar(30) NOT NULL,
    CONSTRAINT [PK_BillReceiptDetail] PRIMARY KEY ([BillNo], [ReceiptBillNo]),
    CONSTRAINT [FK_BillReceiptDetail_Bill_BillNo] FOREIGN KEY ([BillNo]) REFERENCES [Bill] ([BillNo]) ON DELETE CASCADE,
    CONSTRAINT [FK_BillReceiptDetail_ReceiptBill_ReceiptBillNo] FOREIGN KEY ([ReceiptBillNo]) REFERENCES [ReceiptBill] ([BillNo]) ON DELETE CASCADE
);

GO

CREATE TABLE [CashFlowWriteOfDetail] (
    [BillNo] nvarchar(30) NOT NULL,
    [RowId] int NOT NULL,
    [CashFlowBillNo] nvarchar(30) NULL,
    CONSTRAINT [PK_CashFlowWriteOfDetail] PRIMARY KEY ([BillNo], [RowId]),
    CONSTRAINT [FK_CashFlowWriteOfDetail_CashFlowBill_CashFlowBillNo] FOREIGN KEY ([CashFlowBillNo]) REFERENCES [CashFlowBill] ([BillNo]) ON DELETE NO ACTION
);

GO

CREATE TABLE [ChannelWriteOfDetail] (
    [BillNo] nvarchar(30) NOT NULL,
    [RowId] int NOT NULL,
    [ChannelEAccountBillNo] nvarchar(30) NULL,
    CONSTRAINT [PK_ChannelWriteOfDetail] PRIMARY KEY ([BillNo], [RowId]),
    CONSTRAINT [FK_ChannelWriteOfDetail_ChannelEAccountBill_ChannelEAccountBillNo] FOREIGN KEY ([ChannelEAccountBillNo]) REFERENCES [ChannelEAccountBill] ([BillNo]) ON DELETE NO ACTION
);

GO

CREATE TABLE [DisbursementBill] (
    [BillNo] nvarchar(30) NOT NULL,
    [CreateStaff] nvarchar(15) NOT NULL,
    [CreateTime] datetime2 NOT NULL,
    [ModifyStaff] nvarchar(15) NOT NULL,
    [ModifyTime] datetime2 NOT NULL,
    [ApproveStaff] nvarchar(15) NOT NULL,
    [ApproveTime] datetime2 NOT NULL,
    [EndCaseStaff] nvarchar(15) NOT NULL,
    [EndCaseTime] datetime2 NOT NULL,
    [InvalidStaff] nvarchar(15) NOT NULL,
    [InvalidTime] datetime2 NOT NULL,
    [FormStatus] tinyint NOT NULL,
    [InternalId] nvarchar(max) NULL,
    [ChannelWriteOfBillNo] nvarchar(30) NULL,
    CONSTRAINT [PK_DisbursementBill] PRIMARY KEY ([BillNo])
);

GO

CREATE TABLE [ChannelWriteOfBill] (
    [BillNo] nvarchar(30) NOT NULL,
    [CreateStaff] nvarchar(15) NOT NULL,
    [CreateTime] datetime2 NOT NULL,
    [ModifyStaff] nvarchar(15) NOT NULL,
    [ModifyTime] datetime2 NOT NULL,
    [ApproveStaff] nvarchar(15) NOT NULL,
    [ApproveTime] datetime2 NOT NULL,
    [EndCaseStaff] nvarchar(15) NOT NULL,
    [EndCaseTime] datetime2 NOT NULL,
    [InvalidStaff] nvarchar(15) NOT NULL,
    [InvalidTime] datetime2 NOT NULL,
    [FormStatus] tinyint NOT NULL,
    [InternalId] nvarchar(max) NULL,
    [WriteOfStatus] tinyint NOT NULL,
    [PrePayAmount] decimal(18,2) NOT NULL,
    [DisBillNo] nvarchar(30) NULL,
    CONSTRAINT [PK_ChannelWriteOfBill] PRIMARY KEY ([BillNo]),
    CONSTRAINT [FK_ChannelWriteOfBill_DisbursementBill_DisBillNo] FOREIGN KEY ([DisBillNo]) REFERENCES [DisbursementBill] ([BillNo]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_AutoDebitBillModel_VirtualAccountCode] ON [AutoDebitBillModel] ([VirtualAccountCode]);

GO

CREATE INDEX [IX_AutoDebitBillModel_CustomerCode_PayerId] ON [AutoDebitBillModel] ([CustomerCode], [PayerId]);

GO

CREATE INDEX [IX_AutoDebitBillReceiptDetail_ReceiptBillNo] ON [AutoDebitBillReceiptDetail] ([ReceiptBillNo]);

GO

CREATE INDEX [IX_BackendUser_DeptId] ON [BackendUser] ([DeptId]);

GO

CREATE INDEX [IX_BackendUserRole_RoleId] ON [BackendUserRole] ([RoleId]);

GO

CREATE INDEX [IX_Bill_CollectionTypeId] ON [Bill] ([CollectionTypeId]);

GO

CREATE INDEX [IX_Bill_VirtualAccountCode] ON [Bill] ([VirtualAccountCode]);

GO

CREATE INDEX [IX_Bill_CustomerCode_BillTermId] ON [Bill] ([CustomerCode], [BillTermId]);

GO

CREATE INDEX [IX_Bill_CustomerCode_PayerId] ON [Bill] ([CustomerCode], [PayerId]);

GO

CREATE INDEX [IX_BillReceiptDetail_ReceiptBillNo] ON [BillReceiptDetail] ([ReceiptBillNo]);

GO

CREATE INDEX [IX_BizCustomer_AccountDeptId] ON [BizCustomer] ([AccountDeptId]);

GO

CREATE INDEX [IX_BizCustomer_CustomerId] ON [BizCustomer] ([CustomerId]);

GO

CREATE INDEX [IX_BizCustomer_IntroCustomerCode] ON [BizCustomer] ([IntroCustomerCode]);

GO

CREATE INDEX [IX_CashFlowWriteOfDetail_CashFlowBillNo] ON [CashFlowWriteOfDetail] ([CashFlowBillNo]);

GO

CREATE INDEX [IX_ChannelEAccountBill_CollectionTypeId] ON [ChannelEAccountBill] ([CollectionTypeId]);

GO

CREATE INDEX [IX_ChannelId_CollectionTypeId_ExpectRemitDate] ON [ChannelEAccountBill] ([ChannelId], [CollectionTypeId], [ExpectRemitDate]);

GO

CREATE INDEX [IX_ChannelEAccountBillDetail_ReceiptBillNo] ON [ChannelEAccountBillDetail] ([ReceiptBillNo]);

GO

CREATE INDEX [IX_ChannelWriteOfBill_DisBillNo] ON [ChannelWriteOfBill] ([DisBillNo]);

GO

CREATE INDEX [IX_ChannelWriteOfDetail_ChannelEAccountBillNo] ON [ChannelWriteOfDetail] ([ChannelEAccountBillNo]);

GO

CREATE INDEX [IX_CollectionTypeDetail_ChannelId] ON [CollectionTypeDetail] ([ChannelId]);

GO

CREATE INDEX [IX_CollectionTypeVerifyPeriod_ChannelId] ON [CollectionTypeVerifyPeriod] ([ChannelId]);

GO

CREATE INDEX [IX_CustUser_CustomerId] ON [CustUser] ([CustomerId]);

GO

CREATE INDEX [IX_CustUserRole_RoleId] ON [CustUserRole] ([RoleId]);

GO

CREATE INDEX [IX_DepositBillModel_CustomerCode] ON [DepositBillModel] ([CustomerCode]);

GO

CREATE INDEX [IX_DepositBillModel_VirtualAccountCode] ON [DepositBillModel] ([VirtualAccountCode]);

GO

CREATE INDEX [IX_DepositBillReceiptDetail_ReceiptBillNo] ON [DepositBillReceiptDetail] ([ReceiptBillNo]);

GO

CREATE INDEX [IX_DisbursementBill_ChannelWriteOfBillNo] ON [DisbursementBill] ([ChannelWriteOfBillNo]);

GO

CREATE INDEX [IX_ReceiptBill_ChannelId] ON [ReceiptBill] ([ChannelId]);

GO

CREATE INDEX [IX_ReceiptBill_CollectionTypeId] ON [ReceiptBill] ([CollectionTypeId]);

GO

CREATE INDEX [IX_ReceiptBill_CustomerCode] ON [ReceiptBill] ([CustomerCode]);

GO

ALTER TABLE [CashFlowWriteOfDetail] ADD CONSTRAINT [FK_CashFlowWriteOfDetail_ChannelWriteOfBill_BillNo] FOREIGN KEY ([BillNo]) REFERENCES [ChannelWriteOfBill] ([BillNo]) ON DELETE CASCADE;

GO

ALTER TABLE [ChannelWriteOfDetail] ADD CONSTRAINT [FK_ChannelWriteOfDetail_ChannelWriteOfBill_BillNo] FOREIGN KEY ([BillNo]) REFERENCES [ChannelWriteOfBill] ([BillNo]) ON DELETE CASCADE;

GO

ALTER TABLE [DisbursementBill] ADD CONSTRAINT [FK_DisbursementBill_ChannelWriteOfBill_ChannelWriteOfBillNo] FOREIGN KEY ([ChannelWriteOfBillNo]) REFERENCES [ChannelWriteOfBill] ([BillNo]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200415020442_V20200415-1', N'3.1.3');

GO

