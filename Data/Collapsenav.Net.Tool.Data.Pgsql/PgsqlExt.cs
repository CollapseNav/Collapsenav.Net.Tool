using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.Data;
public static class PgsqlExt
{
    public static IServiceCollection AddPgSql<T>(this IServiceCollection services, PgsqlConn conn, Assembly ass = null) where T : DbContext
    {
        BaseEntity.GetNow = () => DateTime.UtcNow;
        return services.AddPgSql<T>(conn.GetConnString(), ass);
    }
    public static IServiceCollection AddPgSql<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd, Assembly ass = null) where T : DbContext
    {
        BaseEntity.GetNow = () => DateTime.UtcNow;
        return services.AddPgSql<T>(new PgsqlConn(source, port, dataBase, user, pwd), ass);
    }
    public static IServiceCollection AddPgSqlPool<T>(this IServiceCollection services, PgsqlConn conn, Assembly ass = null) where T : DbContext
    {
        BaseEntity.GetNow = () => DateTime.UtcNow;
        return services.AddPgSqlPool<T>(conn.GetConnString(), ass);
    }
    public static IServiceCollection AddPgSqlPool<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd, Assembly ass = null) where T : DbContext
    {
        BaseEntity.GetNow = () => DateTime.UtcNow;
        return services.AddPgSqlPool<T>(new PgsqlConn(source, port, dataBase, user, pwd), ass);
    }


    public static IServiceCollection AddPgSql<T>(this IServiceCollection services, string connstring, Assembly ass = null) where T : DbContext
    {
        return services.AddDbContext<T>(builder => builder.UseNpgsql(connstring, ass == null ? null : m => m.MigrationsAssembly(ass?.GetName().Name)));
    }
    public static IServiceCollection AddPgSqlPool<T>(this IServiceCollection services, string connstring, Assembly ass = null) where T : DbContext
    {
        return services.AddDbContextPool<T>(builder => builder.UseNpgsql(connstring, ass == null ? null : m => m.MigrationsAssembly(ass?.GetName().Name)));
    }
}
