using System.Linq.Expressions;
using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;

public interface ICountApplication<T> : IApplication<T> where T : IEntity
{
    /// <summary>
    /// 统计数量
    /// </summary>
    Task<int> CountAsync(Expression<Func<T, bool>> exp = null);
}

#region 无泛型约束
public interface INoConstraintsCountApplication<T> : INoConstraintsApplication<T>
{
    /// <summary>
    /// 统计数量
    /// </summary>
    Task<int> CountAsync(Expression<Func<T, bool>> exp = null);
}
#endregion
