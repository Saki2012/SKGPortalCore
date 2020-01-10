using System;
using System.Collections.Generic;
using System.Text;
using SKGPortalCore.Model.MasterData.OperateSystem;

namespace SKGPortalCore.Data
{
    /// <summary>
    /// 操作日誌系統
    /// </summary>
    public class SysOperateLog
    {
        /// <summary>
        /// 紀錄操作日誌
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ip"></param>
        /// <param name="browser"></param>
        /// <param name="progId"></param>
        /// <param name="pk"></param>
        /// <param name="action"></param>
        public static void SetOperateLog(string userId, string ip, string browser, string progId, string pk, string action)
        {
            try
            {
                using ApplicationDbContext dataAccess = LibDataAccess.CreateDataAccess();
                dataAccess.OperateLog.Add(new OperateLog()
                {
                    UserId = userId,
                    IP = ip,
                    Browser = browser,
                    ProgId = progId,
                    PK = pk,
                    OperateTime = DateTime.Now,
                    Action = action,
                });
                dataAccess.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
