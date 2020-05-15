using System.Collections.Generic;
using System.Runtime.InteropServices;
using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.Libary;
using SKGPortalCore.Core.Repository.Entity;
using SKGPortalCore.Interface.IRepository.Report;
using SKGPortalCore.Model.Report;
using SKGPortalCore.Repository.SKGPortalCore.Business.Report;

namespace SKGPortalCore.Repository.Report
{
    /// <summary>
    /// 帳單相關報表
    /// </summary>
    [ProgId(SystemCP.ProgId_BillRpt)]
    public class BillRptRepository : BasicRptRepository, IBillRptRepository
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
            LibDocument.ExportExcel(BizBillRpt.GetBillPayProgressRpt(DataAccess, customerCode, billTermId));
        }
        #endregion
    }
}
