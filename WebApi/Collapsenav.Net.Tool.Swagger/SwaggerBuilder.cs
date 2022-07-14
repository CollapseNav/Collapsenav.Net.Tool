using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Collapsenav.Net.Tool.WebApi;

public class SwaggerBuilder
{
    private readonly Dictionary<string, Action<SwaggerGenOptions>> Actions;
    /// <summary>
    /// Api信息配置
    /// </summary>
    private OpenApiInfo info;
    /// <summary>
    /// 使用注释
    /// </summary>
    public bool UseComments
    {
        get => Actions?.ContainsKey(nameof(UseComments)) ?? false;
        set
        {
            if (value)
                Actions.AddOrUpdate(nameof(UseComments), options =>
                {
                    foreach (var dir in new DirectoryInfo(AppContext.BaseDirectory).GetFiles("*.xml"))
                        options.IncludeXmlComments(dir.FullName, true);
                });
            else
                Actions.Remove(nameof(UseComments));
        }
    }
    /// <summary>
    /// 使用jwt
    /// </summary>
    public bool UseJwtAuth
    {
        get => Actions?.ContainsKey(nameof(UseJwtAuth)) ?? false;
        set
        {
            if (value)
            {
                Actions.AddOrUpdate(nameof(UseJwtAuth), options =>
                {
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
            }
            else
                Actions.Remove(nameof(UseJwtAuth));
        }
    }
    public OpenApiInfo Info
    {
        get => info;
        set
        {
            info = value;
            if (value != null)
                Actions.AddOrUpdate(nameof(Info), options => options.SwaggerDoc(info.Version, info));
            else
            {
                info = null;
                Actions.Remove(nameof(Info));
            }
        }
    }

    public SwaggerBuilder AddInfo(OpenApiInfo openApiInfo)
    {
        Info = openApiInfo;
        return this;
    }
    public SwaggerBuilder AddJwtAuth(bool useJwtAuth = true)
    {
        if (useJwtAuth && !UseJwtAuth) 
            UseJwtAuth = true;
        else UseJwtAuth = false;
        return this;
    }
    public SwaggerBuilder AddComments(bool useComments = true)
    {
        if (useComments && !UseComments)
            UseComments = true;
        else UseComments = false;
        return this;
    }

    public SwaggerBuilder()
    {
        Actions = new()
        {
            {"Default",options => options.DocInclusionPredicate((_, _) => true)}
        };
    }
    public static SwaggerBuilder DefaultBuilder()
    {
        return new SwaggerBuilder()
        {
            UseComments = true,
            UseJwtAuth = true
        };
    }

    public SwaggerGenOptions BuildGenOptions(SwaggerGenOptions Options)
    {
        Actions.ForEach(item => item.Value(Options));
        return Options;
    }
}