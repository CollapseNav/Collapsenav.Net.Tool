using FreeRedis;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.Ext;

public static class RedisRegisterExt
{
    public static IServiceCollection AddFreeRedis(this IServiceCollection services)
    {
        return services
                .AddTransient<ICache, FreeRedisCache>()
                .AddTransient<IRedisCache, FreeRedisCache>();
    }
    public static IServiceCollection AddFreeRedis(this IServiceCollection services, string connection)
    {
        return services
                .AddFreeRedis()
                .AddSingleton<IRedisClient>(new RedisClient(connection))
                ;
    }
    /// <summary>
    /// 主从集群
    /// </summary>
    public static IServiceCollection AddFreeRedis(this IServiceCollection services, params string[] connections)
    {
        var conns = connections.Select(ConnectionStringBuilder.Parse);
        return services
                .AddFreeRedis()
                .AddSingleton<IRedisClient>(new RedisClient(conns.First(), conns.Skip(1).ToArray()))
                ;
    }

    public static IServiceCollection AddFreeRedisCluster(this IServiceCollection services, params string[] connections)
    {
        return services.AddFreeRedisCluster(connections.ToList());
    }
    public static IServiceCollection AddFreeRedisCluster(this IServiceCollection services, IEnumerable<string> connections)
    {
        return services
        .AddFreeRedis()
        .AddSingleton<IRedisClient>(new RedisClient(clusterConnectionStrings: connections.Select(ConnectionStringBuilder.Parse).ToArray()))
        ;
    }
}