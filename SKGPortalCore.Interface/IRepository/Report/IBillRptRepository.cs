using System;
using System.Collections.Generic;
using System.Text;
using SKGPortalCore.Model.Report;

namespace SKGPortalCore.Interface.IRepository.Report
{
    public interface IBillRptRepository         
    {
        /// <summary>
        /// 帳單繳費進度報表
        /// (舊：銷帳報表列印)
        /// </summary>
        public List<BillPayProgressRptModel> BillPayProgressRpt(string customerCode, string billTermId);
        /// <summary>
        /// 
        /// </summary>
        public void BillPayProgressRptDoc(string customerCode, string billTermId);
    }
}
