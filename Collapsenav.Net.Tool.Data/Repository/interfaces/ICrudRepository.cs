namespace Collapsenav.Net.Tool.Data;

public interface ICrudRepository<T> : IModifyRepository<T>, IQueryRepository<T> where T : class, IEntity
{
}
public interface ICrudRepository<TKey, T> : ICrudRepository<T>, IModifyRepository<TKey, T>, IQueryRepository<TKey, T> where T : class, IEntity<TKey>
{
}

#region 无泛型约束
public interface INoConstraintsCrudRepository<T> : INoConstraintsModifyRepository<T>, INoConstraintsQueryRepository<T>
{
}
public interface INoConstraintsCrudRepository<TKey, T> : INoConstraintsCrudRepository<T>, INoConstraintsModifyRepository<TKey, T>, INoConstraintsQueryRepository<TKey, T>
{
}
#endregion
