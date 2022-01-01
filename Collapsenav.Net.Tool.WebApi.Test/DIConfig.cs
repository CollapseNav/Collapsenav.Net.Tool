using Collapsenav.Net.Tool.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.WebApi.Test;
    public class DIConfig
    {
        public static ServiceProvider GetProvider()
        {
            return new ServiceCollection()
            .AddAutoMapper(typeof(MappingProfile))
            .AddDbContext<TestDbContext>(options =>
            {
                options.UseSqlite("Data Source = Test.db;");
            })
            .AddTransient(typeof(DbContext), typeof(TestDbContext))
            .AddRepository()
            .AddController()
            .BuildServiceProvider();
        }
        public static ServiceProvider GetTestRepositoryProvider()
        {
            return new ServiceCollection()
            .AddDbContext<TestDbContext>(options =>
            {
                options.UseSqlite("Data Source = Test.db;");
            })
            .AddAutoMapper(typeof(MappingProfile))
            .AddTransient(typeof(DbContext), typeof(TestDbContext))
            .AddRepository()
            .AddController()
            .BuildServiceProvider();
        }
}
