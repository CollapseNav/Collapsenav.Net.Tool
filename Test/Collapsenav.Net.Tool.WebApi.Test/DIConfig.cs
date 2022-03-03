using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.WebApi.Test;
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
        .AddRepController()
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
        .AddRepController()
        .BuildServiceProvider();
    }

    public static ServiceProvider GetAppProvider()
    {
        return new ServiceCollection()
        .AddDbContext<TestDbContext>(options =>
        {
            options.UseSqlite("Data Source = TestApp.db;");
        })
        .AddTransient(typeof(DbContext), typeof(TestDbContext))
        .AddAppController()
        .BuildServiceProvider();
    }
    public static ServiceProvider GetNotBaseAppProvider()
    {
        return new ServiceCollection()
        .AddDbContext<TestNotBaseDbContext>(options =>
        {
            options.UseSqlite("Data Source = TestApp.db;");
        })
        .AddMap(typeof(MappingProfile))
        .AddTransient(typeof(DbContext), typeof(TestNotBaseDbContext))
        .AddAppController()
        .BuildServiceProvider();
    }
    public static ServiceProvider GetTestRepositoryProvider()
    {
        return new ServiceCollection()
        .AddDbContext<TestDbContext>(options =>
        {
            options.UseSqlite("Data Source = Test.db;");
        })
        .AddMap<MappingProfile>()
        .AddTransient(typeof(DbContext), typeof(TestDbContext))
        .AddRepController()
        .BuildServiceProvider();
    }
}
