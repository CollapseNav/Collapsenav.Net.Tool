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
    public virtual IQueryable<T> Query(Expression<Func<T, bool>> exp)
    {
        if (exp == null)
            return dbSet.AsNoTracking().AsQueryable();
        return dbSet.AsNoTracking().Where(exp);
    }
    /// <summary>
    /// 保存修改
    /// </summary>
    public int Save()
    {
        var count = _db.SaveChanges();
        _db.ChangeTracker.Clear();
        return count;
    }
    /// <summary>
    /// 保存修改
    /// </summary>
    public async Task<int> SaveAsync()
    {
        var count = await _db.SaveChangesAsync();
        _db.ChangeTracker.Clear();
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
