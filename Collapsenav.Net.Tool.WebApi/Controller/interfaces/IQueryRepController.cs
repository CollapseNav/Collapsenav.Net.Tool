using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;
public interface IQueryRepController<T, GetT> : IRepController
    where T : IEntity
    where GetT : IBaseGet<T>
{
    [NonAction]
    IQueryable<T> GetQuery(GetT input);
    /// <summary>
    /// 带条件分页
    /// </summary>
    [HttpGet]
    Task<PageData<T>> QueryPageAsync([FromQuery] GetT input, [FromQuery] PageRequest page = null);
    /// <summary>
    /// 带条件查询(不分页)
    /// </summary>
    [HttpGet, Route("Query")]
    Task<IEnumerable<T>> QueryAsync([FromQuery] GetT input);
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    [HttpGet, Route("{id}")]
    Task<T> QueryAsync(string id);
}
public interface IQueryRepController<TKey, T, GetT> : IQueryRepController<T, GetT>
    where T : IEntity<TKey>
    where GetT : IBaseGet<T>
{
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    [HttpGet, Route("{id}")]
    Task<T> QueryAsync(TKey id);
    /// <summary>
    /// 根据Id查询
    /// </summary>
    [HttpGet, Route("ByIds")]
    Task<IEnumerable<T>> QueryByIdsAsync([FromQuery] IEnumerable<TKey> ids);
    /// <summary>
    /// 根据Id查询
    /// </summary>
    [HttpPost, Route("ByIds")]
    Task<IEnumerable<T>> QueryByIdsPostAsync(IEnumerable<TKey> ids);
}
