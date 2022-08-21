using System.Text.RegularExpressions;

namespace Collapsenav.Net.Tool;
public static partial class DateTimeExt
{
    /// <summary>
    /// DateTime转为时间戳
    /// </summary>
    public static long ToTimestamp(this DateTime time)
    {
        return new DateTimeOffset(time).ToUnixTimeMilliseconds();
    }
    /// <summary>
    /// DateTime转为短时间戳(10位)
    /// </summary>
    public static long ToShortTimestamp(this DateTime time)
    {
        return new DateTimeOffset(time).ToUnixTimeSeconds();
    }
    /// <summary>
    /// 时间戳转为DateTime
    /// </summary>
    public static DateTime ToDateTime(this long timestamp)
    {
        return timestamp <= 0 ? DateTime.Now : DateTimeOffset.FromUnixTimeMilliseconds(timestamp).DateTime;
    }
    /// <summary>
    /// 时间戳转为DateTime
    /// </summary>
    public static DateTime ToShortDateTime(this long timestamp)
    {
        return timestamp <= 0 ? DateTime.Now : DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
    }


    /// <summary>
    /// 转为默认日期格式字符串(yyyy-MM-dd)
    /// </summary>
    /// <param name="date"></param>
    /// <param name="s">中间的分隔符,'-' </param>
    public static string ToDefaultDateString(this DateTime date, string s)
    {
        return date.ToString($"yyyy-MM-dd").Replace("-", s);
    }
    /// <summary>
    /// 转为默认日期格式字符串(yyyy-MM-dd)
    /// </summary>
    /// <param name="date"></param>
    public static string ToDefaultDateString(this DateTime date)
    {
        return date.ToString($"yyyy-MM-dd");
    }


    /// <summary>
    /// 转为默认时间格式字符串(yyyy-MM-dd HH:mm:ss)
    /// </summary>
    /// <param name="time"></param>
    public static string ToDefaultTimeString(this DateTime time)
    {
        return time.ToString($"yyyy-MM-dd HH:mm:ss");
    }
    /// <summary>
    /// 转为默认时间格式字符串(yyyy-MM-dd HH:mm:ss)
    /// </summary>
    /// <param name="time"></param>
    /// <param name="f">分隔符,'-' </param>
    public static string ToDefaultTimeString(this DateTime time, string f)
    {
        return time.ToString($"yyyy-MM-dd HH:mm:ss").Replace("-", f);
    }
    /// <summary>
    /// 转为默认时间格式字符串(yyyy-MM-dd HH:mm:ss)
    /// </summary>
    /// <param name="time"></param>
    /// <param name="f">前半段分隔符,'-' </param>
    /// <param name="e">后半段分隔符,'-' </param>
    public static string ToDefaultTimeString(this DateTime time, string f, string e)
    {
        var map = new Dictionary<string, string> { { "-", f }, { ":", e } };
        return new Regex(@"(-)|(:)").Replace(time.ToString($"yyyy-MM-dd HH:mm:ss"), m => map[m.Value]);
    }


    /// <summary>
    /// 转为默认时间格式字符串(yyyy-MM-dd HH:mm:ss.fff)
    /// </summary>
    /// <param name="time"></param>
    public static string ToDefaultMilliString(this DateTime time)
    {
        return time.ToString($"yyyy-MM-dd HH:mm:ss.fff");
    }
    /// <summary>
    /// 转为默认时间格式字符串(yyyy-MM-dd HH:mm:ss.fff)
    /// </summary>
    /// <param name="time"></param>
    /// <param name="f">前段的分隔符,'-' </param>
    public static string ToDefaultMilliString(this DateTime time, string f)
    {
        return time.ToString($"yyyy-MM-dd HH:mm:ss.fff").Replace("-", f);
    }
    /// <summary>
    /// 转为默认时间格式字符串(yyyy-MM-dd HH:mm:ss.fff)
    /// </summary>
    /// <param name="time"></param>
    /// <param name="f">前段的分隔符,'-' </param>
    /// <param name="m">中段的分隔符,':' </param>
    public static string ToDefaultMilliString(this DateTime time, string f, string m)
    {
        var map = new Dictionary<string, string> { { "-", f }, { ":", m } };
        return new Regex(@"(-)|(:)").Replace(time.ToString($"yyyy-MM-dd HH:mm:ss.fff"), m => map[m.Value]);
    }
    /// <summary>
    /// 转为默认时间格式字符串(yyyy-MM-dd HH:mm:ss.fff)
    /// </summary>
    /// <param name="time"></param>
    /// <param name="f">前段的分隔符,'-' </param>
    /// <param name="m">中段的分隔符,':' </param>
    /// <param name="e">末尾的分隔符,'.'</param>
    public static string ToDefaultMilliString(this DateTime time, string f, string m, string e)
    {
        var map = new Dictionary<string, string> { { "-", f }, { ":", m }, { ".", e } };
        return new Regex(@"(-)|(:)|(\.)").Replace(time.ToString($"yyyy-MM-dd HH:mm:ss.fff"), m => map[m.Value]);
    }
}
