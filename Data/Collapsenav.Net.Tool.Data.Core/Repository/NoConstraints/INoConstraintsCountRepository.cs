using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.Data;

public interface INoConstraintsCountRepository<T> : INoConstraintsRepository<T>
{
    /// <summary>
    /// 统计数量
    /// </summary>
    Task<int> CountAsync(Expression<Func<T, bool>> exp = null);
}