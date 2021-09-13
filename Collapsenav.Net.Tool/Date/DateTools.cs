using System;

namespace Collapsenav.Net.Tool
{
    public class DateTools
    {
        public static long ToTimeStamp(DateTime time)
        {
            return new DateTimeOffset(time).ToUnixTimeMilliseconds();
        }

        public static DateTime ToDateTime(long? timestamp = null)
        {
            if (!timestamp.HasValue || timestamp <= 0) return DateTime.Now;
            return DateTimeOffset.FromUnixTimeMilliseconds(timestamp.Value).DateTime;
        }

        public static DateTime TransformDateTime(DateTime time, DateLevel level)
        {
            var trans = level switch
            {
                DateLevel.Year => new(time.Year, 0, 0),
                DateLevel.Month => new(time.Year, time.Month, 0),
                DateLevel.Day => new(time.Year, time.Month, time.Day),
                DateLevel.Hour => new(time.Year, time.Month, time.Day, time.Hour, 0, 0),
                DateLevel.Minute => new(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0),
                DateLevel.Second => new(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second),
                DateLevel.Millisecond => new(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second, time.Millisecond),
                _ => time
            };
            return trans;
        }
    }
}
