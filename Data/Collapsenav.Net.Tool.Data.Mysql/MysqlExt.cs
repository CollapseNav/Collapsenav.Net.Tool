using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.Data;
public static class MysqlExt
{
    public static IServiceCollection AddMysql<T>(this IServiceCollection services, MysqlConn conn, Assembly ass = null) where T : DbContext
    {
        return services.AddDbContext<T, MysqlConn>(conn, ass);
    }
    public static IServiceCollection AddMariaDb<T>(this IServiceCollection services, MariaDbConn conn, Assembly ass = null) where T : DbContext
    {
        return services.AddDbContext<T, MariaDbConn>(conn, ass);
    }
    public static IServiceCollection AddMysql<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd, Assembly ass = null) where T : DbContext
    {
        return services.AddDbContext<T, MysqlConn>(new MysqlConn(source, port, dataBase, user, pwd), ass);
    }
    public static IServiceCollection AddMariaDb<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd, Assembly ass = null) where T : DbContext
    {
        return services.AddDbContext<T, MariaDbConn>(new MariaDbConn(source, port, dataBase, user, pwd), ass);
    }
    public static IServiceCollection AddMysqlPool<T>(this IServiceCollection services, MysqlConn conn, Assembly ass = null) where T : DbContext
    {
        return services.AddDbContextPool<T, MysqlConn>(conn, ass);
    }
    public static IServiceCollection AddMariaDbPool<T>(this IServiceCollection services, MariaDbConn conn, Assembly ass = null) where T : DbContext
    {
        return services.AddDbContextPool<T, MariaDbConn>(conn, ass);
    }
    public static IServiceCollection AddMysqlPool<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd, Assembly ass = null) where T : DbContext
    {
        return services.AddDbContextPool<T, MysqlConn>(new MysqlConn(source, port, dataBase, user, pwd), ass);
    }
    public static IServiceCollection AddMariaDbPool<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd, Assembly ass = null) where T : DbContext
    {
        return services.AddDbContextPool<T, MariaDbConn>(new MariaDbConn(source, port, dataBase, user, pwd), ass);
    }

}
