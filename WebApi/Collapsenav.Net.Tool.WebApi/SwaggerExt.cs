using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Collapsenav.Net.Tool.WebApi;

public static class SwaggerExt
{
    /// <summary>
    /// 带注释的Swagger
    /// </summary>
    public static IServiceCollection AddSwaggerWithComments(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.DocInclusionPredicate((_, _) => true);
            foreach (var dir in new DirectoryInfo(AppContext.BaseDirectory).GetFiles("*.xml"))
                options.IncludeXmlComments(dir.FullName, true);
        });
        return services;
    }
    /// <summary>
    /// 带JWT的Swagger
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwaggerWithJwtAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.DocInclusionPredicate((_, _) => true);
            foreach (var dir in new DirectoryInfo(AppContext.BaseDirectory).GetFiles("*.xml"))
                options.IncludeXmlComments(dir.FullName, true);
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JwtBearer Token",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = "Bearer",
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        return services;
    }
    /// <summary>
    /// 带JWT的Swagger
    /// </summary>
    /// <param name="services"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwaggerWithOAuth(this IServiceCollection services, string url)
    {
        services.AddSwaggerGen(options =>
        {
            options.DocInclusionPredicate((_, _) => true);
            foreach (var dir in new DirectoryInfo(AppContext.BaseDirectory).GetFiles("*.xml"))
                options.IncludeXmlComments(dir.FullName, true);
            options.AddSecurityDefinition("OAuth", new OpenApiSecurityScheme
            {
                Description = "OAuth",
                Type = SecuritySchemeType.OAuth2,
                Flows = new()
                {
                    Implicit = new()
                    {
                        AuthorizationUrl = new($"{url}/connect/authorize"),
                    }
                }
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "OAuth"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        return services;
    }
}