using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.Data;
public interface IRepository<T> where T : IEntity
{
    IQueryable<T> Query(Expression<Func<T, bool>> exp);
    Task<int> SaveAsync();
    int Save();
}
public interface IRepository<TKey, T> : IRepository<T> where T : IEntity<TKey>
{
}

