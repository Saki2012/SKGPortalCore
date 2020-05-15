using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.Libary;
using SKGPortalCore.Core.Model.User;
using System;

namespace SKGPortalCore.Core.Repository.Entity
{
    public class BasicRptRepository : IDisposable
    {
        #region Property
        /// <summary>
        /// 
        /// </summary>
        public IUserModel User { get; set; }
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
        #endregion

        #region Construct
        public BasicRptRepository(ApplicationDbContext dataAccess)
        {
            DataAccess = dataAccess as ApplicationDbContext;
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
        // ~BasicRptRepository()
        // {
        //   // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 加入這個程式碼的目的在正確實作可處置的模式。
        public void Dispose()
        {
            // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果上方的完成項已被覆寫，即取消下行的註解狀態。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
