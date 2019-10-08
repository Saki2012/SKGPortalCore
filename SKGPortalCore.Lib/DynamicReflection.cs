using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SKGPortalCore.Lib
{
    /// <summary>
    /// 利用Expression進行反射獲取資料
    /// </summary>
    public class DynamicReflection : IMemberAccessor
    {
        #region Internal
        internal Func<object, string, object> GetValueDelegate;
        internal Action<object, string, object> SetValueDelegate;
        #endregion

        #region Constructor
        public DynamicReflection(object instance)
        {
            new DynamicReflection(instance.GetType());
        }
        public DynamicReflection(Type type)
        {
            GetValueDelegate = GenerateGetValue(type);
            SetValueDelegate = GenerateSetValue(type);
        }
        #endregion

        #region Private
        private Func<object, string, object> GenerateGetValue(Type type)
        {
            ParameterExpression instance = Expression.Parameter(typeof(object), "instance");
            ParameterExpression memberName = Expression.Parameter(typeof(string), "memberName");
            ParameterExpression nameHash = Expression.Variable(typeof(int), "nameHash");
            BinaryExpression calHash = Expression.Assign(nameHash, Expression.Call(memberName, typeof(object).GetMethod("GetHashCode")));
            List<SwitchCase> cases = new List<SwitchCase>();
            foreach (System.Reflection.PropertyInfo propertyInfo in type.GetProperties())
            {
                MemberExpression property = Expression.Property(Expression.Convert(instance, type), propertyInfo.Name);
                ConstantExpression propertyHash = Expression.Constant(propertyInfo.Name.GetHashCode(), typeof(int));

                cases.Add(Expression.SwitchCase(Expression.Convert(property, typeof(object)), propertyHash));
            }
            SwitchExpression switchEx = Expression.Switch(nameHash, Expression.Constant(null), cases.ToArray());
            BlockExpression methodBody = Expression.Block(typeof(object), new[] { nameHash }, calHash, switchEx);

            return Expression.Lambda<Func<object, string, object>>(methodBody, instance, memberName).Compile();
        }
        private Action<object, string, object> GenerateSetValue(Type type)
        {
            ParameterExpression instance = Expression.Parameter(typeof(object), "instance");
            ParameterExpression memberName = Expression.Parameter(typeof(string), "memberName");
            ParameterExpression newValue = Expression.Parameter(typeof(object), "newValue");
            ParameterExpression nameHash = Expression.Variable(typeof(int), "nameHash");
            BinaryExpression getHashCode = Expression.Assign(nameHash, Expression.Call(memberName, typeof(object).GetMethod("GetHashCode")));
            List<SwitchCase> cases = new List<SwitchCase>();
            foreach (System.Reflection.PropertyInfo propertyInfo in type.GetProperties())
            {
                MemberExpression property = Expression.Property(Expression.Convert(instance, type), propertyInfo.Name);
                BinaryExpression setValue = Expression.Assign(property, Expression.Convert(newValue, propertyInfo.PropertyType));
                ConstantExpression propertyHash = Expression.Constant(propertyInfo.Name.GetHashCode(), typeof(int));

                cases.Add(Expression.SwitchCase(Expression.Convert(setValue, typeof(object)), propertyHash));
            }
            SwitchExpression switchEx = Expression.Switch(nameHash, Expression.Constant(null), cases.ToArray());
            BlockExpression methodBody = Expression.Block(typeof(object), new[] { nameHash }, getHashCode, switchEx);

            return Expression.Lambda<Action<object, string, object>>(methodBody, instance, memberName, newValue).Compile();
        }
        #endregion

        #region Public
        public object GetValue(object instance, string memberName)
        {
            return GetValueDelegate(instance, memberName);
        }
        public void SetValue(object instance, string memberName, object newValue)
        {
            SetValueDelegate(instance, memberName, newValue);
        }
        #endregion
    }
    /// <summary>
    /// 利用Expression進行反射獲取資料 (效率優於非泛型使用)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DynamicReflection<T> : IMemberAccessor
    {
        #region Internal
        internal Func<object, string, object> GetValueDelegate;
        internal Action<object, string, object> SetValueDelegate;
        #endregion

        #region Constructor
        public DynamicReflection()
        {
            GetValueDelegate = GenerateGetValue();
            SetValueDelegate = GenerateSetValue();
        }
        #endregion

        #region Private
        private Func<object, string, object> GenerateGetValue()
        {

            Type type = typeof(T);
            ParameterExpression instance = Expression.Parameter(typeof(object), "instance");
            ParameterExpression memberName = Expression.Parameter(typeof(string), "memberName");
            ParameterExpression nameHash = Expression.Variable(typeof(int), "nameHash");
            BinaryExpression calHash = Expression.Assign(nameHash, Expression.Call(memberName, typeof(object).GetMethod("GetHashCode")));
            List<SwitchCase> cases = new List<SwitchCase>();
            foreach (System.Reflection.PropertyInfo propertyInfo in type.GetProperties())
            {
                MemberExpression property = Expression.Property(Expression.Convert(instance, typeof(T)), propertyInfo.Name);
                ConstantExpression propertyHash = Expression.Constant(propertyInfo.Name.GetHashCode(), typeof(int));

                cases.Add(Expression.SwitchCase(Expression.Convert(property, typeof(object)), propertyHash));
            }
            SwitchExpression switchEx = Expression.Switch(nameHash, Expression.Constant(null), cases.ToArray());
            BlockExpression methodBody = Expression.Block(typeof(object), new[] { nameHash }, calHash, switchEx);

            return Expression.Lambda<Func<object, string, object>>(methodBody, instance, memberName).Compile();
        }
        private Action<object, string, object> GenerateSetValue()
        {
            Type type = typeof(T);
            ParameterExpression instance = Expression.Parameter(typeof(object), "instance");
            ParameterExpression memberName = Expression.Parameter(typeof(string), "memberName");
            ParameterExpression newValue = Expression.Parameter(typeof(object), "newValue");
            ParameterExpression nameHash = Expression.Variable(typeof(int), "nameHash");
            BinaryExpression getHashCode = Expression.Assign(nameHash, Expression.Call(memberName, typeof(object).GetMethod("GetHashCode")));
            List<SwitchCase> cases = new List<SwitchCase>();
            foreach (System.Reflection.PropertyInfo propertyInfo in type.GetProperties())
            {
                MemberExpression property = Expression.Property(Expression.Convert(instance, typeof(T)), propertyInfo.Name);
                BinaryExpression setValue = Expression.Assign(property, Expression.Convert(newValue, propertyInfo.PropertyType));
                ConstantExpression propertyHash = Expression.Constant(propertyInfo.Name.GetHashCode(), typeof(int));

                cases.Add(Expression.SwitchCase(Expression.Convert(setValue, typeof(object)), propertyHash));
            }
            SwitchExpression switchEx = Expression.Switch(nameHash, Expression.Constant(null), cases.ToArray());
            BlockExpression methodBody = Expression.Block(typeof(object), new[] { nameHash }, getHashCode, switchEx);

            return Expression.Lambda<Action<object, string, object>>(methodBody, instance, memberName, newValue).Compile();
        }
        #endregion

        #region Public
        public object GetValue(object instance, string memberName)
        {
            return GetValueDelegate(instance, memberName);
        }
        public void SetValue(object instance, string memberName, object newValue)
        {
            SetValueDelegate(instance, memberName, newValue);
        }
        #endregion
    }

    public interface IMemberAccessor
    {
        object GetValue(object instance, string memberName);
        void SetValue(object instance, string memberName, object newValue);
    }
}
