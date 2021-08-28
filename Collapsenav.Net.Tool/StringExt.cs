using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Collapsenav.Net.Tool
{
    public static class StringExt
    {
        public static bool IsNull(this string str) => string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
        public static string PadLeft(this int num, int total, char? fill) => fill.HasValue ? num.ToString().PadLeft(total, fill.Value) : num.ToString().PadLeft(total);
        public static string PadLeft(this double num, int total, char? fill) => fill.HasValue ? num.ToString().PadLeft(total, fill.Value) : num.ToString().PadLeft(total);
        public static string PadLeft(this long num, int total, char? fill) => fill.HasValue ? num.ToString().PadLeft(total, fill.Value) : num.ToString().PadLeft(total);
        public static string PadRight(this int num, int total, char? fill) => fill.HasValue ? num.ToString().PadRight(total, fill.Value) : num.ToString().PadRight(total);
        public static string PadRight(this double num, int total, char? fill) => fill.HasValue ? num.ToString().PadRight(total, fill.Value) : num.ToString().PadRight(total);
        public static string PadRight(this long num, int total, char? fill) => fill.HasValue ? num.ToString().PadRight(total, fill.Value) : num.ToString().PadRight(total);
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
    }
}
