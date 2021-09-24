using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Collapsenav.Net.Tool
{
    public static class StringExt
    {
        public static bool IsNull(this string str) => string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
        public static bool IsEmpty(this string str) => string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
        public static bool NotNull(this string str) => !str.IsNull();
        public static bool NotEmpty(this string str) => !str.IsEmpty();

        #region PadLeft/PadRight
        public static string PadLeft(this int num, int total, char? fill) => fill.HasValue ? num.ToString().PadLeft(total, fill.Value) : num.ToString().PadLeft(total);
        public static string PadLeft(this double num, int total, char? fill) => fill.HasValue ? num.ToString().PadLeft(total, fill.Value) : num.ToString().PadLeft(total);
        public static string PadLeft(this long num, int total, char? fill) => fill.HasValue ? num.ToString().PadLeft(total, fill.Value) : num.ToString().PadLeft(total);
        public static string PadRight(this int num, int total, char? fill) => fill.HasValue ? num.ToString().PadRight(total, fill.Value) : num.ToString().PadRight(total);
        public static string PadRight(this double num, int total, char? fill) => fill.HasValue ? num.ToString().PadRight(total, fill.Value) : num.ToString().PadRight(total);
        public static string PadRight(this long num, int total, char? fill) => fill.HasValue ? num.ToString().PadRight(total, fill.Value) : num.ToString().PadRight(total);
        #endregion

        /// <summary>
        /// 数字转为中文,暂时只支持整数,支持最大的整数长度为16位
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ToChinese(this string num)
        {
            string[] nums = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
            string[] units = { "", "十", "百", "千", "万", "十", "百", "千", "亿", "十", "百", "千", "万", "十", "百", "千" };
            StringBuilder sb = new();
            var numChar = num.ToArray();
            var curUnits = units.Take(num.Length).Reverse().ToArray();
            foreach (var (ch, index) in numChar.Select((nchar, i) => (nchar - 48, i)))
            {
                sb.Append(nums[ch]);
                // Console.WriteLine(curUnits[index]);
                if (ch == 0 && !"亿万".Contains(curUnits[index]))
                    continue;
                sb.Append(curUnits[index]);
            }
            Regex matchZero = new("零+");
            Regex matchLastZero = new("零+$");
            var result = matchZero.Replace(sb.ToString(), "零");
            result = matchLastZero.Replace(result.Replace("零万", "万零").Replace("零亿", "亿零"), "");
            return result;
        }

        #region Convert
        public static DateTime? ToDateTime(this string input) => DateTime.TryParse(input, out DateTime result) ? result : null;
        public static int? ToInt(this string input) => int.TryParse(input, out int result) ? result : null;
        public static double? ToDouble(this string input) => double.TryParse(input, out double result) ? result : null;
        public static Guid? ToGuid(this string input) => Guid.TryParse(input, out Guid result) ? result : null;
        public static long? ToLong(this string input) => long.TryParse(input, out long result) ? result : null;
        #endregion

        #region  Join
        public static string Join<T>(this IEnumerable<T> query, string separate, Func<T, object> getStr = null)
        => string.Join(separate, query.Select(getStr ?? (item => item.ToString())));
        #endregion

        /// <summary>
        /// 检查是否邮箱格式
        /// </summary>
        public static bool IsEmail(this string input)
        {
            if (input.IsNull())
                return false;
            return Regex.IsMatch(input, "^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+.[a-zA-Z0-9]+$");
        }

        /// <summary>
        /// 检查是否 Url 格式
        /// </summary>
        public static bool IsUrl(this string input, bool ping = false)
        {
            // TODO 日后写正则判断...
            var isHttp = input.StartsWith("https://", "http://");
            if (!isHttp) return false;
            // ping测试
            if (ping && !input.GetDomain().CanPing()) return false;

            return true;
        }

        /// <summary>
        /// 能ping通
        /// </summary>
        public static bool CanPing(this string input, int timeout = 200)
        {
            Ping pingObj = new();
            var reply = pingObj.Send(input, timeout);
            return reply.Status == IPStatus.Success;
        }

        /// <summary>
        /// 获取 Domain 域名
        /// </summary>
        public static string GetDomain(this string input)
        {
            if (input.IsUrl())
            {
                int headerIndex = input.IndexOf("//") + 2;
                return input.Substring(headerIndex, input.IndexOf('/', headerIndex, input.Length - headerIndex) - headerIndex);
            }
            // TODO 还需要添加其他判断
            return input;
        }

        public static bool StartsWith(this string input, params string[] filters) => filters.Any(item => input.StartsWith(item));
        public static bool EndsWith(this string input, params string[] filters) => filters.Any(item => input.EndsWith(item));

        public static string ToString(this char input, int count)
        {
            return new string(input, count);
        }
    }
}
