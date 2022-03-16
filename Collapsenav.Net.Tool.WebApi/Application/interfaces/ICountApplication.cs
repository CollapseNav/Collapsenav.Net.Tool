using System.Linq.Expressions;
using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;

public interface ICountApplication<T> : IApplication<T> where T : IEntity
{
    Task<int> CountAsync(Expression<Func<T, bool>> exp = null);
}