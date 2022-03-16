using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.Data;

public interface IQueryRepository<T> : IReadRepository<T>, ICountRepository<T>, ICheckExistRepository<T> where T : class, IEntity
{
    Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> exp = null);
    Task<PageData<T>> QueryPageAsync(Expression<Func<T, bool>> exp, PageRequest page = null);
    Task<PageData<T>> QueryPageAsync<E>(Expression<Func<T, bool>> exp, Expression<Func<T, E>> orderBy, bool isAsc = true, PageRequest page = null);
}
public interface IQueryRepository<TKey, T> : IQueryRepository<T>, IReadRepository<TKey, T> where T : class, IEntity<TKey>
{
    Task<IEnumerable<T>> QueryAsync(IEnumerable<TKey> ids);
}