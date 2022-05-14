using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace Collapsenav.Net.Tool;

public partial class StringExt
{
    /// <summary>
    /// 是空的
    /// </summary>
    public static bool IsNull(this string str) => string.IsNullOrEmpty(str);
    /// <summary>
    /// 若空则返回value
    /// </summary>
    public static string IsNull(this string str, string value) => str.IsNull() ? value : str;
    /// <summary>
    /// 是空的
    /// </summary>
    public static bool IsEmpty(this string str) => string.IsNullOrEmpty(str);
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
    /// 是空白字符串
    /// </summary>
    public static bool IsWhite(this string str) => string.IsNullOrWhiteSpace(str);
    /// <summary>
    /// 不是空白字符串
    /// </summary>
    public static bool NotWhite(this string str) => !str.IsWhite();
    /// <summary>
    /// 若是空白字符串则返回value
    /// </summary>
    public static string IsWhite(this string str, string value) => str.IsWhite() ? value : str;
    /// <summary>
    /// 全包含
    /// </summary>
    /// <param name="origin">源字符串</param>
    /// <param name="keys"></param>
    /// <param name="ignoreCase">是否忽略大小写(默认不忽略)</param>
    public static bool AllContain(this string origin, IEnumerable<string> keys, bool ignoreCase = false)
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
    public static bool HasContain(this string origin, IEnumerable<string> keys, bool ignoreCase = false)
    {
#if NETSTANDARD2_0
        return keys.Where(key => origin.ToLower().Contains(key.ToLower())).Any();
#else
        return keys.Where(key => origin.Contains(key, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)).Any();
#endif
    }
    /// <summary>
    /// 全包含(不忽略大小写)
    /// </summary>
    /// <param name="origin">源字符串</param>
    /// <param name="keys"></param>
    public static bool AllContain(this string origin, params string[] keys)
    {
        return keys.All(key => origin.Contains(key));
    }
    /// <summary>
    /// 部分包含(不忽略大小写)
    /// </summary>
    /// <param name="origin">源字符串</param>
    /// <param name="keys"></param>
    public static bool HasContain(this string origin, params string[] keys)
    {
        return keys.Where(key => origin.Contains(key)).Any();
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
}
