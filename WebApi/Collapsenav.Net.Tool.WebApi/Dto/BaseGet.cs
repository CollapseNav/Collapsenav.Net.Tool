using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.WebApi;
public class BaseGet : IBaseGet
{
    public virtual Expression GetExpression(Expression exp)
    {
        return exp;
    }
}
public class BaseGet<T> : IBaseGet<T>
{
    public virtual Expression<Func<T, bool>> GetExpression(Expression<Func<T, bool>> exp = null)
    {
        exp ??= item => true;
        return exp;
    }

}
