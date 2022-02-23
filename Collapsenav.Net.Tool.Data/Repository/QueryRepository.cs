using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;

public class QueryRepository<TKey, T> : Repository<TKey, T>, IQueryRepository<TKey, T> where T : class, IBaseEntity<TKey>
{
    public QueryRepository(DbContext db) : base(db) { }

    /// <summary>
    /// 判断是否有符合条件的数据
    /// </summary>
    public virtual async Task<bool> IsExistAsync(Expression<Func<T, bool>> exp)
    {
        return await dbSet.AnyAsync(exp);
    }
    /// <summary>
    /// 计算符合条件的数据数量
    /// </summary>
    public virtual async Task<int> CountAsync(Expression<Func<T, bool>> exp = null)
    {
        return await Query(exp).CountAsync();
    }

    /// <summary>
    /// 查询数据
    /// </summary>
    public virtual async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> exp)
    {
        return await Query(exp).ToListAsync();
    }

    public async Task<PageData<T>> QueryPageAsync(Expression<Func<T, bool>> exp, PageRequest page = null)
    {
        return await QueryPageAsync(exp, item => item.Id, true, page);
    }

    public async Task<PageData<T>> QueryPageAsync<E>(Expression<Func<T, bool>> exp, Expression<Func<T, E>> orderBy, bool isAsc = true, PageRequest page = null)
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
    public virtual async Task<T> QueryAsync(TKey id)
    {
        return await dbSet.FindAsync(id);
    }
    public virtual async Task<IEnumerable<T>> QueryAsync(IEnumerable<TKey> ids)
    {
        return await dbSet.Where(item => ids.Contains(item.Id)).ToListAsync();
    }
}
