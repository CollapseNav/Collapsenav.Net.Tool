using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace Collapsenav.Net.Tool.Ext;

public static class QuartzServiceExt
{
    /// <summary>
    /// 添加默认的job工厂, 为job提供依赖注入功能
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
        QuartzNode.Builder.AddJob<Job>(len);
        return services.AddScoped<Job>().AddDIJobFactory();
    }
    /// <summary>
    /// 添加一个job
    /// </summary>
    public static IServiceCollection AddJob<Job>(this IServiceCollection services, string cron) where Job : class, IJob
    {
        QuartzNode.Builder.AddJob<Job>(cron);
        return services.AddScoped<Job>().AddDIJobFactory();
    }
    /// <summary>
    /// 添加多个job
    /// </summary>
    public static IServiceCollection AddJobs<Job>(this IServiceCollection services, params int[] len) where Job : class, IJob
    {
        QuartzNode.Builder.AddJob<Job>(len);
        return services.AddScoped<Job>().AddDIJobFactory();
    }
    /// <summary>
    /// 添加多个job
    /// </summary>
    public static IServiceCollection AddJobs<Job>(this IServiceCollection services, IEnumerable<string> crons) where Job : class, IJob
    {
        QuartzNode.Builder.AddJob<Job>(crons);
        return services.AddScoped<Job>().AddDIJobFactory();
    }
    /// <summary>
    /// 从xml配置文件中添加job
    /// </summary>
    public static IServiceCollection AddJobsFromXml(this IServiceCollection services, string path)
    {
        QuartzNode.Builder.AddXmlConfig(path);
        return services.AddDIJobFactory();
    }

    /// <summary>
    /// 添加默认的hosted service, 用于初始化quartz
    /// </summary>
    public static IServiceCollection AddDefaultQuartzService(this IServiceCollection services)
    {
        return services.AddScheduler().AddDIJobFactory().AddHostedService<EasyJobService>();
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

    public static IServiceCollection AddScheduler(this IServiceCollection services, IScheduler scheduler = null)
    {
        if (scheduler == null)
        {
            if (QuartzNode.Scheduler == null)
                QuartzNode.InitSchedulerAsync().Wait();
            scheduler = QuartzNode.Scheduler;
            QuartzNode.SetService(services);
        }
        return services.AddSingleton(scheduler);
    }


    public static IServiceCollection AddQuartzJsonConfig(this IServiceCollection services, string jsonString)
    {
        IEnumerable<IQuartzJsonConfig> configs = null;
        // 尝试转为简单的 keyvalue 配置
        try
        {
            var dicts = jsonString.ToObj<IDictionary<string, string>>();
            if (dicts.NotEmpty() && dicts.First().Key.NotEmpty() && dicts.First().Value.NotEmpty())
                configs ??= dicts.Select(item => QuartzConfigNode.ConvertFromKeyValue(item.Key, item.Value));
        }
        catch { }

        // 当上一种没有转换成功时尝试直接转为 QuartzConfigNode 格式
        try
        {
            configs ??= jsonString.ToObj<IEnumerable<QuartzConfigNode>>();
        }
        catch { }
        // 如果两种转化都失效, 那就抛出异常让他们感受到第三方包的险恶
        if (configs.IsEmpty() || configs.All(config => !config.CanUse()))
            throw new ArgumentNullException(jsonString);
        return services.AddQuartzJsonConfig(configs);
    }
    public static IServiceCollection AddQuartzJsonConfig(this IServiceCollection services, IConfigurationSection section)
    {
        IEnumerable<IQuartzJsonConfig> configs = null;
        // 尝试转为简单的 keyvalue 配置
        try
        {
            var dicts = section.Get<IDictionary<string, string>>();
            if (dicts.NotEmpty() && dicts.First().Key.NotEmpty() && dicts.First().Value.NotEmpty())
                configs ??= dicts.Select(item => QuartzConfigNode.ConvertFromKeyValue(item.Key, item.Value));
        }
        catch { }

        // 当上一种没有转换成功时尝试直接转为 QuartzConfigNode 格式
        try
        {
            configs ??= section.Get<IEnumerable<QuartzConfigNode>>();
        }
        catch { }
        // 如果两种转化都失效, 那就抛出异常让他们感受到第三方包的险恶
        if (configs.IsEmpty() || configs.All(config => !config.CanUse()))
            throw new ArgumentNullException(section.Key);

        return services.AddQuartzJsonConfig(configs);
    }
    public static IServiceCollection AddQuartzJsonConfig(this IServiceCollection services, IQuartzJsonConfig config)
    {
        QuartzNode.Builder.AddQuartzJsonConfig(config);
        return services;
    }
    public static IServiceCollection AddQuartzJsonConfig(this IServiceCollection services, IEnumerable<IQuartzJsonConfig> configs)
    {
        QuartzNode.Builder.AddQuartzJsonConfig(configs);
        return services;
    }
    /// <summary>
    /// 注册所有Job
    /// </summary>
    public static IServiceCollection AddAllJob(this IServiceCollection services, bool scanAll = false)
    {
        var jobs = (scanAll ? AppDomain.CurrentDomain.GetTypes<IJob>() : AppDomain.CurrentDomain.GetCustomerTypes<IJob>()).Where(item => !item.IsInterface && !item.IsAbstract);
        if (jobs.NotEmpty())
            jobs.ForEach(job => services.AddScoped(job));
        return services;
    }
}