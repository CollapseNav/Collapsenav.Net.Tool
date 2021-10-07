using System;
using System.Linq;
using Xunit;

namespace Collapsenav.Net.Tool.Test
{
    public class DateTimeTest
    {
        [Fact]
        public void TimeStampTest()
        {
            DateTime now = DateTime.Now.ToUniversalTime();
            Assert.True(now.ToString() == now.ToTimestamp().ToDateTime().ToString());
        }

        [Fact]
        public void ToDateLevelTest()
        {
            DateTime now = new DateTime(2010, 10, 10, 10, 10, 10, 10);
            var tampTime = now;
            Assert.True(now.ToMillisecond() == tampTime);
            tampTime = tampTime.AddMilliseconds(-10);
            Assert.True(now.ToSecond() == tampTime);
            tampTime = tampTime.AddSeconds(-10);
            Assert.True(now.ToMinute() == tampTime);
            tampTime = tampTime.AddMinutes(-10);
            Assert.True(now.ToHour() == tampTime);
            tampTime = tampTime.AddHours(-10);
            Assert.True(now.ToDay() == tampTime);
            tampTime = tampTime.AddDays(-10 + 1);
            Assert.True(now.ToMonth() == tampTime);
            tampTime = tampTime.AddMonths(-10 + 1);
            Assert.True(now.ToYear() == tampTime);
            Assert.True(now.To(DateLevel.Year) == tampTime);
        }
    }
}
