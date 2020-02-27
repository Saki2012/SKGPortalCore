﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Model.System;

namespace SKGPortalCore.Repository
{
    public class BasicRepository<TSet> : IDisposable
    {
        #region Property
        /// <summary>
        /// 
        /// </summary>
        public IUserModel User { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private readonly DynamicReflection<TSet> SetReflect = new DynamicReflection<TSet>();
        /// <summary>
        /// 
        /// </summary>
        private readonly List<DynamicReflection> ModelReflects = new List<DynamicReflection>();
        /// <summary>
        /// 
        /// </summary>
        protected bool IsSetRefModel { get; set; } = false;//true?
        /// <summary>
        /// 
        /// </summary>
        public string ProgId
        {
            get
            {
                return ResxManage.GetProgId(this);
            }
        }
        #endregion
        #region SystemProperty
        /// <summary>
        /// 
        /// </summary>
        protected readonly ApplicationDbContext DataAccess;
        /// <summary>
        /// 
        /// </summary>
        private SysMessageLog _message;
        /// <summary>
        /// 
        /// </summary>
        public SysMessageLog Message
        {
            get
            {
                if (null == _message)
                {
                    _message = new SysMessageLog(User);
                }

                return _message;
            }
            set => _message = value;
        }
        /// <summary>
        /// 
        /// </summary>
        private DataFlowNo dataFlowNo;
        /// <summary>
        /// 
        /// </summary>
        public DataFlowNo DataFlowNo
        {
            get
            {
                if (null == dataFlowNo)
                {
                    dataFlowNo = DataAccess.Find<DataFlowNo>(ProgId);
                    if (null == dataFlowNo)
                    {
                        dataFlowNo = new DataFlowNo() { ProgId = ProgId, FlowDate = DateTime.Today, FlowNo = 0 };
                        DataAccess.Add(dataFlowNo);
                    }
                }
                else
                {
                    if (dataFlowNo.FlowDate != DateTime.Today)
                    {
                        dataFlowNo.FlowDate = DateTime.Today;
                        dataFlowNo.FlowNo = 0;
                    }
                    DataAccess.Update(dataFlowNo);
                }
                return dataFlowNo;
            }
            set => dataFlowNo = null;
        }
        /// <summary>
        /// 設置編碼規則
        /// </summary>
        protected Action<TSet> SetFlowNo { get; set; }
        /// <summary>
        /// 變更日誌系統
        /// </summary>
        private SysChangeLog SysChangeLog { get; }
        #endregion
        #region Construct
        public BasicRepository(ApplicationDbContext dataAccess)
        {
            DataAccess = dataAccess;
            foreach (var val in typeof(TSet).GetProperties())
            {
                Type[] args = val.PropertyType.GetGenericArguments();
                if (args.Length == 0)
                    ModelReflects.Add(new DynamicReflection(val.PropertyType));
                else
                    ModelReflects.Add(new DynamicReflection(args[0]));
            }
            SysChangeLog = new SysChangeLog(dataAccess, ModelReflects);
        }
        #endregion
        #region Public
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public virtual TSet Create(TSet set)
        {
            try
            {
                SetFlowNo?.Invoke(set);
                DoCreate(set);
                AfterSetEntity(set, FuncAction.Create);
                SysChangeLog.SaveChangeLog();
            }
            catch (Exception ex)
            {
                Message.AddExceptionError(ex);
            }
            return set;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public virtual TSet Update(TSet set)
        {
            try
            {
                dynamic masterData = SetReflect.GetValue(set, typeof(TSet).GetProperties()[0].Name);
                if (masterData is BasicDataModel) SetModifyInfo(masterData);
                DoUpdate(set);
                AfterSetEntity(set, FuncAction.Update);
                SysChangeLog.SaveChangeLog();
            }
            catch (Exception ex)
            {
                Message.AddExceptionError(ex);
            }
            return set;
        }
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="key"></param>
        public virtual void Delete(object[] key)
        {
            try
            {
                TSet set = QueryData(key);
                BeforeRemoveEntity(set);
                dynamic master = SetReflect.GetValue(set, typeof(TSet).GetProperties()[0].Name);
                DataAccess.Remove(master);
                SysChangeLog.RemoveChangeLog(master.InternalId);
                AfterRemoveEntity(set);
            }
            catch (Exception ex)
            {
                Message.AddExceptionError(ex);
            }
        }
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TSet QueryData(object[] key)
        {
            PropertyInfo[] setProperties = typeof(TSet).GetProperties();
            TSet instance = Activator.CreateInstance<TSet>();
            int setLen = 0;
            string pkCondition = string.Empty;
            foreach (PropertyInfo setProperty in setProperties)
            {
                if (typeof(IEnumerable).IsAssignableFrom(setProperty.PropertyType))
                {
                    Type modelType = setProperty.PropertyType.GetGenericArguments()[0];
                    Type tp = typeof(List<>).MakeGenericType(new[] { modelType });
                    IList list = (IList)Activator.CreateInstance(tp);
                    object dbSet = DataAccess.GetType().GetMethod("Set").MakeGenericMethod(modelType).Invoke(DataAccess, null);
                    IQueryable models = ((IQueryable)dbSet).Where(pkCondition, key);
                    foreach (object model in models)
                    {
                        SetRefModel(model);
                        list.Add(model);
                    }
                    SetReflect.SetValue(instance, setProperty.Name, list);
                }
                else
                {
                    pkCondition = GetPKCondition(GetKeyPropertiesByModelType(setProperty.PropertyType));
                    object model = DataAccess.Find(setProperty.PropertyType, key);
                    if (null == model) return default;
                    SetRefModel(model);
                    SetReflect.SetValue(instance, setProperty.Name, model);
                }
                setLen++;
            }
            return instance;
        }
        /// <summary>
        /// 查詢明細
        /// </summary>
        /// <returns></returns>
        public virtual IList QueryList(string condition)
        {
            Type masterType = typeof(TSet).GetProperties()[0].PropertyType;
            object dbSet = DataAccess.GetType().GetMethod("Set").MakeGenericMethod(masterType).Invoke(DataAccess, null);
            var query = ((IQueryable)dbSet).Where(LibData.Merge(" And ", false, "1=1", condition));
            var cast = typeof(Queryable).GetMethod("Cast").MakeGenericMethod(typeof(TSet).GetProperties()[0].PropertyType).Invoke(null, new object[] { query });
            var result = typeof(Enumerable).GetMethod("ToList").MakeGenericMethod(typeof(TSet).GetProperties()[0].PropertyType).Invoke(null, new object[] { cast }) as IList;
            return result;
        }
        /// <summary>.
        /// 審核
        /// </summary>
        /// <param name="key"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public virtual TSet Approve(object[] key, bool status)
        {
            TSet set = QueryData(key);
            try
            {
                dynamic masterData = SetReflect.GetValue(set, typeof(TSet).GetProperties()[0].Name);
                if (masterData is BasicDataModel)
                {
                    SetApproveInfo(masterData, status);
                    ((BasicDataModel)masterData).FormStatus = status ? FormStatus.Approved : FormStatus.Saved;
                }
                AfterApprove(set, status);
            }
            catch (Exception ex)
            {
                Message.AddExceptionError(ex);
            }
            return set;
        }
        /// <summary>
        /// 作廢
        /// </summary>
        public virtual TSet Invalid(object[] key, bool status)
        {
            TSet set = QueryData(key);
            try
            {
                dynamic masterData = SetReflect.GetValue(set, typeof(TSet).GetProperties()[0].Name);
                if (masterData is BasicDataModel)
                {
                    SetInvalidInfo(masterData, status);
                    ((BasicDataModel)masterData).FormStatus = status ? FormStatus.Obsoleted : FormStatus.Saved;
                }
                AfterInvalid(set, status);
            }
            catch (Exception ex)
            {
                Message.AddExceptionError(ex);
            }
            return set;
        }
        /// <summary>
        /// 結案
        /// </summary>
        public virtual TSet EndCase(object[] key, bool status)
        {
            TSet set = QueryData(key);
            try
            {
                dynamic masterData = SetReflect.GetValue(set, typeof(TSet).GetProperties()[0].Name);
                if (masterData is BasicDataModel)
                {
                    SetEndCaseInfo(masterData, status);
                    ((BasicDataModel)masterData).FormStatus = status ? FormStatus.EndCase : FormStatus.Approved;
                }
                AfterEndCase(set, status);
            }
            catch (Exception ex)
            {
                Message.AddExceptionError(ex);
            }
            return set;
        }
        /// <summary>
        /// 執行更新
        /// </summary>
        /// <param name="action"></param>
        public virtual void CommitData(FuncAction action)
        {
            try
            {
                if (Message.Errors.Count > 0) { return; }
                DataAccess.BulkSaveChanges();
                AfterSaveChanges(action);
            }
            catch (Exception ex)
            {
                Message.AddExceptionError(ex);
            }
            finally
            {
                ErrorRollbackEntities();
            }
        }
        /// <summary>
        /// 獲取表頭主鍵
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public object[] GetPKVals(TSet set)
        {
            foreach (dynamic s in set.GetType().GetProperties())
            {
                dynamic val = SetReflect.GetValue(set, s.Name);
                if (null == val) continue;
                if (val is BasicDataModel) return GetKeyVals(val);
            }
            return null;
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
        protected virtual void AfterRemoveEntity(TSet set) { }
        /// <summary>
        /// 執行SaveChanges後
        /// </summary>
        /// <param name="set"></param>
        protected virtual void AfterSaveChanges(FuncAction action) { }
        /// <summary>
        /// 審核後
        /// </summary>
        /// <param name="set"></param>
        /// <param name="status"></param>
        protected virtual void AfterApprove(TSet set, bool status) { }
        /// <summary>
        /// 作廢後
        /// </summary>
        /// <param name="set"></param>
        /// <param name="status"></param>
        protected virtual void AfterInvalid(TSet set, bool status) { }
        /// <summary>
        /// 結案後
        /// </summary>
        /// <param name="set"></param>
        /// <param name="status"></param>
        protected virtual void AfterEndCase(TSet set, bool status) { }
        #endregion
        #region Private
        /// <summary>
        /// 處理新增動作
        /// </summary>
        /// <param name="set"></param>
        private void DoCreate(TSet set)
        {
            int tbIdx = 0;
            foreach (PropertyInfo props in set.GetType().GetProperties())
            {
                dynamic entity = SetReflect.GetValue(set, props.Name);
                if (null == entity) { tbIdx++; continue; }
                if (entity is IEnumerable)
                {
                    foreach (dynamic ety in entity)
                    {
                        DataAccess.Add(ety);
                        SetRefModel(ety);
                        SysChangeLog.SetChangeLogDetail(tbIdx, null, ety, RowState.Insert);
                    }
                }
                else
                {
                    if (entity is BasicDataModel) SetCreateInfo(entity);
                    DataAccess.Add(entity);
                    SetRefModel(entity);
                    SysChangeLog.SetChangeLog(ProgId, entity.InternalId, User.KeyId);
                    SysChangeLog.SetChangeLogDetail(tbIdx, null, entity, RowState.Insert);
                }
                tbIdx++;
            }
        }
        /// <summary>
        /// 處理修改動作
        /// </summary>
        /// <param name="set"></param>
        private void DoUpdate(TSet set)
        {
            int tbIdx = 0;
            object[] keys = null;
            foreach (PropertyInfo props in set.GetType().GetProperties())
            {
                dynamic val = SetReflect.GetValue(set, props.Name);
                if (null == val) { tbIdx++; continue; }
                if (val is IEnumerable<DetailRowState>)
                {
                    List<DetailRowState> detail = Enumerable.ToList((IEnumerable<DetailRowState>)val);
                    List<DetailRowState> insertList = detail.Where(p => p.RowState == RowState.Insert).ToList();
                    foreach (dynamic entity in insertList)
                    {
                        DataAccess.Add(entity);
                        SetRefModel(entity);
                        SysChangeLog.SetChangeLogDetail(tbIdx, null, entity, RowState.Insert);
                    }
                    List<DetailRowState> updateList = detail.Where(p => p.RowState == RowState.Update).ToList();
                    foreach (dynamic entity in updateList)
                    {
                        dynamic oldEntity = DataAccess.Find(entity.GetType(), GetKeyVals(entity));
                        if (null != oldEntity) DataAccess.Remove(oldEntity);
                        if (DataAccess.Entry(entity).State != EntityState.Detached) DataAccess.Remove(entity);
                        DataAccess.Update(entity);
                        SetRefModel(entity);
                        SysChangeLog.SetChangeLogDetail(tbIdx, oldEntity, entity, RowState.Update);
                    }
                    List<DetailRowState> deleteList = detail.Where(p => p.RowState == RowState.Delete).ToList();
                    foreach (dynamic entity in deleteList)
                    {
                        dynamic oldEntity = DataAccess.Find(entity.GetType(), GetKeyVals(entity));
                        if (null != oldEntity) DataAccess.Remove(oldEntity);
                        SysChangeLog.SetChangeLogDetail(tbIdx, oldEntity, null, RowState.Delete);
                    }
                }
                else
                {
                    keys = GetKeyVals(val);
                    dynamic oldEntity = DataAccess.Find(val.GetType(), keys);
                    if (null != oldEntity) DataAccess.Remove(oldEntity);
                    DataAccess.Update(val);
                    SysChangeLog.SetChangeLog(ProgId, val.InternalId, User.KeyId);
                    SysChangeLog.SetChangeLogDetail(tbIdx, null, val, RowState.Insert);
                    SetRefModel(val);
                }
                tbIdx++;
            }

        }
        /// <summary>
        /// 設置新增時資料
        /// </summary>
        /// <param name="model"></param>
        private void SetCreateInfo(BasicDataModel model)
        {
            DateTime now = DateTime.Now;
            model.CreateStaff = User.KeyId;
            model.CreateTime = now;
            model.ModifyStaff = User.KeyId;
            model.ModifyTime = now;
            model.InternalId = Guid.NewGuid().ToString();
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
            //return;//todo:設置關聯欄位讀取
            if (!IsSetRefModel) return;
            if (null == model) return;
            IEnumerable<ReferenceEntry> refs = DataAccess.Entry(model).References;
            foreach (ReferenceEntry refE in refs)
            {
                if (!refE.IsLoaded) refE.Load();
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
            {
                result = LibData.Merge(" And ", false, result, $"{keysInfo[i].Name}=@{i}");
            }

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
            foreach (PropertyInfo prop in props)
                result.Add(ModelReflects[0].GetValue(model, prop.Name));
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
        /// <summary>
        /// 異常時回滾變更的Entities
        /// </summary>
        private void ErrorRollbackEntities()
        {
            if (Message.Errors.Count > 0)
            {
                EntityEntry[] entitys = DataAccess.ChangeTracker.Entries().ToArray();
                foreach (EntityEntry entity in entitys)
                    entity.State = EntityState.Detached;
                entitys = DataAccess.ChangeTracker.Entries().ToArray();
                foreach (EntityEntry entity in entitys)
                    entity.State = EntityState.Unchanged;
            }
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
