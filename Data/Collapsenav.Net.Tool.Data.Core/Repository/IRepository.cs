using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.Data;
public interface IRepository<T> where T : IEntity
{
    /// <summary>
    /// 获取 Query
    /// </summary>
    IQueryable<T> Query(Expression<Func<T, bool>> exp);
    /// <summary>
    /// 保存
    /// </summary>
    Task<int> SaveAsync();
    /// <summary>
    /// 保存
    /// </summary>
    int Save();
    /// <summary>
    /// 获取主键type
    /// </summary>
    Type KeyType();
}
public interface IRepository<TKey, T> : IRepository<T> where T : IEntity<TKey>
{
}


#region 无泛型约束
public interface INoConstraintsRepository<T>
{
    /// <summary>
    /// 获取 Query
    /// </summary>
    IQueryable<T> Query(Expression<Func<T, bool>> exp);
    /// <summary>
    /// 保存
    /// </summary>
    Task<int> SaveAsync();
    /// <summary>
    /// 保存
    /// </summary>
    int Save();
    /// <summary>
    /// 获取主键type
    /// </summary>
    Type KeyType();
}
public interface INoConstraintsRepository<TKey, T> : INoConstraintsRepository<T>
{
}
#endregion
