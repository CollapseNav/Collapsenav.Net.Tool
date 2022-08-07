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
    /// <remarks>根据Job自动生成JobKey和TriggerKey</remarks>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, string cron) where Job : IJob
    {
        await scheduler.ScheduleJob(CreateJob<Job>(), CreateTrigger<Job>(cron));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, TriggerKey tkey, string cron) where Job : IJob
    {
        await scheduler.ScheduleJob(CreateJob<Job>(), CreateTrigger(tkey, cron));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, JobKey jkey, TriggerKey tkey, string cron) where Job : IJob
    {
        await scheduler.ScheduleJob(CreateJob<Job>(jkey), CreateTrigger(tkey, cron));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, string name, string group, string cron) where Job : IJob
    {
        await scheduler.ScheduleJob(CreateJob<Job>(name, group), CreateTrigger(name, group, cron));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, string name, string cron) where Job : IJob
    {
        await scheduler.ScheduleJob(CreateJob<Job>(name), CreateTrigger(name, cron));
    }


    /// <summary>
    /// 添加一个Job
    /// </summary>
    /// <remarks>根据Job自动生成JobKey和TriggerKey</remarks>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, int len) where Job : IJob
    {
        await scheduler.ScheduleJob(CreateJob<Job>(), CreateTrigger<Job>(len));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, TriggerKey tkey, int len) where Job : IJob
    {
        await scheduler.ScheduleJob(CreateJob<Job>(), CreateTrigger(tkey, len));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, JobKey jkey, TriggerKey tkey, int len) where Job : IJob
    {
        await scheduler.ScheduleJob(CreateJob<Job>(jkey), CreateTrigger(tkey, len));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, string name, string group, int len) where Job : IJob
    {
        await scheduler.ScheduleJob(CreateJob<Job>(name, group), CreateTrigger(name, group, len));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob<Job>(this IScheduler scheduler, string name, int len) where Job : IJob
    {
        await scheduler.ScheduleJob(CreateJob<Job>(name), CreateTrigger(name, len));
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
    /// <remarks>根据Job自动生成JobKey和TriggerKey</remarks>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, string cron)
    {
        await scheduler.ScheduleJob(CreateJob(type), CreateTrigger(type, cron));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, TriggerKey tkey, string cron)
    {
        await scheduler.ScheduleJob(CreateJob(type), CreateTrigger(tkey, cron));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, JobKey jkey, TriggerKey tkey, string cron)
    {
        await scheduler.ScheduleJob(CreateJob(type, jkey), CreateTrigger(tkey, cron));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, string name, string group, string cron)
    {
        await scheduler.ScheduleJob(CreateJob(type, name, group), CreateTrigger(name, group, cron));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, string name, string cron)
    {
        await scheduler.ScheduleJob(CreateJob(type, name), CreateTrigger(name, cron));
    }


    /// <summary>
    /// 添加一个Job
    /// </summary>
    /// <remarks>根据Job自动生成JobKey和TriggerKey</remarks>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, int len)
    {
        await scheduler.ScheduleJob(CreateJob(type), CreateTrigger(type, len));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, TriggerKey tkey, int len)
    {
        await scheduler.ScheduleJob(CreateJob(type), CreateTrigger(tkey, len));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, JobKey jkey, TriggerKey tkey, int len)
    {
        await scheduler.ScheduleJob(CreateJob(type, jkey), CreateTrigger(tkey, len));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, string name, string group, int len)
    {
        await scheduler.ScheduleJob(CreateJob(type, name, group), CreateTrigger(name, group, len));
    }
    /// <summary>
    /// 添加一个Job
    /// </summary>
    public static async Task ScheduleJob(this IScheduler scheduler, Type type, string name, int len)
    {
        await scheduler.ScheduleJob(CreateJob(type, name), CreateTrigger(name, len));
    }




    /// <summary>
    /// 根据 crons 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs<Job>(this IScheduler scheduler, string name, IEnumerable<string> crons) where Job : IJob
    {
        var triggers = CreateTriggers(name, crons);
        foreach (var trigger in triggers)
        {
            var job = CreateJob<Job>(trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }

    /// <summary>
    /// 根据 crons 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs<Job>(this IScheduler scheduler, string name, string group, IEnumerable<string> crons) where Job : IJob
    {
        var triggers = CreateTriggers(name, group, crons);
        foreach (var trigger in triggers)
        {
            var job = CreateJob<Job>(trigger.Key.Name, trigger.Key.Group);
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
        var triggers = CreateTriggers(typeof(Job), crons);
        foreach (var trigger in triggers)
        {
            var job = CreateJob<Job>(trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }
    /// <summary>
    /// 根据 crons 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs(this IScheduler scheduler, Type type, string name, IEnumerable<string> crons)
    {
        var triggers = CreateTriggers(name, crons);
        foreach (var trigger in triggers)
        {
            var job = CreateJob(type, trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }
    /// <summary>
    /// 根据 crons 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs(this IScheduler scheduler, Type type, string name, string group, IEnumerable<string> crons)
    {
        var triggers = CreateTriggers(name, group, crons);
        foreach (var trigger in triggers)
        {
            var job = CreateJob(type, trigger.Key.Name, trigger.Key.Group);
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
        var triggers = CreateTriggers(type, crons);
        foreach (var trigger in triggers)
        {
            var job = CreateJob(type, trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }


    /// <summary>
    /// 根据 lens 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs<Job>(this IScheduler scheduler, string name, IEnumerable<int> lens) where Job : IJob
    {
        var triggers = CreateTriggers(name, lens);
        foreach (var trigger in triggers)
        {
            var job = CreateJob<Job>(trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }

    /// <summary>
    /// 根据 lens 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs<Job>(this IScheduler scheduler, string name, string group, IEnumerable<int> lens) where Job : IJob
    {
        var triggers = CreateTriggers(name, group, lens);
        foreach (var trigger in triggers)
        {
            var job = CreateJob<Job>(trigger.Key.Name, trigger.Key.Group);
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
        var triggers = CreateTriggers(typeof(Job), lens);
        foreach (var trigger in triggers)
        {
            var job = CreateJob<Job>(trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }
    /// <summary>
    /// 根据 lens 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs(this IScheduler scheduler, Type type, string name, IEnumerable<int> lens)
    {
        var triggers = CreateTriggers(name, lens);
        foreach (var trigger in triggers)
        {
            var job = CreateJob(type, trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }
    /// <summary>
    /// 根据 lens 创建 trigger 添加job
    /// </summary>
    public static async Task ScheduleJobs(this IScheduler scheduler, Type type, string name, string group, IEnumerable<int> lens)
    {
        var triggers = CreateTriggers(name, group, lens);
        foreach (var trigger in triggers)
        {
            var job = CreateJob(type, trigger.Key.Name, trigger.Key.Group);
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
        var triggers = CreateTriggers(type, lens);
        foreach (var trigger in triggers)
        {
            var job = CreateJob(type, trigger.Key.Name, trigger.Key.Group);
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}