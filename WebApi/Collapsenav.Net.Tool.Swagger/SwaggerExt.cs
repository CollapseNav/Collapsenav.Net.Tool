using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.WebApi;

public static class SwaggerExt
{
    public static IServiceCollection AddDefaultSwaggerGen(this IServiceCollection services)
    {
        return services.AddSwaggerGen(options => SwaggerBuilder.DefaultBuilder().BuildGenOptions(options));
    }
    public static IServiceCollection AddSwaggerGen(this IServiceCollection services, SwaggerBuilder builder)
    {
        return services.AddSwaggerGen(options => (builder ?? SwaggerBuilder.DefaultBuilder()).BuildGenOptions(options));
    }
    /// <summary>
    /// 带注释的Swagger
    /// </summary>
    public static IServiceCollection AddSwaggerWithComments(this IServiceCollection services)
    {
        return services.AddSwaggerGen(new SwaggerBuilder { UseComments = true });
    }
    /// <summary>
    /// 带JWT的Swagger
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwaggerWithJwtAuth(this IServiceCollection services)
    {
        return services.AddSwaggerGen(new SwaggerBuilder { UseJwtAuth = true, UseComments = true });
    }
}