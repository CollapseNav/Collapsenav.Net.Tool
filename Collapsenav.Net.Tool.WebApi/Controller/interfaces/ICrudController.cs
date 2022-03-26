using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;

public interface ICrudController<T, CreateT, GetT> : IQueryController<T, GetT>, IModifyController<T, CreateT>
    where T : class, IEntity
    where CreateT : IBaseCreate<T>
    where GetT : IBaseGet<T>
{
}
public interface ICrudController<TKey, T, CreateT, GetT> : IQueryController<TKey, T, GetT>, IModifyController<TKey, T, CreateT>
    where T : class, IEntity<TKey>
    where CreateT : IBaseCreate<T>
    where GetT : IBaseGet<T>
{
}

#region 无泛型约束
public interface INoConstraintsCrudController<T, CreateT, GetT> : INoConstraintsQueryController<T, GetT>, INoConstraintsModifyController<T, CreateT>
{
}
public interface INoConstraintsCrudController<TKey, T, CreateT, GetT> : INoConstraintsQueryController<TKey, T, GetT>, INoConstraintsModifyController<TKey, T, CreateT>
{
}
#endregion
