using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace Collapsenav.Net.Tool.Ext;

public static class QuartzServiceExt
{
    /// <summary>
    /// 添加默认的job工厂
    /// </summary>
    public static IServiceCollection AddDIJobFactory(this IServiceCollection services)
    {
        if (!services.Any(item => item.Lifetime == ServiceLifetime.Singleton && item.ServiceType == typeof(DIJobFactory)))
            services.AddSingleton<IJobFactory, DIJobFactory>();
        return services;
    }
    /// <summary>
    /// 添加一个job
    /// </summary>
    public static IServiceCollection AddJob<Job>(this IServiceCollection services, int len) where Job : class, IJob
    {
        QuartzNode.Builder ??= new();
        QuartzNode.Builder.AddJob<Job>(len);
        return services.AddTransient<Job>().AddDIJobFactory();
    }
    /// <summary>
    /// 添加一个job
    /// </summary>
    public static IServiceCollection AddJob<Job>(this IServiceCollection services, string cron) where Job : class, IJob
    {
        QuartzNode.Builder ??= new();
        QuartzNode.Builder.AddJob<Job>(cron);
        return services.AddTransient<Job>().AddDIJobFactory();
    }
    /// <summary>
    /// 添加多个job
    /// </summary>
    public static IServiceCollection AddJobs<Job>(this IServiceCollection services, params int[] len) where Job : class, IJob
    {
        QuartzNode.Builder ??= new();
        QuartzNode.Builder.AddJob<Job>(len);
        return services.AddTransient<Job>().AddDIJobFactory();
    }
    /// <summary>
    /// 添加多个job
    /// </summary>
    public static IServiceCollection AddJobs<Job>(this IServiceCollection services, IEnumerable<string> crons) where Job : class, IJob
    {
        QuartzNode.Builder ??= new();
        QuartzNode.Builder.AddJob<Job>(crons);
        return services.AddTransient<Job>().AddDIJobFactory();
    }
    /// <summary>
    /// 从xml配置文件中添加job
    /// </summary>
    public static IServiceCollection AddJobsFromXml(this IServiceCollection services, string path)
    {
        QuartzNode.Builder ??= new();
        QuartzNode.Builder.AddXmlConfig(path);
        return services.AddDIJobFactory();
    }

    /// <summary>
    /// 添加默认的hosted service, 用于初始化quartz
    /// </summary>
    public static IServiceCollection AddDefaultQuartzService(this IServiceCollection services)
    {
        return services.AddDIJobFactory().AddHostedService<EasyJobService>();
    }
    /// <summary>
    /// 添加默认的hosted service, 用于初始化quartz
    /// </summary>
    public static IServiceCollection AddDefaultQuartzService(this IServiceCollection services, Action<QuartzJobBuilder> action)
    {
        action?.Invoke(QuartzNode.Builder ??= new());
        services
        .AddDIJobFactory()
        .AddHostedService<EasyJobService>();
        return services;
    }
}