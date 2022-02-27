using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.Data;

public interface IModifyRepository<T> : IRepository<T> where T : class, IEntity
{
    Task<int> AddAsync(IEnumerable<T> entityList);
    Task<bool> DeleteAsync(object id, bool isTrue = false);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task<int> DeleteAsync(Expression<Func<T, bool>> exp, bool isTrue = false);
    Task<int> UpdateAsync(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity);
}
public interface IModifyRepository<TKey, T> : IModifyRepository<T>, IRepository<TKey, T> where T : class, IEntity<TKey>
{
    Task<bool> DeleteAsync(TKey id, bool isTrue = false);
    Task<int> DeleteAsync(IEnumerable<TKey> id, bool isTrue = false);
}
