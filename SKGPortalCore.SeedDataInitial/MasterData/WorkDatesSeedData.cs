using SKGPortalCore.Core.DB;
using SKGPortalCore.Model.MasterData;
using System;
using System.Collections.Generic;

namespace SKGPortalCore.SeedDataInitial.MasterData
{
    public static class WorkDatesSeedData
    {
        public static void CreateWorkDates(ApplicationDbContext dataAccess)
        {
            List<WorkDateModel> workDates = new List<WorkDateModel>();
            DateTime date = DateTime.Now.AddYears(-1).Date;
            DateTime date2 = DateTime.Now.AddYears(1).Date;
            while (date != date2)
            {
                //if (null == dataAccess.WorkDate.FirstOrDefault(p => p.Date == date))
                //    workDates.Add(new WorkDateModel() { Date = date, Description = "", HolidayCategory = "", IsWorkDate = !date.DayOfWeek.In(DayOfWeek.Sunday, DayOfWeek.Saturday), HolidayName = "" });
                date = date.AddDays(1);
            }
            //dataAccess.WorkDate.AddRange(workDates);
        }
    }
}
