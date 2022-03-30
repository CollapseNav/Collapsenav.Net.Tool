using System.Reflection;
using Collapsenav.Net.Tool.Data;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.Ids;

public static class IdsServiceExt
{
    public static IIdentityServerBuilder AddOperationalStore(this IIdentityServerBuilder builder, Conn conn, Assembly ass = null)
    {
        return builder.AddOperationalStore(options => options.ConfigureDbContext = conn.GenBuilder(ass));
    }
    public static IIdentityServerBuilder AddConfigurationStore(this IIdentityServerBuilder builder, Conn conn, Assembly ass = null)
    {
        return builder.AddConfigurationStore(options => options.ConfigureDbContext = conn.GenBuilder(ass));
    }
    public static IIdentityServerBuilder AddOperationalStore<T>(this IIdentityServerBuilder builder, Conn conn, Assembly ass = null)
        where T : DbContext, IPersistedGrantDbContext
    {
        return builder.AddOperationalStore<T>(options => options.ConfigureDbContext = conn.GenBuilder(ass));
    }
    public static IIdentityServerBuilder AddConfigurationStore<T>(this IIdentityServerBuilder builder, Conn conn, Assembly ass = null)
        where T : DbContext, IConfigurationDbContext
    {
        return builder.AddConfigurationStore<T>(options => options.ConfigureDbContext = conn.GenBuilder(ass));
    }
    public static IIdentityServerBuilder AddIdsStore(this IIdentityServerBuilder builder, Conn conn, Assembly ass = null)
    {
        builder.AddConfigurationStore(conn, ass)
        .AddOperationalStore(conn, ass);
        return builder;
    }
    public static IIdentityServerBuilder AddIdsStore<T, E>(this IIdentityServerBuilder builder, Conn conn, Assembly ass = null)
        where T : DbContext, IConfigurationDbContext
        where E : DbContext, IPersistedGrantDbContext
    {
        builder.AddConfigurationStore<T>(conn, ass)
        .AddOperationalStore<E>(conn, ass);
        return builder;
    }
}
