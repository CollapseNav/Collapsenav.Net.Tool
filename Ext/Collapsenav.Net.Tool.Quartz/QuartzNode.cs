using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Collapsenav.Net.Tool.Ext;

public class QuartzNode
{
    private static IServiceCollection Services;
    public static IScheduler Scheduler = null;
    public static QuartzJobBuilder Builder = new();
    public static void SetService(IServiceCollection services)
    {
        Services = services;
    }
    public static async Task InitSchedulerAsync(IScheduler scheduler = null)
    {
        Scheduler = scheduler ?? await new StdSchedulerFactory().GetScheduler();
    }

    public static void InitFactory(IJobFactory factory)
    {
        if (Scheduler != null && factory != null)
            Scheduler.JobFactory = factory;
    }
    public static async Task InitFromBuilderAsync(QuartzJobBuilder builder)
    {
        await InitSchedulerAsync();
        Builder = builder;
        await Builder.Build();
    }

    /// <summary>
    /// 使用 QuartzJobBuilder 构建任务
    /// </summary>
    public static async Task Build(QuartzJobBuilder builder = null)
    {
        if (builder != null)
            Builder ??= builder;
        await Builder?.Build();
    }

    public static Type GetJobType(string typeName)
    {
        // 当 service 为空时自动扫描程序集查找type
        if (Services == null)
            return AppDomain.CurrentDomain.GetCustomerTypesByPrefix(typeName).FirstOrDefault();
        // 当 service 非空时使用注册的type
        return Services.FirstOrDefault(item => item.ServiceType.Name == typeName || item.ServiceType.FullName == typeName)?.ServiceType;
    }
}