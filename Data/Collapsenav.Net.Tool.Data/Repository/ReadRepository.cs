using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;

public class ReadRepository<T> : Repository<T>, IReadRepository<T> where T : class, IEntity
{
    public ReadRepository(DbContext db) : base(db) { }

    public virtual async Task<T> GetByIdAsync(object id)
    {
        return KeyType().Name switch
        {
            nameof(Int32) => await dbSet.FindAsync(int.Parse(id.ToString())),
            nameof(Int64) => await dbSet.FindAsync(long.Parse(id.ToString())),
            nameof(String) => await dbSet.FindAsync(id.ToString()),
            nameof(Guid) => await dbSet.FindAsync(Guid.Parse(id.ToString())),
            _ => null,
        };
    }

    public virtual async Task<IEnumerable<T>> QueryDataAsync(IQueryable<T> query)
    {
        return await query.ToListAsync();
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
}
public class ReadRepository<TKey, T> : ReadRepository<T>, IReadRepository<TKey, T> where T : class, IBaseEntity<TKey>
{
    public ReadRepository(DbContext db) : base(db) { }

    public virtual async Task<T> GetByIdAsync(TKey id)
    {
        return await dbSet.FindAsync(id);
    }
}
