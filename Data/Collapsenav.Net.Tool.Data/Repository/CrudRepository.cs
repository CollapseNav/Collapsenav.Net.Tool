using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;

public class CrudRepository<T> : Repository<T>, ICrudRepository<T> where T : class, IEntity, new()
{
    protected readonly IQueryRepository<T> Read;
    protected readonly IModifyRepository<T> Write;
    public CrudRepository(DbContext db) : base(db)
    {
        Read = new QueryRepository<T>(db);
        Write = new ModifyRepository<T>(db);
    }
    public virtual async Task<int> AddAsync(IEnumerable<T> entityList)
    {
        return await Write.AddAsync(entityList);
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        return await Write.AddAsync(entity);
    }

    public virtual async Task<int> CountAsync(Expression<Func<T, bool>> exp)
    {
        return await Read.CountAsync(exp);
    }

    public virtual async Task<int> DeleteAsync(Expression<Func<T, bool>> exp, bool isTrue = false)
    {
        return await Write.DeleteAsync(exp, isTrue);
    }
    public virtual async Task<PageData<T>> QueryPageAsync(Expression<Func<T, bool>> exp, PageRequest page = null)
    {
        return await Read.QueryPageAsync(exp, page);
    }

    public virtual async Task<PageData<T>> QueryPageAsync<E>(Expression<Func<T, bool>> exp, Expression<Func<T, E>> orderBy, bool isAsc = true, PageRequest page = null)
    {
        return await Read.QueryPageAsync(exp, orderBy, isAsc, page);
    }

    public virtual async Task<bool> IsExistAsync(Expression<Func<T, bool>> exp)
    {
        return await Read.IsExistAsync(exp);
    }

    public virtual async Task<int> UpdateAsync(T entity)
    {
        return await Write.UpdateAsync(entity);
    }

    public virtual async Task<int> UpdateAsync(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity)
    {
        return await Write.UpdateAsync(where, entity);
    }

    public virtual async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> exp)
    {
        return await Read.QueryAsync(exp);
    }

    public virtual async Task<T> QueryAsync(object id)
    {
        return await Read.QueryAsync(id);
    }

    public virtual async Task<bool> DeleteAsync(object id, bool isTrue = false)
    {
        return await Write.DeleteAsync(id, isTrue);
    }
}
public class CrudRepository<TKey, T> : CrudRepository<T>, ICrudRepository<TKey, T> where T : class, IBaseEntity<TKey>, new()
{
    protected new readonly IQueryRepository<TKey, T> Read;
    protected new readonly IModifyRepository<TKey, T> Write;
    public CrudRepository(DbContext db) : base(db)
    {
        Read = new QueryRepository<TKey, T>(db);
        Write = new ModifyRepository<TKey, T>(db);
    }

    public virtual async Task<bool> DeleteAsync(TKey id, bool isTrue = false)
    {
        return await Write.DeleteAsync(id, isTrue);
    }

    public virtual async Task<int> DeleteAsync(IEnumerable<TKey> id, bool isTrue = false)
    {
        return await Write.DeleteAsync(id, isTrue);
    }

    public override async Task<int> DeleteAsync(Expression<Func<T, bool>> exp, bool isTrue = false)
    {
        return await Write.DeleteAsync(exp, isTrue);
    }

    public virtual async Task<T> QueryAsync(TKey id)
    {
        return await Read.QueryAsync(id);
    }

    public virtual async Task<IEnumerable<T>> QueryAsync(IEnumerable<TKey> ids)
    {
        return await Read.QueryAsync(ids);
    }
}

