using Quartz;
using Quartz.Impl.Matchers;

namespace Collapsenav.Net.Tool.Ext;

public static class QuartzExt
{
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, JobKey jkey, TriggerKey tkey, string cron) where Job : IJob
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob<Job>(jkey), QuartzTool.CreateTrigger(tkey, cron));
    }

    public static async Task ScheduleJob<Job>(this IScheduler scheduler, string name, string group, string cron) where Job : IJob
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob<Job>(name, group), QuartzTool.CreateTrigger(name, group, cron));
    }

    public static async Task ScheduleJob<Job>(this IScheduler scheduler, string name, string cron) where Job : IJob
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob<Job>(name), QuartzTool.CreateTrigger(name, cron));
    }
    public static async Task ScheduleJobs<Job>(this IScheduler scheduler, string name, IEnumerable<string> crons) where Job : IJob
    {
        var jobAndTriggers = QuartzTool.CreateJobAndTriggers<Job>(name, name, crons);
        foreach (var item in jobAndTriggers)
            await scheduler.ScheduleJob(item.Job, item.Trigger);
    }
    public static async Task ScheduleJobs<Job>(this IScheduler scheduler, string name, string group, IEnumerable<string> crons) where Job : IJob
    {
        var jobAndTriggers = QuartzTool.CreateJobAndTriggers<Job>(name, group, crons);
        foreach (var item in jobAndTriggers)
            await scheduler.ScheduleJob(item.Job, item.Trigger);
    }
    /// <summary>
    /// 不指定任务名称, 使用泛型名称作为任务的name和group
    /// </summary>
    /// <remarks>
    /// 反正就算需要指定名称, 最后和泛型名称也不会有太大区别, 不如干脆放手!
    /// </remarks>
    public static async Task ScheduleJobs<Job>(this IScheduler scheduler, IEnumerable<string> crons) where Job : IJob
    {
        var name = typeof(Job).Name;
        var jobAndTriggers = QuartzTool.CreateJobAndTriggers<Job>(name, name, crons);
        foreach (var item in jobAndTriggers)
            await scheduler.ScheduleJob(item.Job, item.Trigger);
    }


    public static async Task RescheduleJob(this IScheduler scheduler, ITrigger trigger)
    {
        await scheduler.RescheduleJob(trigger.Key, trigger);
    }

    public static async Task RescheduleJob(this IScheduler scheduler, IEnumerable<ITrigger> triggers)
    {
        if (triggers.IsEmpty())
            return;
        var tkeys = await scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals(triggers.First().Key.Group));
        foreach (var tkey in tkeys)
            await scheduler.PauseTrigger(tkey);

        foreach (var trigger in triggers)
            await scheduler.RescheduleJob(trigger.Key, trigger);
    }
}


