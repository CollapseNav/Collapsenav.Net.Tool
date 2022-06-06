using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.Data;
public static class MssqlExt
{
    public static IServiceCollection AddSqlServer<T>(this IServiceCollection services, SqlServerConn conn, Assembly ass = null) where T : DbContext
    {
        return services.AddDbContext<T, SqlServerConn>(conn, ass);
    }
    public static IServiceCollection AddSqlServer<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd, Assembly ass = null) where T : DbContext
    {
        return services.AddDbContext<T, SqlServerConn>(new SqlServerConn(source, port, dataBase, user, pwd), ass);
    }
    public static IServiceCollection AddSqlServerPool<T>(this IServiceCollection services, SqlServerConn conn, Assembly ass = null) where T : DbContext
    {
        return services.AddDbContextPool<T, SqlServerConn>(conn, ass);
    }
    public static IServiceCollection AddSqlServerPool<T>(this IServiceCollection services, string source, int? port, string dataBase, string user, string pwd, Assembly ass = null) where T : DbContext
    {
        return services.AddDbContextPool<T, SqlServerConn>(new SqlServerConn(source, port, dataBase, user, pwd), ass);
    }
}
