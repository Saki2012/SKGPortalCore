using System;
using System.Collections;
using System.Collections.Generic;
using GraphQL;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;

namespace SKGPortalCore.Schedule.Import
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
            }
            catch (Exception ex)
            {
                Exception innerEx = ex.GetInnermostException();
                ExecutionError exErr = new ExecutionError("異常發生", innerEx) { Source = innerEx.ToString() };
                Message.Errors.Add(exErr);
            }
            finally
            {
                Message.WriteLogTxt();
            }
            MoveToOverFolder(Message.Errors.Count == 0);
        }
        /// <summary>
        /// 讀資料檔
        /// </summary>
        /// <returns></returns>
        private protected Dictionary<int, string> ReadFile();
        /// <summary>
        /// 分析檔案內容
        /// </summary>
        /// <param name="sources"></param>
        /// <returns></returns>
        private protected IList AnalyzeFile(Dictionary<int, string> sources);
        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="modelSources"></param>
        private protected void CreateData(IList modelSources);
        /// <summary>
        /// 將源檔案移動至成功/失敗的資料夾裡
        /// </summary>
        private protected void MoveToOverFolder(bool isSuccess);
    }
}
