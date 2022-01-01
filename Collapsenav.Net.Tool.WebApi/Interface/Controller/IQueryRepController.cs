using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;
public interface IQueryRepController<TKey, T, GetT> : IRepController
    where T : IBaseEntity<TKey>
    where GetT : IBaseGet<T>
{
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    [HttpGet, Route("{id}")]
    Task<T> FindAsync(TKey id);
    /// <summary>
    /// 带条件分页
    /// </summary>
    [HttpGet]
    Task<PageData<T>> FindPageAsync([FromQuery] GetT input, [FromQuery] PageRequest page = null);
    /// <summary>
    /// 带条件查询(不分页)
    /// </summary>
    [HttpGet, Route("Query")]
    Task<IEnumerable<T>> FindQueryAsync([FromQuery] GetT input);
    /// <summary>
    /// 根据Id查询
    /// </summary>
    [HttpGet, Route("ByIds")]
    Task<IEnumerable<T>> FindByIdsAsync([FromQuery] IEnumerable<TKey> ids);
    /// <summary>
    /// 根据Id查询
    /// </summary>
    [HttpPost, Route("ByIds")]
    Task<IEnumerable<T>> FindByIdsPostAsync(IEnumerable<TKey> ids);
    [NonAction]
    IQueryable<T> GetQuery(GetT input);
}
