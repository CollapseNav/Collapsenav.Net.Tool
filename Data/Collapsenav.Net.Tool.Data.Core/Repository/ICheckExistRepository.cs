using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.Data;

public interface ICheckExistRepository<T> : IRepository<T> where T : class, IEntity
{
    /// <summary>
    /// 是否存在
    /// </summary>
    Task<bool> IsExistAsync(Expression<Func<T, bool>> exp);
}
