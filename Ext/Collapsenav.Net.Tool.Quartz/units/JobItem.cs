using Quartz;

namespace Collapsenav.Net.Tool.Ext;

public abstract class JobItem
{
    public Type JobType { get; set; }
    public JobKey JKey { get => jKey ?? new JobKey(JobType.Name, JobType.Name); set => jKey = value; }
    private JobKey jKey;
    public TriggerKey TKey { get => tKey ?? new TriggerKey(JobType.Name, JobType.Name); set => tKey = value; }
    private TriggerKey tKey;
    public abstract ITrigger GetTrigger();
    public virtual IJobDetail GetJobDetail()
    {
        return QuartzTool.CreateJob(JobType, JKey);
    }
}

public class CronJob : JobItem
{
    public string Cron { get; set; }

    public override ITrigger GetTrigger()
    {
        return QuartzTool.CreateTrigger(TKey, Cron);
    }
}
public class SimpleJob : JobItem
{
    public int Len { get; set; }

    public override ITrigger GetTrigger()
    {
        return QuartzTool.CreateTrigger(TKey, Len);
    }
}
