using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
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
        return _db.SaveChanges();
    }
    /// <summary>
    /// 保存修改
    /// </summary>
    public async Task<int> SaveAsync()
    {
        return await _db.SaveChangesAsync();
    }
    /// <summary>
    /// 获取主键
    /// </summary>
    public Type KeyType()
    {
        var types = typeof(T).AttrValues<KeyAttribute>();
        if (types.IsEmpty())
            throw new Exception("");
        var type = types.First().Key.PropertyType;
        if (type.IsGenericType)
            return type.GenericTypeArguments.First();
        return type;
    }
}
public class Repository<TKey, T> : Repository<T>, IRepository<TKey, T> where T : class, IEntity<TKey>
{
    public Repository(DbContext db) : base(db) { }
}
