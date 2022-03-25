namespace Collapsenav.Net.Tool.Data;

public interface IWriteRepository<T> : IRepository<T> where T : class, IEntity
{
    /// <summary>
    /// 添加
    /// </summary>
    Task<T> AddAsync(T entity);
    /// <summary>
    /// 删除
    /// </summary>
    Task<bool> DeleteAsync(object id, bool isTrue = false);
    /// <summary>
    /// 更新
    /// </summary>
    Task UpdateAsync(T entity);
}
public interface IWriteRepository<TKey, T> : IRepository<TKey, T> where T : class, IEntity<TKey>
{
    /// <summary>
    /// 删除
    /// </summary>
    Task<bool> DeleteAsync(TKey id, bool isTrue = false);
}

#region 无泛型约束
public interface INoConstraintsWriteRepository<T> : INoConstraintsRepository<T>
{
    /// <summary>
    /// 添加
    /// </summary>
    Task<T> AddAsync(T entity);
    /// <summary>
    /// 删除
    /// </summary>
    Task<bool> DeleteAsync(object id, bool isTrue = false);
    /// <summary>
    /// 更新
    /// </summary>
    Task UpdateAsync(T entity);
}
public interface INoConstraintsWriteRepository<TKey, T> : INoConstraintsRepository<TKey, T>
{
    /// <summary>
    /// 删除
    /// </summary>
    Task<bool> DeleteAsync(TKey id, bool isTrue = false);
}
#endregion
