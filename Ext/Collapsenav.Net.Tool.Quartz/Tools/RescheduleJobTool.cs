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
        await RescheduleJob(scheduler, trigger.Key, trigger);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJobs(IScheduler scheduler, IEnumerable<ITrigger> triggers)
    {
        if (triggers.IsEmpty())
            return;
        foreach (var trigger in triggers)
            await RescheduleJob(scheduler, trigger.Key, trigger);
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
        await RescheduleJob(scheduler, new TriggerKey(name, group), trigger);
    }
    /// <summary>
    /// 重设trigger, 根据group查询
    /// </summary>
    public static async Task RescheduleJob(IScheduler scheduler, string group, ITrigger trigger)
    {
        // 查询group下的所有trigger
        var keys = await scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals(group));
        if (keys.IsEmpty())
            return;
        // 暂停所有group
        foreach (var key in keys)
            await scheduler.PauseTrigger(key);
        // 只重设一个trigger
        await RescheduleJob(scheduler, keys.First(), trigger);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(IScheduler scheduler, TriggerKey key, int len)
    {
        await RescheduleJob(scheduler, key, CreateTrigger(key, len));
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
    public static async Task RescheduleJob(IScheduler scheduler, TriggerKey key, string cron)
    {
        await RescheduleJob(scheduler, key, CreateTrigger(key, cron));
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
    public static async Task RescheduleJob(IScheduler scheduler, Type type, ITrigger trigger)
    {
        await RescheduleJob(scheduler, type, trigger.Key, trigger);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJobs(IScheduler scheduler, Type type, IEnumerable<ITrigger> triggers)
    {
        if (triggers.IsEmpty())
            return;
        foreach (var trigger in triggers)
            await RescheduleJob(scheduler, type, trigger.Key, trigger);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(IScheduler scheduler, Type type, TriggerKey triggerKey, ITrigger trigger)
    {
        await scheduler.PauseTrigger(triggerKey);
        var offset = await scheduler.RescheduleJob(trigger.Key, trigger);
        if (!offset.HasValue)
            await scheduler.ScheduleJob(type, trigger);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(IScheduler scheduler, Type type, string name, string group, ITrigger trigger)
    {
        await RescheduleJob(scheduler, type, new TriggerKey(name, group), trigger);
    }
    /// <summary>
    /// 重设trigger, 根据group查询
    /// </summary>
    public static async Task RescheduleJob(IScheduler scheduler, Type type, string group, ITrigger trigger)
    {
        // 查询group下的所有trigger
        var keys = await scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals(group));
        if (keys.IsEmpty())
            return;
        // 暂停所有group
        foreach (var key in keys)
            await scheduler.PauseTrigger(key);
        // 只重设一个trigger
        await RescheduleJob(scheduler, type, keys.First(), trigger);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(IScheduler scheduler, Type type, TriggerKey key, int len)
    {
        await RescheduleJob(scheduler, type, key, CreateTrigger(key, len));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(IScheduler scheduler, Type type, string name, string group, int len)
    {
        await RescheduleJob(scheduler, type, CreateTrigger(name, group, len));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(IScheduler scheduler, Type type, string group, int len)
    {
        await RescheduleJob(scheduler, type, CreateTrigger(group, len));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(IScheduler scheduler, Type type, TriggerKey key, string cron)
    {
        await RescheduleJob(scheduler, type, key, CreateTrigger(key, cron));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(IScheduler scheduler, Type type, string name, string group, string cron)
    {
        await RescheduleJob(scheduler, type, CreateTrigger(name, group, cron));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(IScheduler scheduler, Type type, string group, string cron)
    {
        await RescheduleJob(scheduler, type, CreateTrigger(group, cron));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(IScheduler scheduler, Type type, int len)
    {
        await RescheduleJob(scheduler, type, CreateTrigger(type, len));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(IScheduler scheduler, Type type, string cron)
    {
        await RescheduleJob(scheduler, type, CreateTrigger(type, cron));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(IScheduler scheduler, Type type, IEnumerable<string> crons)
    {
        await RescheduleJobs(scheduler, type, CreateTriggers(type, crons));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(IScheduler scheduler, Type type, params int[] lens)
    {
        await RescheduleJobs(scheduler, type, CreateTriggers(type, lens));
    }



    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(IScheduler scheduler, ITrigger trigger) where Job : IJob
    {
        await RescheduleJob<Job>(scheduler, trigger.Key, trigger);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJobs<Job>(IScheduler scheduler, IEnumerable<ITrigger> triggers) where Job : IJob
    {
        if (triggers.IsEmpty())
            return;
        foreach (var trigger in triggers)
            await RescheduleJob<Job>(scheduler, trigger.Key, trigger);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(IScheduler scheduler, TriggerKey triggerKey, ITrigger trigger) where Job : IJob
    {
        await scheduler.PauseTrigger(triggerKey);
        var offset = await scheduler.RescheduleJob(trigger.Key, trigger);
        if (!offset.HasValue)
            await scheduler.ScheduleJob<Job>(trigger);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(IScheduler scheduler, string name, string group, ITrigger trigger) where Job : IJob
    {
        await RescheduleJob<Job>(scheduler, new TriggerKey(name, group), trigger);
    }
    /// <summary>
    /// 重设trigger, 根据group查询
    /// </summary>
    public static async Task RescheduleJob<Job>(IScheduler scheduler, string group, ITrigger trigger) where Job : IJob
    {
        // 查询group下的所有trigger
        var keys = await scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals(group));
        if (keys.IsEmpty())
            return;
        // 暂停所有group
        foreach (var key in keys)
            await scheduler.PauseTrigger(key);
        // 只重设一个trigger
        await RescheduleJob<Job>(scheduler, keys.First(), trigger);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(IScheduler scheduler, TriggerKey key, int len) where Job : IJob
    {
        await RescheduleJob<Job>(scheduler, key, CreateTrigger(key, len));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(IScheduler scheduler, string name, string group, int len) where Job : IJob
    {
        await RescheduleJob<Job>(scheduler, CreateTrigger(name, group, len));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(IScheduler scheduler, string group, int len) where Job : IJob
    {
        await RescheduleJob<Job>(scheduler, CreateTrigger(group, len));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(IScheduler scheduler, TriggerKey key, string cron) where Job : IJob
    {
        await RescheduleJob<Job>(scheduler, key, CreateTrigger(key, cron));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(IScheduler scheduler, string name, string group, string cron) where Job : IJob
    {
        await RescheduleJob<Job>(scheduler, CreateTrigger(name, group, cron));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(IScheduler scheduler, string group, string cron) where Job : IJob
    {
        await RescheduleJob<Job>(scheduler, CreateTrigger(group, cron));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(IScheduler scheduler, int len) where Job : IJob
    {
        await RescheduleJob<Job>(scheduler, CreateTrigger<Job>(len));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(IScheduler scheduler, string cron) where Job : IJob
    {
        await RescheduleJob<Job>(scheduler, CreateTrigger<Job>(cron));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(IScheduler scheduler, IEnumerable<string> crons) where Job : IJob
    {
        await RescheduleJobs<Job>(scheduler, CreateTriggers<Job>(crons));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(IScheduler scheduler, params int[] lens) where Job : IJob
    {
        await RescheduleJobs<Job>(scheduler, CreateTriggers<Job>(lens));
    }
}