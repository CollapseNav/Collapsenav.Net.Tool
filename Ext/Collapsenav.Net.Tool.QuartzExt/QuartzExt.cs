using Quartz;
using Quartz.Impl.Matchers;

namespace Collapsenav.Net.Tool.Ext;

public static partial class QuartzExt
{
    public static async Task DeleteJob(this IScheduler scheduler, JobKey jkey)
    {
        await scheduler.DeleteJob(jkey);
    }

    public static async Task DeleteJobs(this IScheduler scheduler, string group)
    {
        var keys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(group));
        if (keys.IsEmpty())
            return;
        foreach (var key in keys)
            await scheduler.DeleteJob(key);
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


