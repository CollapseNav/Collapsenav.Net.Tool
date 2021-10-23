using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Collapsenav.Net.Tool.Data
{
    public partial class ModifyRepository<TKey, T> : QueryRepository<TKey, T>, IModifyRepository<TKey, T>
    where T : class, IBaseEntity<TKey>, new()
    {
        public ModifyRepository(DbContext db) : base(db) { }
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
            if (isTrue)
            {
                return await dbSet.Where(exp).DeleteAsync();
            }
            return await UpdateAsync(exp, entity => new T { IsDeleted = true, LastModificationTime = DateTime.Now });
        }
        /// <summary>
        /// 根据id删除数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="isTrue">是否真删</param>
        public virtual async Task<int> DeleteAsync(IEnumerable<TKey> id, bool isTrue = false)
        {
            Expression<Func<T, bool>> exp = item => id.Contains(item.Id);
            return await DeleteAsync(exp, isTrue);
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
}
