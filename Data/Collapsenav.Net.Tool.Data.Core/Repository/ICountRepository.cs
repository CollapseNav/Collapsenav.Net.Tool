using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.Data;

public interface ICountRepository<T> : IRepository<T> where T : class, IEntity
{
    /// <summary>
    /// 统计数量
    /// </summary>
    Task<int> CountAsync(Expression<Func<T, bool>> exp = null);
}

#region 无泛型约束
public interface INoConstraintsCountRepository<T> : INoConstraintsRepository<T>
{
    /// <summary>
    /// 统计数量
    /// </summary>
    Task<int> CountAsync(Expression<Func<T, bool>> exp = null);
}
#endregion
