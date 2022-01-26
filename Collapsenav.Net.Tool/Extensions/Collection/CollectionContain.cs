namespace Collapsenav.Net.Tool;
public static partial class CollectionExt
{
    /// <summary>
    /// 全包含
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="filters">条件</param>
    public static bool ContainAnd<T>(this IEnumerable<T> query, params T[] filters)
    {
        foreach (var filter in filters)
            if (!query.Contains(filter))
                return false;
        return true;
    }
    /// <summary>
    /// 全包含
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="comparer">怎么去重</param>
    /// <param name="filters">条件</param>
    public static bool ContainAnd<T>(this IEnumerable<T> query, Func<T, T, bool> comparer, params T[] filters)
    {
        foreach (var filter in filters)
            if (!query.Contains(filter, new CollapseNavEqualityComparer<T>(comparer)))
                return false;
        return true;
    }
    /// <summary>
    /// 全包含
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="comparer">怎么去重</param>
    /// <param name="filters">条件</param>
    public static bool ContainAnd<T>(this IEnumerable<T> query, Func<T, T, bool> comparer, IEnumerable<T> filters)
    {
        return query.ContainAnd(comparer, filters.ToArray());
    }

    /// <summary>
    /// 部分包含
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="filters">条件</param>
    public static bool ContainOr<T>(this IEnumerable<T> query, params T[] filters)
    {
        foreach (var filter in filters)
            if (query.Contains(filter))
                return true;
        return false;
    }
    /// <summary>
    /// 部分包含
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="comparer">怎么去重</param>
    /// <param name="filters">条件</param>
    public static bool ContainOr<T>(this IEnumerable<T> query, Func<T, T, bool> comparer, params T[] filters)
    {
        foreach (var filter in filters)
            if (query.Contains(filter, new CollapseNavEqualityComparer<T>(comparer)))
                return true;
        return false;
    }
    /// <summary>
    /// 部分包含
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="comparer">怎么判断重复</param>
    /// <param name="filters">条件</param>
    public static bool ContainOr<T>(this IEnumerable<T> query, Func<T, T, bool> comparer, IEnumerable<T> filters)
    {
        return query.ContainOr(comparer, filters.ToArray());
    }
}