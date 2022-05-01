using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Collapsenav.Net.Tool.WebApi;

public class SwaggerBuilder
{
    private Dictionary<string, Action<SwaggerGenOptions>> Actions;
    public bool? UseComments
    {
        get => Actions?.ContainsKey(nameof(UseComments)); set
        {
            if (value.Value)
                Actions.AddOrUpdate(nameof(UseComments), options =>
                {
                    foreach (var dir in new DirectoryInfo(AppContext.BaseDirectory).GetFiles("*.xml"))
                        options.IncludeXmlComments(dir.FullName, true);
                });
            else
                Actions.Remove(nameof(UseComments));
        }
    }
    public bool? UseJwtAuth
    {
        get => Actions?.ContainsKey(nameof(UseJwtAuth)); set
        {
            if (value.Value)
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
        get => info; set
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
    private OpenApiInfo info;

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