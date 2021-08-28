using System;

namespace Collapsenav.Net.Tool
{
    public static class DateTimeExt
    {
        public static long ToTimestamp(this DateTime time)
        {
            return new DateTimeOffset(time).ToUnixTimeMilliseconds();
        }

        public static DateTime ToDateTime(this long timestamp)
        {
            if (timestamp <= 0) return DateTime.Now;
            return DateTimeOffset.FromUnixTimeMilliseconds(timestamp).DateTime;
        }
        /// <summary>
        /// 获取到年
        /// </summary>
        public static DateTime ToYear(this DateTime time) => new(time.Year, 0, 0);

        /// <summary>
        /// 获取到月
        /// </summary>
        public static DateTime ToMonth(this DateTime time) => new(time.Year, time.Month, 0);

        /// <summary>
        /// 获取到天
        /// </summary>
        public static DateTime ToDay(this DateTime time) => new(time.Year, time.Month, time.Day);

        /// <summary>
        /// 获取到小时
        /// </summary>
        public static DateTime ToHour(this DateTime time) => new(time.Year, time.Month, time.Day, time.Hour, 0, 0);

        /// <summary>
        /// 获取到分钟
        /// </summary>
        public static DateTime ToMinute(this DateTime time) => new(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0);

        /// <summary>
        /// 获取到秒
        /// </summary>
        public static DateTime ToSecond(this DateTime time) => new(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second);

        /// <summary>
        /// 获取到毫秒
        /// </summary>
        public static DateTime ToMillisecond(this DateTime time) => new(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second, time.Millisecond);

    }
}
