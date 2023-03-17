using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;
namespace Collapsenav.Net.Tool.WebApi;

[ApiController]
[Route("[controller]")]
public class QueryRepController<T, GetT> : ControllerBase, IQueryController<T, GetT>
    where T : class, IEntity
    where GetT : IBaseGet<T>
{
    protected readonly IQueryRepository<T> Repository;
    public QueryRepController(IQueryRepository<T> repository) : base()
    {
        Repository = repository;
    }
    /// <summary>
    /// 带条件分页
    /// </summary>
    [HttpGet]
    public virtual async Task<PageData<T>> QueryPageAsync([FromQuery] GetT input, [FromQuery] PageRequest page = null) => await Repository.QueryPageAsync(input.GetQuery(Repository.Query()), page);
    /// <summary>
    /// 带条件查询(不分页)
    /// </summary>
    [HttpGet, Route("Query")]
    public virtual async Task<IEnumerable<T>> QueryAsync([FromQuery] GetT input) => await Repository.QueryAsync(input.GetQuery(Repository.Query()));
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    [HttpGet, Route("{id}")]
    public virtual async Task<T> QueryAsync(string id) => await Repository.GetByIdAsync(id);
}
[ApiController]
[Route("[controller]")]
public class QueryRepController<TKey, T, GetT> : QueryRepController<T, GetT>, IQueryController<TKey, T, GetT>
    where T : class, IEntity<TKey>
    where GetT : IBaseGet<T>
{
    protected new IQueryRepository<TKey, T> Repository;
    public QueryRepController(IQueryRepository<TKey, T> repository) : base(repository)
    {
        Repository = repository;
    }
    [NonAction]
    public new Task<T> QueryAsync(string id) => base.QueryAsync(id);
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    [HttpGet, Route("{id}")]
    public virtual async Task<T> QueryAsync(TKey id) => await Repository.GetByIdAsync(id);
    /// <summary>
    /// 根据Ids查询
    /// </summary>
    [HttpGet, Route("ByIds")]
    public virtual async Task<IEnumerable<T>> QueryByIdsAsync([FromQuery] IEnumerable<TKey> ids) => await Repository.QueryAsync(ids);
    /// <summary>
    /// 根据Ids查询
    /// </summary>
    [HttpPost, Route("ByIds")]
    public virtual async Task<IEnumerable<T>> QueryByIdsPostAsync(IEnumerable<TKey> ids) => await Repository.QueryAsync(ids);
}

