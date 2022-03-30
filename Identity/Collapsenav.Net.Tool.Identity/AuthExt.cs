using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Collapsenav.Net.Tool.Identity;

public static class AuthExt
{
    public static AuthenticationBuilder AddJwtAuth(this IServiceCollection services, TokenValidationParameters tokenparam, bool requireHttps = false, bool saveToken = true)
    {
        return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.SaveToken = saveToken;
            options.RequireHttpsMetadata = requireHttps;
            options.TokenValidationParameters = tokenparam;
        });
    }

    public static AuthenticationBuilder AddJwt(this AuthenticationBuilder builder, TokenValidationParameters tokenparam, bool requireHttps = false, bool saveToken = true)
    {
        return builder.AddJwtBearer(options =>
        {
            options.SaveToken = saveToken;
            options.RequireHttpsMetadata = requireHttps;
            options.TokenValidationParameters = tokenparam;
        });
    }
}