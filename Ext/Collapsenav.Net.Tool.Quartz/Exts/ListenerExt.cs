using Quartz;
using Quartz.Impl.Matchers;

namespace Collapsenav.Net.Tool.Ext;

public static partial class QuartzTool
{
    public static void UseJobListener<T>(this IScheduler scheduler, params IMatcher<JobKey>[] matchers) where T : IJobListener
        => scheduler.ListenerManager.AddJobListener(Activator.CreateInstance<T>(), matchers ?? new[] { GroupMatcher<JobKey>.AnyGroup() });
    public static void UseTriggerListener<T>(this IScheduler scheduler, params IMatcher<TriggerKey>[] matchers) where T : ITriggerListener
        => scheduler.ListenerManager.AddTriggerListener(Activator.CreateInstance<T>(), matchers ?? new[] { GroupMatcher<TriggerKey>.AnyGroup() });
    public static void UseSchedulerListener<T>(this IScheduler scheduler) where T : ISchedulerListener
        => scheduler.ListenerManager.AddSchedulerListener(Activator.CreateInstance<T>());
    public static void UseJobListener(this IScheduler scheduler, Type type, params IMatcher<JobKey>[] matchers)
        => scheduler.ListenerManager.AddJobListener(Activator.CreateInstance(type) as IJobListener, matchers ?? new[] { GroupMatcher<JobKey>.AnyGroup() });
    public static void UseTriggerListener(this IScheduler scheduler, Type type, params IMatcher<TriggerKey>[] matchers)
        => scheduler.ListenerManager.AddTriggerListener(Activator.CreateInstance(type) as ITriggerListener, matchers ?? new[] { GroupMatcher<TriggerKey>.AnyGroup() });
    public static void UseSchedulerListener(this IScheduler scheduler, Type type)
        => scheduler.ListenerManager.AddSchedulerListener(Activator.CreateInstance(type) as ISchedulerListener);
}