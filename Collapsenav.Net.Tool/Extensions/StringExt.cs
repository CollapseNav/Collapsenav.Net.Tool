using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collapsenav.Net.Tool
{
    public static partial class StringExt
    {
        /// <summary>
        /// 是空的
        /// </summary>
        public static bool IsNull(this string str) => string.IsNullOrWhiteSpace(str);
        /// <summary>
        /// 是空的
        /// </summary>
        public static bool IsEmpty(this string str) => string.IsNullOrWhiteSpace(str);
        /// <summary>
        /// 没空
        /// </summary>
        public static bool NotNull(this string str) => !str.IsNull();
        /// <summary>
        /// 没空
        /// </summary>
        public static bool NotEmpty(this string str) => !str.IsEmpty();


        /// <summary>
        /// 左填充
        /// </summary>
        /// <param name="obj">源</param>
        /// <param name="total">总长度</param>
        /// <param name="fill">填充字符</param>
        public static string PadLeft(this object obj, int total, char? fill = ' ') => obj.ToString().PadLeft(total, fill ?? ' ');
        /// <summary>
        /// 右填充
        /// </summary>
        /// <param name="obj">源</param>
        /// <param name="total">总长度</param>
        /// <param name="fill">填充字符</param>
        public static string PadRight(this object obj, int total, char? fill = ' ') => obj.ToString().PadRight(total, fill ?? ' ');
        /// <summary>
        /// 左填充
        /// </summary>
        /// <param name="obj">源</param>
        /// <param name="total">总长度</param>
        /// <param name="act">一个委托</param>
        /// <param name="fill">填充字符</param>
        public static string PadLeft<T>(this T obj, int total, Func<T, object> act, char? fill = ' ') => act(obj).ToString().PadLeft(total, fill ?? ' ');
        /// <summary>
        /// 右填充
        /// </summary>
        /// <param name="obj">源</param>
        /// <param name="total">总长度</param>
        /// <param name="act">一个委托</param>
        /// <param name="fill">填充字符</param>
        public static string PadRight<T>(this T obj, int total, Func<T, object> act, char? fill = ' ') => act(obj).ToString().PadRight(total, fill ?? ' ');



        /// <summary>
        /// 数字转为中文,暂时只支持整数,支持最大的整数长度为16位
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ToChinese(this string num)
        {
            return StringTool.ToChinese(num);
        }

        public static DateTime? ToDateTime(this string input) => DateTime.TryParse(input, out DateTime result) ? result : null;
        public static int? ToInt(this string input) => int.TryParse(input, out int result) ? result : null;
        public static double? ToDouble(this string input) => double.TryParse(input, out double result) ? result : null;
        public static Guid? ToGuid(this string input) => Guid.TryParse(input, out Guid result) ? result : null;
        public static long? ToLong(this string input) => long.TryParse(input, out long result) ? result : null;



        public static string Join<T>(this IEnumerable<T> query, string separate, Func<T, object> getStr = null)
        {
            return StringTool.Join(query, separate, getStr);
        }

        /// <summary>
        /// 检查是否邮箱格式
        /// </summary>
        public static bool IsEmail(this string input)
        {
            return StringTool.IsEmail(input);
        }

        /// <summary>
        /// 检查是否 Url 格式
        /// </summary>
        public static bool IsUrl(this string input, bool ping = false)
        {
            return StringTool.IsUrl(input, ping);
        }

        /// <summary>
        /// 能ping通
        /// </summary>
        public static bool CanPing(this string input, int timeout = 200)
        {
            return StringTool.CanPing(input, timeout);
        }

        /// <summary>
        /// 获取 Domain 域名
        /// </summary>
        public static string GetDomain(this string input)
        {
            return StringTool.GetDomain(input);
        }

        /// <summary>
        /// 存在以啥啥啥开头
        /// </summary>
        public static bool HasStartsWith(this string input, params string[] filters) => filters.Any(item => input.StartsWith(item));
        /// <summary>
        /// 存在以啥啥啥结尾
        /// </summary>
        public static bool HasEndsWith(this string input, params string[] filters) => filters.Any(item => input.EndsWith(item));

        /// <summary>
        /// 全部以啥啥啥开头
        /// </summary>
        public static bool AllStartsWith(this string input, params string[] filters) => filters.All(item => input.StartsWith(item));
        /// <summary>
        /// 全部以啥啥啥结尾
        /// </summary>
        public static bool AllEndsWith(this string input, params string[] filters) => filters.All(item => input.EndsWith(item));

        public static string ToString(this char input, int count)
        {
            return new string(input, count);
        }

        /// <summary>
        /// 自动遮罩(偷懒)
        /// </summary>
        public static string AutoMask(this string origin, string mask = "*")
        {
            return StringTool.AutoMask(origin, mask);
        }

        /// <summary>
        /// 字符串转为byte[] 默认 utf8
        /// </summary>
        public static byte[] ToBytes(this string origin, Encoding encode = null)
        {
            encode ??= Encoding.UTF8;
            return encode.GetBytes(origin);
        }
    }
}
