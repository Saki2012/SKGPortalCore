using OfficeOpenXml;
using OfficeOpenXml.Table;
using pdftron.PDF;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace SKGPortalCore.Lib
{
    public static class LibDocument 
    {
        /// <summary>
        /// 產生Excel報表
        /// </summary>
        /// <param name="bills"></param>
        public static byte[] ExportExcel<T>(List<T> rpt)
        {
            using ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add(ResxManage.GetDescription<T>());
            workSheet.Cells["A1"].LoadFromCollection(rpt, true, TableStyles.Medium12);
            return excel.GetAsByteArray();
        }
        /// <summary>
        /// 產生Excel報表
        /// </summary>
        /// <param name="bills"></param>
        public static byte[] ExportExcel(DataTable rpt)
        {
            using ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Test");
            workSheet.Cells["A1"].LoadFromDataTable(rpt, true, TableStyles.Medium12);
            excel.SaveAs(new FileInfo(@"D:\ibankRoot\Ftp_SKGPortalCore\Doc\Test.xls"));
            return excel.GetAsByteArray();
        }
        /// <summary>
        /// 
        /// </summary>
        public static void ReadExcel()
        {
            using FileStream fs = new FileStream(@"C:\Read.xlsx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using ExcelPackage excel = new ExcelPackage(fs);
            ExcelWorksheet sheet = excel.Workbook.Worksheets[1];//取得Sheet1
            int startRowNumber = sheet.Dimension.Start.Row;//起始列編號，從1算起
            int endRowNumber = sheet.Dimension.End.Row;//結束列編號，從1算起
            int startColumn = sheet.Dimension.Start.Column;//開始欄編號，從1算起
            int endColumn = sheet.Dimension.End.Column;//結束欄編號，從1算起
            bool isHeader = true;//有包含標題
            if (isHeader) startRowNumber += 1;
            for (int currentRow = startRowNumber; currentRow <= endRowNumber; currentRow++)
            {
                ExcelRange range = sheet.Cells[currentRow, startColumn, currentRow, endColumn];//抓出目前的Excel列
                if (!range.Any(c => !string.IsNullOrEmpty(c.Text)))//這是一個完全空白列(使用者用Delete鍵刪除動作)
                    continue;//略過此列
                //讀值
                string cellValue = sheet.Cells[currentRow, 1].Text;//讀取格式化過後的文字(讀取使用者看到的文字)
            }
        }
        /// <summary>
        /// 產生PDF報表
        /// </summary>
        /// <param name="set"></param>
        public static void PrintBill(/*BillSet set*/)
        {
            using PDFDoc pdfdoc = new PDFDoc();
            //pdftron.PDF.Convert.OfficeToPDF(pdfdoc, $"{ReportTemplate.TemplatePath}{ReportTemplate.BillTemplate}.docx", null);
            Page page = pdfdoc.GetPage(1);
            ContentReplacer replacer = new ContentReplacer();
            //SetData();
            //foreach (string key in Dic.Keys) replacer.AddString(key, Dic[key]);
            replacer.Process(page);
            //pdfdoc.Save($"{ReportTemplate.TemplateOutputPath}{ReportTemplate.ReceiptTemplate}{ReportTemplate.Resx}.pdf", SDFDoc.SaveOptions.e_linearized);
        }
    }
}
