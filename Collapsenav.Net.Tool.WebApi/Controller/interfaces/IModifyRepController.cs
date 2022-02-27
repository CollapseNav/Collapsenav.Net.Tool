using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;
public interface IModifyRepController<T, CreateT> : IRepController, IDisposable
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
    /// <summary>
    /// 添加(多个)
    /// </summary>
    [HttpPost, Route("AddRange")]
    Task<int> AddRangeAsync(IEnumerable<CreateT> entitys);
}
public interface IModifyRepController<TKey, T, CreateT> : IModifyRepController<T, CreateT>, IDisposable
    where T : IEntity<TKey>
    where CreateT : IBaseCreate
{
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    [HttpDelete, Route("{id}")]
    Task DeleteAsync(TKey id, [FromQuery] bool isTrue = false);
    /// <summary>
    /// 删除(多个 id)
    /// </summary>
    [HttpDelete]
    Task<int> DeleteRangeAsync(IEnumerable<TKey> id, [FromQuery] bool isTrue = false);
    /// <summary>
    /// 更新
    /// </summary>
    [HttpPut, Route("{id}")]
    Task UpdateAsync(TKey id, CreateT entity);
}

