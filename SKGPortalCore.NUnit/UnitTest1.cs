using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.SourceData;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ListRecordComparisonTest()
        {
            Random r = new Random(300);
            int num = 10000;
            for (int times = 0; times < 10; times++)
            {
                List<ChannelWriteOfDetailModel> channelWriteOfDetail = new List<ChannelWriteOfDetailModel>();
                List<CashFlowWriteOfDetailModel> cashFlowWriteOfDetail = new List<CashFlowWriteOfDetailModel>();

                for (int i = 0; i < num * times; i++)
                    channelWriteOfDetail.Add(new ChannelWriteOfDetailModel() { ChannelEAccountBill = new ChannelEAccountBillModel() { ChannelId = r.Next(10).ToString(), CollectionTypeId = r.Next(10).ToString(), ExpectRemitAmount = 1 } });
                for (int i = 0; i < num * times; i++)
                    cashFlowWriteOfDetail.Add(new CashFlowWriteOfDetailModel() { CashFlowBill = new CashFlowBillModel() { ChannelId = r.Next(10).ToString(), CollectionTypeId = r.Next(10).ToString(), Amount = 1 } });
                CompareData(channelWriteOfDetail, cashFlowWriteOfDetail);
            }
            Assert.Pass();
        }
        [Test]
        public void CheckACCFTT() {
            ACCFTT accftt = new ACCFTT() {  };
            if (accftt.Source != accftt.Src) {/*Error*/ }
        }


        private static void CompareData(List<ChannelWriteOfDetailModel> channelWriteOfDetail, List<CashFlowWriteOfDetailModel> cashFlowWriteOfDetail)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            RecComparison<ChannelWriteOfDetailModel, CashFlowWriteOfDetailModel> rc = new RecComparison<ChannelWriteOfDetailModel, CashFlowWriteOfDetailModel>(channelWriteOfDetail, cashFlowWriteOfDetail);
            rc.Master.Sort(new Comparison<ChannelWriteOfDetailModel>((x, y) =>
            {
                int result = x.ChannelEAccountBill.ChannelId.CompareTo(y.ChannelEAccountBill.ChannelId);
                if (result == 0) result = x.ChannelEAccountBill.CollectionTypeId.CompareTo(y.ChannelEAccountBill.CollectionTypeId);
                else return result;
                return result;
            }));
            rc.Detail.Sort(new Comparison<CashFlowWriteOfDetailModel>((x, y) =>
            {
                int result = x.CashFlowBill.ChannelId.CompareTo(y.CashFlowBill.ChannelId);
                if (result == 0) result = x.CashFlowBill.CollectionTypeId.CompareTo(y.CashFlowBill.CollectionTypeId);
                else return result;
                return result;
            }));
            rc.CompareFunc = new Func<ChannelWriteOfDetailModel, CashFlowWriteOfDetailModel, int>((x, y) =>
            {
                int result = x.ChannelEAccountBill.ChannelId.CompareTo(y.CashFlowBill.ChannelId);
                if (result == 0) result = x.ChannelEAccountBill.CollectionTypeId.CompareTo(y.CashFlowBill.CollectionTypeId);
                else return result;
                return result;
            });
            int times1 = 0, times2 = 0;
            decimal val = 0m;
            if (rc.Enable)
                while (!rc.IsEof)
                {
                    times1++;
                    rc.BackToBookMark();
                    val += rc.CurrentRow.ChannelEAccountBill.ExpectRemitAmount;
                    while (rc.Compare())
                    {
                        times2++;
                        rc.SetBookMark();
                        val -= rc.DetailRow.CashFlowBill.Amount;
                        rc.DetailMoveNext();
                    }
                    rc.MoveNext();
                }
            sw.Stop();
            Console.WriteLine(Pad("Status:¤Ï®gCompare"));
            Console.WriteLine($"Time:{ sw.ElapsedMilliseconds},MasterTimes:{times1},DetailTimes:{times2},Val:{val}");
        }
        private static string Pad(string s)
        {
            return $"-------------------{s}".PadRight(45, '-');
        }
    }
}