using Quartz;
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
        await scheduler.PauseTrigger(triggerKey ?? trigger.Key);
        await scheduler.RescheduleJob(triggerKey ?? trigger.Key, trigger);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, ITrigger trigger, string name, string group = null)
    {
        if (group.IsEmpty())
        {
            // 查询group下的所有trigger
            var keys = await scheduler.GetJobKeys(name);
            if (keys.IsEmpty())
                return;
            await scheduler.PauseJobs(keys);
            // 只重设一个trigger
            await scheduler.RescheduleJob(trigger, keys.First().Name, keys.First().Group);
        }
        else
            await scheduler.RescheduleJob(trigger, new TriggerKey(name, group));
    }

    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, object cron, TriggerKey key)
        => await scheduler.RescheduleJob(key, CreateTrigger(cron, key));
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, object cron, string name, string group = null)
        => await scheduler.RescheduleJob(CreateTrigger(cron, name, group), name, group);


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
        await scheduler.PauseTrigger(triggerKey ?? trigger.Key);
        var offset = await scheduler.RescheduleJob(trigger.Key, trigger);
        // 如果不存在对应的job, 则建一个
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
            var keys = await scheduler.GetJobKeys(name);
            if (keys.IsEmpty())
                return;
            await scheduler.PauseJobs(keys);
            // 只重设一个trigger
            await scheduler.RescheduleJob(type, trigger, keys.First().Name, keys.First().Group);
        }
        else
            await scheduler.RescheduleJob(type, trigger, new TriggerKey(name, group));
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, object cron, TriggerKey key)
    {
        await scheduler.RescheduleJob(type, CreateTrigger(cron, key), key);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, object cron, string name = null, string group = null)
    {
        name ??= type.Name;
        await scheduler.RescheduleJob(type, CreateTrigger(cron, name, group), name, group);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJobs(this IScheduler scheduler, Type type, params object[] lens)
    {
        await scheduler.RescheduleJobs(type, CreateTriggers(type, lens));
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
        await scheduler.PauseTrigger(triggerKey ?? trigger.Key);
        var offset = await scheduler.RescheduleJob(trigger.Key, trigger);
        if (!offset.HasValue)
            await scheduler.ScheduleJob<Job>(trigger);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, ITrigger trigger, string name, string group = null) where Job : IJob
        => await scheduler.RescheduleJob(typeof(Job), trigger, name, group);
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, object cron, TriggerKey key) where Job : IJob
        => await RescheduleJob<Job>(scheduler, CreateTrigger(cron, key), key);
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, object cron, string name = null, string group = null) where Job : IJob
        => await RescheduleJob<Job>(scheduler, CreateTrigger(cron, name.IsEmpty(typeof(Job).Name), group), name.IsEmpty(typeof(Job).Name), group);
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJobs<Job>(this IScheduler scheduler, params object[] lens) where Job : IJob
        => await scheduler.RescheduleJobs<Job>(CreateTriggers<Job>(lens));
}