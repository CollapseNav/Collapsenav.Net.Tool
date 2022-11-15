using Quartz;
using Quartz.Impl.Matchers;
using Quartz.Simpl;
using Quartz.Xml;

namespace Collapsenav.Net.Tool.Ext;

public static partial class QuartzTool
{
    public static async Task DeleteJobs(this IScheduler scheduler, string group)
    {
        var keys = await scheduler.GetJobKeys(group);
        if (keys.IsEmpty())
            return;
        foreach (var key in keys)
            await scheduler.DeleteJob(key);
    }
    public static async Task DeleteAllJobs(this IScheduler scheduler)
    {
        var triggerKeys = await scheduler.GetTriggerKeys();
        var jobKeys = await scheduler.GetJobKeys();
        foreach (var key in triggerKeys)
            await scheduler.PauseTrigger(key);
        foreach (var key in jobKeys)
            await scheduler.DeleteJob(key);
    }
    public static async Task PauseJobs(this IScheduler scheduler, string group)
    {
        var keys = await scheduler.GetJobKeys(group);
        if (keys.IsEmpty())
            return;
        foreach (var key in keys)
            await scheduler.PauseJob(key);
    }
    public static async Task PauseJobs(this IScheduler scheduler, IEnumerable<JobKey> keys)
    {
        if (keys.IsEmpty())
            return;
        foreach (var key in keys)
            await scheduler.PauseJob(key);
    }
    public static async Task PauseTriggers(this IScheduler scheduler, string group)
    {
        var keys = await scheduler.GetTriggerKeys(group);
        if (keys.IsEmpty())
            return;
        foreach (var key in keys)
            await scheduler.PauseTrigger(key);
    }
    public static async Task<IReadOnlyCollection<TriggerKey>> GetTriggerKeys(this IScheduler scheduler, string group = null)
    {
        var matches = group.IsEmpty() ? GroupMatcher<TriggerKey>.AnyGroup() : GroupMatcher<TriggerKey>.GroupEquals(group);
        return await scheduler.GetTriggerKeys(matches);
    }
    public static async Task<IReadOnlyCollection<JobKey>> GetJobKeys(this IScheduler scheduler, string group = null)
    {
        var matches = group.IsEmpty() ? GroupMatcher<JobKey>.AnyGroup() : GroupMatcher<JobKey>.GroupEquals(group);
        return await scheduler.GetJobKeys(matches);
    }

    public static async Task LoadXmlConfig(this IScheduler scheduler, string path)
    {
        if (!path.ToLower().EndsWith("xml"))
            throw new Exception("配置文件必须是xml格式");
        var processor = new XMLSchedulingDataProcessor(new SimpleTypeLoadHelper());
        await processor.ProcessFileAndScheduleJobs(path, scheduler);
    }
    public static async Task<IReadOnlyCollection<IJobDetail>> GetJobDetails(this IScheduler scheduler, string group = null)
    {
        var jobKeys = await scheduler.GetJobKeys(group);
        List<IJobDetail> details = new();
        foreach (var key in jobKeys)
            details.Add(await scheduler.GetJobDetail(key));
        return details;
    }
    public static async Task<IReadOnlyCollection<ITrigger>> GetTriggers(this IScheduler scheduler, string group = null)
    {
        var triggerKeys = await scheduler.GetTriggerKeys(group);
        List<ITrigger> triggers = new();
        foreach (var key in triggerKeys)
            triggers.Add(await scheduler.GetTrigger(key));
        return triggers;
    }
    public static async Task<IReadOnlyCollection<JobStatus>> GetJobStatus(this IScheduler scheduler)
    {
        var details = await scheduler.GetJobDetails();
        var triggers = await scheduler.GetTriggers();
        List<JobStatus> list = new();
        foreach (var detail in details)
        {
            var jobTriggers = triggers.Where(item => item.JobKey.Name == detail.Key.Name && item.JobKey.Group == detail.Key.Group);
            if (jobTriggers.NotEmpty())
                list.Add(JobStatus.GenJobStatus(detail, jobTriggers.ToList()));
        }
        return list;
    }
}
