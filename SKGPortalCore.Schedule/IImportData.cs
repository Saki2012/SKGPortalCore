using System;
using System.Collections;
using System.Collections.Generic;
using GraphQL;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;

namespace SKGPortalCore.Schedule
{
    /// <summary>
    /// 資訊流導入
    /// </summary>
    public interface IImportData
    {
        /// <summary>
        /// 
        /// </summary>
        public MessageLog Message { get; }
        /// <summary>
        /// 
        /// </summary>
        public ApplicationDbContext DataAccess { get; }
        /// <summary>
        /// 執行資訊流導入
        /// </summary>
        public void ExecuteImport()
        {
            Dictionary<int, string> sources = ReadFile();
            IList sets = AnalyzeFile(sources);
            try
            {
                CreateData(sets);
                MoveToSuccessFolder();
            }
            catch (Exception ex)
            {
                var innerEx = ex.GetInnermostException();
                var exErr = new ExecutionError("異常發生", innerEx) { Source = innerEx.ToString() };
                Message.Errors.Add(exErr);
                MoveToFailFolder();
            }
            finally
            {
                Message.WriteLogTxt();
            }
        }
        /// <summary>
        /// 讀資訊流檔
        /// </summary>
        /// <returns></returns>
        private protected Dictionary<int, string> ReadFile();
        /// <summary>
        /// 分析資訊流
        /// </summary>
        /// <param name="sources"></param>
        /// <returns></returns>
        private protected IList AnalyzeFile(Dictionary<int, string> sources);
        /// <summary>
        /// 新增繳款資訊
        /// </summary>
        /// <param name="modelSources"></param>
        private protected void CreateData(IList modelSources);
        /// <summary>
        /// 將源檔案移動至成功的資料夾裡
        /// </summary>
        private protected void MoveToSuccessFolder();
        /// <summary>
        /// 將源檔案移動至失敗的資料夾裡
        /// </summary>
        private protected void MoveToFailFolder();
    }
}
