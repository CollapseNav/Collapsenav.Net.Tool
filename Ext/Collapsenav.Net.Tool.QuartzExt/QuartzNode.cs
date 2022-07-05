using Quartz;

namespace Collapsenav.Net.Tool.Ext;

public class QuartzNode
{
    public static IScheduler Scheduler;
    public static QuartzJobBuilder Builder;
}

public class JobItem
{
    public Type JobType { get; set; }
    public JobKey JKey { get => jKey ?? new JobKey(JobType.Name, JobType.Name); set => jKey = value; }
    private JobKey jKey;
    public TriggerKey TKey { get => tKey ?? new TriggerKey(JobType.Name, JobType.Name); set => tKey = value; }
    private TriggerKey tKey;
}

public class CronJob : JobItem
{
    public string Cron { get; set; }
}
public class SimpleJob : JobItem
{
    public int Len { get; set; }
}

