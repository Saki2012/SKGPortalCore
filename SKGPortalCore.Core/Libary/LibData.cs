using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using GraphQL;
using GraphQL.Types;
using SKGPortalCore.Core.Libary;

namespace SKGPortalCore.Core.Libary
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
            if (null == strs || strs.Length == 0) return string.Empty;
            int len = strs.Length;
            //StringBuilder results =new StringBuilder();
            string result = strs[0].ToString(), s;
            for (int i = 1; i < len; i++)
            {
                //Q:Null Value And DbNull
                s = strs[i] == null ? string.Empty : strs[i].ToString();
                if (i == 1 && string.IsNullOrEmpty(result))
                {
                    result = s;
                }
                else if (hasEmpty || !string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(s))
                {
                    result = $"{result}{mergeStr}{s}";
                }
                else if (!string.IsNullOrEmpty(s))
                {
                    result = s;
                }
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
            if (null == val)
            {
                return true;
            }

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
                Type type when type == typeof(DateTime) => (DateTime)val == DateTime.MinValue,
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
            return str.ByteSubString(idx, len, Encoding.GetEncoding(950));
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
            return str.ByteLen(Encoding.GetEncoding(950));
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
                if (element.GetType() == val.GetType() && string.Compare(element.ToString(), val.ToString()) == 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 獲取營業日
        /// </summary>
        /// <param name="workDateDic"></param>
        /// <param name="date"></param>
        /// <param name="isNext"></param>
        /// <returns></returns>
        public static DateTime GetWorkDate(Dictionary<DateTime, bool> workDateDic, DateTime date, int days = 0, bool isNext = true)
        {
            return isNext ?
                workDateDic.Where(p => p.Value == true && p.Key >= date).OrderBy(p => p.Key).ElementAt(Math.Abs(days)).Key :
                workDateDic.Where(p => p.Value == true && p.Key <= date).OrderByDescending(p => p.Key).ElementAt(Math.Abs(days)).Key;
        }
        /// <summary>
        /// 轉換駝峰式文字
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string s)
        {
            var x = s.Replace("_", "");
            if (x.Length == 0) return s;
            x = Regex.Replace(x, "([A-Z])([A-Z]+)($|[A-Z])",
                m => m.Groups[1].Value + m.Groups[2].Value.ToLower() + m.Groups[3].Value);
            return char.ToLower(x[0]) + x.Substring(1);
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
            if (null == val || DBNull.Value == val) return decimal.Zero;
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


        /// <summary>
        /// 獲取月初日
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static DateTime GetFirstDate(this DateTime val)
        {
            return val.AddDays(-(val.Day - 1));
        }
        /// <summary>
        /// 獲取月末日
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static DateTime GetLastDate(this DateTime val)
        {
            return val.GetFirstDate().AddMonths(1).AddDays(-1);
        }
        public static DateTime ToDateTime(this string val)
        {
            val = val.ToADDateFormat();
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
        /// 民國年轉西元年
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static DateTime ROCDateToCEDate(this string val)
        {
            return val.Length == 7 ? $"{val.Substring(0, 3).ToInt32() + 1911}/{val.Substring(3, 2)}/{val.Substring(5, 2)}".ToDateTime() : DateTime.MinValue;
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
                    if (num >= 48 && num <= 57 || num >= 65 && num <= 90 || num >= 97 && num <= 122)
                    {
                        c = (char)num;
                    }
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
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using MemoryStream memoryStream = new MemoryStream();
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
            using MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            memoryStream.Write(bytes, 0, bytes.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);
            object obj = binaryFormatter.Deserialize(memoryStream);
            return (T)obj;
        }
        /// <summary>
        /// 將 Stream 轉成 byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] StreamToBytes(this Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        public static List<T> SumListData<T>(this List<T> val, Expression<Func<T, T, object>> propertyExpression)
        {
            //var propertyInfo = (propertyExpression.Body.NodeType == ExpressionType.Convert) ?
            //(PropertyInfo)((MemberExpression)((UnaryExpression)propertyExpression.Body).Operand).Member
            //: (PropertyInfo)((MemberExpression)propertyExpression.Body).Member;
            return val;
        }

        public static Func<object[], T> Build<T>()
        {
            var t = typeof(T);

            var param = Expression.Parameter(typeof(object[]), "args");

            var ctor = t.GetConstructors()[0];

            var argsExp = ctor.GetParameters().Select(
                (p, i) =>
                {
                    Expression index = Expression.Constant(i);
                    Expression paramAccessorExp = Expression.ArrayIndex(param, index);
                    Expression paramCastExp = Expression.Convert(paramAccessorExp, p.ParameterType);

                    return paramCastExp;
                });

            var exp = Expression.New(ctor, argsExp);

            return Expression.Lambda<Func<object[], T>>(exp, param).Compile();
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
                    if (type.IsEnum) return typeof(BaseEnumerationGraphType<>).MakeGenericType(new[] { type });
                    return type;
            }
        }
    }
    public class BaseEnumerationGraphType<TEnum> : EnumerationGraphType where TEnum : Enum
    {
        public BaseEnumerationGraphType()
        {                                
            Name = typeof(TEnum).Name;
            Description = ResxManage.GetDescription<TEnum>();
            foreach (Enum val in Enum.GetValues(typeof(TEnum)))
                AddValue(val.ToString(), ResxManage.GetDescription(val), val.GetValue());
        }
    }
}