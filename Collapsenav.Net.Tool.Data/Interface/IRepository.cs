using System.Threading.Tasks;

namespace Collapsenav.Net.Tool.Data
{
    public interface IRepository
    {
    }
    public interface IRepository<TKey, T> : IRepository where T : class, IBaseEntity<TKey>
    {
        Task<T> AddAsync(T entity);
        Task<bool> DeleteAsync(TKey id, bool isTrue = false);
        Task UpdateAsync(T entity);
        Task<T> FindAsync(TKey id);
        Task<int> SaveAsync();
        int Save();
    }
}
