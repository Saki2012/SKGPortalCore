﻿using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Linq;
using System.Linq.Expressions;
using SKGPortalCore.Core.Libary;

namespace SKGPortalCore.Core.Libary
{
    public static class ResxManage
    {
        public static string GetProgId(object member)
        {
            ProgIdAttribute attribute = member.GetType().GetCustomAttribute<ProgIdAttribute>();
            return GetProgId(attribute);
        }
        public static string GetProgId(PropertyInfo property)
        {
            ProgIdAttribute attribute = property.GetCustomAttribute<ProgIdAttribute>();
            return GetProgId(attribute);
        }
        public static string GetProgId<T>()
        {
            ProgIdAttribute attribute = typeof(T).GetCustomAttribute<ProgIdAttribute>();
            return GetProgId(attribute);
        }
        private static string GetProgId(ProgIdAttribute attribute)
        {
            return null == attribute ? string.Empty : attribute.Value;
        }

        public static string GetDescription(Enum member)
        {
            DescriptionAttribute attribute = member.GetType().GetField(member.ToString()).GetCustomAttribute<DescriptionAttribute>();
            return GetDescription(attribute);
        }
        public static string GetDescription<T>()
        {
            DescriptionAttribute attribute = typeof(T).GetCustomAttribute<DescriptionAttribute>();
            return GetDescription(attribute);
        }
        public static string GetDescription<T>(Expression<Func<T, object>> propertyExpression)
        {
            PropertyInfo propertyInfo = propertyExpression.Body.NodeType == ExpressionType.Convert ?
                               (PropertyInfo)((MemberExpression)((UnaryExpression)propertyExpression.Body).Operand).Member
                              : (PropertyInfo)((MemberExpression)propertyExpression.Body).Member;
            return GetDescription(propertyInfo);
        }

        public static string GetDescription<T>(string name, bool isField)
        {
            if (isField) return GetDescription(typeof(T).GetProperty(name));
            else return GetDescription(typeof(T).GetMethod(name));
        }

        public static string GetDescription(MethodInfo property)
        {
            DescriptionAttribute attribute = property.GetCustomAttribute<DescriptionAttribute>();
            return GetDescription(attribute);
        }

        public static string GetDescription(PropertyInfo property)
        {
            DescriptionAttribute attribute = property.GetCustomAttribute<DescriptionAttribute>();
            return GetDescription(attribute);
        }
        private static string GetDescription(DescriptionAttribute attribute)
        {
            return null == attribute ? string.Empty : attribute.Description;
        }
    }
}
