using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.Report;
using SKGPortalCore.Repository.SKGPortalCore.Business.Report;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SKGPortalCore.Repository.Report
{
    /// <summary>
    /// 帳單相關報表
    /// </summary>
    [ProgId(SystemCP.ProgId_BillRpt)]
    public class BillRptRepository : BasicRptRepository
    {
        #region Construct
        public BillRptRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
        #endregion

        #region Public
        /// <summary>
        /// 帳單繳費進度報表
        /// (舊：銷帳報表列印)
        /// </summary>
        public List<BillPayProgressRptModel> BillPayProgressRpt(string customerCode, string billTermId)
        {
            return BizBillRpt.GetBillPayProgressRpt(DataAccess, customerCode, billTermId);
        }
        /// <summary>
        /// 
        /// </summary>
        public void BillPayProgressRptDoc(string customerCode, string billTermId)
        {
            using LibDocument doc = new LibDocument(); doc.ExportExcel(BizBillRpt.GetBillPayProgressRpt(DataAccess, customerCode, billTermId));
        }
        #endregion
    }
}
