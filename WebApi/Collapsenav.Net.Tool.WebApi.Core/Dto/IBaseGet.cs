using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.WebApi;
public interface IBaseGet
{
    // Expression GetExpression(Expression exp);
    IQueryable GetQuery(IQueryable query);
}
public interface IBaseGet<T> : IBaseGet
{
    // Expression<Func<T, bool>> GetExpression(Expression<Func<T, bool>> exp = null);
    IQueryable<T> GetQuery(IQueryable<T> query);
}
