using System;
using System.Linq;
using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.WebApi
{
    public interface IBaseGet
    {
        IQueryable GetQuery(IQueryable query);
        Expression GetExpression(Expression exp);
    }
    public interface IBaseGet<T> : IBaseGet
    {
        IQueryable<T> GetQuery(IQueryable<T> query);
        Expression<Func<T, bool>> GetExpression(Expression<Func<T, bool>> exp);
    }
}
