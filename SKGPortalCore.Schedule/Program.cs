using System;

namespace SKGPortalCore.Schedule
{
    class Program
    {
        static void Main(string[] args)
        {
            IReceiptInfoImport infoImport = new ReceiptInfoImportBANK();
            infoImport.ExecuteImport();
            RemitInfoImport remitInfoImport = new RemitInfoImport();
            remitInfoImport.ExecuteImport();
        }
    }
}
