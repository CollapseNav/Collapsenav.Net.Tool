using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public interface IModifyApplication<T, CreateT> : IApplication, IDisposable
    where T : IEntity
    where CreateT : IBaseCreate
{
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    Task DeleteAsync(string id, bool isTrue = false);
    /// <summary>
    /// 添加(单个)
    /// </summary>
    Task<T> AddAsync(CreateT entity);
    /// <summary>
    /// 添加(多个)
    /// </summary>
    Task<int> AddRangeAsync(IEnumerable<CreateT> entitys);
}
public interface IModifyApplication<TKey, T, CreateT> : IModifyApplication<T, CreateT>
    where T : IEntity<TKey>
    where CreateT : IBaseCreate
{
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    Task DeleteAsync(TKey id, bool isTrue = false);
    /// <summary>
    /// 删除(多个 id)
    /// </summary>
    Task<int> DeleteRangeAsync(IEnumerable<TKey> id, bool isTrue = false);
    /// <summary>
    /// 更新
    /// </summary>
    Task UpdateAsync(TKey id, CreateT entity);
}

