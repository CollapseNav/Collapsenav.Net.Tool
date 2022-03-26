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
    [HttpDelete("ByIds")]
    Task<int> DeleteRangeAsync([FromQuery] IEnumerable<TKey> id, [FromQuery] bool isTrue = false);
}

#region 无泛型约束
public interface INoConstraintsModifyController<T, CreateT> : INoConstraintsWriteController<T, CreateT>, IDisposable
{
    /// <summary>
    /// 添加(多个)
    /// </summary>
    [HttpPost, Route("AddRange")]
    Task<int> AddRangeAsync(IEnumerable<CreateT> entitys);
}
public interface INoConstraintsModifyController<TKey, T, CreateT> : INoConstraintsWriteController<TKey, T, CreateT>, INoConstraintsModifyController<T, CreateT>
{
    /// <summary>
    /// 删除(多个 id)
    /// </summary>
    [HttpDelete("ByIds")]
    Task<int> DeleteRangeAsync(IEnumerable<TKey> id, [FromQuery] bool isTrue = false);
}
#endregion
