using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;

public class QueryRepository<T> : ReadRepository<T>, IQueryRepository<T> where T : class, IEntity
{
    public QueryRepository(DbContext db) : base(db) { }

    /// <summary>
    /// 查询数据
    /// </summary>
    public virtual async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> exp = null) => await Query(exp)?.ToListAsync();
    public virtual async Task<PageData<T>> QueryPageAsync(Expression<Func<T, bool>> exp, PageRequest page = null)
    {
        var query = Query(exp);
        page ??= new PageRequest();
        return new PageData<T>
        {
            Total = await query.CountAsync(),
            Data = await query.Skip(page.Skip).Take(page.Max)?.ToListAsync()
        };
    }

    public virtual async Task<PageData<T>> QueryPageAsync<E>(Expression<Func<T, bool>> exp, Expression<Func<T, E>> orderBy, bool isAsc = true, PageRequest page = null)
    {
        var query = Query(exp);
        if (page == null)
            page = new PageRequest();
        if (orderBy != null)
            query = isAsc ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
        return new PageData<T>
        {
            Total = await query.CountAsync(),
            Data = await query.Skip(page.Skip).Take(page.Max).ToListAsync()
        };
    }
}
public class QueryRepository<TKey, T> : ReadRepository<TKey, T>, IQueryRepository<TKey, T> where T : class, IEntity<TKey>
{
    protected IQueryRepository<T> Repo;
    public QueryRepository(DbContext db) : base(db)
    {
        Repo = new QueryRepository<T>(db);
    }
    public virtual async Task<IEnumerable<T>> QueryAsync(IEnumerable<TKey> ids) => await dbSet.Where(item => ids.Contains(item.Id)).ToListAsync();
    public virtual async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> exp = null) => await Repo.QueryAsync(exp);
    public virtual async Task<PageData<T>> QueryPageAsync(Expression<Func<T, bool>> exp, PageRequest page = null) => await Repo.QueryPageAsync(exp, page);
    public virtual async Task<PageData<T>> QueryPageAsync<E>(Expression<Func<T, bool>> exp, Expression<Func<T, E>> orderBy, bool isAsc = true, PageRequest page = null) => await Repo.QueryPageAsync(exp, orderBy, isAsc, page);
}
