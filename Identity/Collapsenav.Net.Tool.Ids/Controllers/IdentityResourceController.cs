using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.Ids;

#if NETCOREAPP3_1_OR_GREATER
[ApiController]
[Route("[controller]")]
public class IdentityResourceController : ControllerBase, IIdentityResourceController
{
    private readonly IIdentityResourceApplication app;
    public IdentityResourceController(IIdentityResourceApplication application)
    {
        app = application;
    }
    [HttpPost("Add")]
    public virtual async Task<IdentityResource> AddAsync(IdentityResourceDto entity)
    {
        return await app.AddAsync(entity);
    }
    [HttpPost("AddRange")]
    public virtual async Task<int> AddRangeAsync(IEnumerable<IdentityResourceDto> entitys)
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
    public IQueryable<IdentityResource> GetQuery(IdentityResourceGet input)
    {
        return null;
    }

    [HttpGet("Query")]
    public virtual async Task<IEnumerable<IdentityResource>> QueryAsync([FromQuery] IdentityResourceGet input)
    {
        return await app.QueryAsync(input);
    }
    [HttpGet("{id}")]
    public virtual async Task<IdentityResource> QueryAsync(int id)
    {
        return await app.QueryAsync(id);
    }
    [NonAction]
    public Task<IdentityResource> QueryAsync(string id)
    {
        return null;
    }

    [HttpGet("ByIds")]
    public virtual async Task<IEnumerable<IdentityResource>> QueryByIdsAsync([FromQuery] IEnumerable<int> ids)
    {
        return await app.QueryByIdsAsync(ids);
    }

    [HttpPost("ByIds")]
    public virtual async Task<IEnumerable<IdentityResource>> QueryByIdsPostAsync(IEnumerable<int> ids)
    {
        return await app.QueryByIdsAsync(ids);
    }

    [HttpGet("QueryPage")]
    public virtual async Task<PageData<IdentityResource>> QueryPageAsync([FromQuery] IdentityResourceGet input, [FromQuery] PageRequest page = null)
    {
        return await app.QueryPageAsync(input, page);
    }
    [HttpPut("Update")]
    public virtual async Task UpdateAsync(int id, IdentityResourceDto entity)
    {
        await app.UpdateAsync(id, entity);
    }
}
#endif