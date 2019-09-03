using System;
using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Data;
using SKGPortalCore.Schedule.Import;

namespace SKGPortalCore.Schedule
{
    class Program
    {
        static void Main()
        {
            DbContextOptionsBuilder<ApplicationDbContext> builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer("Server=.;Database=SKGPortalCore;Trusted_Connection=True;MultipleActiveResultSets=true");
            using ApplicationDbContext dataAccess = new ApplicationDbContext(builder.Options);

            IImportData infoImport = new ReceiptInfoImportBANK(dataAccess);
            infoImport.ExecuteImport();
            infoImport = new RemitInfoImport(dataAccess);
            infoImport.ExecuteImport();
            infoImport = new ACCFTTImport(dataAccess);
            infoImport.ExecuteImport();
        }
    }
}
