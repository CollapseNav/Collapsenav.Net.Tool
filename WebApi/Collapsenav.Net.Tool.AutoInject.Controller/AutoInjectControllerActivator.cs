using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
namespace Collapsenav.Net.Tool.AutoInject;
public class AutoInjectControllerActivator : IControllerActivator
{
    public virtual object Create(ControllerContext context)
    {
        var provider = context.HttpContext.RequestServices;
        provider = provider is AutoInjectServiceProvider ? provider : new AutoInjectServiceProvider(provider);
        return provider.GetRequiredService(context.ActionDescriptor.ControllerTypeInfo.AsType());
    }
    public virtual void Release(ControllerContext context, object controller)
    {
        if (controller is IDisposable disposeable)
            disposeable.Dispose();
    }
}