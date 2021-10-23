using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Collapsenav.Net.Tool.Data
{
    public interface IQueryRepository<TKey, T> : IRepository<TKey, T> where T : class, IBaseEntity<TKey>
    {

        IQueryable<T> Query(Expression<Func<T, bool>> exp);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> exp);
        Task<int> CountAsync(Expression<Func<T, bool>> exp);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> exp);
        Task<IEnumerable<T>> FindAsync(IEnumerable<TKey> ids);
        Task<PageData<T>> FindPageAsync<E>(Expression<Func<T, bool>> exp = null, PageRequest page = null, Expression<Func<T, E>> orderBy = null, bool isAsc = true);
        Task<PageData<T>> FindPageAsync(Expression<Func<T, bool>> exp = null, PageRequest page = null, bool isAsc = true);
    }
}
