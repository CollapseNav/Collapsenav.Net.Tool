using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.Data;
public static class MysqlExt
{
    public static IServiceCollection AddMysql<T>(this IServiceCollection services, MysqlConn conn, Assembly ass = null) where T : DbContext
    {
        return services.AddMysql<T>(conn.GetConnString(), ass);
    }
    public static IServiceCollection AddMariaDb<T>(this IServiceCollection services, MariaDbConn conn, Assembly ass = null) where T : DbContext
    {
        return services.AddMariaDb<T>(conn.GetConnString(), ass);
    }
    public static IServiceCollection AddMysql<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd, Assembly ass = null) where T : DbContext
    {
        return services.AddMysql<T>(new MysqlConn(source, port, dataBase, user, pwd), ass);
    }
    public static IServiceCollection AddMariaDb<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd, Assembly ass = null) where T : DbContext
    {
        return services.AddMariaDb<T>(new MariaDbConn(source, port, dataBase, user, pwd), ass);
    }
    public static IServiceCollection AddMysqlPool<T>(this IServiceCollection services, MysqlConn conn, Assembly ass = null) where T : DbContext
    {
        return services.AddMysqlPool<T>(conn.GetConnString(), ass);
    }
    public static IServiceCollection AddMariaDbPool<T>(this IServiceCollection services, MariaDbConn conn, Assembly ass = null) where T : DbContext
    {
        return services.AddMariaDbPool<T>(conn.GetConnString(), ass);
    }
    public static IServiceCollection AddMysqlPool<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd, Assembly ass = null) where T : DbContext
    {
        return services.AddMysqlPool<T>(new MysqlConn(source, port, dataBase, user, pwd), ass);
    }
    public static IServiceCollection AddMariaDbPool<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd, Assembly ass = null) where T : DbContext
    {
        return services.AddMariaDbPool<T>(new MariaDbConn(source, port, dataBase, user, pwd), ass);
    }


    public static IServiceCollection AddMysql<T>(this IServiceCollection services, string connstring, Assembly ass = null) where T : DbContext
    {
#if NETSTANDARD2_0
        return services.AddDbContext<T>(builder => builder.UseMySql(connstring, ass == null ? null : m => m.MigrationsAssembly(ass?.GetName().Name)));
#else
        return services.AddDbContext<T>(builder => builder.UseMySql(connstring, ServerVersion.AutoDetect(connstring), ass == null ? null : m => m.MigrationsAssembly(ass?.GetName().Name)));
#endif
    }
    public static IServiceCollection AddMysqlPool<T>(this IServiceCollection services, string connstring, Assembly ass = null) where T : DbContext
    {
#if NETSTANDARD2_0
        return services.AddDbContextPool<T>(builder => builder.UseMySql(connstring, ass == null ? null : m => m.MigrationsAssembly(ass?.GetName().Name)));
#else
        return services.AddDbContextPool<T>(builder => builder.UseMySql(connstring, ServerVersion.AutoDetect(connstring), ass == null ? null : m => m.MigrationsAssembly(ass?.GetName().Name)));
#endif
    }
    public static IServiceCollection AddMariaDb<T>(this IServiceCollection services, string connstring, Assembly ass = null) where T : DbContext
    {
#if NETSTANDARD2_0
        return services.AddDbContext<T>(builder => builder.UseMySql(connstring, ass == null ? null : m => m.MigrationsAssembly(ass?.GetName().Name)));
#else
        return services.AddDbContext<T>(builder => builder.UseMySql(connstring, ServerVersion.AutoDetect(connstring), ass == null ? null : m => m.MigrationsAssembly(ass?.GetName().Name)));
#endif
    }
    public static IServiceCollection AddMariaDbPool<T>(this IServiceCollection services, string connstring, Assembly ass = null) where T : DbContext
    {
#if NETSTANDARD2_0
        return services.AddDbContextPool<T>(builder => builder.UseMySql(connstring, ass == null ? null : m => m.MigrationsAssembly(ass?.GetName().Name)));
#else
        return services.AddDbContextPool<T>(builder => builder.UseMySql(connstring, ServerVersion.AutoDetect(connstring), ass == null ? null : m => m.MigrationsAssembly(ass?.GetName().Name)));
#endif
    }
}
