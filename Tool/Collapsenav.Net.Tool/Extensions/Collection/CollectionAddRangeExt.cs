namespace Collapsenav.Net.Tool;
public partial class CollectionExt
{
    /// <summary>
    /// 向一个集合中添加多个对象
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="values">添加的对象</param>
    public static IEnumerable<T> AddRange<T>(this IEnumerable<T> query, params T[] values)
    {
        return query.Concat(values);
    }
    /// <summary>
    /// 向一个集合中添加多个对象(带去重)
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="comparer">去重依据</param>
    /// <param name="values">添加的对象</param>
    public static IEnumerable<T> AddRange<T>(this IEnumerable<T> query, Func<T, T, bool> comparer, params T[] values)
    {
        return query.AddRange(comparer, null, values);
    }
    /// <summary>
    /// 向一个集合中添加多个对象(带去重)
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="hashCodeFunc">去重依据(hash)</param>
    /// <param name="values">添加的对象</param>
    public static IEnumerable<T> AddRange<T>(this IEnumerable<T> query, Func<T, int> hashCodeFunc, params T[] values)
    {
        return query.AddRange(null, hashCodeFunc, values);
    }
    /// <summary>
    /// 向一个集合中添加多个对象(带去重)
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="comparer">去重依据</param>
    /// <param name="hashCodeFunc">去重依据(hash)</param>
    /// <param name="values">添加的对象</param>
    public static IEnumerable<T> AddRange<T>(this IEnumerable<T> query, Func<T, T, bool> comparer, Func<T, int> hashCodeFunc, params T[] values)
    {
        return query.Union(values, new CollapseNavEqualityComparer<T>(comparer, hashCodeFunc));
    }

    /// <summary>
    /// 向一个集合中添加多个对象
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="values">添加的对象</param>
    public static IEnumerable<T> AddRange<T>(this IEnumerable<T> query, IEnumerable<T> values)
    {
        return query.Concat(values);
    }
    /// <summary>
    /// 向一个集合中添加多个对象(带去重)
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="comparer">去重依据</param>
    /// <param name="values">添加的对象</param>
    public static IEnumerable<T> AddRange<T>(this IEnumerable<T> query, Func<T, T, bool> comparer, IEnumerable<T> values)
    {
        return query.AddRange(comparer, null, values);
    }
    /// <summary>
    /// 向一个集合中添加多个对象(带去重)
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="hashCodeFunc">去重依据(hash)</param>
    /// <param name="values">添加的对象</param>
    public static IEnumerable<T> AddRange<T>(this IEnumerable<T> query, Func<T, int> hashCodeFunc, IEnumerable<T> values)
    {
        return query.AddRange(null, hashCodeFunc, values);
    }
    /// <summary>
    /// 向一个集合中添加多个对象(带去重)
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="comparer">去重依据</param>
    /// <param name="hashCodeFunc">去重依据(hash)</param>
    /// <param name="values">添加的对象</param>
    public static IEnumerable<T> AddRange<T>(this IEnumerable<T> query, Func<T, T, bool> comparer, Func<T, int> hashCodeFunc, IEnumerable<T> values)
    {
        return query.Union(values, new CollapseNavEqualityComparer<T>(comparer, hashCodeFunc));
    }





    /// <summary>
    /// 向一个集合中添加多个对象
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="values">添加的对象</param>
    public static void AddRange<T>(this ICollection<T> query, params T[] values)
    {
        foreach (var item in values)
            query.Add(item);
    }
    /// <summary>
    /// 向一个集合中添加多个对象(带去重)
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="comparer">去重依据</param>
    /// <param name="values">添加的对象</param>
    public static void AddRange<T>(this ICollection<T> query, Func<T, T, bool> comparer, params T[] values)
    {
        query.AddRange(comparer, null, values);
    }
    /// <summary>
    /// 向一个集合中添加多个对象(带去重)
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="hashCodeFunc">去重依据(hash)</param>
    /// <param name="values">添加的对象</param>
    public static void AddRange<T>(this ICollection<T> query, Func<T, int> hashCodeFunc, params T[] values)
    {
        query.AddRange(null, hashCodeFunc, values);
    }
    /// <summary>
    /// 向一个集合中添加多个对象(带去重)
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="comparer">去重依据</param>
    /// <param name="hashCodeFunc">去重依据(hash)</param>
    /// <param name="values">添加的对象</param>
    public static void AddRange<T>(this ICollection<T> query, Func<T, T, bool> comparer, Func<T, int> hashCodeFunc, params T[] values)
    {
        var uniqueComparer = new CollapseNavEqualityComparer<T>(comparer, hashCodeFunc);
        var uniqueData = values.Distinct(uniqueComparer);
        foreach (var item in uniqueData)
            if (!query.Contains(item, uniqueComparer))
                query.Add(item);
    }

    /// <summary>
    /// 向一个集合中添加多个对象
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="values">添加的对象</param>
    public static void AddRange<T>(this ICollection<T> query, IEnumerable<T> values)
    {
        query.AddRange(values.ToArray());
    }
    /// <summary>
    /// 向一个集合中添加多个对象(带去重)
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="comparer">去重依据</param>
    /// <param name="values">添加的对象</param>
    public static void AddRange<T>(this ICollection<T> query, Func<T, T, bool> comparer, IEnumerable<T> values)
    {
        query.AddRange(comparer, null, values.ToArray());
    }
    /// <summary>
    /// 向一个集合中添加多个对象(带去重)
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="hashCodeFunc">去重依据(hash)</param>
    /// <param name="values">添加的对象</param>
    public static void AddRange<T>(this ICollection<T> query, Func<T, int> hashCodeFunc, IEnumerable<T> values)
    {
        query.AddRange(null, hashCodeFunc, values.ToArray());
    }
    /// <summary>
    /// 向一个集合中添加多个对象(带去重)
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="comparer">去重依据</param>
    /// <param name="hashCodeFunc">去重依据(hash)</param>
    /// <param name="values">添加的对象</param>
    public static void AddRange<T>(this ICollection<T> query, Func<T, T, bool> comparer, Func<T, int> hashCodeFunc, IEnumerable<T> values)
    {
        query.AddRange(comparer, hashCodeFunc, values.ToArray());
    }
}