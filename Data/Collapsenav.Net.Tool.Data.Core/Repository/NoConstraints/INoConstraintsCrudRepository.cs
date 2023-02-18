namespace Collapsenav.Net.Tool.Data;


public interface INoConstraintsCrudRepository<T> : INoConstraintsModifyRepository<T>, INoConstraintsQueryRepository<T>
{
}
public interface INoConstraintsCrudRepository<TKey, T> : INoConstraintsCrudRepository<T>, INoConstraintsModifyRepository<TKey, T>, INoConstraintsQueryRepository<TKey, T>
{
}