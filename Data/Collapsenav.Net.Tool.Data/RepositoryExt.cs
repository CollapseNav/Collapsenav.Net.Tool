using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.Data;
public static class RepositoryExt
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services
        .AddScoped(typeof(IRepository<>), typeof(Repository<>))
        .AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>))
        .AddScoped(typeof(ICheckExistRepository<>), typeof(ReadRepository<>))
        .AddScoped(typeof(ICountRepository<>), typeof(ReadRepository<>))
        .AddScoped(typeof(IModifyRepository<>), typeof(ModifyRepository<>))
        .AddScoped(typeof(ICrudRepository<>), typeof(CrudRepository<>))
        .AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>))



        .AddScoped(typeof(IRepository<,>), typeof(Repository<,>))
        .AddScoped(typeof(IReadRepository<,>), typeof(ReadRepository<,>))
        .AddScoped(typeof(IQueryRepository<,>), typeof(QueryRepository<,>))
        .AddScoped(typeof(IModifyRepository<,>), typeof(ModifyRepository<,>))
        .AddScoped(typeof(ICrudRepository<,>), typeof(CrudRepository<,>))
        .AddDefaultIdGenerator()
        ;
        return services;
    }
    public static IServiceCollection AddRepository(this IServiceCollection services, params Type[] types)
    {
        services.AddRepository();
        foreach (var type in types)
            services.AddScoped(type);
        return services;
    }
}
