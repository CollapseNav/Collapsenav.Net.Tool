namespace Collapsenav.Net.Tool;
public static partial class CollectionExt
{
    /// <summary>
    /// 合并集合
    /// </summary>
    /// <param name="querys">合并目标</param>
    /// <param name="comparer">怎么去重啊</param>
    /// <param name="hashCodeFunc">hash去重</param>
    public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>> querys, Func<T, T, bool> comparer = null, Func<T, int> hashCodeFunc = null)
    {
        if (querys.IsEmpty())
            return null;
        var result = querys.FirstOrDefault();
        if (comparer == null)
            foreach (var query in querys.Skip(1))
                result = result.Concat(query);
        else
            foreach (var query in querys.Skip(1))
                result = result.Union(query, new CollapseNavEqualityComparer<T>(comparer, hashCodeFunc));
        return result;
    }

    /// <summary>
    /// 合并集合
    /// </summary>
    /// <param name="querys">合并目标</param>
    /// <param name="query">多加一行</param>
    /// <param name="comparer">怎么去重啊</param>
    /// <param name="hashCodeFunc">hash去重</param>
    public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>> querys, IEnumerable<T> query, Func<T, T, bool> comparer = null, Func<T, int> hashCodeFunc = null)
    {
        querys = querys.Append(query);
        return querys.Merge(comparer, hashCodeFunc);
    }

    /// <summary>
    /// 合并集合
    /// </summary>
    /// <param name="querys">合并目标</param>
    /// <param name="concatQuerys">多加一个同级选手</param>
    /// <param name="comparer">怎么去重啊</param>
    /// <param name="hashCodeFunc">hash去重</param>
    public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>> querys, IEnumerable<IEnumerable<T>> concatQuerys, Func<T, T, bool> comparer = null, Func<T, int> hashCodeFunc = null)
    {
        querys = querys.Concat(concatQuerys);
        return querys.Merge(comparer, hashCodeFunc);
    }

    /// <summary>
    /// 合并集合
    /// </summary>
    /// <param name="querys">合并目标</param>
    /// <param name="unique">是否去重</param>
    public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>> querys, bool unique)
    {
        return unique ? querys.Merge().Unique() : querys.Merge();
    }

    /// <summary>
    /// 合并集合
    /// </summary>
    /// <param name="querys">合并目标</param>
    /// <param name="query">多加一行</param>
    /// <param name="unique">是否去重</param>
    public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>> querys, IEnumerable<T> query, bool unique)
    {
        querys = querys.Append(query);
        return querys.Merge(unique);
    }

    /// <summary>
    /// 合并集合
    /// </summary>
    /// <param name="querys">合并目标</param>
    /// <param name="concatQuerys">多加一个同级选手</param>
    /// <param name="unique">是否去重</param>
    public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>> querys, IEnumerable<IEnumerable<T>> concatQuerys, bool unique)
    {
        querys = querys.Concat(concatQuerys);
        return querys.Merge(unique);
    }

}