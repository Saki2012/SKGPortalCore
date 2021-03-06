﻿using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.Model;
using System;

namespace SKGPortalCore.Core
{
    /// <summary>
    /// 操作日誌系統
    /// </summary>
    public static class SysOperateLog
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
        public static void SetOperateLog(string userId, string ip, string browser, string progId, string pk, string action, string memo)
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
                    Memo = memo,
                });
                dataAccess.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
    }
}
