namespace Collapsenav.Net.Tool.Data;

public interface ICrudRepository<TKey, T> : IModifyRepository<TKey, T>, IQueryRepository<TKey, T> where T : class, IBaseEntity<TKey>
{
}
