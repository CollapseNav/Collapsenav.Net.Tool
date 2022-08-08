using Quartz;
using Quartz.Impl.Matchers;

namespace Collapsenav.Net.Tool.Ext;

public static partial class QuartzTool
{
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJobs(this IScheduler scheduler, IEnumerable<ITrigger> triggers)
    {
        if (triggers.IsEmpty())
            return;
        foreach (var trigger in triggers)
            await scheduler.RescheduleJob(trigger, trigger.Key);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, ITrigger trigger, TriggerKey triggerKey = null)
    {
        triggerKey ??= trigger.Key;
        await scheduler.PauseTrigger(triggerKey);
        await scheduler.RescheduleJob(triggerKey, trigger);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, ITrigger trigger, string name, string group = null)
    {
        if (group.IsEmpty())
        {
            // 查询group下的所有trigger
            var keys = await scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals(name));
            if (keys.IsEmpty())
                return;
            // 暂停所有group
            foreach (var key in keys)
                await scheduler.PauseTrigger(key);
            // 只重设一个trigger
            await scheduler.RescheduleJob(trigger, keys.First());
        }
        else
            await scheduler.RescheduleJob(trigger, new TriggerKey(name, group));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, int len, TriggerKey key)
    {
        await scheduler.RescheduleJob(key, CreateTrigger(len, key));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, string cron, TriggerKey key)
    {
        await scheduler.RescheduleJob(key, CreateTrigger(cron, key));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, int len, string name, string group = null)
    {
        await scheduler.RescheduleJob(CreateTrigger(len, name, group), name, group);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, string cron, string name, string group = null)
    {
        await scheduler.RescheduleJob(CreateTrigger(cron, name, group), name, group);
    }



    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJobs(this IScheduler scheduler, Type type, IEnumerable<ITrigger> triggers)
    {
        if (triggers.IsEmpty())
            return;
        foreach (var trigger in triggers)
            await scheduler.RescheduleJob(type, trigger, trigger.Key);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, ITrigger trigger, TriggerKey triggerKey = null)
    {
        triggerKey ??= trigger.Key;
        await scheduler.PauseTrigger(triggerKey);
        var offset = await scheduler.RescheduleJob(trigger.Key, trigger);
        if (!offset.HasValue)
            await scheduler.ScheduleJob(type, trigger);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, ITrigger trigger, string name, string group = null)
    {
        if (group.IsEmpty())
        {
            // 查询group下的所有trigger
            var keys = await scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals(name));
            if (keys.IsEmpty())
                return;
            // 暂停所有group
            foreach (var key in keys)
                await scheduler.PauseTrigger(key);
            // 只重设一个trigger
            await scheduler.RescheduleJob(type, trigger, keys.First());
        }
        else
            await scheduler.RescheduleJob(type, trigger, new TriggerKey(name, group));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, int len, TriggerKey key)
    {
        await scheduler.RescheduleJob(type, CreateTrigger(len, key), key);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, string cron, TriggerKey key)
    {
        await scheduler.RescheduleJob(type, CreateTrigger(cron, key), key);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, int len, string name = null, string group = null)
    {
        name ??= type.Name;
        await scheduler.RescheduleJob(type, CreateTrigger(len, name, group), name, group);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, string cron, string name = null, string group = null)
    {
        name ??= type.Name;
        await scheduler.RescheduleJob(type, CreateTrigger(cron, name, group), name, group);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, IEnumerable<string> crons)
    {
        await scheduler.RescheduleJobs(type, CreateTriggers(crons, type));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, params int[] lens)
    {
        await scheduler.RescheduleJobs(type, CreateTriggers(lens, type));
    }


    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJobs<Job>(this IScheduler scheduler, IEnumerable<ITrigger> triggers) where Job : IJob
    {
        if (triggers.IsEmpty())
            return;
        foreach (var trigger in triggers)
            await RescheduleJob<Job>(scheduler, trigger, trigger.Key);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, ITrigger trigger, TriggerKey triggerKey = null) where Job : IJob
    {
        triggerKey ??= trigger.Key;
        await scheduler.PauseTrigger(triggerKey);
        var offset = await scheduler.RescheduleJob(trigger.Key, trigger);
        if (!offset.HasValue)
            await scheduler.ScheduleJob<Job>(trigger);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, ITrigger trigger, string name, string group = null) where Job : IJob
    {
        await scheduler.RescheduleJob(typeof(Job), trigger, name, group);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, int len, TriggerKey key) where Job : IJob
    {
        await RescheduleJob<Job>(scheduler, CreateTrigger(len, key), key);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, string cron, TriggerKey key) where Job : IJob
    {
        await RescheduleJob<Job>(scheduler, CreateTrigger(cron, key), key);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, int len, string name = null, string group = null) where Job : IJob
    {
        name ??= typeof(Job).Name;
        await RescheduleJob<Job>(scheduler, CreateTrigger(len, name, group), name, group);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, string name, string group = null, string cron = null) where Job : IJob
    {
        name ??= typeof(Job).Name;
        await RescheduleJob<Job>(scheduler, CreateTrigger(cron, name, group), name, group);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, IEnumerable<string> crons) where Job : IJob
    {
        await scheduler.RescheduleJobs<Job>(CreateTriggers<Job>(crons));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, params int[] lens) where Job : IJob
    {
        await scheduler.RescheduleJobs<Job>(CreateTriggers<Job>(lens));
    }
}