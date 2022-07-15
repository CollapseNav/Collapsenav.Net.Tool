using Quartz;

namespace Collapsenav.Net.Tool.Ext;

public static partial class QuartzExt
{
    /// <summary>
    /// 添加一个Job
    /// </summary>
    /// <remarks>根据Job自动生成JobKey和TriggerKey</remarks>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, string cron) where Job : IJob
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob<Job>(), QuartzTool.CreateTrigger<Job>(cron));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, TriggerKey tkey, string cron) where Job : IJob
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob<Job>(), QuartzTool.CreateTrigger(tkey, cron));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, JobKey jkey, TriggerKey tkey, string cron) where Job : IJob
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob<Job>(jkey), QuartzTool.CreateTrigger(tkey, cron));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, string name, string group, string cron) where Job : IJob
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob<Job>(name, group), QuartzTool.CreateTrigger(name, group, cron));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, string name, string cron) where Job : IJob
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob<Job>(name), QuartzTool.CreateTrigger(name, cron));
    }


    /// <summary>
    /// 添加一个Job
    /// </summary>
    /// <remarks>根据Job自动生成JobKey和TriggerKey</remarks>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, int len) where Job : IJob
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob<Job>(), QuartzTool.CreateTrigger<Job>(len));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, TriggerKey tkey, int len) where Job : IJob
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob<Job>(), QuartzTool.CreateTrigger(tkey, len));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, JobKey jkey, TriggerKey tkey, int len) where Job : IJob
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob<Job>(jkey), QuartzTool.CreateTrigger(tkey, len));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, string name, string group, int len) where Job : IJob
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob<Job>(name, group), QuartzTool.CreateTrigger(name, group, len));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, string name, int len) where Job : IJob
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob<Job>(name), QuartzTool.CreateTrigger(name, len));
    }


    /// <summary>
    /// 添加一个Job
    /// </summary>
    /// <remarks>根据Job自动生成JobKey和TriggerKey</remarks>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, string cron)
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob(type), QuartzTool.CreateTrigger(type, cron));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, TriggerKey tkey, string cron)
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob(type), QuartzTool.CreateTrigger(tkey, cron));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, JobKey jkey, TriggerKey tkey, string cron)
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob(type, jkey), QuartzTool.CreateTrigger(tkey, cron));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, string name, string group, string cron)
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob(type, name, group), QuartzTool.CreateTrigger(name, group, cron));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, string name, string cron)
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob(type, name), QuartzTool.CreateTrigger(name, cron));
    }


    /// <summary>
    /// 添加一个Job
    /// </summary>
    /// <remarks>根据Job自动生成JobKey和TriggerKey</remarks>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, int len)
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob(type), QuartzTool.CreateTrigger(type, len));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, TriggerKey tkey, int len)
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob(type), QuartzTool.CreateTrigger(tkey, len));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, JobKey jkey, TriggerKey tkey, int len)
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob(type, jkey), QuartzTool.CreateTrigger(tkey, len));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, string name, string group, int len)
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob(type, name, group), QuartzTool.CreateTrigger(name, group, len));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, string name, int len)
    {
        await scheduler.ScheduleJob(QuartzTool.CreateJob(type, name), QuartzTool.CreateTrigger(name, len));
    }




    /// <summary>
    /// 根据 crons 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs<Job>(this IScheduler scheduler, string name, IEnumerable<string> crons) where Job : IJob
    {
        var triggers = QuartzTool.CreateTriggers(name, crons);
        foreach (var trigger in triggers)
        {
            var job = QuartzTool.CreateJob<Job>(trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }

    /// <summary>
    /// 根据 crons 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs<Job>(this IScheduler scheduler, string name, string group, IEnumerable<string> crons) where Job : IJob
    {
        var triggers = QuartzTool.CreateTriggers(name, group, crons);
        foreach (var trigger in triggers)
        {
            var job = QuartzTool.CreateJob<Job>(trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }
    /// <summary>
    /// 不指定任务名称, 使用泛型名称作为任务的name和group
    /// </summary>
    /// <remarks>
    /// 反正就算需要指定名称, 最后和泛型名称也不会有太大区别, 不如干脆放手!
    /// </remarks>
    public static async Task ScheduleJobs<Job>(this IScheduler scheduler, IEnumerable<string> crons) where Job : IJob
    {
        var triggers = QuartzTool.CreateTriggers(typeof(Job), crons);
        foreach (var trigger in triggers)
        {
            var job = QuartzTool.CreateJob<Job>(trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }
    /// <summary>
    /// 根据 crons 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs(this IScheduler scheduler, Type type, string name, IEnumerable<string> crons)
    {
        var triggers = QuartzTool.CreateTriggers(name, crons);
        foreach (var trigger in triggers)
        {
            var job = QuartzTool.CreateJob(type, trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }
    /// <summary>
    /// 根据 crons 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs(this IScheduler scheduler, Type type, string name, string group, IEnumerable<string> crons)
    {
        var triggers = QuartzTool.CreateTriggers(name, group, crons);
        foreach (var trigger in triggers)
        {
            var job = QuartzTool.CreateJob(type, trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }
    /// <summary>
    /// 不指定任务名称, 使用泛型名称作为任务的name和group
    /// </summary>
    /// <remarks>
    /// 反正就算需要指定名称, 最后和泛型名称也不会有太大区别, 不如干脆放手!
    /// </remarks>
    public static async Task ScheduleJobs(this IScheduler scheduler, Type type, IEnumerable<string> crons)
    {
        var triggers = QuartzTool.CreateTriggers(type, crons);
        foreach (var trigger in triggers)
        {
            var job = QuartzTool.CreateJob(type, trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }


    /// <summary>
    /// 根据 lens 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs<Job>(this IScheduler scheduler, string name, IEnumerable<int> lens) where Job : IJob
    {
        var triggers = QuartzTool.CreateTriggers(name, lens);
        foreach (var trigger in triggers)
        {
            var job = QuartzTool.CreateJob<Job>(trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }

    /// <summary>
    /// 根据 lens 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs<Job>(this IScheduler scheduler, string name, string group, IEnumerable<int> lens) where Job : IJob
    {
        var triggers = QuartzTool.CreateTriggers(name, group, lens);
        foreach (var trigger in triggers)
        {
            var job = QuartzTool.CreateJob<Job>(trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }
    /// <summary>
    /// 不指定任务名称, 使用泛型名称作为任务的name和group
    /// </summary>
    /// <remarks>
    /// 反正就算需要指定名称, 最后和泛型名称也不会有太大区别, 不如干脆放手!
    /// </remarks>
    public static async Task ScheduleJobs<Job>(this IScheduler scheduler, IEnumerable<int> lens) where Job : IJob
    {
        var triggers = QuartzTool.CreateTriggers(typeof(Job), lens);
        foreach (var trigger in triggers)
        {
            var job = QuartzTool.CreateJob<Job>(trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }
    /// <summary>
    /// 根据 lens 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs(this IScheduler scheduler, Type type, string name, IEnumerable<int> lens)
    {
        var triggers = QuartzTool.CreateTriggers(name, lens);
        foreach (var trigger in triggers)
        {
            var job = QuartzTool.CreateJob(type, trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }
    /// <summary>
    /// 根据 lens 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs(this IScheduler scheduler, Type type, string name, string group, IEnumerable<int> lens)
    {
        var triggers = QuartzTool.CreateTriggers(name, group, lens);
        foreach (var trigger in triggers)
        {
            var job = QuartzTool.CreateJob(type, trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }
    /// <summary>
    /// 不指定任务名称, 使用泛型名称作为任务的name和group
    /// </summary>
    /// <remarks>
    /// 反正就算需要指定名称, 最后和泛型名称也不会有太大区别, 不如干脆放手!
    /// </remarks>
    public static async Task ScheduleJobs(this IScheduler scheduler, Type type, IEnumerable<int> lens)
    {
        var triggers = QuartzTool.CreateTriggers(type, lens);
        foreach (var trigger in triggers)
        {
            var job = QuartzTool.CreateJob(type, trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}