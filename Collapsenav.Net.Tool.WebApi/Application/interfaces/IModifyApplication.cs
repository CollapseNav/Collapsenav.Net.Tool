using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public interface IModifyApplication<T, CreateT> : IWriteApplication<T>, IDisposable
    where T : IEntity
    where CreateT : IBaseCreate
{
    /// <summary>
    /// 添加(单个)
    /// </summary>
    Task<T> AddAsync(CreateT entity);
    /// <summary>
    /// 添加(多个)
    /// </summary>
    Task<int> AddRangeAsync(IEnumerable<CreateT> entitys);
}
public interface IModifyApplication<TKey, T, CreateT> : IModifyApplication<T, CreateT>, IWriteApplication<TKey, T>
    where T : IEntity<TKey>
    where CreateT : IBaseCreate
{
    /// <summary>
    /// 删除(多个 id)
    /// </summary>
    Task<int> DeleteRangeAsync(IEnumerable<TKey> id, bool isTrue = false);
    /// <summary>
    /// 更新
    /// </summary>
    Task UpdateAsync(TKey id, CreateT entity);
}

