using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.Data;
public interface IQueryRepository<T> : IRepository<T>
{
    IQueryable<T> Query(Expression<Func<T, bool>> exp);
    Task<bool> IsExistAsync(Expression<Func<T, bool>> exp);
    Task<int> CountAsync(Expression<Func<T, bool>> exp);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> exp);
    Task<PageData<T>> FindPageAsync<E>(Expression<Func<T, bool>> exp = null, PageRequest page = null, Expression<Func<T, E>> orderBy = null, bool isAsc = true);
    Task<PageData<T>> FindPageAsync(Expression<Func<T, bool>> exp = null, PageRequest page = null, bool isAsc = true);
    Task<PageData<T>> FindPageAsync<E>(IQueryable<T> query, PageRequest page = null, Expression<Func<T, E>> orderBy = null, bool isAsc = true);
    Task<PageData<T>> FindPageAsync(IQueryable<T> query, PageRequest page = null, bool isAsc = true);
}
