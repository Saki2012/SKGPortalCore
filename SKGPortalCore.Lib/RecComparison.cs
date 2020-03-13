using System;
using System.Collections;
using System.Collections.Generic;

namespace SKGPortalCore.Lib
{
    /// <summary>
    /// 快速比對
    /// 註1:比對之鍵值不應進行重新賦值動作
    /// 註2:若比對效率過慢時，請重寫Comparer Delegate
    /// 註3:多對多時，請使用BackToBookMark、SetBookMark等兩個Function
    ///     ex：
    ///     if (rc.Enable)
    ///      while (!rc.IsEof)
    ///      {
    ///         rc.BackToBookMark();
    ///         while (rc.Compare())
    ///         {
    ///          rc.SetBookMark();
    ///          //do something...
    ///          rc.DetailMoveNext();
    ///         }
    ///         rc.MoveNext();
    ///      }
    /// </summary>
    /// <typeparam name="T1">Model1</typeparam>
    /// <typeparam name="T2">Model2</typeparam>
    public class RecComparison<T1, T2>
    {
        #region Property
        /// <summary>
        /// 主表
        /// </summary>
        public List<T1> Master { get; }
        /// <summary>
        /// 子表
        /// </summary>
        public List<T2> Detail { get; }
        /// <summary>
        /// 主表當前行項
        /// </summary>
        public T1 CurrentRow => Master[CurrentIdx];
        /// <summary>
        /// 子表當前行項
        /// </summary>
        public T2 DetailRow => Detail[DetailIdx];
        /// <summary>
        /// 主表Index
        /// </summary>
        public int CurrentIdx { get; private set; }
        /// <summary>
        /// 子表Index
        /// </summary>
        public int DetailIdx { get; private set; }
        /// <summary>
        /// 比對規則
        /// </summary>
        /// <param name="m"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public Func<T1, T2, int> CompareFunc;
        /// <summary>
        /// 是否到尾
        /// </summary>
        public bool IsEof => Master?.Count == CurrentIdx;
        /// <summary>
        /// 是否可比對
        /// </summary>
        public bool Enable => Master != null && Detail != null;
        /// <summary>
        /// 子表Index標籤
        /// </summary>
        private int BookMark = -1;
        /// <summary>
        /// 主表主鍵欄位
        /// </summary>
        private string[] MasterFields { get; set; }
        /// <summary>
        /// 子表主鍵欄位
        /// </summary>
        private string[] DetailFields { get; set; }
        /// <summary>
        /// 主表動態反射
        /// </summary>
        private readonly DynamicReflection<T1> T1Reflec = new DynamicReflection<T1>();
        /// <summary>
        /// 子表動態反射
        /// </summary>
        private readonly DynamicReflection<T2> T2Reflec = new DynamicReflection<T2>();
        #endregion
        #region Construct
        /// <summary>
        /// 快速比對
        /// </summary>
        /// <param name="master">主表</param>
        /// <param name="detail">子表</param>
        /// <param name="masterFields">主表主鍵(,分開)</param>
        /// <param name="detailFields">子表主鍵(,分開)</param>
        public RecComparison(List<T1> master, List<T2> detail, string masterFields = "", string detailFields = "") : this(master, detail, masterFields.Split(','), detailFields.Split(','))
        {
        }
        /// <summary>
        /// 快速比對
        /// </summary>
        /// <param name="master">主表</param>
        /// <param name="detail">子表</param>
        /// <param name="masterFields">主表主鍵</param>
        /// <param name="detailFields">子表主鍵</param>
        public RecComparison(List<T1> master, List<T2> detail, string[] masterFields, string[] detailFields)
        {
            Master = master;
            Detail = detail;
            MasterFields = masterFields;
            DetailFields = detailFields;
            CurrentIdx = 0;
            DetailIdx = 0;
            if (!MasterFields[0].IsNullOrEmpty())
            {
                SortMaster();
                SortDetail();
                SetDefaultComparer();
            }
        }
        #endregion
        #region Public
        /// <summary>
        /// 主表往下一行
        /// </summary>
        public void MoveNext()
        {
            CurrentIdx++;
        }
        /// <summary>
        /// 子表往下一行
        /// </summary>
        public void DetailMoveNext()
        {
            DetailIdx++;
        }
        /// <summary>
        /// 子表往回至BookMark行項
        /// </summary>
        public void BackToBookMark()
        {
            if (BookMark != -1)
            {
                DetailIdx = BookMark;
                BookMark = -1;
            }
        }
        /// <summary>
        /// 設置子表之BookMark
        /// </summary>
        public void SetBookMark()
        {
            if (BookMark == -1)
            {
                BookMark = DetailIdx;
            }
        }
        /// <summary>
        /// 比對
        /// </summary>
        /// <returns></returns>
        public bool Compare()
        {
            if (Detail?.Count == DetailIdx)
            {
                return false;
            }

            while (CompareFunc(CurrentRow, DetailRow) > 0)
            {
                DetailMoveNext();
                if (Detail?.Count == DetailIdx)
                {
                    return false;
                }
            }
            return CompareFunc(CurrentRow, DetailRow) >= 0;
        }
        #endregion
        #region Private
        /// <summary>
        /// 主表排序
        /// </summary>
        private void SortMaster()
        {
            Comparison<T1> c = new Comparison<T1>((x, y) =>
            {
                int result = 0;
                foreach (string field in MasterFields)
                {
                    if (result == 0)
                    {
                        result = T1Reflec.GetValue(x, field).ToString().CompareTo(T1Reflec.GetValue(y, field).ToString());
                    }
                    else
                    {
                        return result;
                    }
                }
                return result;
            });
            Master.Sort(c);
        }
        /// <summary>
        /// 子表排序
        /// </summary>
        private void SortDetail()
        {
            Comparison<T2> c = new Comparison<T2>((x, y) =>
            {
                int result = 0;
                foreach (string field in MasterFields)
                {
                    if (result == 0)
                    {
                        result = T2Reflec.GetValue(x, field).ToString().CompareTo(T2Reflec.GetValue(y, field).ToString());
                    }
                    else
                    {
                        return result;
                    }
                }
                return result;
            });
            Detail.Sort(c);
        }
        /// <summary>
        /// 默認比對規則
        /// </summary>
        private void SetDefaultComparer()
        {
            CompareFunc = new Func<T1, T2, int>((x, y) =>
            {
                int result = 0;
                int len = MasterFields.Length;
                for (int i = 0; i < len; i++)
                {
                    if (result == 0)
                    {
                        result = T1Reflec.GetValue(x, MasterFields[i]).ToString().CompareTo(T2Reflec.GetValue(y, DetailFields[i]).ToString());
                    }
                    else
                    {
                        return result;
                    }
                }
                return result;
            });
        }
        #endregion
    }

