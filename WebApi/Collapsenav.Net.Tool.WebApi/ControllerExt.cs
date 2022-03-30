using AutoMapper;
using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
namespace Collapsenav.Net.Tool.WebApi;
public static class ControllerExt
{
    /// <summary>
    /// 添加动态controller支持
    /// </summary>
    public static IServiceCollection AddDynamicController(this IServiceCollection services)
    {
        if (services.Any(item => item.ServiceType == typeof(AddControllerChangeProvider)))
            return services;
        return services
        .AddMap()
        .AddSingleton<IActionDescriptorChangeProvider>(AddControllerChangeProvider.Instance)
        // .AddSingleton(AddControllerChangeProvider.Instance)
        .AddHostedService<ChangeActionService>();
    }
    /// <summary>
    /// 注册泛型controller(基于Repository)
    /// </summary>
    public static IServiceCollection AddRepController(this IServiceCollection services)
    {
        services
        .AddRepository()
        .AddTransient(typeof(IModifyController<,>), typeof(ModifyRepController<,>))
        .AddTransient(typeof(IQueryController<,>), typeof(QueryRepController<,>))
        .AddTransient(typeof(ICrudController<,,>), typeof(CrudRepController<,,>))
        .AddTransient(typeof(IModifyController<,,>), typeof(ModifyRepController<,,>))
        .AddTransient(typeof(IQueryController<,,>), typeof(QueryRepController<,,>))
        .AddTransient(typeof(ICrudController<,,,>), typeof(CrudRepController<,,,>))
        .AddMap()
        .AddDynamicController()
        ;
        return services;
    }

    /// <summary>
    /// 注册泛型controller(基于Application)
    /// </summary>
    public static IServiceCollection AddAppController(this IServiceCollection services)
    {
        services
        .AddApplication()
        .AddTransient(typeof(IModifyController<,>), typeof(ModifyAppController<,>))
        .AddTransient(typeof(IQueryController<,>), typeof(QueryAppController<,>))
        .AddTransient(typeof(ICrudController<,,>), typeof(CrudAppController<,,>))
        .AddTransient(typeof(IModifyController<,,>), typeof(ModifyAppController<,,>))
        .AddTransient(typeof(IQueryController<,,>), typeof(QueryAppController<,,>))
        .AddTransient(typeof(ICrudController<,,,>), typeof(CrudAppController<,,,>))
        .AddMap()
        .AddDynamicController()
        ;
        return services;
    }

    /// <summary>
    /// 注册泛型application
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
        .AddRepository()
        .AddTransient(typeof(IModifyApplication<,>), typeof(ModifyRepApplication<,>))
        .AddTransient(typeof(IQueryApplication<,>), typeof(QueryRepApplication<,>))
        .AddTransient(typeof(ICrudApplication<,,>), typeof(CrudRepApplication<,,>))
        .AddTransient(typeof(IModifyApplication<,,>), typeof(ModifyRepApplication<,,>))
        .AddTransient(typeof(IQueryApplication<,,>), typeof(QueryRepApplication<,,>))
        .AddTransient(typeof(ICrudApplication<,,,>), typeof(CrudRepApplication<,,,>))
        .AddMap()
        .AddDynamicController()
        ;
        return services;
    }
    /// <summary>
    /// 添加自定义controller
    /// </summary>
    public static IServiceCollection AddRepController(this IServiceCollection services, params Type[] types)
    {
        services.AddRepController();
        foreach (var type in types)
            services.AddTransient(type);
        return services;
    }
    /// <summary>
    /// 添加map(使用jsonmap)
    /// </summary>
    public static IServiceCollection AddMap(this IServiceCollection services)
    {
        var existIMap = services.Where(item => item.ServiceType == typeof(IMap)).ToList();
        if (existIMap.IsEmpty())
            services.AddTransient<IMap, JsonMap>();
        return services;
    }
    /// <summary>
    /// 添加map(使用automap)
    /// </summary>
    public static IServiceCollection AddMap<T>(this IServiceCollection services) where T : Profile
    {
        services.AddAutoMapper(typeof(T));
        var existIMap = services.Where(item => item.ServiceType == typeof(IMap)).ToList();
        existIMap.ForEach(item => services.Remove(item));

        services.AddTransient<IMap, AutoMap>();
        return services;
    }
    /// <summary>
    /// 添加map(使用automap)
    /// </summary>
    public static IServiceCollection AddMap(this IServiceCollection services, Type type)
    {
        services.AddAutoMapper(type);
        var existIMap = services.Where(item => item.ServiceType == typeof(IMap)).ToList();
        existIMap.ForEach(item => services.Remove(item));

        services.AddTransient<IMap, AutoMap>();
        return services;
    }
}
