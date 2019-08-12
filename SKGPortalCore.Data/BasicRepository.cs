using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData.OperateSystem;

namespace SKGPortalCore.Data
{
    public class BasicRepository<TSet> : IDisposable
    {
        #region Property
        protected readonly DbContext DataAccess;
        public IUserModel User { get; set; }
        private readonly DynamicReflection<TSet> Reflect = new DynamicReflection<TSet>();
        #endregion
        #region Construct
        public BasicRepository(DbContext database)
        {
            DataAccess = database;
        }
        #endregion
        #region Public
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public TSet Create(TSet set)
        {
            try
            {
                dynamic masterData = set.GetType().GetProperties()[0].GetValue(set);
                if (masterData is BasicDataModel) SetCreateInfo(masterData);
                BeforeSetEntity(set);
                foreach (var s in set.GetType().GetProperties())
                {
                    dynamic val = Reflect.GetValue(set, s.Name);
                    if (null == val) continue;
                    if (val is IEnumerable) { DataAccess.AddRange(val); }
                    else { DataAccess.Add(val); }
                }
                AfterSetEntity(set);
                return set;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public TSet Update(TSet set)
        {
            try
            {
                dynamic masterData = set.GetType().GetProperties()[0].GetValue(set);
                if (masterData is BasicDataModel) SetModifyInfo(masterData);
                BeforeSetEntity(set);
                foreach (var s in set.GetType().GetProperties())
                {
                    dynamic val = Reflect.GetValue(set, s.Name);
                    if (val is IEnumerable) { DataAccess.Update(val); }
                    else
                    {
                        if (val is DetailRowState)
                        {
                            List<DetailRowState> c = val;
                            DataAccess.AddRange(c.Where(p => p.RowState == RowState.Insert));
                            DataAccess.UpdateRange(c.Where(p => p.RowState == RowState.Update));
                            DataAccess.RemoveRange(c.Where(p => p.RowState == RowState.Delete));
                        }
                    }
                }
                AfterSetEntity(set);
                return set;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="key"></param>
        public void Delete(string key)
        {
            Delete(new[] { key });
        }
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="key"></param>
        public void Delete(object[] key)
        {
            try
            {
                BeforeRemoveEntity(default);
                DataAccess.Remove(DataAccess.Find(typeof(TSet).GetProperties()[0].PropertyType, key));
                AfterRemoveEntity(default);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TSet QueryData(string key)
        {
            return QueryData(new[] { key });
        }
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TSet QueryData(object[] key)
        {
            var setProperties = typeof(TSet).GetProperties();
            var instance = Activator.CreateInstance<TSet>();
            int setLen = 0;
            string pkCondition = string.Empty;
            foreach (var setProperty in setProperties)
            {
                if (typeof(IEnumerable).IsAssignableFrom(setProperty.PropertyType))
                {
                    Type modelType = setProperty.PropertyType.GetGenericArguments()[0];
                    var dbSet = DataAccess.GetType().GetMethod("Set").MakeGenericMethod(modelType).Invoke(DataAccess, null);
                    var models = ((IQueryable)dbSet).Where(pkCondition, key);
                    foreach (var model in models)
                    {
                        SetRefModel(model);
                    }
                    Type listType = typeof(List<>).MakeGenericType(new[] { modelType });
                    IList list = (IList)Activator.CreateInstance(listType, models);
                    instance.GetType().GetProperties()[setLen].SetValue(instance, list);
                }
                else
                {
                    pkCondition = GetPKCondition(GetKeyPropertiesByModelType(setProperty.PropertyType));
                    var model = DataAccess.Find(setProperty.PropertyType, key);
                    if (null == model) return default;
                    SetRefModel(model);
                    instance.GetType().GetProperties()[setLen].SetValue(instance, model);
                }
                setLen++;
            }
            return instance;
        }
        /// <summary>
        /// 查詢明細
        /// </summary>
        /// <returns></returns>
        public List<object> QueryList()
        {


            return new List<object>();
        }
        /// <summary>
        /// 作廢
        /// </summary>
        public TSet Invalid(object[] key,bool Status)
        {
            return default;
        }


        /// <summary>
        /// 執行更新
        /// </summary>
        /// <param name="action"></param>
        public void CommitData(FuncAction action)
        {
            DataAccess.BulkSaveChanges();
            AfterSaveChanges(action);
        }
        #endregion
        #region Protected
        /// <summary>
        /// 進Entity前
        /// </summary>
        /// <param name="set"></param>
        protected virtual void BeforeSetEntity(TSet set)
        {
            DateTime now = DateTime.Now;
            foreach (var p in set.GetType().GetProperties())
            {
                if (p.GetValue(set) is BasicDataModel basic)
                {
                    basic.CreateStaff = User.KeyId;
                    basic.ModifyStaff = User.KeyId;
                    basic.CreateTime = now;
                    basic.ModifyTime = now;
                }
            }
        }
        /// <summary>
        /// 進Entity後
        /// </summary>
        /// <param name="set"></param>
        protected virtual void AfterSetEntity(TSet set)
        {

        }
        /// <summary>
        /// 移除Entity前
        /// </summary>
        /// <param name="set"></param>
        protected virtual void BeforeRemoveEntity(TSet set) { }
        /// <summary>
        /// 移除Entity後
        /// </summary>
        /// <param name="set"></param>
        protected virtual void AfterRemoveEntity(TSet set)
        {

        }
        /// <summary>
        /// 執行SaveChanges後
        /// </summary>
        /// <param name="set"></param>
        protected virtual void AfterSaveChanges(FuncAction action)
        {


        }
        #endregion
        #region Private
        /// <summary>
        /// 設置新增時使用者資料
        /// </summary>
        /// <param name="model"></param>
        private void SetCreateInfo(BasicDataModel model)
        {
            DateTime now = DateTime.Now;
            model.CreateStaff = User.KeyId;
            model.CreateTime = now;
            model.ModifyStaff = User.KeyId;
            model.ModifyTime = now;
        }
        /// <summary>
        /// 設置修改時使用者資料
        /// </summary>
        /// <param name="model"></param>
        private void SetModifyInfo(BasicDataModel model)
        {
            DateTime now = DateTime.Now;
            model.ModifyStaff = User.KeyId;
            model.ModifyTime = now;
        }
        /// <summary>
        /// 設置作廢時使用者資料
        /// </summary>
        /// <param name="model"></param>
        /// <param name="status"></param>
        private void SetInvalidInfo(BasicDataModel model, bool status)
        {
            if (status)
            {

                model.ModifyStaff = User.KeyId;
                model.ModifyTime = DateTime.Now;
            }
            else
            {
                model.ModifyStaff = null;
                model.ModifyTime = DateTime.MinValue;
            }
        }
        /// <summary>
        /// 設置審核時使用者資料
        /// </summary>
        /// <param name="model"></param>
        /// <param name="status"></param>
        private void SetApproveInfo(BasicDataModel model, bool status)
        {
            if (status)
            {

                model.ModifyStaff = User.KeyId;
                model.ModifyTime = DateTime.Now;
            }
            else
            {
                model.ModifyStaff = null;
                model.ModifyTime = DateTime.MinValue;
            }
        }
        /// <summary>
        /// 讀取關聯表值
        /// </summary>
        /// <param name="_DB"></param>
        /// <param name="model"></param>
        /// <param name="fieldsProp"></param>
        private void SetRefModel(object model)
        {
            if (null == model) return;
            IEnumerable<ReferenceEntry> refs = DataAccess.Entry(model).References;
            foreach (ReferenceEntry refE in refs)
            {
                refE.Load();
            }
        }
        /// <summary>
        /// 獲取主鍵值條件
        /// </summary>
        /// <param name="keysInfo"></param>
        /// <returns></returns>
        private static string GetPKCondition(PropertyInfo[] keysInfo)
        {
            string result = string.Empty;
            int len = keysInfo.Length;
            for (int i = 0; i < len; i++)
                result = DataHelper.Merge(" And ", false, result, $"{keysInfo[i].Name}=@{i}");
            return result;
        }
        /// <summary>
        /// 獲取表的主鍵欄位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static PropertyInfo[] GetKeyPropertiesByModelType(Type modelType)
        {
            return modelType.GetProperties().Where(prop => prop.GetCustomAttributes<KeyAttribute>(true).Count() > 0).ToArray();
        }
        #endregion
        #region IDisposable Support
        private bool disposedValue = false; // 偵測多餘的呼叫

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 處置 Managed 狀態 (Managed 物件)。
                }

                // TODO: 釋放 Unmanaged 資源 (Unmanaged 物件) 並覆寫下方的完成項。
                // TODO: 將大型欄位設為 null。

                disposedValue = true;
            }
        }

        // TODO: 僅當上方的 Dispose(bool disposing) 具有會釋放 Unmanaged 資源的程式碼時，才覆寫完成項。
        // ~BasicRepository()
        // {
        //   // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 加入這個程式碼的目的在正確實作可處置的模式。
        void IDisposable.Dispose()
        {
            // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果上方的完成項已被覆寫，即取消下行的註解狀態。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
