using Quartz;
using Quartz.Impl.Matchers;

namespace Collapsenav.Net.Tool.Ext;

public partial class QuartzTool
{
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(IScheduler scheduler, ITrigger trigger)
    {
        await scheduler.PauseTrigger(trigger.Key);
        await scheduler.RescheduleJob(trigger.Key, trigger);
    }

    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(IScheduler scheduler, IEnumerable<ITrigger> triggers)
    {
        if (triggers.IsEmpty())
            return;
        foreach (var trigger in triggers)
        {
            await scheduler.PauseTrigger(trigger.Key);
            await scheduler.RescheduleJob(trigger.Key, trigger);
        }
    }

    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(IScheduler scheduler, string name, string group, ITrigger trigger)
    {
        await scheduler.PauseTrigger(new TriggerKey(name, group));
        await scheduler.RescheduleJob(new TriggerKey(name, group), trigger);
    }

    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(IScheduler scheduler, string group, ITrigger trigger)
    {
        var keys = await scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals(group));
        if (keys.IsEmpty())
            return;
        foreach (var key in keys)
            await scheduler.PauseTrigger(key);
        await scheduler.RescheduleJob(keys.First(), trigger);
    }


    /// <summary>
    /// 重设trigger
    /// </summary>
    /// <remarks>重置triggerkey 为 (Job.Name,Job.Name) 的trigger</remarks>
    public static async Task RescheduleJob<Job>(IScheduler scheduler, int len) where Job : IJob
    {
        await scheduler.RescheduleJob<Job>(CreateTrigger<Job>(len));
    }

    /// <summary>
    /// 重设trigger
    /// </summary>
    /// <remarks>重置triggerkey 为 (Job.Name,Job.Name) 的trigger</remarks>
    public static async Task RescheduleJob<Job>(IScheduler scheduler, string cron) where Job : IJob
    {
        await scheduler.RescheduleJob<Job>(CreateTrigger<Job>(cron));
    }
}