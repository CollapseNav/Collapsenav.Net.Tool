using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.Data;
public static class DbContextConnExt
{
    public static IServiceCollection AddSqlite<T>(this IServiceCollection services, SqliteConn conn) where T : DbContext
    {
        services.AddDbContext<T>(options => options.UseSqlite(conn.ToString()));
        return services;
    }
    public static IServiceCollection AddSqlServer<T>(this IServiceCollection services, SqlServerConn conn) where T : DbContext
    {
        services.AddDbContext<T>(options => options.UseSqlServer(conn.ToString()));
        return services;
    }
    public static IServiceCollection AddMysql<T>(this IServiceCollection services, MysqlConn conn) where T : DbContext
    {
#if NETSTANDARD2_0
        services.AddDbContext<T>(options => options.UseMySql(conn.ToString()));
#else
        services.AddDbContext<T>(options => options.UseMySql(conn.ToString(), new MySqlServerVersion("8.0")));
#endif
        return services;
    }
    public static IServiceCollection AddPgSql<T>(this IServiceCollection services, PgsqlConn conn) where T : DbContext
    {
        BaseEntity.GetNow = () => DateTime.UtcNow;
        services.AddDbContext<T>(options => options.UseNpgsql(conn.ToString()));
        return services;
    }


    public static IServiceCollection AddSqlite<T>(this IServiceCollection services, string db) where T : DbContext
    {
        services.AddSqlite<T>(new SqliteConn(db));
        return services;
    }
    public static IServiceCollection AddSqlServer<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd) where T : DbContext
    {
        services.AddSqlServer<T>(new SqlServerConn(source, port, dataBase, user, pwd));
        return services;
    }
    public static IServiceCollection AddMysql<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd) where T : DbContext
    {
        services.AddMysql<T>(new MysqlConn(source, port, dataBase, user, pwd));
        return services;
    }
    public static IServiceCollection AddPgSql<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd) where T : DbContext
    {
        services.AddPgSql<T>(new PgsqlConn(source, port, dataBase, user, pwd));
        return services;
    }



    public static IServiceCollection AddSqlitePool<T>(this IServiceCollection services, SqliteConn conn) where T : DbContext
    {
        services.AddDbContextPool<T>(options => options.UseSqlite(conn.ToString()));
        return services;
    }
    public static IServiceCollection AddSqlServerPool<T>(this IServiceCollection services, SqlServerConn conn) where T : DbContext
    {
        services.AddDbContextPool<T>(options => options.UseSqlServer(conn.ToString()));
        return services;
    }
    public static IServiceCollection AddMysqlPool<T>(this IServiceCollection services, MysqlConn conn) where T : DbContext
    {
        services.AddDbContextPool<T>(options => options.UseMySql(conn.ToString(), new MySqlServerVersion("8.0")));
        return services;
    }
    public static IServiceCollection AddPgSqlPool<T>(this IServiceCollection services, PgsqlConn conn) where T : DbContext
    {
        BaseEntity.GetNow = () => DateTime.UtcNow;
        services.AddDbContextPool<T>(options => options.UseNpgsql(conn.ToString()));
        return services;
    }


    public static IServiceCollection AddSqlitePool<T>(this IServiceCollection services, string db) where T : DbContext
    {
        services.AddSqlitePool<T>(new SqliteConn(db));
        return services;
    }
    public static IServiceCollection AddSqlServerPool<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd) where T : DbContext
    {
        services.AddSqlServerPool<T>(new SqlServerConn(source, port, dataBase, user, pwd));
        return services;
    }
    public static IServiceCollection AddMysqlPool<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd) where T : DbContext
    {
        services.AddMysqlPool<T>(new MysqlConn(source, port, dataBase, user, pwd));
        return services;
    }
    public static IServiceCollection AddPgSqlPool<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd) where T : DbContext
    {
        services.AddPgSqlPool<T>(new PgsqlConn(source, port, dataBase, user, pwd));
        return services;
    }

    /// <summary>
    /// 将 T 注册为默认的 DbContext
    /// </summary>
    public static IServiceCollection AddDefaultDbContext<T>(this IServiceCollection services) where T : DbContext
    {
        services.AddScoped<DbContext, T>();
        return services;
    }
}
