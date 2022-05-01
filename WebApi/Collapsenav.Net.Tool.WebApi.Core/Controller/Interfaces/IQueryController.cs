using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;
public interface IQueryController<T, GetT> : IReadController<T, GetT>
    where T : IEntity
    where GetT : IBaseGet<T>
{
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
}
public interface IQueryController<TKey, T, GetT> : IReadController<TKey, T, GetT>, IQueryController<T, GetT>
    where T : IEntity<TKey>
    where GetT : IBaseGet<T>
{
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

#region 无泛型约束
public interface INoConstraintsQueryController<T, GetT> : INoConstraintsReadController<T, GetT>
{
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
}
public interface INoConstraintsQueryController<TKey, T, GetT> : INoConstraintsReadController<TKey, T, GetT>, INoConstraintsQueryController<T, GetT>
{
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
#endregion
