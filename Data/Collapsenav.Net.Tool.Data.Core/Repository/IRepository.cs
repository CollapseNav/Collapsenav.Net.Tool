using System.Linq.Expressions;
using System.Reflection;

namespace Collapsenav.Net.Tool.Data;
public interface IRepository<T> where T : IEntity
{
    /// <summary>
    /// 获取 Query
    /// </summary>
    IQueryable<T> Query(Expression<Func<T, bool>> exp = null);
    IQueryable<T> QueryWithTrack(Expression<Func<T, bool>> exp);
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
    PropertyInfo KeyProp();
}
public interface IRepository<TKey, T> : IRepository<T> where T : IEntity<TKey>
{
}

