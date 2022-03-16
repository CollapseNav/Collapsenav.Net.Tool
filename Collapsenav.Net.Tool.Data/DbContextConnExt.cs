using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.Data;
public static class DbContextConnExt
{
    public static IServiceCollection AddDbContext<T, E>(this IServiceCollection services, E conn, bool isPool = false) where T : DbContext where E : Conn
    {
        services.AddDefaultIdGenerator();
        if (typeof(T) == typeof(PgsqlConn))
            BaseEntity.GetNow = () => DateTime.UtcNow;
        if (isPool)
            return conn switch
            {
                SqliteConn => services.AddDbContextPool<T>(options => options.UseSqlite(conn.ToString())),
                SqlServerConn => services.AddDbContextPool<T>(options => options.UseSqlServer(conn.ToString())),
#if NETSTANDARD2_0
            MysqlConn => services.AddDbContextPool<T>(options => options.UseMySql(conn.ToString())),
            MariaDbConn => services.AddDbContextPool<T>(options => options.UseMySql(conn.ToString())),
#else
                MysqlConn => services.AddDbContextPool<T>(options => options.UseMySql(conn.ToString(), new MySqlServerVersion("8.0"))),
                MariaDbConn => services.AddDbContextPool<T>(options => options.UseMySql(conn.ToString(), new MySqlServerVersion("8.0"))),
#endif
                PgsqlConn => services.AddDbContextPool<T>(options => options.UseSqlite(conn.ToString())),
                _ => services
            };
        return conn switch
        {
            SqliteConn => services.AddDbContext<T>(options => options.UseSqlite(conn.ToString())),
            SqlServerConn => services.AddDbContext<T>(options => options.UseSqlServer(conn.ToString())),
#if NETSTANDARD2_0
            MysqlConn => services.AddDbContext<T>(options => options.UseMySql(conn.ToString())),
            MariaDbConn => services.AddDbContext<T>(options => options.UseMySql(conn.ToString())),
#else
            MysqlConn => services.AddDbContext<T>(options => options.UseMySql(conn.ToString(), new MySqlServerVersion("8.0"))),
            MariaDbConn => services.AddDbContext<T>(options => options.UseMySql(conn.ToString(), new MySqlServerVersion("8.0"))),
#endif
            PgsqlConn => services.AddDbContext<T>(options => options.UseSqlite(conn.ToString())),
            _ => services
        };
    }
    public static IServiceCollection AddSqlite<T>(this IServiceCollection services, SqliteConn conn) where T : DbContext
    {
        return services.AddDbContext<T, SqliteConn>(conn);
    }
    public static IServiceCollection AddSqlServer<T>(this IServiceCollection services, SqlServerConn conn) where T : DbContext
    {
        return services.AddDbContext<T, SqlServerConn>(conn);
    }
    public static IServiceCollection AddMysql<T>(this IServiceCollection services, MysqlConn conn) where T : DbContext
    {
        return services.AddDbContext<T, MysqlConn>(conn);
    }
    public static IServiceCollection AddMariaDb<T>(this IServiceCollection services, MariaDbConn conn) where T : DbContext
    {
        return services.AddDbContext<T, MariaDbConn>(conn);
    }
    public static IServiceCollection AddPgSql<T>(this IServiceCollection services, PgsqlConn conn) where T : DbContext
    {
        return services.AddDbContext<T, PgsqlConn>(conn);
    }


    public static IServiceCollection AddSqlite<T>(this IServiceCollection services, string db) where T : DbContext
    {
        return services.AddDbContext<T, SqliteConn>(new SqliteConn(db));
    }
    public static IServiceCollection AddSqlServer<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd) where T : DbContext
    {
        return services.AddDbContext<T, SqlServerConn>(new SqlServerConn(source, port, dataBase, user, pwd));
    }
    public static IServiceCollection AddMysql<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd) where T : DbContext
    {
        return services.AddDbContext<T, MysqlConn>(new MysqlConn(source, port, dataBase, user, pwd));
    }
    public static IServiceCollection AddMariaDb<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd) where T : DbContext
    {
        return services.AddDbContext<T, MariaDbConn>(new MariaDbConn(source, port, dataBase, user, pwd));
    }
    public static IServiceCollection AddPgSql<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd) where T : DbContext
    {
        return services.AddDbContext<T, PgsqlConn>(new PgsqlConn(source, port, dataBase, user, pwd));
    }



    public static IServiceCollection AddSqlitePool<T>(this IServiceCollection services, SqliteConn conn) where T : DbContext
    {
        return services.AddDbContext<T, SqliteConn>(conn, true);
    }
    public static IServiceCollection AddSqlServerPool<T>(this IServiceCollection services, SqlServerConn conn) where T : DbContext
    {
        return services.AddDbContext<T, SqlServerConn>(conn, true);
    }
    public static IServiceCollection AddMysqlPool<T>(this IServiceCollection services, MysqlConn conn) where T : DbContext
    {
        return services.AddDbContext<T, MysqlConn>(conn, true);
    }
    public static IServiceCollection AddMariaDbPool<T>(this IServiceCollection services, MariaDbConn conn) where T : DbContext
    {
        return services.AddDbContext<T, MariaDbConn>(conn, true);
    }
    public static IServiceCollection AddPgSqlPool<T>(this IServiceCollection services, PgsqlConn conn) where T : DbContext
    {
        return services.AddDbContext<T, PgsqlConn>(conn, true);
    }


    public static IServiceCollection AddSqlitePool<T>(this IServiceCollection services, string db) where T : DbContext
    {
        return services.AddDbContext<T, SqliteConn>(new SqliteConn(db), true);
    }
    public static IServiceCollection AddSqlServerPool<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd) where T : DbContext
    {
        return services.AddDbContext<T, SqlServerConn>(new SqlServerConn(source, port, dataBase, user, pwd), true);
    }
    public static IServiceCollection AddMysqlPool<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd) where T : DbContext
    {
        return services.AddDbContext<T, MysqlConn>(new MysqlConn(source, port, dataBase, user, pwd), true);
    }
    public static IServiceCollection AddMariaDbPool<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd) where T : DbContext
    {
        return services.AddDbContext<T, MariaDbConn>(new MariaDbConn(source, port, dataBase, user, pwd), true);
    }
    public static IServiceCollection AddPgSqlPool<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd) where T : DbContext
    {
        return services.AddDbContext<T, PgsqlConn>(new PgsqlConn(source, port, dataBase, user, pwd), true);
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
