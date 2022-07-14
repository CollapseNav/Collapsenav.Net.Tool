using Quartz;
using Quartz.Impl.Matchers;
using Quartz.Simpl;
using Quartz.Xml;

namespace Collapsenav.Net.Tool.Ext;

public static partial class QuartzExt
{
    public static async Task DeleteJob(this IScheduler scheduler, JobKey jkey)
    {
        await scheduler.DeleteJob(jkey);
    }

    public static async Task DeleteJobs(this IScheduler scheduler, string group)
    {
        var keys = await scheduler.GetJobKeys(group);
        if (keys.IsEmpty())
            return;
        foreach (var key in keys)
            await scheduler.DeleteJob(key);
    }

    public static async Task PauseJob(this IScheduler scheduler, JobKey jkey)
    {
        await scheduler.PauseJob(jkey);
    }

    public static async Task PauseJobs(this IScheduler scheduler, string group)
    {
        var keys = await scheduler.GetJobKeys(group);
        if (keys.IsEmpty())
            return;
        foreach (var key in keys)
            await scheduler.PauseJob(key);
    }

    public static async Task PauseTrigger(this IScheduler scheduler, TriggerKey tkey)
    {
        await scheduler.PauseTrigger(tkey);
    }

    public static async Task PauseTriggers(this IScheduler scheduler, string group)
    {
        var keys = await scheduler.GetTriggerKeys(group);
        if (keys.IsEmpty())
            return;
        foreach (var key in keys)
            await scheduler.PauseTrigger(key);
    }


    public static async Task<IReadOnlyCollection<TriggerKey>> GetTriggerKeys(this IScheduler scheduler, string group)
    {
        return await scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals(group));
    }

    public static async Task<IReadOnlyCollection<JobKey>> GetJobKeys(this IScheduler scheduler, string group)
    {
        return await scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(group));
    }

    public static async Task LoadXmlConfig(this IScheduler scheduler, string path)
    {
        if (!path.ToLower().EndsWith("xml"))
            throw new Exception("配置文件必须是xml格式");
        var processor = new XMLSchedulingDataProcessor(new SimpleTypeLoadHelper());
        await processor.ProcessFileAndScheduleJobs(path, scheduler);
    }
}
