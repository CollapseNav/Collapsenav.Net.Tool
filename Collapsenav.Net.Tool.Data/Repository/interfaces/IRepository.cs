using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.Data;
public interface IRepository<TKey, T> where T : class, IBaseEntity<TKey>
{
    IQueryable<T> Query(Expression<Func<T, bool>> exp);
    Task<int> SaveAsync();
    int Save();
}

