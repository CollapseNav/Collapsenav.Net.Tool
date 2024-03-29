using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.Data;

public interface IQueryRepository<T> : IReadRepository<T> where T : class, IEntity
{
    /// <summary>
    /// 列表查询
    /// </summary>
    Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> exp = null);
    /// <summary>
    /// 分页查询
    /// </summary>
    Task<PageData<T>> QueryPageAsync(Expression<Func<T, bool>> exp, PageRequest page = null);
    /// <summary>
    /// 分页查询
    /// </summary>
    Task<PageData<T>> QueryPageAsync<E>(Expression<Func<T, bool>> exp, Expression<Func<T, E>> orderBy, bool isAsc = true, PageRequest page = null);
}
public interface IQueryRepository<TKey, T> : IQueryRepository<T>, IReadRepository<TKey, T> where T : class, IEntity<TKey>
{
    /// <summary>
    /// 根据Id集合查询
    /// </summary>
    Task<IEnumerable<T>> QueryAsync(IEnumerable<TKey> ids);
}
