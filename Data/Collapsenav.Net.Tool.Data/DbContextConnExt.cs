using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.Data;
public static class DbContextConnExt
{
    public static IServiceCollection AddSqlite<T>(this IServiceCollection services, SqliteConn conn) where T : DbContext
    {
        return services.AddSqlite<T>(conn.GetConnString());
    }
    public static IServiceCollection AddSqlite<T>(this IServiceCollection services, string db) where T : DbContext
    {
        return services.AddDbContext<T>(options => options.UseSqlite(db.Contains('=') ? db : new SqliteConn(db).GetConnString()));
    }
    public static IServiceCollection AddSqlitePool<T>(this IServiceCollection services, SqliteConn conn, Assembly ass = null) where T : DbContext
    {
        return services.AddSqlitePool<T>(conn.GetConnString());
    }
    public static IServiceCollection AddSqlitePool<T>(this IServiceCollection services, string db, Assembly ass = null) where T : DbContext
    {
        return services.AddDbContextPool<T>(options => options.UseSqlite(db.Contains('=') ? db : new SqliteConn(db).GetConnString()));
    }

    /// <summary>
    /// 将 T 注册为默认的 DbContext
    /// </summary>
    public static IServiceCollection AddDefaultDbContext<T>(this IServiceCollection services) where T : DbContext
    {
        services.AddScoped<DbContext, T>();
        return services;
    }
    /// <summary>
    /// 注册默认id生成
    /// </summary>
    /// <remarks>
    /// 暂时只支持 Guid, Guid?, long, long?
    /// </remarks>
    public static IServiceCollection AddDefaultIdGenerator(this IServiceCollection services)
    {
        BaseEntity<Guid>.GetKey ??= () => Guid.NewGuid();
        BaseEntity<Guid?>.GetKey ??= () => Guid.NewGuid();
        BaseEntity<long>.GetKey ??= () => SnowFlake.NextId();
        BaseEntity<long?>.GetKey ??= () => SnowFlake.NextId();
        return services;
    }
}
