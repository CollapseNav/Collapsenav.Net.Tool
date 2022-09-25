using Collapsenav.Net.Tool.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.WebApi.Test;
public class DIConfig
{
    public static ServiceProvider GetProvider()
    {
        return new ServiceCollection()
        .AddSqlite<TestDbContext>("Test.db")
        .AddDefaultDbContext<TestDbContext>()
        .AddRepController()
        .AddMap<MapProfile>()
        .BuildServiceProvider();
    }
    public static ServiceProvider GetNotBaseProvider()
    {
        return new ServiceCollection()
        .AddSqlite<TestNotBaseDbContext>("Test.db")
        .AddDefaultDbContext<TestNotBaseDbContext>()
        .AddRepController()
        .BuildServiceProvider();
    }

    public static ServiceProvider GetAppProvider()
    {
        return new ServiceCollection()
        .AddSqlite<TestDbContext>("TestApp.db")
        .AddDefaultDbContext<TestDbContext>()
        .AddAppController()
        .BuildServiceProvider();
    }
    public static ServiceProvider GetNotBaseAppProvider()
    {
        return new ServiceCollection()
        .AddSqlite<TestNotBaseDbContext>("TestApp.db")
        .AddMap(typeof(MappingProfile))
        .AddDefaultDbContext<TestNotBaseDbContext>()
        .AddAppController()
        .BuildServiceProvider();
    }
}
