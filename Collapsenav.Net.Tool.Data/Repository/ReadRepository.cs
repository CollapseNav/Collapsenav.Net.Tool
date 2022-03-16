using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;

public class ReadRepository<T> : Repository<T>, IReadRepository<T> where T : class, IEntity
{
    public ReadRepository(DbContext db) : base(db) { }

    public virtual async Task<T> QueryAsync(object id)
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
}
public class ReadRepository<TKey, T> : ReadRepository<T>, IReadRepository<TKey, T> where T : class, IBaseEntity<TKey>
{
    public ReadRepository(DbContext db) : base(db) { }

    public virtual async Task<T> QueryAsync(TKey id)
    {
        return await dbSet.FindAsync(id);
    }
}
