using AutoMapper;
using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;

[ApiController]
[Route("[controller]")]
public class CrudRepController<TKey, T, CreateT, GetT> : ControllerBase, ICrudRepController<TKey, T, CreateT, GetT>
    where T : class, IBaseEntity<TKey>
    where CreateT : IBaseCreate<T>
    where GetT : IBaseGet<T>
{
    ICrudRepository<TKey, T> Repo;
    IModifyRepController<TKey, T, CreateT> Write;
    IQueryRepController<TKey, T, GetT> Read;
    public CrudRepController(ICrudRepository<TKey, T> repo, IMapper mapper)
    {
        Repo = repo;
        Write = new ModifyRepController<TKey, T, CreateT>(Repo, mapper);
        Read = new QueryRepController<TKey, T, GetT>(Repo);
    }

    /// <summary>
    /// 添加(单个)
    /// </summary>
    [HttpPost]
    public virtual async Task<T> AddAsync([FromBody] CreateT entity)
    {
        return await Write.AddAsync(entity);
    }

    /// <summary>
    /// 添加(多个)
    /// </summary>
    [HttpPost, Route("AddRange")]
    public virtual async Task<int> AddRangeAsync(IEnumerable<CreateT> entitys)
    {
        return await Write.AddRangeAsync(entitys);
    }

    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    [HttpDelete, Route("{id}")]
    public virtual async Task DeleteAsync(TKey id, [FromQuery] bool isTrue = false)
    {
        await Write.DeleteAsync(id, isTrue);
    }

    /// <summary>
    /// 删除(多个 id)
    /// </summary>
    [HttpDelete]
    public virtual async Task<int> DeleteRangeAsync(IEnumerable<TKey> id, [FromQuery] bool isTrue = false)
    {
        return await Write.DeleteRangeAsync(id, isTrue);
    }
    /// <summary>
    /// 更新
    /// </summary>
    [HttpPut, Route("{id}")]
    public virtual async Task UpdateAsync(TKey id, CreateT entity)
    {
        await Write.UpdateAsync(id, entity);
    }

    public virtual void Dispose()
    {
        Repo.Save();
        Write.Dispose();
        // Read.Dispose();
    }
    [NonAction]
    public virtual IQueryable<T> GetQuery(GetT input)
    {
        return Read.GetQuery(input);
    }
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    [HttpGet, Route("{id}")]
    public virtual async Task<T> QueryAsync(TKey id)
    {
        return await Read.QueryAsync(id);
    }
    /// <summary>
    /// 带条件查询(不分页)
    /// </summary>
    [HttpGet, Route("Query")]
    public virtual async Task<IEnumerable<T>> QueryAsync([FromQuery] GetT input)
    {
        return await Read.QueryAsync(input);
    }
    /// <summary>
    /// 根据Ids查询
    /// </summary>
    [HttpGet, Route("ByIds")]
    public virtual async Task<IEnumerable<T>> QueryByIdsAsync([FromQuery] IEnumerable<TKey> ids)
    {
        return await Read.QueryByIdsAsync(ids);
    }
    /// <summary>
    /// 根据Ids查询
    /// </summary>
    [HttpPost, Route("ByIds")]
    public virtual async Task<IEnumerable<T>> QueryByIdsPostAsync(IEnumerable<TKey> ids)
    {
        return await Read.QueryByIdsPostAsync(ids);
    }
    /// <summary>
    /// 带条件分页
    /// </summary>
    [HttpGet]
    public virtual async Task<PageData<T>> QueryPageAsync([FromQuery] GetT input, [FromQuery] PageRequest page = null)
    {
        return await Read.QueryPageAsync(input, page);
    }
}