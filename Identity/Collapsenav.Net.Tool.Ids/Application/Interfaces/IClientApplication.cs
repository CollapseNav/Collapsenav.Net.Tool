using Collapsenav.Net.Tool.WebApi;
using IdentityServer4.EntityFramework.Entities;

namespace Collapsenav.Net.Tool.Ids;

public interface IClientApplication : INoConstraintsCrudApplication<int, Client, ClientDto, ClientGet>
{
    /// <summary>
    /// 检查该 clientid 是否被使用
    /// </summary>
    Task<bool> IsExist(string clientId);
    /// <summary>
    /// 检查 clientid 是否被使用
    /// </summary>
    Task<bool> IsExist(IEnumerable<string> clientId);
    /// <summary>
    /// 获取 client
    /// </summary>
    Task<Client> GetAsync(string clientId);
    /// <summary>
    /// 更新 client
    /// </summary>
    Task<int> UpdateAsync(ClientDto input);
    /// <summary>
    /// 更新 client
    /// </summary>
    Task<int> UpdateAsync(Client input);
    /// <summary>
    /// 更新 client
    /// </summary>
    Task<int> UpdateAsync(IdentityServer4.Models.Client input);
    /// <summary>
    /// 修改 grantType
    /// </summary>
    Task<int> UpdateGrantTypeAsync(string clientId, string input);
    /// <summary>
    /// 修改 apiscope
    /// </summary>
    Task<int> UpdateApiScopeAsync(string clientId, IEnumerable<string> input);
    /// <summary>
    /// 修改 redirectUri
    /// </summary>
    Task<int> UpdateRedirectUriAsync(string clientId, IEnumerable<string> input);
    /// <summary>
    /// 修改 logoutRedirectUri
    /// </summary>
    Task<int> UpdateLogoutRedirectUriAsync(string clientId, IEnumerable<string> input);
    /// <summary>
    /// 修改 corsOrigins
    /// </summary>
    Task<int> UpdateCorsOriginsAsync(string clientId, IEnumerable<string> input);
    /// <summary>
    /// 修改 tokenLifeTime
    /// </summary>
    Task<int> UpdateTokenLifeTimeAsync(string clientId, int minute);
}