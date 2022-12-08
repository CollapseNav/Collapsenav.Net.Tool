namespace Collapsenav.Net.Tool;
public static partial class CollectionExt
{
    /// <summary>
    /// 全包含
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="filters">条件</param>
    public static bool AllContain<T>(this IEnumerable<T> query, params T[] filters)
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
    public static bool AllContain<T>(this IEnumerable<T> query, Func<T, T, bool> comparer, params T[] filters)
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
    public static bool AllContain<T>(this IEnumerable<T> query, Func<T, T, bool> comparer, IEnumerable<T> filters)
    {
        return query.AllContain(comparer, filters.ToArray());
    }

    /// <summary>
    /// 部分包含
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="filters">条件</param>
    public static bool HasContain<T>(this IEnumerable<T> query, params T[] filters)
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
    public static bool HasContain<T>(this IEnumerable<T> query, Func<T, T, bool> comparer, params T[] filters)
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
    public static bool HasContain<T>(this IEnumerable<T> query, Func<T, T, bool> comparer, IEnumerable<T> filters)
    {
        return query.HasContain(comparer, filters.ToArray());
    }
    /// <summary>
    /// 对象是否在集合中
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="items"></param>
    /// <param name="comparer">对比</param>
    public static bool In<T>(this T origin, IEnumerable<T> items, Func<T, T, bool> comparer = null)
    {
        return comparer == null ? items.Contains(origin) : items.Contains(origin, new CollapseNavEqualityComparer<T>(comparer));
    }
    /// <summary>
    /// 对象是否在集合中
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="items"></param>
    /// <param name="comparer">对比</param>
    public static bool In<T>(this T origin, Func<T, T, bool> comparer, params T[] items)
    {
        return items.Contains(origin, new CollapseNavEqualityComparer<T>(comparer));
    }
    /// <summary>
    /// 对象是否在集合中
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="items"></param>
    public static bool In<T>(this T origin, params T[] items)
    {
        return items.Contains(origin);
    }
    /// <summary>
    /// 取交集
    /// </summary>
    public static IEnumerable<T> GetItemIn<T>(this IEnumerable<T> origin, IEnumerable<T> target)
    {
        return origin.Intersect(target);
    }
    /// <summary>
    /// 取交集
    /// </summary>
    public static IEnumerable<T> Intersect<T>(this IEnumerable<T> origin, IEnumerable<T> target, Func<T, T, bool> comparer)
    {
        return origin.GetItemIn(target, comparer);
    }
    /// <summary>
    /// 取交集
    /// </summary>
    public static IEnumerable<T> GetItemIn<T>(this IEnumerable<T> origin, IEnumerable<T> target, Func<T, T, bool> comparer)
    {
        return origin.Intersect(target, new CollapseNavEqualityComparer<T>(comparer));
    }
    /// <summary>
    /// 取差集
    /// </summary>
    public static IEnumerable<T> GetItemNotIn<T>(this IEnumerable<T> origin, IEnumerable<T> target)
    {
        return origin.Except(target);
    }
    /// <summary>
    /// 取差集
    /// </summary>
    public static IEnumerable<T> Except<T>(this IEnumerable<T> origin, IEnumerable<T> target, Func<T, T, bool> comparer)
    {
        return origin.GetItemNotIn(target, comparer);
    }
    /// <summary>
    /// 取差集
    /// </summary>
    public static IEnumerable<T> GetItemNotIn<T>(this IEnumerable<T> origin, IEnumerable<T> target, Func<T, T, bool> comparer)
    {
        return origin.Except(target, new CollapseNavEqualityComparer<T>(comparer));
    }
}