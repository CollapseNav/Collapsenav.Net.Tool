using Microsoft.Extensions.Hosting;

namespace Collapsenav.Net.Tool.AutoInject;
public static class AutoInjectExt
{
    public static IHostBuilder UseAutoInjectProviderFactory(this IHostBuilder builder)
    {
        return builder.UseServiceProviderFactory(new AutoInjectServiceProviderFactory());
    }
}