    public class RecComparison
    {
        #region Property
        /// <summary>
        /// 主表
        /// </summary>
        public dynamic Master { get; }
        /// <summary>
        /// 子表
        /// </summary>
        public dynamic Detail { get; }
        /// <summary>
        /// 主表當前行項
        /// </summary>
        public dynamic CurrentRow => Master[CurrentIdx];
        /// <summary>
        /// 子表當前行項
        /// </summary>
        public dynamic DetailRow => Detail[DetailIdx];
        /// <summary>
        /// 主表Index
        /// </summary>
        public int CurrentIdx { get; private set; }
        /// <summary>
        /// 子表Index
        /// </summary>
        public int DetailIdx { get; private set; }
        /// <summary>
        /// 比對規則
        /// </summary>
        /// <param name="m"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public Func<dynamic, dynamic, int> CompareFunc;
        /// <summary>
        /// 是否到尾
        /// </summary>
        public bool IsEof => Master?.Count == CurrentIdx;
        /// <summary>
        /// 是否可比對
        /// </summary>
        public bool Enable => Master != null && Detail != null;
        /// <summary>
        /// 子表Index標籤
        /// </summary>
        private int BookMark = -1;
        /// <summary>
        /// 主表主鍵欄位
        /// </summary>
        private string[] MasterFields { get; set; }
        /// <summary>
        /// 子表主鍵欄位
        /// </summary>
        private string[] DetailFields { get; set; }
        /// <summary>
        /// 主表動態反射
        /// </summary>
        public readonly DynamicReflection T1Reflec;
        /// <summary>
        /// 子表動態反射
        /// </summary>
        public readonly DynamicReflection T2Reflec;
        #endregion
        #region Construct
        /// <summary>
        /// 快速比對
        /// </summary>
        /// <param name="master">主表</param>
        /// <param name="detail">子表</param>
        /// <param name="masterFields">主表主鍵</param>
        /// <param name="detailFields">子表主鍵</param>
        public RecComparison(dynamic master, dynamic detail, DynamicReflection t1Reflec, DynamicReflection t2Reflec, string[] masterFields, string[] detailFields)
        {
            Master = master;
            Detail = detail;
            T1Reflec = t1Reflec;
            T2Reflec = t2Reflec;
            MasterFields = masterFields;
            DetailFields = detailFields;
            CurrentIdx = 0;
            DetailIdx = 0;
            if (!MasterFields[0].IsNullOrEmpty())
            {
                SortMaster();
                SortDetail();
                SetDefaultComparer();
            }
        }
        #endregion
        #region Public
        /// <summary>
        /// 主表往下一行
        /// </summary>
        public void MoveNext()
        {
            CurrentIdx++;
        }
        /// <summary>
        /// 子表往下一行
        /// </summary>
        public void DetailMoveNext()
        {
            DetailIdx++;
        }
        /// <summary>
        /// 子表往回至BookMark行項
        /// </summary>
        public void BackToBookMark()
        {
            if (BookMark != -1)
            {
                DetailIdx = BookMark;
                BookMark = -1;
            }
        }
        /// <summary>
        /// 設置子表之BookMark
        /// </summary>
        public void SetBookMark()
        {
            if (BookMark == -1)
            {
                BookMark = DetailIdx;
            }
        }
        /// <summary>
        /// 比對
        /// </summary>
        /// <returns></returns>
        public bool Compare()
        {
            if (Detail?.Count == DetailIdx)
            {
                return false;
            }
            while (CompareFunc(CurrentRow, DetailRow) > 0)
            {
                DetailMoveNext();
                if (Detail?.Count == DetailIdx)
                {
                    return false;
                }
            }
            return CompareFunc(CurrentRow, DetailRow) >= 0;
        }
        #endregion
        #region Private
        /// <summary>
        /// 主表排序
        /// </summary>
        private void SortMaster()
        {
            Comparison<dynamic> c = new Comparison<dynamic>((x, y) =>
            {
                int result = 0;
                foreach (string field in MasterFields)
                {
                    if (result == 0)
                    {
                        result = T1Reflec.GetValue(x, field).ToString().CompareTo(T1Reflec.GetValue(y, field).ToString());
                    }
                    else
                    {
                        return result;
                    }
                }
                return result;
            });
            Master.Sort(c);
        }
        /// <summary>
        /// 子表排序
        /// </summary>
        private void SortDetail()
        {
            Comparison<dynamic> c = new Comparison<dynamic>((x, y) =>
            {
                int result = 0;
                foreach (string field in MasterFields)
                {
                    if (result == 0)
                    {
                        result = T2Reflec.GetValue(x, field).ToString().CompareTo(T2Reflec.GetValue(y, field).ToString());
                    }
                    else
                    {
                        return result;
                    }
                }
                return result;
            });
            Detail.Sort(c);
        }
        /// <summary>
        /// 默認比對規則
        /// </summary>
        private void SetDefaultComparer()
        {
            CompareFunc = new Func<dynamic, dynamic, int>((x, y) =>
            {
                int result = 0;
                int len = MasterFields.Length;
                for (int i = 0; i < len; i++)
                {
                    if (result == 0)
                    {
                        result = T1Reflec.GetValue(x, MasterFields[i]).ToString().CompareTo(T2Reflec.GetValue(y, DetailFields[i]).ToString());
                    }
                    else
                    {
                        return result;
                    }
                }
                return result;
            });
        }
        #endregion
    }
}
