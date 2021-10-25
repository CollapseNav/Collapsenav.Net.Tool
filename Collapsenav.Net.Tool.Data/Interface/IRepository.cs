using System.Collections.Generic;
using System.Threading.Tasks;

namespace Collapsenav.Net.Tool.Data
{
    public interface IRepository
    {
    }
    public interface IRepository<T> : IRepository
    {
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> FindAsync<E>(E id);
        Task<bool> DeleteAsync<E>(E id, bool isTrue = false);
        Task<int> SaveAsync();
        int Save();
    }
    public interface IRepository<TKey, T> : IRepository<T> where T : class, IBaseEntity<TKey>
    {
        Task<bool> DeleteAsync(TKey id, bool isTrue = false);
        Task<int> DeleteAsync(IEnumerable<TKey> id, bool isTrue = false);
        Task<T> FindAsync(TKey id);
        Task<IEnumerable<T>> FindAsync(IEnumerable<TKey> ids);
    }
}
