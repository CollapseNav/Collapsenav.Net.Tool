using System.Runtime.InteropServices;

namespace Collapsenav.Net.Tool;

public enum CronEnum
{
    Second = 0,
    Minute = 1,
    Hour = 2,
    Day = 3,
    Month = 4,
}
public class CronTool
{
    internal static Dictionary<CronEnum, int> CronStep = new() {
        { CronEnum.Second, 60 },
        { CronEnum.Minute, 60 },
        { CronEnum.Hour, 24 },
        { CronEnum.Day, 30 },
        { CronEnum.Month, 12 },
    };
    public static IEnumerable<string> CreateCron(int? len = null, CronEnum? cronEnum = CronEnum.Second)
    {
        len ??= 1;
        cronEnum ??= CronEnum.Second;
        if (len.Value <= 0) return null;
        List<string> crons = new();
        List<string> values = new();
        (int nextLen, int baseLen) = (len.Value / CronStep[cronEnum.Value], len.Value % CronStep[cronEnum.Value]);
        if (nextLen == 0)
        {
            values.Add($"0/{baseLen}");
            crons.Add(CreateCron(cronEnum.Value, values.ToArray()));
            return crons;
        }
        if (nextLen >= CronStep[cronEnum.Value + 1]) return null;

        if (baseLen == 0)
        {
            values.Add("0");
            values.Add($"0/{nextLen}");
            crons.Add(CreateCron(cronEnum.Value, values.ToArray()));
            return crons;
        }
        int round = 2;
        while ((len * (round - 1) % CronStep[cronEnum.Value]) != 0 && ((baseLen * round) <= (CronStep[cronEnum.Value] * CronStep[cronEnum.Value + 1])))
            round++;
        int cycleTime = (round - 1) * len.Value;

        //确定周期
        for (int i = 1; i < round; i++)
        {
            int itemHour = len.Value * i / CronStep[cronEnum.Value];
            int itemMins = len.Value * i % CronStep[cronEnum.Value];
            if ((itemHour - (cycleTime / CronStep[cronEnum.Value])) == 0) itemHour = 0;     //确保从0点开始
            values.Clear();
            values.Add(itemMins.ToString());
            values.Add($"{itemHour}/{cycleTime / CronStep[cronEnum.Value]}");
            crons.Add(CreateCron(cronEnum.Value, values.ToArray()));
        }
        return crons;
    }

    internal static string CreateCron(CronEnum cronEnum, params string[] values)
    {
        if (values.Length == 0)
            return null;
        var start = (int)cronEnum;
        int sum = start;
        string cron = string.Empty;
        for (var i = 0; i < start; i++)
            cron += "0 ";

        for (var i = 0; i < values.Length; i++)
        {
            cron += $"{values[i]} ";
            sum++;
        }

        for (; sum < 5; sum++)
            cron += "* ";
        cron += "?";
        return cron;
    }
}