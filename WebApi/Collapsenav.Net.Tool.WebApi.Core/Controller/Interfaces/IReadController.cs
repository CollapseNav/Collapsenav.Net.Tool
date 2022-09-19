using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;
public interface IReadController<T, GetT> : IController
    where T : IEntity
    where GetT : IBaseGet<T>
{
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    [HttpGet, Route("{id}")]
    Task<T> QueryAsync(string id);
}
public interface IReadController<TKey, T, GetT> : IReadController<T, GetT>
    where T : IEntity<TKey>
    where GetT : IBaseGet<T>
{
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    [HttpGet, Route("{id}")]
    Task<T> QueryAsync(TKey id);
}

#region 无泛型约束
public interface INoConstraintsReadController<T, GetT> : IController
{
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    [HttpGet, Route("{id}")]
    Task<T> QueryAsync(string id);
}
public interface INoConstraintsReadController<TKey, T, GetT> : INoConstraintsReadController<T, GetT>
{
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    [HttpGet, Route("{id}")]
    Task<T> QueryAsync(TKey id);
}
#endregion
