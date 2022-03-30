using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.Ids;

#if NETCOREAPP3_1_OR_GREATER
[ApiController]
[Route("[controller]")]
public class ApiScopeController : ControllerBase, IApiScopeController
{
    private readonly IApiScopeApplication app;
    public ApiScopeController(IApiScopeApplication application)
    {
        app = application;
    }
    [HttpPost("Add")]
    public virtual async Task<ApiScope> AddAsync(ApiScopeDto entity)
    {
        return await app.AddAsync(entity);
    }
    [HttpPost("AddRange")]
    public virtual async Task<int> AddRangeAsync(IEnumerable<ApiScopeDto> entitys)
    {
        return await app.AddRangeAsync(entitys);
    }
    [HttpDelete("Delete")]
    public virtual async Task DeleteAsync(int id, [FromQuery] bool isTrue = false)
    {
        await app.DeleteAsync(id, isTrue);
    }

    [NonAction]
    public virtual async Task DeleteAsync(string id, bool isTrue = false)
    {
        await app.DeleteAsync(id, isTrue);
    }

    [HttpDelete("DeleteRange")]
    public virtual async Task<int> DeleteRangeAsync(IEnumerable<int> id, bool isTrue = false)
    {
        return await app.DeleteRangeAsync(id, isTrue);
    }

    public void Dispose()
    {
    }

    [NonAction]
    public IQueryable<ApiScope> GetQuery(ApiScopeGet input)
    {
        return null;
    }

    [HttpGet("Query")]
    public virtual async Task<IEnumerable<ApiScope>> QueryAsync([FromQuery] ApiScopeGet input)
    {
        return await app.QueryAsync(input);
    }
    [HttpGet("{id}")]
    public virtual async Task<ApiScope> QueryAsync(int id)
    {
        return await app.QueryAsync(id);
    }
    [NonAction]
    public Task<ApiScope> QueryAsync(string id)
    {
        return null;
    }

    [HttpGet("ByIds")]
    public virtual async Task<IEnumerable<ApiScope>> QueryByIdsAsync([FromQuery] IEnumerable<int> ids)
    {
        return await app.QueryByIdsAsync(ids);
    }

    [HttpPost("ByIds")]
    public virtual async Task<IEnumerable<ApiScope>> QueryByIdsPostAsync(IEnumerable<int> ids)
    {
        return await app.QueryByIdsAsync(ids);
    }

    [HttpGet("QueryPage")]
    public virtual async Task<PageData<ApiScope>> QueryPageAsync([FromQuery] ApiScopeGet input, [FromQuery] PageRequest page = null)
    {
        return await app.QueryPageAsync(input, page);
    }
    [HttpPut("Update")]
    public virtual async Task UpdateAsync(int id, ApiScopeDto entity)
    {
        await app.UpdateAsync(id, entity);
    }
}
#endif