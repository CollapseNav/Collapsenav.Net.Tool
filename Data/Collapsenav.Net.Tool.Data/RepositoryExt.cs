using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.Data;
public static class RepositoryExt
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services
        .AddTransient(typeof(IRepository<>), typeof(Repository<>))
        .AddTransient(typeof(IQueryRepository<>), typeof(QueryRepository<>))
        .AddTransient(typeof(ICheckExistRepository<>), typeof(ReadRepository<>))
        .AddTransient(typeof(ICountRepository<>), typeof(ReadRepository<>))
        .AddTransient(typeof(IModifyRepository<>), typeof(ModifyRepository<>))
        .AddTransient(typeof(ICrudRepository<>), typeof(CrudRepository<>))
        .AddTransient(typeof(IReadRepository<>), typeof(ReadRepository<>))



        .AddTransient(typeof(IRepository<,>), typeof(Repository<,>))
        .AddTransient(typeof(IReadRepository<,>), typeof(ReadRepository<,>))
        .AddTransient(typeof(IQueryRepository<,>), typeof(QueryRepository<,>))
        .AddTransient(typeof(IModifyRepository<,>), typeof(ModifyRepository<,>))
        .AddTransient(typeof(ICrudRepository<,>), typeof(CrudRepository<,>))
        .AddDefaultIdGenerator()
        ;
        return services;
    }
    public static IServiceCollection AddRepository(this IServiceCollection services, params Type[] types)
    {
        services.AddRepository();
        foreach (var type in types)
            services.AddTransient(type);
        return services;
    }
}
