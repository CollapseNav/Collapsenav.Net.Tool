using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.Data;

public interface ICountRepository<T> : IRepository<T> where T : class, IEntity
{
    /// <summary>
    /// 统计数量
    /// </summary>
    Task<int> CountAsync(Expression<Func<T, bool>> exp = null);
}