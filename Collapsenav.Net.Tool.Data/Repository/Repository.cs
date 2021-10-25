using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Collapsenav.Net.Tool.Data
{
    public partial class Repository<T> : IRepository<T> where T : class, IBaseEntity, new()
    {
        protected readonly DbContext _db;
        protected readonly DbSet<T> dbSet;
        public Repository(DbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
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
        /// 保存修改
        /// </summary>
        public virtual async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
        /// <summary>
        /// 保存修改
        /// </summary>
        public virtual int Save()
        {
            return _db.SaveChanges();
        }

        public virtual async Task<T> FindAsync<TKey>(TKey id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<bool> DeleteAsync<TKey>(TKey id, bool isTrue = false)
        {
            var entity = await dbSet.FindAsync(id);
            if (isTrue)
                dbSet.Remove(entity);
            else
            {
                entity.SoftDelete();
                await UpdateAsync(entity);
            }
            return true;
        }
    }
    public partial class Repository<TKey, T> : Repository<T>, IRepository<T>, IRepository<TKey, T>, IRepository where T : class, IBaseEntity<TKey>, new()
    {
        public Repository(DbContext db) : base(db)
        {
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
        /// 根据id删除数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="isTrue">是否真删</param>
        public virtual async Task<int> DeleteAsync(IEnumerable<TKey> id, bool isTrue = false)
        {
            if (isTrue)
                return await dbSet.Where(item => id.Contains(item.Id)).DeleteAsync();
            return await dbSet.UpdateAsync(item => id.Contains(item.Id), entity => new T
            {
                LastModificationTime = DateTime.Now,
                IsDeleted = true
            });
        }
        public virtual async Task<T> FindAsync(TKey id)
        {
            return await base.FindAsync(id);
        }
        public virtual async Task<IEnumerable<T>> FindAsync(IEnumerable<TKey> ids)
        {
            return await dbSet.Where(item => ids.Contains(item.Id)).ToListAsync();
        }
    }
}
