using System.Linq.Expressions;
using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;

public interface ICheckExistApplication<T> : IApplication<T> where T : IEntity
{
    /// <summary>
    /// 是否存在
    /// </summary>
    Task<bool> IsExistAsync(Expression<Func<T, bool>> exp);
}