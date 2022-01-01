using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.WebApi;
public static class ControllerExt
{
    public static IServiceCollection AddController(this IServiceCollection services)
    {
        services
        .AddTransient(typeof(IModifyRepController<,,,>), typeof(ModifyRepController<,,,>))
        .AddTransient(typeof(IQueryRepController<,,>), typeof(QueryRepController<,,>))
        ;
        return services;
    }
    public static IServiceCollection AddController(this IServiceCollection services, params Type[] types)
    {
        services.AddController();
        foreach (var type in types)
            services.AddTransient(type);
        return services;
    }
}
