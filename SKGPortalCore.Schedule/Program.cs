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
            using ApplicationDbContext dataAccess = LibDataAccess.CreateDataAccess();
            IImportData infoImport = new ReceiptInfoImportBANK(dataAccess);
            infoImport.ExecuteImport();
            infoImport = new RemitInfoImport(dataAccess);
            infoImport.ExecuteImport();
            infoImport = new ACCFTTImport(dataAccess);
            infoImport.ExecuteImport();
        }
    }
}
