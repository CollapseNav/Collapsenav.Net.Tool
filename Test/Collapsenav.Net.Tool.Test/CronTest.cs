using System.Linq;
using Xunit;

namespace Collapsenav.Net.Tool.Test;

public class CronTest
{
    public CronTest() { }

    [Theory]
    [InlineData(10, "0/10 * * * * ?")]
    public void SingleCronTest(int len, string cron)
    {
        var getCron = CronTool.CreateCron(len);
        Assert.True(cron == getCron);
    }

    [Theory]
    [InlineData(10, CronEnum.Second, "0/10 * * * * ?")]
    [InlineData(20, CronEnum.Second, "0/20 * * * * ?")]
    [InlineData(60, CronEnum.Second, "0 0/1 * * * ?")]
    [InlineData(90, CronEnum.Second, "30 1/3 * * * ?", "0 0/3 * * * ?")]
    [InlineData(80, CronEnum.Second, "20 1/4 * * * ?", "40 2/4 * * * ?", "0 0/4 * * * ?")]
    [InlineData(70, CronEnum.Second, "10 1/7 * * * ?", "20 2/7 * * * ?", "30 3/7 * * * ?", "40 4/7 * * * ?", "50 5/7 * * * ?", "0 0/7 * * * ?")]
    [InlineData(10, CronEnum.Minute, "0 0/10 * * * ?")]
    [InlineData(20, CronEnum.Minute, "0 0/20 * * * ?")]
    [InlineData(90, CronEnum.Minute, "0 30 1/3 * * ?", "0 0 0/3 * * ?")]
    [InlineData(80, CronEnum.Minute, "0 20 1/4 * * ?", "0 40 2/4 * * ?", "0 0 0/4 * * ?")]
    [InlineData(70, CronEnum.Minute, "0 10 1/7 * * ?", "0 20 2/7 * * ?", "0 30 3/7 * * ?", "0 40 4/7 * * ?", "0 50 5/7 * * ?", "0 0 0/7 * * ?")]
    public void CronsTest(int len, CronEnum cronType, params string[] crons)
    {
        var getCrons = CronTool.CreateCrons(len, cronType);
        Assert.True(crons.Length == getCrons.Count());
        Assert.True(getCrons.AllContain(crons));
    }
}