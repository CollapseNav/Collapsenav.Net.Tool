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
        .AddScoped(typeof(IModifyController<,>), typeof(ModifyRepController<,>))
        .AddScoped(typeof(IQueryController<,>), typeof(QueryRepController<,>))
        .AddScoped(typeof(ICrudController<,,>), typeof(CrudRepController<,,>))
        .AddScoped(typeof(IModifyController<,,>), typeof(ModifyRepController<,,>))
        .AddScoped(typeof(IQueryController<,,>), typeof(QueryRepController<,,>))
        .AddScoped(typeof(ICrudController<,,,>), typeof(CrudRepController<,,,>))
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
        .AddScoped(typeof(IModifyController<,>), typeof(ModifyAppController<,>))
        .AddScoped(typeof(IQueryController<,>), typeof(QueryAppController<,>))
        .AddScoped(typeof(ICrudController<,,>), typeof(CrudAppController<,,>))
        .AddScoped(typeof(IModifyController<,,>), typeof(ModifyAppController<,,>))
        .AddScoped(typeof(IQueryController<,,>), typeof(QueryAppController<,,>))
        .AddScoped(typeof(ICrudController<,,,>), typeof(CrudAppController<,,,>))
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
        .AddScoped(typeof(IModifyApplication<,>), typeof(ModifyRepApplication<,>))
        .AddScoped(typeof(IQueryApplication<,>), typeof(QueryRepApplication<,>))
        .AddScoped(typeof(ICheckExistApplication<>), typeof(ReadRepApplication<>))
        .AddScoped(typeof(ICountApplication<>), typeof(ReadRepApplication<>))
        .AddScoped(typeof(ICrudApplication<,,>), typeof(CrudRepApplication<,,>))
        .AddScoped(typeof(IModifyApplication<,,>), typeof(ModifyRepApplication<,,>))
        .AddScoped(typeof(IQueryApplication<,,>), typeof(QueryRepApplication<,,>))
        .AddScoped(typeof(ICrudApplication<,,,>), typeof(CrudRepApplication<,,,>))
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
            services.AddScoped(type);
        return services;
    }
    /// <summary>
    /// 添加map(使用jsonmap)
    /// </summary>
    public static IServiceCollection AddMap(this IServiceCollection services)
    {
        var existIMap = services.Where(item => item.ServiceType == typeof(IMap)).ToList();
        if (existIMap.IsEmpty())
            services.AddScoped<IMap, JsonMap>();
        return services;
    }
    // 使用automapper
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        // 使用 automap 作为 imap 的实现
        var existIMap = services.Where(item => item.ServiceType == typeof(IMap)).ToList();
        existIMap.ForEach(item => services.Remove(item));
        services.AddScoped<IMap, AutoMap>();

        // 注册所有自定义的 profile
        var types = AppDomain.CurrentDomain.GetCustomerTypes<Profile>().Where(item => !item.IsInterface && !item.IsAbstract);
        types.ForEach(type => services.AddAutoMapper(type));
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

        services.AddScoped<IMap, AutoMap>();
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

        services.AddScoped<IMap, AutoMap>();
        return services;
    }
}
