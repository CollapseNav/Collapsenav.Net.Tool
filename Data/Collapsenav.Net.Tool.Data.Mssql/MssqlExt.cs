using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.Data;
public static class MssqlExt
{
    public static IServiceCollection AddSqlServer<T>(this IServiceCollection services, SqlServerConn conn, Assembly ass = null) where T : DbContext
    {
        return services.AddSqlServer<T>(conn.GetConnString(), ass);
    }
    public static IServiceCollection AddSqlServer<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd, Assembly ass = null) where T : DbContext
    {
        return services.AddSqlServer<T>(new SqlServerConn(source, port, dataBase, user, pwd), ass);
    }
    public static IServiceCollection AddSqlServerPool<T>(this IServiceCollection services, SqlServerConn conn, Assembly ass = null) where T : DbContext
    {
        return services.AddSqlServerPool<T>(conn.GetConnString(), ass);
    }
    public static IServiceCollection AddSqlServerPool<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd, Assembly ass = null) where T : DbContext
    {
        return services.AddSqlServerPool<T>(new SqlServerConn(source, port, dataBase, user, pwd), ass);
    }


    public static IServiceCollection AddSqlServer<T>(this IServiceCollection services, string connstring, Assembly ass = null) where T : DbContext
    {
        return services.AddDbContext<T>(builder => builder.UseSqlServer(connstring, ass == null ? null : m => m.MigrationsAssembly(ass?.GetName().Name)));
    }
    public static IServiceCollection AddSqlServerPool<T>(this IServiceCollection services, string connstring, Assembly ass = null) where T : DbContext
    {
        return services.AddDbContextPool<T>(builder => builder.UseSqlServer(connstring, ass == null ? null : m => m.MigrationsAssembly(ass?.GetName().Name)));
    }
}
