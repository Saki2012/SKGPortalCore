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
            int num = 1000000;
            for (int times = 0; times < 10; times++)
            {
                List<ChannelWriteOfDetailModel> channelWriteOfDetail = new List<ChannelWriteOfDetailModel>();
                List<CashFlowWriteOfDetailModel> cashFlowWriteOfDetail = new List<CashFlowWriteOfDetailModel>();
                for (int i = 0; i < num; i++)
                    channelWriteOfDetail.Add(new ChannelWriteOfDetailModel() { BillNo = i.ToString().PadLeft(3, '0'), RowId = 0, Value = 0 });
                for (int i = 0; i < num; i++)
                    cashFlowWriteOfDetail.Add(new CashFlowWriteOfDetailModel() { BillNo = i.ToString().PadLeft(3, '0'), RowId = 0, Value = r.Next(300) });

                List<ChannelWriteOfDetailModel> channelWriteOfDetailEx = new List<ChannelWriteOfDetailModel>();
                foreach (var d in channelWriteOfDetail)
                    channelWriteOfDetailEx.Add(new ChannelWriteOfDetailModel() { BillNo = d.BillNo, RowId = d.RowId, Value = d.Value });
                CompareData(channelWriteOfDetail, cashFlowWriteOfDetail);
                CompareDataEx(channelWriteOfDetailEx, cashFlowWriteOfDetail);
            }
        }
        private static void CompareData(List<ChannelWriteOfDetailModel> channelWriteOfDetail, List<CashFlowWriteOfDetailModel> cashFlowWriteOfDetail)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            RecComparison<ChannelWriteOfDetailModel, CashFlowWriteOfDetailModel> rc = new RecComparison<ChannelWriteOfDetailModel, CashFlowWriteOfDetailModel>(channelWriteOfDetail, cashFlowWriteOfDetail, "BillNo,RowId", "BillNo,RowId");
            int times1 = 0, times2 = 0;
            if (rc.Enable)
                while (!rc.IsEof)
                {
                    times1++;
                    rc.BackToBookMark();
                    while (rc.Compare())
                    {
                        times2++;
                        rc.SetBookMark();
                        rc.CurrentRow.Value += rc.DetailRow.Value;
                        rc.DetailMoveNext();
                    }
                    rc.MoveNext();
                }
            sw.Stop();
            Console.WriteLine(Pad("Status:反射Compare"));
            Console.WriteLine($"Time:{ sw.ElapsedMilliseconds},MasterTimes:{times1},DetailTimes:{times2}");
        }

        private static void CompareDataEx(List<ChannelWriteOfDetailModel> channelWriteOfDetail, List<CashFlowWriteOfDetailModel> cashFlowWriteOfDetail)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            RecComparison<ChannelWriteOfDetailModel, CashFlowWriteOfDetailModel> rc = new RecComparison<ChannelWriteOfDetailModel, CashFlowWriteOfDetailModel>(channelWriteOfDetail, cashFlowWriteOfDetail, new string[] { "BillNo", "RowId" }, new string[] { "BillNo", "RowId" });
            rc.CompareFunc =
                (x, y) =>
                {
                    int result = 0;
                    if (result == 0) result = x.BillNo.CompareTo(y.BillNo);
                    if (result == 0) result = x.RowId.ToString().CompareTo(y.RowId.ToString());
                    return result;
                };
            int times1 = 0, times2 = 0;
            if (rc.Enable)
                while (!rc.IsEof)
                {
                    times1++;
                    rc.BackToBookMark();
                    while (rc.Compare())
                    {
                        times2++;
                        rc.SetBookMark();
                        rc.CurrentRow.Value += rc.DetailRow.Value;
                        rc.DetailMoveNext();
                    }
                    rc.MoveNext();
                }
            sw.Stop();
            Console.WriteLine(Pad("StatusEX:實體Compare"));
            Console.WriteLine($"Time:{ sw.ElapsedMilliseconds},MasterTimes:{times1},DetailTimes:{times2}");
        }
        #endregion

        private static string Pad(string s)
        {
            return $"-------------------{s}".PadRight(45, '-');
        }
    }
}
