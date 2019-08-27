using SKGPortalCore.Lib;
using SKGPortalCore.Model.BillData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TestSln
{
    class Program
    {
        static void Main(string[] args)
        {
            ListRecordComparisonTest();
        }
        #region ListRecordComparison Test
        public static void ListRecordComparisonTest()
        {
            Random r = new Random(300);
            int num = 100;
            for (int times = 0; times < 10; times++)
            {
                List<ChannelWriteOfDetailModel> channelWriteOfDetail = new List<ChannelWriteOfDetailModel>();
                List<CashFlowWriteOfDetailModel> cashFlowWriteOfDetail = new List<CashFlowWriteOfDetailModel>();

                for (int i = 0; i < num; i++)
                    channelWriteOfDetail.Add(new ChannelWriteOfDetailModel() { ChannelEAccountBill = new ChannelEAccountBillModel() { ChannelId = r.Next(3).ToString(), CollectionTypeId = r.Next(5).ToString(), ExpectRemitAmount = 100 } });
                for (int i = 0; i < num; i++)
                    cashFlowWriteOfDetail.Add(new CashFlowWriteOfDetailModel() { CashFlowBill = new CashFlowBillModel() { ChannelId = r.Next(3).ToString(), CollectionTypeId = r.Next(5).ToString(), Amount = 100 } });
                CompareData(channelWriteOfDetail, cashFlowWriteOfDetail);
            }
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
                    //rc.BackToBookMark();
                    val += rc.CurrentRow.ChannelEAccountBill.ExpectRemitAmount;
                    while (rc.Compare())
                    {
                        times2++;
                        //rc.SetBookMark();
                        val -= rc.DetailRow.CashFlowBill.Amount;
                        rc.DetailMoveNext();
                    }
                    rc.MoveNext();
                }
            sw.Stop();
            Console.WriteLine(Pad("Status:反射Compare"));
            Console.WriteLine($"Time:{ sw.ElapsedMilliseconds},MasterTimes:{times1},DetailTimes:{times2},Val:{val}");
        }
        #endregion

        private static string Pad(string s)
        {
            return $"-------------------{s}".PadRight(45, '-');
        }
    }
}
