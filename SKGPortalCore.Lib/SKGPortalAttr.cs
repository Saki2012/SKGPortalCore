using SKGPortalCore.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace SKGPortalCore.Lib
{
    /// <summary>
    /// 可否為空(0)值
    /// </summary>
    public class IsEmptyField
    { }
    public class ResxManage
    {
        public static string GetDescription(Enum member)
        {
            var attribute = member.GetType().GetField(member.ToString()).GetCustomAttribute<DescriptionAttribute>();
            return GetDescription(attribute);
        }
        public static string GetDescription(object member)
        {
            var attribute = member.GetType().GetCustomAttribute<DescriptionAttribute>();
            return GetDescription(attribute);
        }
        public static string GetDescription(PropertyInfo property)
        {
            var attribute = property.GetCustomAttribute<DescriptionAttribute>();
            return GetDescription(attribute);
        }
        public static string GetDescription<T>()
        {
            var attribute = typeof(T).GetCustomAttribute<DescriptionAttribute>();
            return GetDescription(attribute);
        }
        private static string GetDescription(DescriptionAttribute attribute)
        {
            return null == attribute ? string.Empty : attribute.Description;
        }
    }
}
