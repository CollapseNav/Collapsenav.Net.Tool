using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Collapsenav.Net.Tool.Ext;

public class QuartzNode
{
    private static IServiceCollection Services;
    public static List<Type> RegJobs = new();
    public static IScheduler Scheduler = null;
    public static QuartzJobBuilder Builder = new();
    public static void SetService(IServiceCollection services)
    {
        Services = services;
    }
    public static async Task InitSchedulerAsync(IScheduler scheduler = null)
    {
        if (scheduler == null)
            Scheduler ??= await new StdSchedulerFactory().GetScheduler();
        else
            Scheduler = scheduler;
    }

    public static void InitFactory(IJobFactory factory)
    {
        if (Scheduler != null && factory != null)
            Scheduler.JobFactory = factory;
    }
    public static async Task InitFromBuilderAsync(QuartzJobBuilder builder)
    {
        Builder = builder;
        await InitSchedulerAsync();
        await Builder.Build(Scheduler);
    }

    public static async Task Build()
    {
        await Builder?.Build(Scheduler);
    }

    public static Type GetJobType(string typeName)
    {
        if (Services == null)
            return null;
        return Services.FirstOrDefault(item => item.ServiceType.Name == typeName || item.ServiceType.FullName == typeName).ServiceType;
    }
}