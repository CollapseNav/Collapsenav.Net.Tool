using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.Data;

public interface IModifyRepository<TKey, T> : IRepository<TKey, T> where T : class, IBaseEntity<TKey>
{
    Task<int> AddAsync(IEnumerable<T> entityList);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task<int> DeleteAsync(Expression<Func<T, bool>> exp, bool isTrue = false);
    Task<int> UpdateAsync(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity);
    Task<bool> DeleteAsync(TKey id, bool isTrue = false);
    Task<int> DeleteAsync(IEnumerable<TKey> id, bool isTrue = false);
}