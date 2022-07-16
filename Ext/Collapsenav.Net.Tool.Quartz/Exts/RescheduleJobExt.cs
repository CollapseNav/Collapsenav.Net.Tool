using Quartz;
using Quartz.Impl.Matchers;

namespace Collapsenav.Net.Tool.Ext;

public partial class QuartzExt
{
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, ITrigger trigger)
    {
        await QuartzTool.RescheduleJob(scheduler, trigger);
    }

    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, IEnumerable<ITrigger> triggers)
    {
        await QuartzTool.RescheduleJob(scheduler, triggers);
    }

    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, string name, string group, ITrigger trigger)
    {
        await QuartzTool.RescheduleJob(scheduler, name, group, trigger);
    }

    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, string group, ITrigger trigger)
    {
        await QuartzTool.RescheduleJob(scheduler, group, trigger);
    }


    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob(this IScheduler scheduler, Type type, ITrigger trigger)
    {
        await QuartzTool.RescheduleJob(scheduler, type.Name, type.Name, trigger);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, ITrigger trigger) where Job : IJob
    {
        await QuartzTool.RescheduleJob(scheduler, typeof(Job).Name, trigger);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    /// <remarks>重置triggerkey 为 (Job.Name,Job.Name) 的trigger</remarks>
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, int len) where Job : IJob
    {
        await QuartzTool.RescheduleJob<Job>(scheduler, len);
    }
    /// <summary>
    /// 重设trigger
    /// </summary>
    /// <remarks>重置triggerkey 为 (Job.Name,Job.Name) 的trigger</remarks>
    public static async Task RescheduleJob<Job>(this IScheduler scheduler, string cron) where Job : IJob
    {
        await QuartzTool.RescheduleJob<Job>(scheduler, cron);
    }
}