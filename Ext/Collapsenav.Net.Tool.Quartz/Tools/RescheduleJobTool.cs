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
    public static async Task RescheduleJob(IScheduler scheduler, TriggerKey triggerKey, ITrigger trigger)
    {
        await scheduler.PauseTrigger(triggerKey);
        await scheduler.RescheduleJob(triggerKey, trigger);
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
    public static async Task RescheduleJob(IScheduler scheduler, string name, string group, int len)
    {
        await RescheduleJob(scheduler, CreateTrigger(name, group, len));
    }

    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(IScheduler scheduler, string group, int len)
    {
        await RescheduleJob(scheduler, CreateTrigger(group, len));
    }

    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(IScheduler scheduler, string name, string group, string cron)
    {
        await RescheduleJob(scheduler, CreateTrigger(name, group, cron));
    }

    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(IScheduler scheduler, string group, string cron)
    {
        await RescheduleJob(scheduler, CreateTrigger(group, cron));
    }

    /// <summary>
    /// 重设trigger
    /// </summary>
    /// <remarks>重置triggerkey 为 (Job.Name,Job.Name) 的trigger</remarks>
    public static async Task RescheduleJob(IScheduler scheduler, Type type, int len)
    {
        await RescheduleJob(scheduler, CreateTrigger(type, len));
    }

    /// <summary>
    /// 重设trigger
    /// </summary>
    /// <remarks>重置triggerkey 为 (Job.Name,Job.Name) 的trigger</remarks>
    public static async Task RescheduleJob(IScheduler scheduler, Type type, string cron)
    {
        await RescheduleJob(scheduler, CreateTrigger(type, cron));
    }


    /// <summary>
    /// 重设trigger(如果没有job,则会自动创建)
    /// </summary>
    public static async Task RescheduleJob<Job>(IScheduler scheduler, ITrigger trigger) where Job : IJob
    {
        await scheduler.PauseTrigger(trigger.Key);
        var offset = await scheduler.RescheduleJob(trigger.Key, trigger);
        if (!offset.HasValue)
        {
            await scheduler.ScheduleJob<Job>(trigger);
        }
    }

    /// <summary>
    /// 重设trigger(如果没有job,则会自动创建)
    /// </summary>
    /// <remarks>重置triggerkey 为 (Job.Name,Job.Name) 的trigger</remarks>
    public static async Task RescheduleJob<Job>(IScheduler scheduler, int len) where Job : IJob
    {
        await RescheduleJob<Job>(scheduler, CreateTrigger<Job>(len));
    }

    /// <summary>
    /// 重设trigger(如果没有job,则会自动创建)
    /// </summary>
    /// <remarks>重置triggerkey 为 (Job.Name,Job.Name) 的trigger</remarks>
    public static async Task RescheduleJob<Job>(IScheduler scheduler, string cron) where Job : IJob
    {
        await RescheduleJob<Job>(scheduler, CreateTrigger<Job>(cron));
    }
    /// <summary>
    /// 重设trigger(如果没有job,则会自动创建)
    /// </summary>
    /// <remarks>重置triggerkey 为 (Job.Name,Job.Name) 的trigger</remarks>
    public static async Task RescheduleJob<Job>(IScheduler scheduler, TriggerKey key, int len) where Job : IJob
    {
        await RescheduleJob<Job>(scheduler, CreateTrigger(key, len));
    }
    /// <summary>
    /// 重设trigger(如果没有job,则会自动创建)
    /// </summary>
    /// <remarks>重置triggerkey 为 (Job.Name,Job.Name) 的trigger</remarks>
    public static async Task RescheduleJob<Job>(IScheduler scheduler, TriggerKey key, string cron) where Job : IJob
    {
        await RescheduleJob<Job>(scheduler, CreateTrigger(key, cron));
    }
}