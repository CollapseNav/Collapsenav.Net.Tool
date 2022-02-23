using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.Data;

public interface IQueryRepository<TKey, T> : IRepository<TKey, T> where T : class, IBaseEntity<TKey>
{
    Task<bool> IsExistAsync(Expression<Func<T, bool>> exp);
    Task<int> CountAsync(Expression<Func<T, bool>> exp);
    Task<PageData<T>> QueryPageAsync(Expression<Func<T, bool>> exp, PageRequest page = null);
    Task<PageData<T>> QueryPageAsync<E>(Expression<Func<T, bool>> exp, Expression<Func<T, E>> orderBy, bool isAsc = true, PageRequest page = null);
    Task<T> QueryAsync(TKey id);
    Task<IEnumerable<T>> QueryAsync(IEnumerable<TKey> ids);
    Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> exp);
}