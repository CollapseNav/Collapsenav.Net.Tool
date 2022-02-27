using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.Data;

public interface ICheckExistRepository<T> : IRepository<T> where T : class, IEntity
{
    Task<bool> IsExistAsync(Expression<Func<T, bool>> exp);
}