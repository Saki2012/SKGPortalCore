using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData.OperateSystem;
using System.Linq;
namespace SKGPortalCore.Data
{
    /// <summary>
    /// 變更日誌系統
    /// </summary>
    public static class SysChangeLog<T>
    {
        #region Public
        /// <summary>
        /// 設置變更日誌
        /// </summary>
        public static void SetDataChangeLog(DbSet<DataChangeLog> dataChangeLog, string progId, string internalId, string userId, dynamic oldModel, dynamic newModel)
        {
            dataChangeLog.Add(new DataChangeLog() { ProgId = progId, InternalId = internalId, UserId = userId, ChangeData = GetChangeData(oldModel, newModel) });
        }
        /// <summary>
        /// 獲取變更日誌
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataChangeLog GetChangeLog(DbSet<DataChangeLog> dataChangeLog, long id)
        {
            return dataChangeLog.FirstOrDefault(p => p.Id == id);
        }
        /// <summary>
        /// 獲取變更內容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetChangeLogData(DbSet<DataChangeLog> dataChangeLog, long id)
        {
            return DecompressJsonVal(dataChangeLog.FirstOrDefault(p => p.Id == id).ChangeData);
        }
        /// <summary>
        /// 獲取變更日誌列表
        /// </summary>
        /// <param name="internalId"></param>
        /// <returns></returns>
        public static List<DataChangeLog> GetChangeLogList(DbSet<DataChangeLog> dataChangeLog, string internalId)
        {
            return dataChangeLog.Where(p => p.InternalId == internalId) as List<DataChangeLog>;
        }
        /// <summary>
        /// 當表單刪除時，移除該表單變更日誌
        /// </summary>
        /// <param name="internalId"></param>
        public static void RemoveChangeLog(DbSet<DataChangeLog> dataChangeLog, string internalId)
        {
            dataChangeLog.RemoveRange(dataChangeLog.Where(p => p.InternalId == internalId));
        }
        #endregion

        #region Private
        /// <summary>
        /// 獲取變更資料
        /// </summary>
        /// <param name="oldModel"></param>
        /// <param name="newModel"></param>
        /// <returns></returns>
        private static byte[] GetChangeData(dynamic oldModel, dynamic newModel)
        {
            Dictionary<string, object[]> changeFieldsDic = GetChangeDataDic(oldModel, newModel);
            string json = JsonConvert.SerializeObject(changeFieldsDic);
            return CompressJsonVal(json);
        }
        /// <summary>
        /// 獲取變更資料
        /// </summary>
        /// <param name="oldModel"></param>
        /// <param name="newModel"></param>
        /// <returns></returns>
        private static Dictionary<string, object[]> GetChangeDataDic(dynamic oldModel, dynamic newModel)
        {
            Dictionary<string, object[]> result = new Dictionary<string, object[]>();
            result.Add("testc", new object[] { DateTime.Now, Guid.NewGuid() });
            return result;
        }
        /// <summary>
        /// 壓縮
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private static byte[] CompressJsonVal(string json)
        {
            string compressString = LibCompress.GZipCompressString(json);
            byte[] bytes = LibCompress.ConvertStringToBytes(compressString);
            return LibCompress.Compress(bytes);
        }
        /// <summary>
        /// 解壓縮
        /// </summary>
        /// <param name="compressJson"></param>
        /// <returns></returns>
        private static string DecompressJsonVal(byte[] compressJson)
        {
            byte[] decompress = LibCompress.Decompress(compressJson);
            string str = LibCompress.ConvertBytesToString(decompress);
            return LibCompress.GZipDecompressString(str);
        }
        #endregion
    }
}
