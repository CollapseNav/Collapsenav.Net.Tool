using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.Data;

public interface IModifyRepository<T> : IWriteRepository<T> where T : class, IEntity
{
    /// <summary>
    /// 添加
    /// </summary>
    Task<int> AddAsync(IEnumerable<T> entityList);
    /// <summary>
    /// 删除
    /// </summary>
    Task<int> DeleteAsync(Expression<Func<T, bool>> exp, bool isTrue = false);
    /// <summary>
    /// 更新
    /// </summary>
    Task<int> UpdateAsync(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity);
}
public interface IModifyRepository<TKey, T> : IModifyRepository<T>, IWriteRepository<TKey, T> where T : class, IEntity<TKey>
{
    /// <summary>
    /// 删除
    /// </summary>
    Task<int> DeleteAsync(IEnumerable<TKey> id, bool isTrue = false);
}

#region 无泛型约束
public interface INoConstraintsModifyRepository<T> : INoConstraintsWriteRepository<T>
{
    /// <summary>
    /// 添加
    /// </summary>
    Task<int> AddAsync(IEnumerable<T> entityList);
    /// <summary>
    /// 删除
    /// </summary>
    Task<int> DeleteAsync(Expression<Func<T, bool>> exp, bool isTrue = false);
    /// <summary>
    /// 更新
    /// </summary>
    Task<int> UpdateAsync(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity);
}
public interface INoConstraintsModifyRepository<TKey, T> : INoConstraintsModifyRepository<T>, INoConstraintsWriteRepository<TKey, T>
{
    /// <summary>
    /// 删除
    /// </summary>
    Task<int> DeleteAsync(IEnumerable<TKey> id, bool isTrue = false);
}
#endregion
