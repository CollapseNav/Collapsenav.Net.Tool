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
        now = now.AddMilliseconds(-now.Millisecond);
        Assert.True(now.ToString() == now.ToShortTimestamp().ToShortDateTime().ToString());
    }

    [Fact]
    public void DefaultTimeStringTest()
    {
        DateTime date = new(2021, 11, 11, 11, 11, 11);
        Assert.True(date.ToDefaultTimeString() == "2021-11-11 11:11:11");
        Assert.True(date.ToDefaultTimeString("WTF") == "2021WTF11WTF11 11:11:11");
        Assert.True(date.ToDefaultTimeString("WTF", "FFF") == "2021WTF11WTF11 11FFF11FFF11");
    }

    [Fact]
    public void DefaultDateStringTest()
    {
        DateTime date = new(2021, 11, 11, 11, 11, 11);
        Assert.True(date.ToDefaultDateString() == "2021-11-11");
        Assert.True(date.ToDefaultDateString("WTF") == "2021WTF11WTF11");
    }

    [Fact]
    public void DefaultMilliStringTest()
    {
        DateTime date = new(2021, 11, 11, 11, 11, 11, 111);
        Assert.True(date.ToDefaultMilliString() == "2021-11-11 11:11:11.111");
        Assert.True(date.ToDefaultMilliString("WTF") == "2021WTF11WTF11 11:11:11.111");
        Assert.True(date.ToDefaultMilliString("WTF", "FFF") == "2021WTF11WTF11 11FFF11FFF11.111");
        Assert.True(date.ToDefaultMilliString("WTF", "FFF", "MMM") == "2021WTF11WTF11 11FFF11FFF11MMM111");
    }
}
