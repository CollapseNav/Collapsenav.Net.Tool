using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Collapsenav.Net.Tool.Data;
public partial class ModifyRepository<T> : Repository<T>, IModifyRepository<T>
    where T : class, IEntity, new()
{
    public ModifyRepository(DbContext db) : base(db)
    {
    }
    /// <summary>
    /// 添加数据(单个)
    /// </summary>
    /// <param name="entity">新的数据</param>
    public virtual async Task<T> AddAsync(T entity)
    {
        entity.Init();
        await dbSet.AddAsync(entity);
        return entity;
    }
    /// <summary>
    /// 更新数据
    /// </summary>
    public virtual async Task UpdateAsync(T entity)
    {
        // dbSet.Update(entity);
        entity.Update();
        var entry = _db.Entry(entity);
        entry.State = EntityState.Modified;
        await Task.FromResult("");
    }
    /// <summary>
    /// 添加数据(集合)
    /// </summary>
    /// <param name="entityList">新的数据集合</param>
    public virtual async Task<int> AddAsync(IEnumerable<T> entityList)
    {
        foreach (var entity in entityList)
            entity.Init();
        await dbSet.AddRangeAsync(entityList);
        return entityList.Count();
    }
    /// <summary>
    /// 有条件地删除数据
    /// </summary>
    /// <param name="exp">筛选条件</param>
    /// <param name="isTrue">是否真删</param>
    public virtual async Task<int> DeleteAsync(Expression<Func<T, bool>> exp, bool isTrue = false)
    {
        return await dbSet.Where(exp).DeleteAsync();
    }
    /// <summary>
    /// 根据id删除数据
    /// </summary>
    /// <param name="id">主键ID</param>
    /// <param name="isTrue">是否真删</param>
    public virtual async Task<bool> DeleteAsync(object id, bool isTrue = false)
    {
        var entity = KeyType().Name switch
        {
            nameof(Int32) => await dbSet.FindAsync(int.Parse(id.ToString())),
            nameof(Int64) => await dbSet.FindAsync(long.Parse(id.ToString())),
            nameof(String) => await dbSet.FindAsync(id.ToString()),
            nameof(Guid) => await dbSet.FindAsync(Guid.Parse(id.ToString())),
            _ => null,
        };
        if (isTrue)
            dbSet.Remove(entity);
        else
        {
            entity.SoftDelete();
            await UpdateAsync(entity);
        }
        return true;
    }
    /// <summary>
    /// 实现按需要只更新部分更新
    /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
    /// </summary>
    public virtual async Task<int> UpdateAsync(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity)
    {
        return await dbSet.Where(where).UpdateAsync(entity);
    }

}
public partial class ModifyRepository<TKey, T> : ModifyRepository<T>, IModifyRepository<TKey, T>
    where T : class, IBaseEntity<TKey>, new()
{
    public ModifyRepository(DbContext db) : base(db) { }

    /// <summary>
    /// 根据id删除数据
    /// </summary>
    /// <param name="id">主键ID</param>
    /// <param name="isTrue">是否真删</param>
    public virtual async Task<int> DeleteAsync(IEnumerable<TKey> id, bool isTrue = false)
    {
        if (isTrue)
            return await dbSet.Where(item => id.Contains(item.Id)).DeleteAsync();
        return await dbSet.Where(item => id.Contains(item.Id)).UpdateAsync(entity => new
        {
            LastModificationTime = DateTime.Now,
            IsDeleted = true
        });
    }
    /// <summary>
    /// 根据id删除数据
    /// </summary>
    /// <param name="id">主键ID</param>
    /// <param name="isTrue">是否真删</param>
    public virtual async Task<bool> DeleteAsync(TKey id, bool isTrue = false)
    {
        return await base.DeleteAsync(id, isTrue);
    }
    /// <summary>
    /// 有条件地删除数据
    /// </summary>
    /// <param name="exp">筛选条件</param>
    /// <param name="isTrue">是否真删</param>
    public override async Task<int> DeleteAsync(Expression<Func<T, bool>> exp, bool isTrue = false)
    {
        if (isTrue)
            return await dbSet.Where(exp).DeleteAsync();
        return await dbSet.Where(exp).UpdateAsync(entity => new { IsDeleted = true, LastModificationTime = DateTime.Now });
    }
}
