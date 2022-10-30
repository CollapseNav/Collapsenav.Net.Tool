using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.Test.Quartz;
public class DIConfig
{
    public static ServiceProvider GetProvider()
    {
        return new ServiceCollection()
        .BuildServiceProvider();
    }
}
