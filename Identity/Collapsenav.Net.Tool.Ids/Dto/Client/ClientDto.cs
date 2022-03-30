using System.ComponentModel.DataAnnotations;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;

namespace Collapsenav.Net.Tool.Ids;

public class ClientDto : IDSDto<Client, IdentityServer4.EntityFramework.Entities.Client>
{
    public int? Id { get; set; }
    /// <summary>
    /// 唯一标识
    /// </summary>
    [Required]
    public string ClientId { get; set; }
    /// <summary>
    /// code/client/password  三选一
    /// </summary>
    [Required]
    public string ClientType { get; set; }
    private string Secret;
    /// <summary>
    /// 客户端的Secret密钥
    /// </summary>
    [Required]
    public string ClientSecret
    {
        get => Secret;
        set => Secret = value;
    }
    /// <summary>
    /// token的生命周期 有效时间 单位分钟
    /// </summary>
    /// <remarks>默认 5 分钟</remarks>
    public int? TokenLifeTime { get; set; } = 5;
    /// <summary>
    /// 跨域设置
    /// </summary>
    public ICollection<string> AllowedCorsOrigins { get; set; }
    /// <summary>
    /// 允许的 api scope
    /// </summary>
    public ICollection<string> AllowedScopes { get; set; }
    /// <summary>
    /// 登录的重定向uri
    /// </summary>
    public ICollection<string> RedirectUris { get; set; }
    /// <summary>
    /// 注销的重定向uri
    /// </summary>
    public ICollection<string> PostLogoutRedirectUris { get; set; }

    public string Description { get; set; }
    /// <summary>
    /// 根据 ClientType 生成 不需要传
    /// </summary>
    public ICollection<string> GrandTypes
    {
        get => ClientType switch
        {
            "code" => GrantTypes.Code,
            "client" => GrantTypes.ClientCredentials,
            "password" => GrantTypes.ResourceOwnerPassword,
            _ => null
        };
    }
    /// <summary>
    /// 建议不填
    /// </summary>
    public bool? RequireConsent { get; set; } = false;
    /// <summary>
    /// 建议不填
    /// </summary>
    public bool? RequirePkce { get; set; } = false;
    /// <summary>
    /// 建议不填
    /// </summary>
    public bool? AllowOfflineAccess { get; set; } = true;
    /// <summary>
    /// 建议不填
    /// </summary>
    public AccessTokenType AccessTokenType { get; set; } = AccessTokenType.Jwt;
    /// <summary>
    /// 建议不填
    /// </summary>
    public bool? AlwaysIncludeUserClaimsInIdToken { get; set; } = true;

    public Client ToItem()
    {
        return new Client
        {
            // 主要参数
            ClientId = ClientId,
            AllowedGrantTypes = GrandTypes,
            ClientSecrets = { new Secret(ClientSecret.Sha256(), $"{ClientId} Sec") },
            AllowedScopes = AllowedScopes,
            AccessTokenLifetime = TokenLifeTime.Value * 60,

            // 次要参数
            RedirectUris = RedirectUris,
            PostLogoutRedirectUris = PostLogoutRedirectUris,
            Description = Description,
            AllowedCorsOrigins = AllowedCorsOrigins,

            //不知道有啥用的参数
            RequireConsent = RequireConsent.Value,
            AllowOfflineAccess = AllowOfflineAccess.Value,
            RequirePkce = RequirePkce.Value,
            AccessTokenType = AccessTokenType,
            AlwaysIncludeUserClaimsInIdToken = AlwaysIncludeUserClaimsInIdToken.Value,
        };
    }

    public IdentityServer4.EntityFramework.Entities.Client ToEntity()
    {
        return ToItem().ToEntity();
    }
}
