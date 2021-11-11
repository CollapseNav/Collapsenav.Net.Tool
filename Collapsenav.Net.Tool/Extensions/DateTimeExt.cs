using System;

namespace Collapsenav.Net.Tool
{
    public static partial class DateTimeExt
    {
        public static long ToTimestamp(this DateTime time)
        {
            return DateTools.ToTimeStamp(time);
        }

        public static DateTime ToDateTime(this long timestamp)
        {
            return DateTools.ToDateTime(timestamp);
        }
        /// <summary>
        /// 获取到年
        /// </summary>
        public static DateTime ToYear(this DateTime time) => DateTools.TransformDateTime(time, DateLevel.Year);

        /// <summary>
        /// 获取到月
        /// </summary>
        public static DateTime ToMonth(this DateTime time) => DateTools.TransformDateTime(time, DateLevel.Month);

        /// <summary>
        /// 获取到天
        /// </summary>
        public static DateTime ToDay(this DateTime time) => DateTools.TransformDateTime(time, DateLevel.Day);

        /// <summary>
        /// 获取到小时
        /// </summary>
        public static DateTime ToHour(this DateTime time) => DateTools.TransformDateTime(time, DateLevel.Hour);

        /// <summary>
        /// 获取到分钟
        /// </summary>
        public static DateTime ToMinute(this DateTime time) => DateTools.TransformDateTime(time, DateLevel.Minute);

        /// <summary>
        /// 获取到秒
        /// </summary>
        public static DateTime ToSecond(this DateTime time) => DateTools.TransformDateTime(time, DateLevel.Second);

        /// <summary>
        /// 获取到毫秒
        /// </summary>
        public static DateTime ToMillisecond(this DateTime time) => DateTools.TransformDateTime(time, DateLevel.Millisecond);
        public static DateTime To(this DateTime time, DateLevel level) => DateTools.TransformDateTime(time, level);

    }
}
