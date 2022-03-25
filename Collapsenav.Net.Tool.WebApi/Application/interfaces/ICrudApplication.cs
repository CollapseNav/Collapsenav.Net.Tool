using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;

public interface ICrudApplication<T, CreateT, GetT> : IQueryApplication<T, GetT>, IModifyApplication<T, CreateT>
    where T : class, IEntity
    where CreateT : IBaseCreate<T>
    where GetT : IBaseGet<T>
{
}
public interface ICrudApplication<TKey, T, CreateT, GetT> : ICrudApplication<T, CreateT, GetT>, IQueryApplication<TKey, T, GetT>, IModifyApplication<TKey, T, CreateT>
    where T : class, IEntity<TKey>
    where CreateT : IBaseCreate<T>
    where GetT : IBaseGet<T>
{
}

#region 无泛型约束
public interface INoConstraintsCrudApplication<T, CreateT, GetT> : INoConstraintsQueryApplication<T, GetT>, INoConstraintsModifyApplication<T, CreateT>
{
}
public interface INoConstraintsCrudApplication<TKey, T, CreateT, GetT> : INoConstraintsCrudApplication<T, CreateT, GetT>, INoConstraintsQueryApplication<TKey, T, GetT>, INoConstraintsModifyApplication<TKey, T, CreateT>
{
}
#endregion
