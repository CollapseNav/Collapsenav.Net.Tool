using Quartz;

namespace Collapsenav.Net.Tool.Ext;

public static partial class QuartzTool
{
    /// <summary>
    /// 添加一个使用和trigger.Key相同的Job
    /// </summary>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, ITrigger trigger) where Job : IJob
        => await scheduler.ScheduleJob(CreateJob<Job>(new JobKey(trigger.Key.Name, trigger.Key.Group)), trigger);
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, object cron, JobKey jkey = null, TriggerKey tkey = null) where Job : IJob
        => await scheduler.ScheduleJob(CreateJob<Job>(jkey), tkey == null ? CreateTrigger<Job>(cron) : CreateTrigger(cron, tkey));
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, object cron, string name, string group = null) where Job : IJob
        => await scheduler.ScheduleJob(CreateJob<Job>(name, group.IsEmpty(name)), CreateTrigger(cron, name, group.IsEmpty(name)));

    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, ITrigger trigger)
        => await scheduler.ScheduleJob(CreateJob(type, new JobKey(trigger.Key.Name, trigger.Key.Group)), trigger);
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, object cron, JobKey jkey = null, TriggerKey tkey = null)
        => await scheduler.ScheduleJob(CreateJob(type, jkey), tkey == null ? CreateTrigger(cron, type) : CreateTrigger(cron, tkey));
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, object cron, string name, string group = null)
        => await scheduler.ScheduleJob(CreateJob(type, name, group), CreateTrigger(cron, name, group));
    /// <summary>
    /// 根据 crons 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs<Job>(this IScheduler scheduler, IEnumerable<object> crons, string name = null, string group = null) where Job : IJob
    {
        var triggers = CreateTriggers(crons, name.IsEmpty(typeof(Job).Name), group);
        foreach (var trigger in triggers)
            await scheduler.ScheduleJob(CreateJob<Job>(trigger.Key.Name, trigger.Key.Group), trigger);
    }
    /// <summary>
    /// 根据 crons 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs<Job>(this IScheduler scheduler, IEnumerable<int> crons, string name = null, string group = null) where Job : IJob
    {
        var triggers = CreateTriggers(crons, name.IsEmpty(typeof(Job).Name), group);
        foreach (var trigger in triggers)
            await scheduler.ScheduleJob(CreateJob<Job>(trigger.Key.Name, trigger.Key.Group), trigger);
    }
    /// <summary>
    /// 根据 crons 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs(this IScheduler scheduler, Type type, IEnumerable<object> crons, string name = null, string group = null)
    {
        var triggers = CreateTriggers(crons, name.IsEmpty(type.Name), group);
        foreach (var trigger in triggers)
            await scheduler.ScheduleJob(CreateJob(type, trigger.Key.Name, trigger.Key.Group), trigger);
    }
    /// <summary>
    /// 根据 crons 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs(this IScheduler scheduler, Type type, IEnumerable<int> crons, string name = null, string group = null)
    {
        var triggers = CreateTriggers(crons, name.IsEmpty(type.Name), group);
        foreach (var trigger in triggers)
            await scheduler.ScheduleJob(CreateJob(type, trigger.Key.Name, trigger.Key.Group), trigger);
    }
}
