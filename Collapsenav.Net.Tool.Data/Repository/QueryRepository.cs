using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;
public partial class QueryRepository<T> : Repository<T>, IQueryRepository<T>
    where T : class, IBaseEntity, new()
{
    public QueryRepository(DbContext db) : base(db)
    {
    }

    /// <summary>
    /// 查询所有符合条件的数据
    /// </summary>
    /// <param name="exp">筛选条件
    /// PS:若使用默认的NULL，则返回所有数据
    /// </param>
    public virtual IQueryable<T> Query(Expression<Func<T, bool>> exp)
    {
        if (exp == null)
            return dbSet.AsNoTracking().AsQueryable();
        return dbSet.AsNoTracking().Where(exp);
    }
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
    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> exp)
    {
        return await Query(exp).ToListAsync();
    }

    /// <summary>
    /// 分页查找所有符合条件的数据
    /// </summary>
    public virtual async Task<PageData<T>> FindPageAsync<E>(Expression<Func<T, bool>> exp = null, PageRequest page = null, Expression<Func<T, E>> orderBy = null, bool isAsc = true)
    {
        return await FindPageAsync(Query(exp), page, orderBy, isAsc);
    }

    /// <summary>
    /// 分页查找所有符合条件的数据
    /// </summary>
    public virtual async Task<PageData<T>> FindPageAsync(Expression<Func<T, bool>> exp = null, PageRequest page = null, bool isAsc = true)
    {
        return await FindPageAsync(Query(exp), page, isAsc);
    }

    public virtual async Task<PageData<T>> FindPageAsync<E>(IQueryable<T> query, PageRequest page = null, Expression<Func<T, E>> orderBy = null, bool isAsc = true)
    {
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
    public virtual async Task<PageData<T>> FindPageAsync(IQueryable<T> query, PageRequest page = null, bool isAsc = true)
    {
        if (page == null)
            page = new PageRequest();

        return new PageData<T>
        {
            Total = await query.CountAsync(),
            Data = await query.Skip(page.Skip).Take(page.Max).ToListAsync()
        };
    }

}
