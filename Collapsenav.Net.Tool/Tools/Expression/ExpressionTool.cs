using System;
using System.Linq.Expressions;

namespace Collapsenav.Net.Tool
{
    public partial class ExpressionTool
    {
        public static Expression<Func<T, bool>> AndAlso<T>(Expression<Func<T, bool>> origin, Expression<Func<T, bool>> exp)
        {
            var sum = Expression.AndAlso(origin.Body, Expression.Invoke(exp, origin.Parameters[0]));
            return Expression.Lambda<Func<T, bool>>(sum, origin.Parameters);
        }
        public static Expression<Func<T, bool>> OrElse<T>(Expression<Func<T, bool>> origin, Expression<Func<T, bool>> exp)
        {
            var sum = Expression.OrElse(origin.Body, Expression.Invoke(exp, origin.Parameters[0]));
            return Expression.Lambda<Func<T, bool>>(sum, origin.Parameters);
        }
        public static Expression<Func<T, bool>> And<T>(Expression<Func<T, bool>> origin, Expression<Func<T, bool>> exp)
        {
            var sum = Expression.And(origin.Body, Expression.Invoke(exp, origin.Parameters[0]));
            return Expression.Lambda<Func<T, bool>>(sum, origin.Parameters);
        }
        public static Expression<Func<T, bool>> Or<T>(Expression<Func<T, bool>> origin, Expression<Func<T, bool>> exp)
        {
            var sum = Expression.Or(origin.Body, Expression.Invoke(exp, origin.Parameters[0]));
            return Expression.Lambda<Func<T, bool>>(sum, origin.Parameters);
        }


        public static Expression<Func<T, bool>> AndAlsoIf<T>(Expression<Func<T, bool>> origin, bool flag, Expression<Func<T, bool>> exp)
        {
            if (!flag)
                return origin;
            var sum = Expression.AndAlso(origin.Body, Expression.Invoke(exp, origin.Parameters[0]));
            return Expression.Lambda<Func<T, bool>>(sum, origin.Parameters);
        }
        public static Expression<Func<T, bool>> OrElseIf<T>(Expression<Func<T, bool>> origin, bool flag, Expression<Func<T, bool>> exp)
        {
            if (!flag)
                return origin;
            var sum = Expression.OrElse(origin.Body, Expression.Invoke(exp, origin.Parameters[0]));
            return Expression.Lambda<Func<T, bool>>(sum, origin.Parameters);
        }
        public static Expression<Func<T, bool>> AndIf<T>(Expression<Func<T, bool>> origin, bool flag, Expression<Func<T, bool>> exp)
        {
            if (!flag)
                return origin;
            var sum = Expression.And(origin.Body, Expression.Invoke(exp, origin.Parameters[0]));
            return Expression.Lambda<Func<T, bool>>(sum, origin.Parameters);
        }
        public static Expression<Func<T, bool>> OrIf<T>(Expression<Func<T, bool>> origin, bool flag, Expression<Func<T, bool>> exp)
        {
            if (!flag)
                return origin;
            var sum = Expression.Or(origin.Body, Expression.Invoke(exp, origin.Parameters[0]));
            return Expression.Lambda<Func<T, bool>>(sum, origin.Parameters);
        }


        public static Expression<Func<T, bool>> AndAlsoIf<T>(Expression<Func<T, bool>> origin, string flag, Expression<Func<T, bool>> exp)
        {
            if (flag.IsEmpty())
                return origin;
            return AndAlsoIf(origin, true, exp);
        }
        public static Expression<Func<T, bool>> OrElseIf<T>(Expression<Func<T, bool>> origin, string flag, Expression<Func<T, bool>> exp)
        {
            if (flag.IsEmpty())
                return origin;
            return OrElseIf(origin, true, exp);
        }
        public static Expression<Func<T, bool>> AndIf<T>(Expression<Func<T, bool>> origin, string flag, Expression<Func<T, bool>> exp)
        {
            if (flag.IsEmpty())
                return origin;
            return AndIf(origin, true, exp);
        }
        public static Expression<Func<T, bool>> OrIf<T>(Expression<Func<T, bool>> origin, string flag, Expression<Func<T, bool>> exp)
        {
            if (flag.IsEmpty())
                return origin;
            return OrIf(origin, true, exp);
        }

    }
}
