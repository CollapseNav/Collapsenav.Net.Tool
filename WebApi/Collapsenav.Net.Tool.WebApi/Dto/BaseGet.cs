using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.WebApi;
public class BaseGet : IBaseGet
{
    public virtual Expression GetExpression(Expression exp)
    {
        return exp;
    }

    public virtual IQueryable GetQuery(IQueryable query)
    {
        return query;
    }
}
public class BaseGet<T> : IBaseGet<T>
{
    public virtual Expression<Func<T, bool>> GetExpression(Expression<Func<T, bool>> exp)
    {
        return exp;
    }

    public virtual IQueryable<T> GetQuery(IQueryable<T> query)
    {
        return query.Where(GetExpression(item => true));
    }
}
