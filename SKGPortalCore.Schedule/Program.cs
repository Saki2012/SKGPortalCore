using System.Configuration;
using System.Text;
using SKGPortalCore.Data;
using SKGPortalCore.Schedule.Import;

namespace SKGPortalCore.Schedule
{
    internal class Program
    {
        private static void Main()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using ApplicationDbContext dataAccess = LibDataAccess.CreateDataAccess();
            IImportData infoImport;
            infoImport = new ACCFTTImport(dataAccess); infoImport.ExecuteImport();
            infoImport = new ReceiptInfoImportBANK(dataAccess); infoImport.ExecuteImport();
            infoImport = new ReceiptInfoImportPOST(dataAccess); infoImport.ExecuteImport();
            infoImport = new ReceiptInfoImportMARKET(dataAccess); infoImport.ExecuteImport();
            infoImport = new ReceiptInfoImportMARKETSPI(dataAccess); infoImport.ExecuteImport();
            infoImport = new ReceiptInfoImportFARM(dataAccess); infoImport.ExecuteImport();
            infoImport = new RemitInfoImport(dataAccess); infoImport.ExecuteImport();
        }
    }
}
