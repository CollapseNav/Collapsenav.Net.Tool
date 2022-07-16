using Quartz;
using Quartz.Impl;

namespace Collapsenav.Net.Tool.Ext;

public class QuartzNode
{
    public static IScheduler Scheduler;
    public static QuartzJobBuilder Builder = new();
    public static async Task InitSchedulerAsync()
    {
        Scheduler = await new StdSchedulerFactory().GetScheduler();
    }
    public static async Task InitFromBuilderAsync(QuartzJobBuilder builder)
    {
        Builder = builder;
        await InitSchedulerAsync();
        await Builder.Build(Scheduler);
    }
}
