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
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    public virtual async Task DeleteAsync(string id, bool isTrue = false)
    {
        await Repo.DeleteAsync(id, isTrue);
    }

    public void Dispose()
    {
        Repo.Save();
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
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    public virtual async Task DeleteAsync(TKey id, bool isTrue = false)
    {
        await Repo.DeleteAsync(id, isTrue);
    }

}

