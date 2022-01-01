using System.Linq.Expressions;

namespace Collapsenav.Net.Tool;
public static partial class ExpressionExt
{
    public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> origin, Expression<Func<T, bool>> exp)
    {
        return ExpressionTool.AndAlso(origin, exp);
    }
    public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> origin, Expression<Func<T, bool>> exp)
    {
        return ExpressionTool.OrElse(origin, exp);
    }
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> origin, Expression<Func<T, bool>> exp)
    {
        return ExpressionTool.And(origin, exp);
    }
    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> origin, Expression<Func<T, bool>> exp)
    {
        return ExpressionTool.Or(origin, exp);
    }
    public static Expression<Func<T, bool>> AndAlsoIf<T>(this Expression<Func<T, bool>> origin, bool flag, Expression<Func<T, bool>> exp)
    {
        return ExpressionTool.AndAlsoIf(origin, flag, exp);
    }
    public static Expression<Func<T, bool>> OrElseIf<T>(this Expression<Func<T, bool>> origin, bool flag, Expression<Func<T, bool>> exp)
    {
        return ExpressionTool.OrElseIf(origin, flag, exp);
    }
    public static Expression<Func<T, bool>> AndIf<T>(this Expression<Func<T, bool>> origin, bool flag, Expression<Func<T, bool>> exp)
    {
        return ExpressionTool.AndIf(origin, flag, exp);
    }
    public static Expression<Func<T, bool>> OrIf<T>(this Expression<Func<T, bool>> origin, bool flag, Expression<Func<T, bool>> exp)
    {
        return ExpressionTool.OrIf(origin, flag, exp);
    }
    public static Expression<Func<T, bool>> AndAlsoIf<T>(this Expression<Func<T, bool>> origin, string flag, Expression<Func<T, bool>> exp)
    {
        return ExpressionTool.AndAlsoIf(origin, flag, exp);
    }
    public static Expression<Func<T, bool>> OrElseIf<T>(this Expression<Func<T, bool>> origin, string flag, Expression<Func<T, bool>> exp)
    {
        return ExpressionTool.OrElseIf(origin, flag, exp);
    }
    public static Expression<Func<T, bool>> AndIf<T>(this Expression<Func<T, bool>> origin, string flag, Expression<Func<T, bool>> exp)
    {
        return ExpressionTool.AndIf(origin, flag, exp);
    }
    public static Expression<Func<T, bool>> OrIf<T>(this Expression<Func<T, bool>> origin, string flag, Expression<Func<T, bool>> exp)
    {
        return ExpressionTool.OrIf(origin, flag, exp);
    }
}
