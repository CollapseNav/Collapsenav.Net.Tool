using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.Data;
public interface INoConstraintsCheckExistRepository<T> : INoConstraintsRepository<T>
{
    /// <summary>
    /// 是否存在
    /// </summary>
    Task<bool> IsExistAsync(Expression<Func<T, bool>> exp);
}