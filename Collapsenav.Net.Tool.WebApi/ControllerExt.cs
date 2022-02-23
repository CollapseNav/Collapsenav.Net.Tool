using Collapsenav.Net.Tool.Data;
using Microsoft.Extensions.DependencyInjection;
namespace Collapsenav.Net.Tool.WebApi;
public static class ControllerExt
{
    public static IServiceCollection AddRepController(this IServiceCollection services)
    {
        services.AddRepository();
        services
        .AddTransient(typeof(IModifyRepController<,,>), typeof(ModifyRepController<,,>))
        .AddTransient(typeof(IQueryRepController<,,>), typeof(QueryRepController<,,>))
        .AddTransient(typeof(ICrudRepController<,,,>), typeof(CrudRepController<,,,>))
        ;
        return services;
    }
    public static IServiceCollection AddRepController(this IServiceCollection services, params Type[] types)
    {
        services.AddRepController();
        foreach (var type in types)
            services.AddTransient(type);
        return services;
    }
}
