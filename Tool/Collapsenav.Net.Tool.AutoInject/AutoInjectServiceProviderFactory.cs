using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.AutoInject;

public class AutoInjectServiceProviderFactory : IServiceProviderFactory<IServiceCollection>
{
    public IServiceProvider CreateServiceProvider(IServiceCollection containerBuilder)
    {
        var provider = containerBuilder.BuildServiceProvider();
        return new AutoInjectServiceProvider(provider);
    }
    public IServiceCollection CreateBuilder(IServiceCollection services)
    {
        if (services == null) return new ServiceCollection();
        return services;
    }
}