using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;
public class Repository<T> : IRepository<T> where T : class, IEntity
{
    protected readonly DbContext _db;
    protected readonly DbSet<T> dbSet;
    public Repository(DbContext db)
    {
        _db = db;
        dbSet = _db.Set<T>();
    }
    /// <summary>
    /// 查询所有符合条件的数据
    /// </summary>
    /// <param name="exp">筛选条件
    /// PS:若使用默认的NULL，则返回所有数据
    /// </param>
    public virtual IQueryable<T> Query(Expression<Func<T, bool>> exp = null) => exp == null ? dbSet.AsNoTracking().AsQueryable() : dbSet.AsNoTracking().Where(exp);
    public virtual IQueryable<T> QueryWithTrack(Expression<Func<T, bool>> exp) => exp == null ? dbSet.AsQueryable() : dbSet.Where(exp);
    /// <summary>
    /// 保存修改
    /// </summary>
    public int Save()
    {
        var count = _db.SaveChanges();
#if NET6_0_OR_GREATER
        _db.ChangeTracker.Clear();
#else
        var entries = _db.ChangeTracker.Entries();
        entries.ForEach(item => item.State = EntityState.Detached);
#endif
        return count;
    }
    /// <summary>
    /// 保存修改
    /// </summary>
    public async Task<int> SaveAsync()
    {
        var count = await _db.SaveChangesAsync();
#if NET6_0_OR_GREATER
        _db.ChangeTracker.Clear();
#else
        var entries = _db.ChangeTracker.Entries();
        entries.ForEach(item => item.State = EntityState.Detached);
#endif
        return count;
    }
    /// <summary>
    /// 获取主键
    /// </summary>
    public Type KeyType()
    {
        var prop = KeyProp();
        if (prop == null)
            throw new Exception("");
        if (prop.PropertyType.IsGenericType)
            return prop.PropertyType.GenericTypeArguments.First();
        return prop.PropertyType;
    }

    public PropertyInfo KeyProp()
    {
        var types = typeof(T).AttrValues<KeyAttribute>();
        if (types.IsEmpty())
            throw new Exception("");
        var prop = types.First().Key;
        return prop;
    }
}
public class Repository<TKey, T> : Repository<T>, IRepository<TKey, T> where T : class, IEntity<TKey>
{
    public Repository(DbContext db) : base(db) { }
}
