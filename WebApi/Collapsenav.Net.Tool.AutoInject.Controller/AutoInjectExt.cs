using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Collapsenav.Net.Tool.AutoInject;
public static class AutoInjectExt
{
    public static IServiceCollection AddAutoInjectController(this IServiceCollection service)
    {
        service.Replace(ServiceDescriptor.Transient<IControllerActivator, AutoInjectControllerActivator>());
        service.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        service.AddAutoInject();
        return service;
    }
}