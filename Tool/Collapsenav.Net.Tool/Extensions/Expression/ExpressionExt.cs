using System.Linq.Expressions;

namespace Collapsenav.Net.Tool;
public static partial class ExpressionExt
{
    public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> origin, Expression<Func<T, bool>> exp)
    {
        var sum = Expression.AndAlso(origin.Body, Expression.Invoke(exp, origin.Parameters[0]));
        return Expression.Lambda<Func<T, bool>>(sum, origin.Parameters);
    }
    public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> origin, Expression<Func<T, bool>> exp)
    {
        var sum = Expression.OrElse(origin.Body, Expression.Invoke(exp, origin.Parameters[0]));
        return Expression.Lambda<Func<T, bool>>(sum, origin.Parameters);
    }
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> origin, Expression<Func<T, bool>> exp)
    {
        var sum = Expression.And(origin.Body, Expression.Invoke(exp, origin.Parameters[0]));
        return Expression.Lambda<Func<T, bool>>(sum, origin.Parameters);
    }
    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> origin, Expression<Func<T, bool>> exp)
    {
        var sum = Expression.Or(origin.Body, Expression.Invoke(exp, origin.Parameters[0]));
        return Expression.Lambda<Func<T, bool>>(sum, origin.Parameters);
    }


    public static Expression<Func<T, bool>> AndAlsoIf<T>(this Expression<Func<T, bool>> origin, bool flag, Expression<Func<T, bool>> exp)
    {
        if (!flag)
            return origin;
        var sum = Expression.AndAlso(origin.Body, Expression.Invoke(exp, origin.Parameters[0]));
        return Expression.Lambda<Func<T, bool>>(sum, origin.Parameters);
    }
    public static Expression<Func<T, bool>> OrElseIf<T>(this Expression<Func<T, bool>> origin, bool flag, Expression<Func<T, bool>> exp)
    {
        if (!flag)
            return origin;
        var sum = Expression.OrElse(origin.Body, Expression.Invoke(exp, origin.Parameters[0]));
        return Expression.Lambda<Func<T, bool>>(sum, origin.Parameters);
    }
    public static Expression<Func<T, bool>> AndIf<T>(this Expression<Func<T, bool>> origin, bool flag, Expression<Func<T, bool>> exp)
    {
        if (!flag)
            return origin;
        var sum = Expression.And(origin.Body, Expression.Invoke(exp, origin.Parameters[0]));
        return Expression.Lambda<Func<T, bool>>(sum, origin.Parameters);
    }
    public static Expression<Func<T, bool>> OrIf<T>(this Expression<Func<T, bool>> origin, bool flag, Expression<Func<T, bool>> exp)
    {
        if (!flag)
            return origin;
        var sum = Expression.Or(origin.Body, Expression.Invoke(exp, origin.Parameters[0]));
        return Expression.Lambda<Func<T, bool>>(sum, origin.Parameters);
    }


    public static Expression<Func<T, bool>> AndAlsoIf<T>(this Expression<Func<T, bool>> origin, string flag, Expression<Func<T, bool>> exp)
    {
        if (flag.IsEmpty())
            return origin;
        return AndAlsoIf(origin, true, exp);
    }
    public static Expression<Func<T, bool>> OrElseIf<T>(this Expression<Func<T, bool>> origin, string flag, Expression<Func<T, bool>> exp)
    {
        if (flag.IsEmpty())
            return origin;
        return OrElseIf(origin, true, exp);
    }
    public static Expression<Func<T, bool>> AndIf<T>(this Expression<Func<T, bool>> origin, string flag, Expression<Func<T, bool>> exp)
    {
        if (flag.IsEmpty())
            return origin;
        return AndIf(origin, true, exp);
    }
    public static Expression<Func<T, bool>> OrIf<T>(this Expression<Func<T, bool>> origin, string flag, Expression<Func<T, bool>> exp)
    {
        if (flag.IsEmpty())
            return origin;
        return OrIf(origin, true, exp);
    }

    public static Expression<Func<E, E>> NewExpression<E>(this object obj, bool ignoreNull = false)
    {
        var type = obj.GetType();
        var paramExp = Expression.Parameter(typeof(E), "item");
        var binds = new List<MemberBinding>();
        type.Props().ForEach(prop =>
        {
            var value = obj.GetValue(prop.Name);
            if (ignoreNull && value == null)
                return;
            var eprop = typeof(E).GetProperty(prop.Name);
            if (eprop == null) return;
            binds.Add(Expression.Bind(eprop, Expression.Constant(value, prop.PropertyType)));
        });
        var init = Expression.MemberInit(Expression.New(typeof(E)), binds.ToArray());
        var lambda = Expression.Lambda<Func<E, E>>(init, new ParameterExpression[] { paramExp });
        return lambda;
    }
}
