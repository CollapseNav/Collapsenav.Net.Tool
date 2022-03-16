using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.Data;

public interface IModifyRepository<T> : IWriteRepository<T> where T : class, IEntity
{
    Task<int> AddAsync(IEnumerable<T> entityList);
    Task<int> DeleteAsync(Expression<Func<T, bool>> exp, bool isTrue = false);
    Task<int> UpdateAsync(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity);
}
public interface IModifyRepository<TKey, T> : IModifyRepository<T>, IWriteRepository<TKey, T> where T : class, IEntity<TKey>
{
    Task<int> DeleteAsync(IEnumerable<TKey> id, bool isTrue = false);
}
