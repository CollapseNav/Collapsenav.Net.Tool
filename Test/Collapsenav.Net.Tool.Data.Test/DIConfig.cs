using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.Data.Test;
public class DIConfig
{
    public static ServiceProvider GetProvider()
    {
        return new ServiceCollection()
        .AddSqlite<TestDbContext>("Test.db")
        .AddDefaultDbContext<TestDbContext>()
        .AddRepository()
        .BuildServiceProvider();
    }
    public static ServiceProvider GetNotBaseProvider()
    {
        return new ServiceCollection()
        .AddSqlite<TestNotBaseDbContext>("Test.db")
        .AddDefaultDbContext<TestNotBaseDbContext>()
        .AddRepository()
        .BuildServiceProvider();
    }
}
