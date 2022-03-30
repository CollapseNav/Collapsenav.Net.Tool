using Collapsenav.Net.Tool.WebApi;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.Ids;

public static class IdsControllerExt
{
    public static IServiceCollection AddIdsController(this IServiceCollection services)
    {
        services.AddTransient<IClientApplication, ClientApplication>()
#if NETCOREAPP3_1_OR_GREATER
        .AddTransient<IApiScopeApplication, ApiScopeApplication>()
        .AddTransient<IIdentityResourceApplication, IdentityResourceApplication>()
#endif
        .AddDynamicController()
        ;
        return services;
    }
}