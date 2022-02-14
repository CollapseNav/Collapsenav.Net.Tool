using System;
using Xunit;

namespace Collapsenav.Net.Tool.Test;
public class DateTimeTest
{
    [Fact]
    public void TimeStampTest()
    {
        DateTime now = DateTime.Now.ToUniversalTime();
        Assert.True(now.ToString() == now.ToTimestamp().ToDateTime().ToString());
    }

    [Fact]
    public void DefaultStringTest()
    {
        DateTime date = new(2021, 11, 11, 11, 11, 11);
        Assert.True(date.ToDefaultDateString() == "2021-11-11");
        Assert.True(date.ToDefaultTimeString() == "2021-11-11 11:11:11");
        Assert.True(date.ToDefaultDateString("WTF") == "2021WTF11WTF11");
        Assert.True(date.ToDefaultTimeString("WTF") == "2021WTF11WTF11 11:11:11");
    }
}
