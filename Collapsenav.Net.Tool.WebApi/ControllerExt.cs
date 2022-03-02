using AutoMapper;
using Collapsenav.Net.Tool.Data;
using Microsoft.Extensions.DependencyInjection;
namespace Collapsenav.Net.Tool.WebApi;
public static class ControllerExt
{
    /// <summary>
    /// 注册泛型controller
    /// </summary>
    public static IServiceCollection AddRepController(this IServiceCollection services)
    {
        services.AddRepository();
        services
        .AddTransient(typeof(IModifyRepController<,>), typeof(ModifyRepController<,>))
        .AddTransient(typeof(IQueryRepController<,>), typeof(QueryRepController<,>))
        .AddTransient(typeof(ICrudRepController<,,>), typeof(CrudRepController<,,>))
        .AddTransient(typeof(IModifyRepController<,,>), typeof(ModifyRepController<,,>))
        .AddTransient(typeof(IQueryRepController<,,>), typeof(QueryRepController<,,>))
        .AddTransient(typeof(ICrudRepController<,,,>), typeof(CrudRepController<,,,>))
        .AddMap()
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
