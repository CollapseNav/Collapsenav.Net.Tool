using System;

namespace Collapsenav.Net.Tool
{
    public partial class DateTimeTool
    {
        /// <summary>
        /// DateTime 转时间戳
        /// </summary>
        public static long ToTimeStamp(DateTime time)
        {
            return new DateTimeOffset(time.ToUniversalTime()).ToUnixTimeMilliseconds();
        }
        /// <summary>
        /// 时间戳转 DateTime
        /// </summary>
        public static DateTime ToDateTime(long? timestamp = null)
        {
            if (!timestamp.HasValue || timestamp <= 0) return DateTime.Now;
            return DateTimeOffset.FromUnixTimeMilliseconds(timestamp.Value).UtcDateTime;
        }
        /// <summary>
        /// 保留时间到 某个指定的leve
        /// </summary>
        public static DateTime TransformDateTime(DateTime time, DateLevel level)
        {
            var trans = level switch
            {
                DateLevel.Year => DateTime.MinValue.AddYears(time.Year - 1),
                DateLevel.Month => DateTime.MinValue.AddYears(time.Year - 1).AddMonths(time.Month - 1),
                DateLevel.Day => time.Date,
                DateLevel.Hour => time.Date.AddHours(time.Hour),
                DateLevel.Minute => time.Date.AddHours(time.Hour).AddMinutes(time.Minute),
                DateLevel.Second => time.Date.AddHours(time.Hour).AddMinutes(time.Minute).AddSeconds(time.Second),
                DateLevel.Millisecond => time.Date.AddHours(time.Hour).AddMinutes(time.Minute).AddSeconds(time.Second).AddMilliseconds(time.Millisecond),
                _ => time
            };
            return trans;
        }

        /// <summary>
        /// 转为默认日期格式字符串(yyyy-MM-dd)
        /// </summary>
        /// <param name="date"></param>
        /// <param name="s">中间的分隔符,默认为 '-' </param>
        public static string ToDefaultDateString(DateTime date, string s = "-")
        {
            return date.ToString($"yyyy-MM-dd").Replace("-", s);
        }

        /// <summary>
        /// 转为默认日期格式字符串(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <param name="time"></param>
        /// <param name="s">中间的分隔符,默认为 '-' </param>
        public static string ToDefaultTimeString(DateTime time, string s = "-")
        {
            return time.ToString($"yyyy-MM-dd HH:mm:ss").Replace("-", s);
        }
    }
}
