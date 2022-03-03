using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.Data.Test;
public class DIConfig
{
    public static ServiceProvider GetProvider()
    {
        return new ServiceCollection()
        .AddDbContext<TestDbContext>(options =>
        {
            options.UseSqlite("Data Source = Test.db;");
        })
        .AddTransient(typeof(DbContext), typeof(TestDbContext))
        .AddRepository()
        .BuildServiceProvider();
    }
    public static ServiceProvider GetNotBaseProvider()
    {
        return new ServiceCollection()
        .AddDbContext<TestNotBaseDbContext>(options =>
        {
            options.UseSqlite("Data Source = Test.db;");
        })
        .AddTransient(typeof(DbContext), typeof(TestNotBaseDbContext))
        .AddRepository()
        .BuildServiceProvider();
    }
    public static ServiceProvider GetTestRepositoryProvider()
    {
        return new ServiceCollection()
        .AddDbContext<TestDbContext>(options =>
        {
            options.UseSqlite("Data Source = Test.db;");
        })
        .AddTransient(typeof(DbContext), typeof(TestDbContext))
        .BuildServiceProvider();
    }
}
