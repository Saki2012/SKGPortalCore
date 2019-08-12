using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData.OperateSystem;

namespace SKGPortalCore.Data
{
    public class BasicRepository<TSet> : IDisposable
    {
        #region Property
        protected readonly DbContext Database;
        protected IUserModel User { get; }
        private readonly DynamicReflection<TSet> Reflect = new DynamicReflection<TSet>();
        #endregion
        #region Construct
        public BasicRepository(DbContext database)
        {
            Database = database;
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
                    if (val is IEnumerable) { Database.Add(val); }
                    else { Database.AddRange(val); }
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
                    if (val is IEnumerable) { Database.Update(val); }
                    else
                    {
                        if (val is DetailRowState)
                        {
                            List<DetailRowState> c = val;
                            Database.AddRange(c.Where(p => p.RowState == RowState.Insert));
                            Database.UpdateRange(c.Where(p => p.RowState == RowState.Update));
                            Database.RemoveRange(c.Where(p => p.RowState == RowState.Delete));
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
                Database.Remove(Database.Find(typeof(TSet).GetProperties()[0].PropertyType, key));
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
            //Condition + User's Data
            return default;
        }
        /// <summary>
        /// 執行更新
        /// </summary>
        /// <param name="action"></param>
        public void CommitData(FuncAction action)
        {
            Database.BulkSaveChanges();
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
