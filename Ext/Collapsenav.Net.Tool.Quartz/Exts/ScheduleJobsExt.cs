using Quartz;

namespace Collapsenav.Net.Tool.Ext;

public static partial class QuartzTool
{
    /// <summary>
    /// 添加一个使用和trigger.Key相同的Job
    /// </summary>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, ITrigger trigger) where Job : IJob
    {
        await scheduler.ScheduleJob(CreateJob<Job>(new JobKey(trigger.Key.Name, trigger.Key.Group)), trigger);
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, object cron, JobKey jkey = null, TriggerKey tkey = null) where Job : IJob
    {
        var jobDetail = jkey == null ? CreateJob<Job>() : CreateJob<Job>(jkey);
        var trigger = tkey == null ? CreateTrigger<Job>(cron) : CreateTrigger(cron, tkey);
        await scheduler.ScheduleJob(jobDetail, trigger);
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, object cron, string name, string group = null) where Job : IJob
    {
        group ??= name;
        await scheduler.ScheduleJob(CreateJob<Job>(name, group), CreateTrigger(cron, name, group));
    }

    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, ITrigger trigger)
    {
        await scheduler.ScheduleJob(CreateJob(type, new JobKey(trigger.Key.Name, trigger.Key.Group)), trigger);
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, object cron, JobKey jkey = null, TriggerKey tkey = null)
    {
        var jobDetail = jkey == null ? CreateJob(type) : CreateJob(type, jkey);
        var trigger = tkey == null ? CreateTrigger(cron, type) : CreateTrigger(cron, tkey);
        await scheduler.ScheduleJob(jobDetail, trigger);
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, object cron, string name, string group = null)
    {
        group ??= name;
        await scheduler.ScheduleJob(CreateJob(type, name, group), CreateTrigger(cron, name, group));
    }
    /// <summary>
    /// 根据 crons 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs<Job>(this IScheduler scheduler, IEnumerable<object> crons, string name = null, string group = null) where Job : IJob
    {
        name ??= typeof(Job).Name;
        group ??= name;
        var triggers = CreateTriggers(crons, name, group);
        foreach (var trigger in triggers)
        {
            var job = CreateJob<Job>(trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }
    /// <summary>
    /// 根据 crons 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs<Job>(this IScheduler scheduler, IEnumerable<int> crons, string name = null, string group = null) where Job : IJob
    {
        name ??= typeof(Job).Name;
        group ??= name;
        var triggers = CreateTriggers(crons, name, group);
        foreach (var trigger in triggers)
        {
            var job = CreateJob<Job>(trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }
    /// <summary>
    /// 根据 crons 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs(this IScheduler scheduler, Type type, IEnumerable<object> crons, string name = null, string group = null)
    {
        name ??= type.Name;
        group ??= name;
        var triggers = CreateTriggers(crons, name, group);
        foreach (var trigger in triggers)
        {
            var job = CreateJob(type, trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }
    /// <summary>
    /// 根据 crons 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs(this IScheduler scheduler, Type type, IEnumerable<int> crons, string name = null, string group = null)
    {
        name ??= type.Name;
        group ??= name;
        var triggers = CreateTriggers(crons, name, group);
        foreach (var trigger in triggers)
        {
            var job = CreateJob(type, trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}