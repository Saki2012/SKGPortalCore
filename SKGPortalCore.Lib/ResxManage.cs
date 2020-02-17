using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SKGPortalCore.Lib
{
    public class ResxManage
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
        public static string GetDescription(object member)
        {
            DescriptionAttribute attribute = member.GetType().GetCustomAttribute<DescriptionAttribute>();
            return GetDescription(attribute);
        }
        public static string GetDescription(PropertyInfo property)
        {
            DescriptionAttribute attribute = property.GetCustomAttribute<DescriptionAttribute>();
            return GetDescription(attribute);
        }
        public static string GetDescription<T>()
        {
            DescriptionAttribute attribute = typeof(T).GetCustomAttribute<DescriptionAttribute>();
            return GetDescription(attribute);
        }
        private static string GetDescription(DescriptionAttribute attribute)
        {
            return null == attribute ? string.Empty : attribute.Description;
        }
    }
}
