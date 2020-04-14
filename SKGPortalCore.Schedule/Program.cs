using System.Configuration;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Repository.SKGPortalCore.Business.Import;

namespace SKGPortalCore.Schedule
{
    internal class Program
    {
        private static readonly IConfiguration Config= new ConfigurationBuilder().SetBasePath(SystemCP.AppSettingsJsonPath).AddJsonFile(SystemCP.AppSettingsJson).Build();
        private static void Main()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using ApplicationDbContext dataAccess = LibDataAccess.CreateDataAccess(Config);
            IImportData infoImport;
            infoImport = new ACCFTTImport(dataAccess); infoImport.ExecuteImport();
            infoImport = new ReceiptInfoImportBANK(dataAccess); infoImport.ExecuteImport();
            infoImport = new ReceiptInfoImportPOST(dataAccess); infoImport.ExecuteImport();
            infoImport = new ReceiptInfoImportMARKET(dataAccess); infoImport.ExecuteImport();
            //infoImport = new ReceiptInfoImportMARKETSPI(dataAccess); infoImport.ExecuteImport();
            //infoImport = new ReceiptInfoImportFARM(dataAccess); infoImport.ExecuteImport();
            infoImport = new RemitInfoImport(dataAccess); infoImport.ExecuteImport();
        }

    }
}
