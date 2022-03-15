namespace Collapsenav.Net.Tool;
public static partial class DateTimeExt
{
    /// <summary>
    /// DateTime转为时间戳
    /// </summary>
    public static long ToTimestamp(this DateTime time)
    {
        return new DateTimeOffset(time.ToUniversalTime()).ToUnixTimeMilliseconds();
    }
    /// <summary>
    /// 时间戳转为DateTime
    /// </summary>
    public static DateTime ToDateTime(this long timestamp)
    {
        return timestamp <= 0 ? DateTime.Now : DateTimeOffset.FromUnixTimeMilliseconds(timestamp).UtcDateTime;
    }
    /// <summary>
    /// 转为默认日期格式字符串(yyyy-MM-dd)
    /// </summary>
    /// <param name="date"></param>
    /// <param name="s">中间的分隔符,默认为 '-' </param>
    public static string ToDefaultDateString(this DateTime date, string s = "-")
    {
        return date.ToString($"yyyy-MM-dd").Replace("-", s);
    }
    /// <summary>
    /// 转为默认时间格式字符串(yyyy-MM-dd HH:mm:ss)
    /// </summary>
    /// <param name="time"></param>
    /// <param name="s">中间的分隔符,默认为 '-' </param>
    public static string ToDefaultTimeString(this DateTime time, string s = "-")
    {
        return time.ToString($"yyyy-MM-dd HH:mm:ss").Replace("-", s);
    }
    /// <summary>
    /// 转为默认时间格式字符串(yyyy-MM-dd HH:mm:ss.fff)
    /// </summary>
    /// <param name="time"></param>
    /// <param name="s">中间的分隔符,默认为 '-' </param>
    public static string ToDefaultMilliString(this DateTime time, string s = "-")
    {
        return time.ToString($"yyyy-MM-dd HH:mm:ss.fff").Replace("-", s);
    }
}
