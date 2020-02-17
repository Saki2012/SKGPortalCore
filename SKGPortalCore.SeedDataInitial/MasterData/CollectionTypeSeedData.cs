﻿using System;
using System.Collections.Generic;
using System.Text;
using SKGPortalCore.Data;
using SKGPortalCore.Model.Enum;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Repository.MasterData;

namespace SKGPortalCore.SeedDataInitial.MasterData
{
    public class CollectionTypeSeedData
    {
        /// <summary>
        /// 新增「代收類別」-初始資料
        /// </summary>
        /// <param name="db"></param>
        public static void CreateCollectionType(SysMessageLog Message, ApplicationDbContext dataAccess)
        {
            try
            {
                Message.Prefix = "新增「代收類別」-初始資料：";
                using CollectionTypeRepository repo = new CollectionTypeRepository(dataAccess) { User = SystemOperator.SysOperator, Message = Message };
                List<CollectionTypeSet> collectionTypes = new List<CollectionTypeSet>() {
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="50084884",CollectionTypeName="郵局特戶", ChargePayType= ChargePayType.Deduction},
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "50084884", ChannelId = "05", SRange = 1, ERange=100 , ChannelFee = 5 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "50084884", ChannelId = "05", SRange = 101, ERange = 1000, ChannelFee = 10 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "50084884", ChannelId = "05", SRange = 1001, ERange = 9999999, ChannelFee = 15 },
                                new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "50084884", ChannelId = "A1", SRange = 1, ERange = 100, ChannelFee = 5 },
                                new CollectionTypeDetailModel() { RowId = 5, CollectionTypeId = "50084884", ChannelId = "A1", SRange = 101, ERange = 1000, ChannelFee = 10 },
                                new CollectionTypeDetailModel() { RowId = 6, CollectionTypeId = "50084884", ChannelId = "A1", SRange = 1001, ERange = 9999999, ChannelFee = 15 }
                            }
                         },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="62H",CollectionTypeName="一般代收(2萬、內扣、日結)", ChargePayType= ChargePayType.Deduction },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "62H", ChannelId = "02", SRange = 1, ERange = 20000, ChannelFee = 12 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "62H", ChannelId = "03", SRange = 1, ERange = 20000, ChannelFee = 13 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "62H", ChannelId = "04", SRange = 1, ERange = 20000, ChannelFee = 12 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="62I",CollectionTypeName="一般代收(4萬、內扣、日結)", ChargePayType= ChargePayType.Deduction },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "62I", ChannelId = "02", SRange = 20001, ERange = 40000, ChannelFee = 16 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "62I", ChannelId = "03", SRange = 20001, ERange = 40000, ChannelFee = 16 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "62I", ChannelId = "04", SRange = 20001, ERange = 40000, ChannelFee = 16 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="62M",CollectionTypeName="一般代收(6萬、內扣、日結)", ChargePayType= ChargePayType.Deduction },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "62M", ChannelId = "02", SRange = 40001, ERange = 60000, ChannelFee = 21 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "62M", ChannelId = "03", SRange = 40001, ERange = 60000, ChannelFee = 21 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "62M", ChannelId = "04", SRange = 40001, ERange = 60000, ChannelFee = 21 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="62N",CollectionTypeName="一般代收(2萬、外加、日結)", ChargePayType= ChargePayType.Increase },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "62N", ChannelId = "02", SRange = 1, ERange = 20000, ChannelFee = 12 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "62N", ChannelId = "03", SRange = 1, ERange = 20000, ChannelFee = 13 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "62N", ChannelId = "04", SRange = 1, ERange = 20000, ChannelFee = 12 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="62T",CollectionTypeName="一般代收(4萬、外加、日結)", ChargePayType= ChargePayType.Increase },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "62T", ChannelId = "02", SRange = 20001, ERange = 40000, ChannelFee = 16 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "62T", ChannelId = "03", SRange = 20001, ERange = 40000, ChannelFee = 16 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "62T", ChannelId = "04", SRange = 20001, ERange = 40000, ChannelFee = 16 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="62U",CollectionTypeName="一般代收(6萬、外加、日結)", ChargePayType= ChargePayType.Increase },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "62U", ChannelId = "02", SRange = 40001, ERange = 60000, ChannelFee = 21 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "62U", ChannelId = "03", SRange = 40001, ERange = 60000, ChannelFee = 21 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "62U", ChannelId = "04", SRange = 40001, ERange = 60000, ChannelFee = 21 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="62V",CollectionTypeName="政黨捐款", ChargePayType= ChargePayType.Deduction },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "62V", ChannelId = "02", SRange = 1, ERange = 20000, ChannelFee = 6 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "62V", ChannelId = "03", SRange = 1, ERange = 20000, ChannelFee = 6 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "62V", ChannelId = "04", SRange = 1, ERange = 20000, ChannelFee = 6 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="62W",CollectionTypeName="慈善捐款", ChargePayType= ChargePayType.Deduction },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "62W", ChannelId = "02", SRange = 1, ERange = 20000, ChannelFee = 6 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "62W", ChannelId = "03", SRange = 1, ERange = 20000, ChannelFee = 6 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "62W", ChannelId = "04", SRange = 1, ERange = 20000, ChannelFee = 6 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="62X",CollectionTypeName="黨費", ChargePayType= ChargePayType.Deduction },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "62X", ChannelId = "02", SRange = 1, ERange = 20000, ChannelFee = 6 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "62X", ChannelId = "03", SRange = 1, ERange = 20000, ChannelFee = 6 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "62X", ChannelId = "04", SRange = 1, ERange = 20000, ChannelFee = 6 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="62Y",CollectionTypeName="有線電視", ChargePayType= ChargePayType.Deduction },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "62Y", ChannelId = "02", SRange = 1, ERange = 20000, ChannelFee = 6 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "62Y", ChannelId = "03", SRange = 1, ERange = 20000, ChannelFee = 6 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "62Y", ChannelId = "04", SRange = 1, ERange = 20000, ChannelFee = 6 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="62Z",CollectionTypeName="瓦斯費", ChargePayType= ChargePayType.Deduction },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "62Z", ChannelId = "02", SRange = 1, ERange = 20000, ChannelFee = 6 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "62Z", ChannelId = "03", SRange = 1, ERange = 20000, ChannelFee = 6 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "62Z", ChannelId = "04", SRange = 1, ERange = 20000, ChannelFee = 6 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6RK",CollectionTypeName="超商代收-學雜費", ChargePayType= ChargePayType.Deduction },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6RK", ChannelId = "01", SRange = 40001, ERange = 60000, ChannelFee = 18 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6RK", ChannelId = "02", SRange = 40001, ERange = 60000, ChannelFee = 18 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6RK", ChannelId = "03", SRange = 40001, ERange = 60000, ChannelFee = 18 },
                                new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6RK", ChannelId = "04", SRange = 40001, ERange = 60000, ChannelFee = 18 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6RL",CollectionTypeName="超商代收-學雜費", ChargePayType= ChargePayType.Increase },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6RL", ChannelId = "01", SRange = 40001, ERange = 60000, ChannelFee = 18 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6RL", ChannelId = "02", SRange = 40001, ERange = 60000, ChannelFee = 18 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6RL", ChannelId = "03", SRange = 40001, ERange = 60000, ChannelFee = 18 },
                                new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6RL", ChannelId = "04", SRange = 40001, ERange = 60000, ChannelFee = 18 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6RM",CollectionTypeName="超商代收-一般", ChargePayType= ChargePayType.Deduction },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6RM", ChannelId = "01", SRange = 40001, ERange = 60000, ChannelFee = 13 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6RM", ChannelId = "02", SRange = 40001, ERange = 60000, ChannelFee = 12 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6RM", ChannelId = "03", SRange = 40001, ERange = 60000, ChannelFee = 12 },
                                new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6RM", ChannelId = "04", SRange = 40001, ERange = 60000, ChannelFee = 13 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6RN",CollectionTypeName="超商代收-一般", ChargePayType= ChargePayType.Increase },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6RN", ChannelId = "01", SRange = 40001, ERange = 60000, ChannelFee = 22 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6RN", ChannelId = "02", SRange = 40001, ERange = 60000, ChannelFee = 22 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6RN", ChannelId = "03", SRange = 40001, ERange = 60000, ChannelFee = 22 },
                                new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6RN", ChannelId = "04", SRange = 40001, ERange = 60000, ChannelFee = 22 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6V0",CollectionTypeName="超商代收-學雜費", ChargePayType= ChargePayType.Increase },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6V0", ChannelId = "01", SRange = 20001, ERange = 40000, ChannelFee = 15 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6V0", ChannelId = "02", SRange = 20001, ERange = 40000, ChannelFee = 15 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6V0", ChannelId = "03", SRange = 20001, ERange = 40000, ChannelFee = 15 },
                                new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6V0", ChannelId = "04", SRange = 20001, ERange = 40000, ChannelFee = 15 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6V1",CollectionTypeName="超商代收-一般", ChargePayType= ChargePayType.Deduction },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6V1", ChannelId = "01", SRange = 1, ERange = 20000, ChannelFee = 10 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6V1", ChannelId = "02", SRange = 1, ERange = 20000, ChannelFee = 10 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6V1", ChannelId = "03", SRange = 1, ERange = 20000, ChannelFee = 10 },
                                new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6V1", ChannelId = "04", SRange = 1, ERange = 20000, ChannelFee = 10 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6V2",CollectionTypeName="超商代收-一般", ChargePayType= ChargePayType.Deduction },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6V2", ChannelId = "01", SRange = 20001, ERange = 40000, ChannelFee = 15 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6V2", ChannelId = "02", SRange = 20001, ERange = 40000, ChannelFee = 15 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6V2", ChannelId = "03", SRange = 20001, ERange = 40000, ChannelFee = 15 },
                                new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6V2", ChannelId = "04", SRange = 20001, ERange = 40000, ChannelFee = 15 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6V3",CollectionTypeName="超商代收-學雜費", ChargePayType= ChargePayType.Deduction },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6V3", ChannelId = "01", SRange = 1, ERange = 20000, ChannelFee = 6 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6V3", ChannelId = "02", SRange = 1, ERange = 20000, ChannelFee = 6 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6V3", ChannelId = "03", SRange = 1, ERange = 20000, ChannelFee = 6 },
                                new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6V3", ChannelId = "04", SRange = 1, ERange = 20000, ChannelFee = 6 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6V4",CollectionTypeName="超商代收-學雜費", ChargePayType= ChargePayType.Deduction },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6V4", ChannelId = "01", SRange = 1, ERange = 20000, ChannelFee = 10 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6V4", ChannelId = "02", SRange = 1, ERange = 20000, ChannelFee = 10 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6V4", ChannelId = "03", SRange = 1, ERange = 20000, ChannelFee = 10 },
                                new CollectionTypeDetailModel() { CollectionTypeId = "6V4", ChannelId = "04", SRange = 1, ERange = 20000, ChannelFee = 10 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6V5",CollectionTypeName="超商代收-學雜費", ChargePayType= ChargePayType.Deduction },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6V5", ChannelId = "01", SRange = 20001, ERange = 40000, ChannelFee = 15 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6V5", ChannelId = "02", SRange = 20001, ERange = 40000, ChannelFee = 15 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6V5", ChannelId = "03", SRange = 20001, ERange = 40000, ChannelFee = 15 },
                                new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6V5", ChannelId = "04", SRange = 20001, ERange = 40000, ChannelFee = 15 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6V6",CollectionTypeName="超商代收-一般", ChargePayType= ChargePayType.Increase },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6V6", ChannelId = "01", SRange = 1, ERange = 20000, ChannelFee = 10 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6V6", ChannelId = "02", SRange = 1, ERange = 20000, ChannelFee = 10 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6V6", ChannelId = "03", SRange = 1, ERange = 20000, ChannelFee = 10 },
                                new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6V6", ChannelId = "04", SRange = 1, ERange = 20000, ChannelFee = 10 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6V7",CollectionTypeName="超商代收-一般", ChargePayType= ChargePayType.Increase },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6V7", ChannelId = "01", SRange = 20001, ERange = 40000, ChannelFee = 15 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6V7", ChannelId = "02", SRange = 20001, ERange = 40000, ChannelFee = 15 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6V7", ChannelId = "03", SRange = 20001, ERange = 40000, ChannelFee = 15 },
                                new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6V7", ChannelId = "04", SRange = 20001, ERange = 40000, ChannelFee = 15 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6V8",CollectionTypeName="超商代收-學雜費", ChargePayType= ChargePayType.Increase },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6V8", ChannelId = "01", SRange = 1, ERange = 20000, ChannelFee = 6 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6V8", ChannelId = "02", SRange = 1, ERange = 20000, ChannelFee = 6 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6V8", ChannelId = "03", SRange = 1, ERange = 20000, ChannelFee = 6 },
                                new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6V8", ChannelId = "04", SRange = 1, ERange = 20000, ChannelFee = 6 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="6V9",CollectionTypeName="超商代收-學雜費", ChargePayType= ChargePayType.Increase},
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "6V9", ChannelId = "01", SRange = 1, ERange = 20000, ChannelFee = 10 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "6V9", ChannelId = "02", SRange = 1, ERange = 20000, ChannelFee = 10 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "6V9", ChannelId = "03", SRange = 1, ERange = 20000, ChannelFee = 10 },
                                new CollectionTypeDetailModel() { RowId = 4, CollectionTypeId = "6V9", ChannelId = "04", SRange = 1, ERange = 20000, ChannelFee = 10 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="Bank999",CollectionTypeName="銀行通路", ChargePayType= ChargePayType.Increase },
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "Bank999", ChannelId = "00", SRange = 1, ERange = 9999999, ChannelFee = 10 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "Bank999", ChannelId = "A2", SRange = 1, ERange = 9999999, ChannelFee = 10 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "Bank999", ChannelId = "06", SRange = 1, ERange = 9999999, ChannelFee = 10 }
                            }
                        },
                        new CollectionTypeSet() { CollectionType = new CollectionTypeModel() {CollectionTypeId="I0O",CollectionTypeName="超商代收-保險費", ChargePayType= ChargePayType.Deduction},
                            CollectionTypeDetail = new List<CollectionTypeDetailModel>() {
                                new CollectionTypeDetailModel() { RowId = 1, CollectionTypeId = "I0O", ChannelId = "01", SRange = 1, ERange = 50000, ChannelFee = 20 },
                                new CollectionTypeDetailModel() { RowId = 2, CollectionTypeId = "I0O", ChannelId = "02", SRange = 1, ERange = 50000, ChannelFee = 20 },
                                new CollectionTypeDetailModel() { RowId = 3, CollectionTypeId = "I0O", ChannelId = "03", SRange = 1, ERange = 50000, ChannelFee = 20 },
                                new CollectionTypeDetailModel() { CollectionTypeId = "I0O", ChannelId = "04", SRange = 1, ERange = 50000, ChannelFee = 20 }
                            }
                        },
                    };
                foreach (CollectionTypeSet collectionType in collectionTypes)
                {
                    if (null == repo.QueryData(new[] { collectionType.CollectionType.CollectionTypeId }))
                    {
                        repo.Create(collectionType);
                    }
                }
            }
            finally
            {
                Message.Prefix = string.Empty;
            }
        }
    }
}
