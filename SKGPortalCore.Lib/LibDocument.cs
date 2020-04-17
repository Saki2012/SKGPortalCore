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
    public class LibDocument : IDisposable
    {
        /// <summary>
        /// 產生Excel報表
        /// </summary>
        /// <param name="bills"></param>
        public byte[] ExportExcel<T>(List<T> rpt)
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
        public byte[] ExportExcel(DataTable rpt)
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
        public void ReadExcel()
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
        public void PrintBill(/*BillSet set*/)
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

        #region IDisposable Support
        private bool disposedValue = false; // 偵測多餘的呼叫

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 處置 Managed 狀態 (Managed 物件)。
                }

                // TODO: 釋放 Unmanaged 資源 (Unmanaged 物件) 並覆寫下方的完成項。
                // TODO: 將大型欄位設為 null。

                disposedValue = true;
            }
        }

        // TODO: 僅當上方的 Dispose(bool disposing) 具有會釋放 Unmanaged 資源的程式碼時，才覆寫完成項。
        // ~LibDocument()
        // {
        //   // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 加入這個程式碼的目的在正確實作可處置的模式。
        public void Dispose()
        {
            // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果上方的完成項已被覆寫，即取消下行的註解狀態。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
