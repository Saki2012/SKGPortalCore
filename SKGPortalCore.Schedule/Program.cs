using System;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Data;
using SKGPortalCore.Schedule.Import;

namespace SKGPortalCore.Schedule
{
    class Program
    {
        static void Main()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using ApplicationDbContext dataAccess = LibDataAccess.CreateDataAccess();
            IImportData infoImport ;
            //infoImport = new ReceiptInfoImportBANK(dataAccess);
            //infoImport.ExecuteImport();
            //infoImport = new RemitInfoImport(dataAccess);
            //infoImport.ExecuteImport();
            infoImport = new ACCFTTImport(dataAccess);
            infoImport.ExecuteImport();
        }
    }
}
