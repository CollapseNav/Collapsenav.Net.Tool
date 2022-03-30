using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.Ids;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase, IClientController
{
    private readonly IClientApplication app;
    public ClientController(IClientApplication app)
    {
        this.app = app;
    }
    /// <summary>
    /// 添加(单个)
    /// </summary>
    [HttpPost("Add")]
    public virtual async Task<Client> AddAsync(ClientDto input)
    {
        return await app.AddAsync(input);
    }
    /// <summary>
    /// 添加(多个)
    /// </summary>
    [HttpPost("AddRange")]
    public virtual async Task<int> AddRangeAsync(IEnumerable<ClientDto> entitys)
    {
        return await app.AddRangeAsync(entitys);
    }
    [NonAction]
    public virtual async Task DeleteAsync(string id, [FromQuery] bool isTrue = false)
    {
        await app.DeleteAsync(id, isTrue);
    }
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    [HttpDelete]
    public virtual async Task DeleteAsync(int id, [FromQuery] bool isTrue = false)
    {
        await app.DeleteAsync(id, isTrue);
    }
    /// <summary>
    /// 删除(多个 id)
    /// </summary>
    [HttpDelete("DeleteRange")]
    public virtual async Task<int> DeleteRangeAsync(IEnumerable<int> id, bool isTrue = false)
    {
        return await app.DeleteRangeAsync(id, isTrue);
    }

    public void Dispose()
    {
    }
    /// <summary>
    /// 获取 client
    /// </summary>
    [HttpGet("Get")]
    public virtual async Task<Client> GetAsync([FromQuery] string clientId)
    {
        return await app.GetAsync(clientId);
    }

    [NonAction]
    public IQueryable<Client> GetQuery(ClientGet input)
    {
        return null;
    }

    /// <summary>
    /// 检查该 clientid 是否被使用
    /// </summary>
    [HttpGet("IsExist/{clientId}")]
    public virtual async Task<bool> IsExist(string clientId)
    {
        return await app.IsExist(clientId);
    }
    /// <summary>
    /// 带条件查询(不分页)
    /// </summary>
    [HttpGet("Query")]
    public virtual async Task<IEnumerable<Client>> QueryAsync([FromQuery] ClientGet input)
    {
        return await app.QueryAsync(input);
    }
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    [HttpGet("{id}")]
    public virtual async Task<Client> QueryAsync(int id)
    {
        return await app.QueryAsync(id);
    }
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    [HttpGet("QueryByStringId")]
    public virtual async Task<Client> QueryAsync(string id)
    {
        return await app.QueryByStringIdAsync(id);
    }
    /// <summary>
    /// 根据Id查询
    /// </summary>
    [HttpGet("ByIds")]
    public virtual async Task<IEnumerable<Client>> QueryByIdsAsync([FromQuery] IEnumerable<int> ids)
    {
        return await app.QueryByIdsAsync(ids);
    }
    /// <summary>
    /// 根据Id查询
    /// </summary>
    [HttpPost("ByIds")]
    public virtual async Task<IEnumerable<Client>> QueryByIdsPostAsync(IEnumerable<int> ids)
    {
        return await app.QueryByIdsAsync(ids);
    }
    /// <summary>
    /// 带条件分页
    /// </summary>
    [HttpGet("QueryPage")]
    public virtual async Task<PageData<Client>> QueryPageAsync([FromQuery] ClientGet input, [FromQuery] PageRequest page = null)
    {
        return await app.QueryPageAsync(input, page);
    }
    /// <summary>
    /// 修改 apiscope
    /// </summary>
    [HttpPut("UpdateApiScope")]
    public virtual async Task<int> UpdateApiScopeAsync(string clientId, IEnumerable<string> input)
    {
        return await app.UpdateApiScopeAsync(clientId, input);
    }

    [HttpPut("Update")]
    public virtual async Task UpdateAsync(int id, ClientDto entity)
    {
        entity.Id = id;
        await app.UpdateAsync(entity);
    }
    /// <summary>
    /// 修改 corsOrigins
    /// </summary>
    [HttpPut("UpdateCorsOrigins")]
    public virtual async Task<int> UpdateCorsOriginsAsync(string clientId, IEnumerable<string> input)
    {
        return await app.UpdateCorsOriginsAsync(clientId, input);
    }
    /// <summary>
    /// 修改 grantType
    /// </summary>
    [HttpPut("UpdateGrantType")]
    public virtual async Task<int> UpdateGrantTypeAsync(string clientId, string input)
    {
        return await app.UpdateGrantTypeAsync(clientId, input);
    }
    /// <summary>
    /// 修改 logoutRedirectUri
    /// </summary>
    [HttpPut("UpdateLogoutRedirectUri")]
    public virtual async Task<int> UpdateLogoutRedirectUriAsync(string clientId, IEnumerable<string> input)
    {
        return await app.UpdateLogoutRedirectUriAsync(clientId, input);
    }
    /// <summary>
    /// 修改 redirectUri
    /// </summary>
    [HttpPut("UpdateRedirectUri")]
    public virtual async Task<int> UpdateRedirectUriAsync(string clientId, IEnumerable<string> input)
    {
        return await app.UpdateRedirectUriAsync(clientId, input);
    }
    /// <summary>
    /// 修改 tokenLifeTime
    /// </summary>
    [HttpPut("UpdateTokenLifeTime")]
    public virtual async Task<int> UpdateTokenLifeTimeAsync(string clientId, int minute)
    {
        return await app.UpdateTokenLifeTimeAsync(clientId, minute);
    }
}