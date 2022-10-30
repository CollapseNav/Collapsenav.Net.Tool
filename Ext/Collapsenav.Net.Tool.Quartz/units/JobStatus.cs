using Quartz;
using Quartz.Impl.Triggers;

namespace Collapsenav.Net.Tool.Ext;

/// <summary>
/// Job 状态
/// </summary>
public class JobStatus
{
    /// <summary>
    /// 根据 jobDetail 和 triggers 生成 JobStatus 
    /// </summary>
    public static JobStatus GenJobStatus(IJobDetail jobDetail, IEnumerable<ITrigger> triggers)
    {
        var crons = triggers?.Where(item => item is CronTriggerImpl).Select(item => (item as CronTriggerImpl).CronExpressionString);
        var lens = triggers?.Where(item => item is SimpleTriggerImpl).Select(item => (item as SimpleTriggerImpl).RepeatInterval.Seconds);
        var status = new JobStatus
        {
            Job = jobDetail,
            Triggers = triggers,
            Crons = crons.IsEmpty() ? null : crons,
            Lens = lens.IsEmpty() ? null : lens,
        };
        return status;
    }
    public JobKey Key { get => Job.Key; }
    public string JobId { get => $"{Key?.Group}.{Key?.Name}"; }
    public string Description { get => Job.Description; }
    public IJobDetail Job { get; set; }
    public IEnumerable<ITrigger> Triggers { get; set; }
    /// <summary>
    /// Crons表达式
    /// </summary>
    public IEnumerable<string> Crons { get; protected set; }
    /// <summary>
    /// Simple 间隔
    /// </summary>
    public IEnumerable<int> Lens { get; protected set; }
}