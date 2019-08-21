using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using GraphQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData.OperateSystem;

namespace SKGPortalCore.Repository
{
    public class BasicRepository<TSet> : IDisposable
    {
        #region Property
        protected readonly ApplicationDbContext DataAccess;
        public IUserModel User { get; set; }
        private readonly DynamicReflection<TSet> Reflect = new DynamicReflection<TSet>();
        protected MessageLog Message = new MessageLog(new ExecutionErrors());
        #endregion
        #region Construct
        public BasicRepository(ApplicationDbContext dataAccess)
        {
            DataAccess = dataAccess;
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
                dynamic masterData = Reflect.GetValue(set, typeof(TSet).GetProperties()[0].Name);
                if (masterData is BasicDataModel) SetCreateInfo(masterData);
                DoCreate(set);
                AfterSetEntity(set, FuncAction.Create);
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
                dynamic masterData = Reflect.GetValue(set, typeof(TSet).GetProperties()[0].Name);
                if (masterData is BasicDataModel) SetModifyInfo(masterData);
                DoUpdate(set);
                AfterSetEntity(set, FuncAction.Update);
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
        public void Delete(object[] key)
        {
            try
            {
                TSet set = QueryData(key);
                BeforeRemoveEntity(set);
                DataAccess.Remove(Reflect.GetValue(set, typeof(TSet).GetProperties()[0].Name));
                AfterRemoveEntity();
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
        /// 審核
        /// </summary>
        /// <param name="key"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public TSet Approve(object[] key, bool status)
        {
            TSet set = QueryData(key);
            dynamic masterData = Reflect.GetValue(set, typeof(TSet).GetProperties()[0].Name);
            if (masterData is BasicDataModel)
            {
                SetApproveInfo(masterData, status);
                ((BasicDataModel)masterData).FormStatus = status ? FormStatus.Approved : FormStatus.Saved;
            }
            return set;
        }
        /// <summary>
        /// 作廢
        /// </summary>
        public TSet Invalid(object[] key, bool status)
        {
            TSet set = QueryData(key);
            dynamic masterData = Reflect.GetValue(set, typeof(TSet).GetProperties()[0].Name);
            if (masterData is BasicDataModel)
            {
                SetInvalidInfo(masterData, status);
                ((BasicDataModel)masterData).FormStatus = status ? FormStatus.Obsoleted : FormStatus.Saved;
            }
            return set;
        }
        /// <summary>
        /// 結案
        /// </summary>
        public TSet EndCase(object[] key, bool status)
        {
            TSet set = QueryData(key);
            dynamic masterData = Reflect.GetValue(set, typeof(TSet).GetProperties()[0].Name);
            if (masterData is BasicDataModel)
            {
                SetEndCaseInfo(masterData, status);
                ((BasicDataModel)masterData).FormStatus = status ? FormStatus.EndCase : FormStatus.Approved;
            }
            return set;
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
        /// 進Entity後
        /// </summary>
        /// <param name="set"></param>
        protected virtual void AfterSetEntity(TSet set, FuncAction action) { }
        /// <summary>
        /// 移除Entity前
        /// </summary>
        /// <param name="set"></param>
        protected virtual void BeforeRemoveEntity(TSet set) { }
        /// <summary>
        /// 移除Entity後
        /// </summary>
        /// <param name="set"></param>
        protected virtual void AfterRemoveEntity() { }
        /// <summary>
        /// 執行SaveChanges後
        /// </summary>
        /// <param name="set"></param>
        protected virtual void AfterSaveChanges(FuncAction action) { }
        #endregion
        #region Private
        /// <summary>
        /// 處理新增動作
        /// </summary>
        /// <param name="set"></param>
        private void DoCreate(TSet set)
        {
            foreach (var props in set.GetType().GetProperties())
            {
                dynamic entity = Reflect.GetValue(set, props.Name);
                if (null == entity) continue;
                if (entity is IEnumerable) { DataAccess.AddRange(entity); }
                else { DataAccess.Add(entity); }
            }
        }
        /// <summary>
        /// 處理修改動作
        /// </summary>
        /// <param name="set"></param>
        private void DoUpdate(TSet set)
        {
            foreach (var s in set.GetType().GetProperties())
            {
                var val = Reflect.GetValue(set, s.Name);
                if (null == val) continue;
                if (val is IEnumerable<DetailRowState>)
                {
                    List<DetailRowState> detail = Enumerable.ToList((IEnumerable<DetailRowState>)val);

                    List<DetailRowState> insertList = detail.Where(p => p.RowState == RowState.Insert).ToList();
                    DataAccess.AddRange(insertList);

                    List<DetailRowState> updateList = detail.Where(p => p.RowState == RowState.Update).ToList();
                    foreach (var entity in updateList)
                    {
                        var oldEntity = DataAccess.Find(entity.GetType(), GetKeyVals(entity));
                        if (null != oldEntity)
                            DataAccess.Remove(oldEntity);
                        DataAccess.Update(entity);
                    }
                    List<DetailRowState> deleteList = detail.Where(p => p.RowState == RowState.Delete).ToList();
                    foreach (var entity in deleteList)
                    {
                        var oldEntity = DataAccess.Find(entity.GetType(), GetKeyVals(entity));
                        if (null != oldEntity)
                            DataAccess.Remove(oldEntity);
                    }
                }
                else
                {
                    var oldEntity = DataAccess.Find(val.GetType(), GetKeyVals(val));
                    if (null != oldEntity)
                        DataAccess.Remove(oldEntity);
                    DataAccess.Update(val);
                }
            }
        }
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
                model.InvalidStaff = User.KeyId;
                model.InvalidTime = DateTime.Now;
            }
            else
            {
                model.InvalidStaff = null;
                model.InvalidTime = DateTime.MinValue;
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
                model.ApproveStaff = User.KeyId;
                model.ApproveTime = DateTime.Now;
            }
            else
            {
                model.ApproveStaff = null;
                model.ApproveTime = DateTime.MinValue;
            }
        }
        /// <summary>
        /// 設置結案時使用者資料
        /// </summary>
        /// <param name="model"></param>
        /// <param name="status"></param>
        private void SetEndCaseInfo(BasicDataModel model, bool status)
        {
            if (status)
            {
                model.EndCaseStaff = User.KeyId;
                model.EndCaseTime = DateTime.Now;
            }
            else
            {
                model.EndCaseStaff = null;
                model.EndCaseTime = DateTime.MinValue;
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
        private string GetPKCondition(PropertyInfo[] keysInfo)
        {
            string result = string.Empty;
            int len = keysInfo.Length;
            for (int i = 0; i < len; i++)
                result = LibData.Merge(" And ", false, result, $"{keysInfo[i].Name}=@{i}");
            return result;
        }
        /// <summary>
        /// 獲取表主鍵值
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private object[] GetKeyVals(object model)
        {
            PropertyInfo[] props = GetKeyPropertiesByModelType(model.GetType());
            List<object> result = new List<object>();
            foreach (var prop in props)
            {
                result.Add(prop.GetValue(model));
            }
            return result.ToArray();
        }
        /// <summary>
        /// 獲取表的主鍵欄位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private PropertyInfo[] GetKeyPropertiesByModelType(Type modelType)
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
