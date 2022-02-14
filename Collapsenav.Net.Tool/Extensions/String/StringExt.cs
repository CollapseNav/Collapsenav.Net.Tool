using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;

namespace Collapsenav.Net.Tool;
public static partial class StringExt
{
    /// <summary>
    /// 是空的
    /// </summary>
    public static bool IsNull(this string str) => string.IsNullOrWhiteSpace(str);
    /// <summary>
    /// 若空则返回value
    /// </summary>
    public static string IsNull(this string str, string value) => str.IsNull() ? value : str;
    /// <summary>
    /// 是空的
    /// </summary>
    public static bool IsEmpty(this string str) => string.IsNullOrWhiteSpace(str);
    /// <summary>
    /// 若空则返回value
    /// </summary>
    public static string IsEmpty(this string str, string value) => str.IsEmpty() ? value : str;
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
        string[] nums = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
        string[] units = { "", "十", "百", "千", "万", "十", "百", "千", "亿", "十", "百", "千", "万", "十", "百", "千" };
        StringBuilder sb = new();
        var numChar = num.ToArray();
        var curUnits = units.Take(num.Length).Reverse().ToArray();
        foreach (var (ch, index) in numChar.Select((nchar, i) => (nchar - 48, i)))
        {
            sb.Append(nums[ch]);
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

    public static DateTime? ToDateTime(this string input) => DateTime.TryParse(input, out DateTime result) ? result : null;
    public static int? ToInt(this string input) => int.TryParse(input, out int result) ? result : null;
    public static double? ToDouble(this string input) => double.TryParse(input, out double result) ? result : null;
    public static Guid? ToGuid(this string input) => Guid.TryParse(input, out Guid result) ? result : null;
    public static long? ToLong(this string input) => long.TryParse(input, out long result) ? result : null;

    /// <summary>
    /// 拼!
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="separate">分隔符</param>
    /// <param name="getStr">针对复杂类型的委托</param>
    public static string Join<T>(this IEnumerable<T> query, string separate = "", Func<T, object> getStr = null)
    {
        return string.Join(separate, query.Select(getStr ?? (item => item.ToString())));
    }

    /// <summary>
    /// 检查是否邮箱格式
    /// </summary>
    public static bool IsEmail(this string email)
    {
        if (email.IsNull())
            return false;
        return Regex.IsMatch(email, "^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+.[a-zA-Z0-9]+$");
    }

    /// <summary>
    /// 检查是否 Url 格式
    /// </summary>
    public static bool IsUrl(this string url, bool check = false)
    {
        // TODO 日后写正则判断...
        var isHttp = url.HasStartsWith("https://", "http://");
        if (!isHttp) return false;
        // ping测试
        if (check && !url.GetDomain().CanPing()) return false;
        return true;
    }

    /// <summary>
    /// 能ping通
    /// </summary>
    public static bool CanPing(this string domain, int timeout = 1000)
    {
        Ping pingObj = new();
        if (domain.IsUrl())
            domain = domain.GetDomain();
        var reply = pingObj.Send(domain, timeout);
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
            return input.Substring(headerIndex, input.IndexOf('/', headerIndex) > 0 ? input.IndexOf('/', headerIndex) - headerIndex : input.Length - headerIndex);
        }
        // TODO 还需要添加其他判断
        return string.Empty;
    }

    public static string ToString(this char input, int count)
    {
        return new string(input, count);
    }

    /// <summary>
    /// 自动遮罩(偷懒)
    /// </summary>
    public static string AutoMask(this string origin, string mask = "*")
    {
        // TODO 邮箱,手机,地址,网址,账号等可能需要特定的遮罩
        if (origin.Trim().IsEmpty())
            return "***";
        if (origin.Length <= 6)
        {
            origin = origin.PadLeft(6, '#');
            return Regex.Replace(origin, "(.{1}).*(.{1})", $"$1{mask}$2");
        }
        else
        {
            return Regex.Replace(origin, "(.{3}).*(.{3})", $"$1{mask}$2");
        }
    }

    /// <summary>
    /// 字符串转为byte[] 默认 utf8
    /// </summary>
    public static byte[] ToBytes(this string origin, Encoding encode = null)
    {
        encode ??= Encoding.UTF8;
        return encode.GetBytes(origin);
    }
    /// <summary>
    /// 从前取
    /// </summary>
    public static string First(this string origin, int len = 1)
    {
        return len > origin.Length ? origin : origin[..len];
    }
    /// <summary>
    /// 从后取
    /// </summary>
    public static string Last(this string origin, int len = 1)
    {
        return len > origin.Length ? origin : origin.Substring(origin.Length - len, len);
    }
    /// <summary>
    /// 全包含
    /// </summary>
    /// <param name="origin">源字符串</param>
    /// <param name="keys"></param>
    /// <param name="ignoreCase">是否忽略大小写(默认不忽略)</param>
    public static bool ContainAnd(this string origin, IEnumerable<string> keys, bool ignoreCase = false)
    {

#if NETSTANDARD2_0
        return keys.All(key => origin.ToLower().Contains(key.ToLower()));
#else
        return keys.All(key => origin.Contains(key, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal));
#endif
    }
    /// <summary>
    /// 部分包含
    /// </summary>
    /// <param name="origin">源字符串</param>
    /// <param name="keys"></param>
    /// <param name="ignoreCase">是否忽略大小写(默认不忽略)</param>
    public static bool ContainOr(this string origin, IEnumerable<string> keys, bool ignoreCase = false)
    {
#if NETSTANDARD2_0
        return keys.Any(key => origin.ToLower().Contains(key.ToLower()));
#else
        return keys.Any(key => origin.Contains(key, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal));
#endif
    }
    /// <summary>
    /// 全包含(不忽略大小写)
    /// </summary>
    /// <param name="origin">源字符串</param>
    /// <param name="keys"></param>
    public static bool ContainAnd(this string origin, params string[] keys)
    {
        return keys.All(key => origin.Contains(key));
    }
    /// <summary>
    /// 部分包含(不忽略大小写)
    /// </summary>
    /// <param name="origin">源字符串</param>
    /// <param name="keys"></param>
    public static bool ContainOr(this string origin, params string[] keys)
    {
        return keys.Any(key => origin.Contains(key));
    }

    /// <summary>
    /// byte[] 转为 hex 字符串
    /// </summary>
    public static string ToHexString(this byte[] bytes)
    {
        return BitConverter.ToString(bytes).Replace("-", string.Empty).ToLower();
    }

    /// <summary>
    /// hex 字符串转为 byte[]
    /// </summary>
    public static byte[] HexToBytes(this string hex)
    {
        var bytes = new byte[hex.Length / 2];
        for (var i = 0; i < bytes.Length; i++)
            bytes[i] = (byte)Convert.ToInt32(hex[(i * 2)..(i * 2 + 2)], 16);
        return bytes;
    }

    /// <summary>
    /// bytes转为string
    /// </summary>
    public static string BytesToString(this byte[] bytes, Encoding encoding = null)
    {
        encoding ??= Encoding.UTF8;
        return encoding.GetString(bytes);
    }

    /// <summary>
    /// 打乱字符串顺序
    /// </summary>
    public static string Shuffle(this string input)
    {
        return input.Shuffle<char>().Join();
    }

    public static string AddIf(this string origin, bool check, string value)
    {
        if (check)
            origin += value;
        return origin;
    }
    public static string Add(this string origin, string value)
    {
        return origin + value;
    }

    public static string AddIf<T>(this string origin, T? check, string value) where T : struct
    {
        if (check.HasValue)
            origin += value;
        return origin;
    }
}
