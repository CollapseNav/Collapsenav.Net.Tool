using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;

public interface ICrudRepController<T, CreateT, GetT> : IQueryRepController<T, GetT>, IModifyRepController<T, CreateT>
    where T : class, IEntity
    where CreateT : IBaseCreate<T>
    where GetT : IBaseGet<T>
{
}
public interface ICrudRepController<TKey, T, CreateT, GetT> : IQueryRepController<TKey, T, GetT>, IModifyRepController<TKey, T, CreateT>
    where T : class, IEntity<TKey>
    where CreateT : IBaseCreate<T>
    where GetT : IBaseGet<T>
{
}