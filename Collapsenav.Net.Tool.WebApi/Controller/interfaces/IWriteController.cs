using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;
public interface IWriteController<T, CreateT> : IController, IDisposable
    where T : IEntity
    where CreateT : IBaseCreate
{
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    [HttpDelete, Route("{id}")]
    Task DeleteAsync(string id, [FromQuery] bool isTrue = false);
    /// <summary>
    /// 添加(单个)
    /// </summary>
    [HttpPost]
    Task<T> AddAsync([FromBody] CreateT entity);
}
public interface IWriteController<TKey, T, CreateT> : IWriteController<T, CreateT>
    where T : IEntity<TKey>
    where CreateT : IBaseCreate
{
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    [HttpDelete, Route("{id}")]
    Task DeleteAsync(TKey id, [FromQuery] bool isTrue = false);
    /// <summary>
    /// 更新
    /// </summary>
    [HttpPut, Route("{id}")]
    Task UpdateAsync(TKey id, CreateT entity);
}

#region 无泛型约束
public interface INoConstraintsWriteController<T, CreateT> : INoConstraintsController, IDisposable
{
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    [HttpDelete, Route("{id}")]
    Task DeleteAsync(string id, [FromQuery] bool isTrue = false);
    /// <summary>
    /// 添加(单个)
    /// </summary>
    [HttpPost]
    Task<T> AddAsync([FromBody] CreateT entity);
}
public interface INoConstraintsWriteController<TKey, T, CreateT> : INoConstraintsWriteController<T, CreateT>
{
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    [HttpDelete, Route("{id}")]
    Task DeleteAsync(TKey id, [FromQuery] bool isTrue = false);
    /// <summary>
    /// 更新
    /// </summary>
    [HttpPut, Route("{id}")]
    Task UpdateAsync(TKey id, CreateT entity);
}
#endregion
