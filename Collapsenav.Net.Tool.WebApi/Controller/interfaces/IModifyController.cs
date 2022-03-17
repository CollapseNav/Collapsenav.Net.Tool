using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;
public interface IModifyController<T, CreateT> : IWriteController<T, CreateT>, IDisposable
    where T : IEntity
    where CreateT : IBaseCreate
{
    /// <summary>
    /// 添加(多个)
    /// </summary>
    [HttpPost, Route("AddRange")]
    Task<int> AddRangeAsync(IEnumerable<CreateT> entitys);
}
public interface IModifyController<TKey, T, CreateT> : IWriteController<TKey, T, CreateT>, IModifyController<T, CreateT>
    where T : IEntity<TKey>
    where CreateT : IBaseCreate
{
    /// <summary>
    /// 删除(多个 id)
    /// </summary>
    [HttpDelete]
    Task<int> DeleteRangeAsync(IEnumerable<TKey> id, [FromQuery] bool isTrue = false);
}

