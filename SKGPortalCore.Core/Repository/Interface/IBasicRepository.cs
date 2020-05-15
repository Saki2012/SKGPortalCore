using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Model.User;
using SKGPortalCore.Core.SystemTable;
using System;
using System.Collections;

namespace SKGPortalCore.Core.Repository.Interface
{
    public interface IBasicRepository<TSet> : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public IUserModel User { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProgId { get; }
        /// <summary>
        /// 
        /// </summary>
        public SysMessageLog Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataFlowNo DataFlowNo { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public TSet Create(TSet set);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="inputSet"></param>
        /// <returns></returns>
        public TSet Update(object[] key, TSet inputSet);
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="key"></param>
        public void Delete(object[] key);
        /// <summary>
        /// 查看表單
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TSet QueryData(object[] key);
        /// <summary>
        /// 查詢明細
        /// </summary>
        /// <returns></returns>
        public IList QueryList(string selectFields, string condition, int pageCt, int takeCt);
        /// <summary>.
        /// 審核
        /// </summary>
        /// <param name="key"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public TSet Approve(object[] key, bool status);
        /// <summary>
        /// 作廢
        /// </summary>
        public TSet Invalid(object[] key, bool status);
        /// <summary>
        /// 結案
        /// </summary>
        public TSet EndCase(object[] key, bool status);
        /// <summary>
        /// 執行更新
        /// </summary>
        /// <param name="action"></param>
        public void CommitData(FuncAction action);
    }
}
