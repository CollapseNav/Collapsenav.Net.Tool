using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.WebApi;
public interface IBaseGet
{
    Expression GetExpression(Expression exp);
}
public interface IBaseGet<T>
{
    Expression<Func<T, bool>> GetExpression(Expression<Func<T, bool>> exp = null);
}
