namespace Collapsenav.Net.Tool;
public static partial class DateTimeExt
{
    public static long ToTimestamp(this DateTime time)
    {
        return DateTimeTool.ToTimeStamp(time);
    }

    public static DateTime ToDateTime(this long timestamp)
    {
        return DateTimeTool.ToDateTime(timestamp);
    }
    /// <summary>
    /// 获取到年
    /// </summary>
    public static DateTime ToYear(this DateTime time) => DateTimeTool.TransformDateTime(time, DateLevel.Year);

    /// <summary>
    /// 获取到月
    /// </summary>
    public static DateTime ToMonth(this DateTime time) => DateTimeTool.TransformDateTime(time, DateLevel.Month);

    /// <summary>
    /// 获取到天
    /// </summary>
    public static DateTime ToDay(this DateTime time) => DateTimeTool.TransformDateTime(time, DateLevel.Day);

    /// <summary>
    /// 获取到小时
    /// </summary>
    public static DateTime ToHour(this DateTime time) => DateTimeTool.TransformDateTime(time, DateLevel.Hour);

    /// <summary>
    /// 获取到分钟
    /// </summary>
    public static DateTime ToMinute(this DateTime time) => DateTimeTool.TransformDateTime(time, DateLevel.Minute);

    /// <summary>
    /// 获取到秒
    /// </summary>
    public static DateTime ToSecond(this DateTime time) => DateTimeTool.TransformDateTime(time, DateLevel.Second);

    /// <summary>
    /// 获取到毫秒
    /// </summary>
    public static DateTime ToMillisecond(this DateTime time) => DateTimeTool.TransformDateTime(time, DateLevel.Millisecond);
    public static DateTime To(this DateTime time, DateLevel level) => DateTimeTool.TransformDateTime(time, level);

    /// <summary>
    /// 转为默认日期格式字符串(yyyy-MM-dd)
    /// </summary>
    /// <param name="date"></param>
    /// <param name="s">中间的分隔符,默认为 '-' </param>
    public static string ToDefaultDateString(this DateTime date, string s = "-")
    {
        return DateTimeTool.ToDefaultDateString(date, s);
    }
    /// <summary>
    /// 转为默认时间格式字符串(yyyy-MM-dd HH:mm:ss)
    /// </summary>
    /// <param name="time"></param>
    /// <param name="s">中间的分隔符,默认为 '-' </param>
    public static string ToDefaultTimeString(this DateTime time, string s = "-")
    {
        return DateTimeTool.ToDefaultTimeString(time, s);
    }
}
