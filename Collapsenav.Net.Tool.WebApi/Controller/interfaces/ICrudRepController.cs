using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;

public interface ICrudRepController<TKey, T, CreateT, GetT> : IQueryRepController<TKey, T, GetT>, IModifyRepController<TKey, T, CreateT>
    where T : class, IBaseEntity<TKey>
    where CreateT : IBaseCreate<T>
    where GetT : IBaseGet<T>
{
}