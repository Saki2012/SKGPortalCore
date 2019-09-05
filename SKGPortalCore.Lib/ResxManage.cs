using SKGPortalCore.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace SKGPortalCore.Lib
{
    public class ResxManage
    {
        public static string GetProgId(object member)
        {
            var attribute = member.GetType().GetCustomAttribute<ProgIdAttribute>();
            return GetProgId(attribute);
        }
        public static string GetProgId(PropertyInfo property)
        {
            var attribute = property.GetCustomAttribute<ProgIdAttribute>();
            return GetProgId(attribute);
        }
        public static string GetProgId<T>()
        {
            var attribute = typeof(T).GetCustomAttribute<ProgIdAttribute>();
            return GetProgId(attribute);
        }
        private static string GetProgId(ProgIdAttribute attribute)
        {
            return null == attribute ? string.Empty : attribute.Value;
        }



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
