using System.Collections;
using System.Collections.Generic;
using SKGPortalCore.Data;

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
        public ApplicationDbContext DataAccess { get; }
        /// <summary>
        /// 執行資訊流導入
        /// </summary>
        public void ExecuteImport()
        {
            Dictionary<int, string> sources = ReadFile();
            IList sets = AnalyzeFile(sources);
            CreateData(sets);
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
    }
}
