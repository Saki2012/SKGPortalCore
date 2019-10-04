﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using GraphQL.Types;

namespace SKGPortalCore.Lib
{
    public static class LibData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
        public static string Format(string str, params object[] vals)
        {
            int len = vals.Length;
            string[] qVals = new string[len];
            for (int i = 0; i < len; i++)
            {
                qVals[i] = vals[i].Quote();
            }
            return string.Format(str, qVals);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string Quote(this object val)
        {
            //Q:Null Value And DbNull
            string result = val.GetType() switch
            {
                Type stringType when stringType == typeof(string) => $"'{val}'",
                null => "Is Null",
                _ => val.ToString(),
            };
            return result;
        }
        /// <summary>
        /// 合併
        /// </summary>
        /// <param name="mergeStr">合併連接字 Ex:,</param>
        /// <param name="mergeEmpty"></param>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static string Merge(string mergeStr, bool hasEmpty, params object[] strs)
        {
            int len = strs.Length;
            if (null == strs || len == 0) return string.Empty;
            string result = strs[0].ToString(), s;
            for (int i = 1; i < len; i++)
            {
                //Q:Null Value And DbNull
                s = strs[i].ToString();
                if (i == 1 && string.IsNullOrEmpty(result))
                    result = s;
                else if (hasEmpty || (!string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(s)))
                    result = $"{result}{mergeStr}{s}";
                else if (!string.IsNullOrEmpty(s))
                    result = s;
            }
            return result;
        }
        /// <summary>
        /// 字串是否為空
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this object val)
        {
            if (null == val) return true;
            return val.GetType() switch
            {
                Type type when type == typeof(string) => string.IsNullOrEmpty(val.ToString()),
                Type type when type == typeof(byte) => val.ToByte() == 0,
                Type type when type == typeof(short) => val.ToInt16() == 0,
                Type type when type == typeof(int) => val.ToInt32() == 0,
                Type type when type == typeof(long) => val.ToInt64() == 0,
                Type type when type == typeof(decimal) => val.ToDecimal() == decimal.Zero,
                Type type when type == typeof(float) => val.ToFloat() == 0f,
                Type type when type == typeof(double) => val.ToDouble() == 0f,
                _ => null == val || DBNull.Value == val,
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="idx"></param>
        /// <param name="len"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ByteSubString(this string str, int idx, int len)
        {
            return ByteSubString(str, idx, len, Encoding.GetEncoding(950));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="idx"></param>
        /// <param name="len"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ByteSubString(this string str, int idx, int len, Encoding encoding)
        {
            byte[] arr = encoding.GetBytes(str);
            return encoding.GetString(arr, idx, len);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ByteLen(this string str)
        {
            return ByteLen(str, Encoding.GetEncoding(950));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static int ByteLen(this string str, Encoding encoding)
        {
            return encoding.GetBytes(str).Length;
        }
        /// <summary>
        /// 檢查是否為數字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumberString(this string str)
        {
            return Regex.IsMatch(str, "^[0-9]*$");
        }
        /// <summary>
        /// 包含資料
        /// </summary>
        /// <param name="val"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static bool In(this object val, params dynamic[] elements)
        {
            foreach (dynamic element in elements)
            {
                if (element.GetType() == val.GetType() && string.Compare(element.ToString(), val.ToString()) == 0) return true;
            }
            return false;
        }
        /// Convert
        public static short ToInt16(this object val)
        {
            return Convert.ToInt16(val);
        }
        public static int ToInt32(this object val)
        {
            return Convert.ToInt32(val);
        }
        public static long ToInt64(this object val)
        {
            return Convert.ToInt64(val);
        }
        public static string ToString(this object val)
        {
            return Convert.ToString(val);
        }
        public static decimal ToDecimal(this object val)
        {
            return Convert.ToDecimal(val);
        }
        public static double ToDouble(this object val)
        {
            return Convert.ToDouble(val);
        }
        public static float ToFloat(this object val)
        {
            return Convert.ToSingle(val);
        }
        public static byte ToByte(this object val)
        {
            return Convert.ToByte(val);
        }
        public static DateTime ToDateTime(this string val)
        {
            return Convert.ToDateTime(val);
        }
        /// <summary>
        /// 西元年轉民國年
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string ToROCDate(this DateTime datetime)
        {
            string[] strsDate = datetime.AddYears(-1911).ToString("yyy/MM/dd").Split('/');
            return $"{strsDate[0]}{strsDate[1]}{strsDate[2]}";
        }
        /// <summary>
        /// 轉換為yyyy/MM/dd
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToADDateFormat(this string val)
        {
            return val.Length == 8 ? $"{val.Substring(0, 4)}/{val.Substring(4, 2)}/{val.Substring(6, 2)}" : val;
        }
        /// <summary>
        /// 檢查列表是否有值
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool HasData(this IList val)
        {
            return null != val && val.Count > 0;
        }
        /// <summary>
        /// 獲取亂數
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string GenRandomString(int len)
        {
            Random rd = new Random(Convert.ToInt32(DateTime.Now.Ticks % int.MaxValue));
            return GenRandomString(rd, len);
        }
        /// <summary>
        /// 獲取亂數
        /// </summary>
        /// <param name="rd"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string GenRandomString(Random rd, int len)
        {
            string result = string.Empty;
            for (int i = 0; i < len; i++)
            {
                char c = ' ';
                while (c == ' ')
                {
                    int num = rd.Next(48, 122);
                    if ((num >= 48 && num <= 57) || (num >= 65 && num <= 90) || (num >= 97 && num <= 122))
                        c = (char)num;
                }
                result += c.ToString();
            }
            return result;
        }
        /// <summary>
        /// 獲取最底層的Exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static Exception GetInnermostException(this Exception ex)
        {
            Exception innerEx = ex;
            while (innerEx.InnerException != null)
            {
                innerEx = ex.InnerException;
            }
            return innerEx;
        }
        /// <summary>
        /// 序列化成byte[]
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ObjectToByteArray(this object obj)
        {
            var binaryFormatter = new BinaryFormatter();
            using var memoryStream = new MemoryStream();
            binaryFormatter.Serialize(memoryStream, obj);
            return memoryStream.ToArray();
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static T ByteArrayToObject<T>(this byte[] bytes)
        {
            using var memoryStream = new MemoryStream();
            var binaryFormatter = new BinaryFormatter();
            memoryStream.Write(bytes, 0, bytes.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var obj = binaryFormatter.Deserialize(memoryStream);
            return (T)obj;
        }
    }
    public static class GraphQLChangeType
    {
        public static Type ChangeGrcaphQLType(Type type)
        {
            switch (type)
            {
                case Type stringType when stringType == typeof(string):
                    return typeof(StringGraphType);
                case Type byteType when byteType == typeof(byte):
                case Type intType when intType == typeof(int):
                case Type longType when longType == typeof(long):
                    return typeof(IntGraphType);
                case Type decimalType when decimalType == typeof(decimal):
                    return typeof(DecimalGraphType);
                case Type boolType when boolType == typeof(bool):
                    return typeof(BooleanGraphType);
                case Type dateTimeType when dateTimeType == typeof(DateTime):
                    return typeof(DateTimeGraphType);
                default:
                    if (type.IsEnum)
                        return GetEnumerationGraphType(type);
                    return type;
            }
        }
        private static Type GetEnumerationGraphType(Type type)
        {
            return typeof(IntGraphType);
            /*
            return type switch
            {
                Type SysEnums when SysEnums == typeof(SysEnums) => typeof(EnumerationGraphType<SysEnums>),
                Type PayStatus when PayStatus == typeof(PayStatus) => typeof(EnumerationGraphType<PayStatus>),
                Type PayerType when PayerType == typeof(PayerType) => typeof(EnumerationGraphType<PayerType>),
                Type FuncAction when FuncAction == typeof(FuncAction) => typeof(StringGraphType),
                Type EndType when EndType == typeof(EndType) => typeof(EnumerationGraphType<EndType>),
                _ => type,
            };
            */
        }
    }
}