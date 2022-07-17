using Quartz;

namespace Collapsenav.Net.Tool.Ext;

public partial class QuartzExt
{
    public static async Task RescheduleJob(this IScheduler scheduler, ITrigger trigger)
    {
        await QuartzTool.RescheduleJob(scheduler, trigger);
    }
    public static async Task RescheduleJobs(this IScheduler scheduler, IEnumerable<ITrigger> triggers)
    {
        await QuartzTool.RescheduleJobs(scheduler, triggers);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, TriggerKey triggerKey, ITrigger trigger)
    {
        await QuartzTool.RescheduleJob(scheduler, triggerKey, trigger);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, string name, string group, ITrigger trigger)
    {
        await QuartzTool.RescheduleJob(scheduler, name, group, trigger);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, string group, ITrigger trigger)
    {
        await QuartzTool.RescheduleJob(scheduler, group, trigger);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, TriggerKey key, int len)
    {
        await QuartzTool.RescheduleJob(scheduler, key, len);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, string name, string group, int len)
    {
        await QuartzTool.RescheduleJob(scheduler, name, group, len);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, string group, int len)
    {
        await QuartzTool.RescheduleJob(scheduler, group, len);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, TriggerKey key, string cron)
    {
        await QuartzTool.RescheduleJob(scheduler, key, cron);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, string name, string group, string cron)
    {
        await QuartzTool.RescheduleJob(scheduler, name, group, cron);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, string group, string cron)
    {
        await QuartzTool.RescheduleJob(scheduler, group, cron);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, ITrigger trigger)
    {
        await QuartzTool.RescheduleJob(scheduler, type, trigger);
    }
    public static async Task RescheduleJobs(this IScheduler scheduler, Type type, IEnumerable<ITrigger> triggers)
    {
        await QuartzTool.RescheduleJobs(scheduler, type, triggers);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, TriggerKey triggerKey, ITrigger trigger)
    {
        await QuartzTool.RescheduleJob(scheduler, type, triggerKey, trigger);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, string name, string group, ITrigger trigger)
    {
        await QuartzTool.RescheduleJob(scheduler, type, name, group, trigger);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, string group, ITrigger trigger)
    {
        await QuartzTool.RescheduleJob(scheduler, type, group, trigger);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, TriggerKey key, int len)
    {
        await QuartzTool.RescheduleJob(scheduler, type, key, len);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, string name, string group, int len)
    {
        await QuartzTool.RescheduleJob(scheduler, type, name, group, len);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, string group, int len)
    {
        await QuartzTool.RescheduleJob(scheduler, type, group, len);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, TriggerKey key, string cron)
    {
        await QuartzTool.RescheduleJob(scheduler, type, key, cron);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, string name, string group, string cron)
    {
        await QuartzTool.RescheduleJob(scheduler, type, name, group, cron);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, string group, string cron)
    {
        await QuartzTool.RescheduleJob(scheduler, type, group, cron);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, int len)
    {
        await QuartzTool.RescheduleJob(scheduler, type, len);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, string cron)
    {
        await QuartzTool.RescheduleJob(scheduler, type, cron);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, IEnumerable<string> crons)
    {
        await QuartzTool.RescheduleJob(scheduler, type, crons);
    }
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, params int[] lens)
    {
        await QuartzTool.RescheduleJob(scheduler, type, lens);
    }
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, ITrigger trigger) where Job : IJob
    {
        await QuartzTool.RescheduleJob<Job>(scheduler, trigger);
    }
    public static async Task RescheduleJobs<Job>(this IScheduler scheduler, IEnumerable<ITrigger> triggers) where Job : IJob
    {
        await QuartzTool.RescheduleJobs<Job>(scheduler, triggers);
    }
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, TriggerKey triggerKey, ITrigger trigger) where Job : IJob
    {
        await QuartzTool.RescheduleJob<Job>(scheduler, triggerKey, trigger);
    }
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, string name, string group, ITrigger trigger) where Job : IJob
    {
        await QuartzTool.RescheduleJob<Job>(scheduler, name, group, trigger);
    }
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, string group, ITrigger trigger) where Job : IJob
    {
        await QuartzTool.RescheduleJob<Job>(scheduler, group, trigger);
    }
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, TriggerKey key, int len) where Job : IJob
    {
        await QuartzTool.RescheduleJob<Job>(scheduler, key, len);
    }
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, string name, string group, int len) where Job : IJob
    {
        await QuartzTool.RescheduleJob<Job>(scheduler, name, group, len);
    }
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, string group, int len) where Job : IJob
    {
        await QuartzTool.RescheduleJob<Job>(scheduler, group, len);
    }
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, TriggerKey key, string cron) where Job : IJob
    {
        await QuartzTool.RescheduleJob<Job>(scheduler, key, cron);
    }
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, string name, string group, string cron) where Job : IJob
    {
        await QuartzTool.RescheduleJob<Job>(scheduler, name, group, cron);
    }
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, string group, string cron) where Job : IJob
    {
        await QuartzTool.RescheduleJob<Job>(scheduler, group, cron);
    }
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, int len) where Job : IJob
    {
        await QuartzTool.RescheduleJob<Job>(scheduler, len);
    }
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, string cron) where Job : IJob
    {
        await QuartzTool.RescheduleJob<Job>(scheduler, cron);
    }
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, IEnumerable<string> crons) where Job : IJob
    {
        await QuartzTool.RescheduleJob<Job>(scheduler, crons);
    }
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, params int[] lens) where Job : IJob
    {
        await QuartzTool.RescheduleJob<Job>(scheduler, lens);
    }
}