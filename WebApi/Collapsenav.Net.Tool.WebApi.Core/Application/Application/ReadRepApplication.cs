using System.Linq.Expressions;
using Collapsenav.Net.Tool.Data;
namespace Collapsenav.Net.Tool.WebApi;

public class ReadRepApplication<T> : Application<T>, IReadApplication<T>,ICountApplication<T>, ICheckExistApplication<T>
    where T : class, IEntity
{
    protected new readonly IReadRepository<T> Repo;
    public ReadRepApplication(IReadRepository<T> repository) : base(repository)
    {
        Repo = repository;
    }
    public virtual async Task<T> QueryByStringIdAsync(string id) => await Repo.GetByIdAsync(id);
    public virtual async Task<bool> IsExistAsync(Expression<Func<T, bool>> exp) => await Repo.IsExistAsync(exp);
    public virtual async Task<int> CountAsync(Expression<Func<T, bool>> exp = null) => await Repo.CountAsync(exp);
}
public class ReadRepApplication<TKey, T> : ReadRepApplication<T>, IReadApplication<TKey, T>
    where T : class, IEntity<TKey>
{
    protected new IReadRepository<TKey, T> Repo;
    public ReadRepApplication(IReadRepository<TKey, T> repository) : base(repository)
    {
        Repo = repository;
    }
    public virtual async Task<T> QueryAsync(TKey id) => await Repo.GetByIdAsync(id);
}

