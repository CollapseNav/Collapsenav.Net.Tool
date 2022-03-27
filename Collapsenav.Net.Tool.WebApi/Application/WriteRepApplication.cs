using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public class WriteRepApplication<T> : Application<T>, IWriteApplication<T>
    where T : class, IEntity
{
    protected new readonly IWriteRepository<T> Repo;
    public WriteRepApplication(IWriteRepository<T> repository) : base(repository)
    {
        Repo = repository;
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        return await Repo.AddAsync(entity);
    }

    public virtual async Task<bool> DeleteAsync(string id, bool isTrue = false)
    {
        return await Repo.DeleteAsync(id, isTrue);
    }

    public void Dispose()
    {
        Repo.Save();
    }

    public virtual async Task<int> UpdateAsync(string id, T entity)
    {
        entity.SetValue(Repo.KeyType().Name, id);
        return await Repo.UpdateAsync(entity);
    }
}
public class WriteRepApplication<TKey, T> : WriteRepApplication<T>, IWriteApplication<TKey, T>
    where T : class, IEntity<TKey>
{
    protected new readonly IModifyRepository<TKey, T> Repo;
    public WriteRepApplication(IModifyRepository<TKey, T> repository) : base(repository)
    {
        Repo = repository;
    }
    public virtual async Task<bool> DeleteAsync(TKey id, bool isTrue = false)
    {
        return await Repo.DeleteAsync(id, isTrue);
    }

}

