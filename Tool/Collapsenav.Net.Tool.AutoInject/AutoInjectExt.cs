using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Collapsenav.Net.Tool.AutoInject;
public static class AutoInjectExt
{
    /// <summary>
    /// 使用自动注入的工厂(提供 属性注入 和对应的 自动注册 功能)
    /// </summary>
    public static IHostBuilder UseAutoInjectProviderFactory(this IHostBuilder builder)
    {
        return builder.UseServiceProviderFactory(new AutoInjectServiceProviderFactory());
    }

    public static IServiceCollection AddAutoInject(this IServiceCollection services)
    {
        // 获取 CustomerTypes , 但是剔除 Collapsenav.Net.Tool 的 types
        var types = AppDomain.CurrentDomain.GetCustomerTypes().Where(item => !item.FullName.StartsWith("Collapsenav.Net.Tool"));
        // 获取 service 中已经注册的 type
        var registedTypes = services.Select(item => item.ServiceType).ToList();
        // 获取被标记了 AutoInject 的 types
        var autoInjectTypes = types.SelectMany(item => item.AttrMemberTypes<AutoInjectAttribute>())
        // 移除已经手动注册过的 type
        .Where(item => !registedTypes.Contains(item)).Unique();
        // 挑出需要注册的 接口
        var autoInjectInterfaces = autoInjectTypes.Where(item => item.IsInterface).ToList();
        // 挑出需要注册的 非接口
        autoInjectTypes = autoInjectTypes.Where(item => !item.IsInterface).ToList();
        // 挑出所有 "实现"
        types = types.Where(item => !item.IsInterface && !item.IsAbstract);

        if (autoInjectInterfaces.NotEmpty())
        {
            // 尝试根据接口找到可以对应的 实现
            var interfacesDict = autoInjectInterfaces.ToDictionary(item => item, item => types.FirstOrDefault(type => type.HasInterface(item)));
            foreach (var dict in interfacesDict)
            {
                if (dict.Value != null)
                    services.TryAddScoped(dict.Key, dict.Value);
            }
        }
        // 直接注册实现
        foreach (var type in autoInjectTypes)
            services.TryAddScoped(type);
        return services;
    }
}