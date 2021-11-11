using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;

namespace Collapsenav.Net.Tool
{
    public partial class StringTool
    {
        public static string ToChinese(string num)
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

        /// <summary>
        /// 检查是否邮箱格式
        /// </summary>
        public static bool IsEmail(string email, bool check = false)
        {
            if (email.IsNull())
                return false;
            if (!check)
                return Regex.IsMatch(email, "^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+.[a-zA-Z0-9]+$");
            return false;
        }

        /// <summary>
        /// 检查是否 Url 格式
        /// </summary>
        public static bool IsUrl(string url, bool check = false)
        {
            // TODO 日后写正则判断...
            var isHttp = url.StartsWith("https://", "http://");
            if (!isHttp) return false;
            // ping测试
            if (check && !url.GetDomain().CanPing()) return false;
            return true;
        }

        /// <summary>
        /// 能ping通
        /// </summary>
        public static bool CanPing(string domain, int timeout = 200)
        {
            Ping pingObj = new();
            if (IsUrl(domain))
                domain = domain.GetDomain();
            var reply = pingObj.Send(domain, timeout);
            return reply.Status == IPStatus.Success;
        }

        /// <summary>
        /// 获取 Domain 域名
        /// </summary>
        public static string GetDomain(string input)
        {
            if (input.IsUrl())
            {
                int headerIndex = input.IndexOf("//") + 2;
                return input.Substring(headerIndex, input.IndexOf('/', headerIndex) > 0 ? input.IndexOf('/', headerIndex) - headerIndex : input.Length - headerIndex);
            }
            // TODO 还需要添加其他判断
            return string.Empty;
        }

        public static string Join<T>(IEnumerable<T> query, string separate, Func<T, object> getStr = null)
        {
            return string.Join(separate, query.Select(getStr ?? (item => item.ToString())));
        }
    }
}
