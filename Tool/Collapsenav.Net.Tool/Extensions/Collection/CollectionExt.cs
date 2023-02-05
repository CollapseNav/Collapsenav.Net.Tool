using System.Linq.Expressions;

namespace Collapsenav.Net.Tool;
public static partial class CollectionExt
{
    /// <summary>
    /// 分割集合
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="size">每片大小</param>
    public static IEnumerable<IEnumerable<T>> SpliteCollection<T>(this IEnumerable<T> query, int size = 1)
    {
        for (int i = 0; i < (query.Count() / size) + (query.Count() % size == 0 ? 0 : 1); i++)
            yield return query.Skip(i * size).Take(size);
    }

    /// <summary>
    /// 去重
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="groupBy">参考属性/字段/...</param>
    public static IEnumerable<T> Distinct<T, E>(this IEnumerable<T> query, Func<T, E> groupBy)
    {
        return query.Unique(groupBy);
    }
    /// <summary>
    /// 去重
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="groupBy">参考属性/字段/...</param>
    public static IEnumerable<T> Unique<T, E>(this IEnumerable<T> query, Func<T, E> groupBy)
    {
        return query.GroupBy(groupBy).Select(item => item.First());
    }

    /// <summary>
    /// 去重
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="comparer">怎么比</param>
    public static IEnumerable<T> Distinct<T>(this IEnumerable<T> query, Func<T, T, bool> comparer)
    {
        return query.Unique(comparer);
    }
    /// <summary>
    /// 去重
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="comparer">怎么比</param>
    public static IEnumerable<T> Unique<T>(this IEnumerable<T> query, Func<T, T, bool> comparer)
    {
        return query.Unique(comparer, null);
    }

    /// <summary>
    /// 去重
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="hashCodeFunc">hash去重</param>
    public static IEnumerable<T> Distinct<T>(this IEnumerable<T> query, Func<T, int> hashCodeFunc)
    {
        return query.Unique(hashCodeFunc);
    }
    /// <summary>
    /// 去重
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="hashCodeFunc">hash去重</param>
    public static IEnumerable<T> Unique<T>(this IEnumerable<T> query, Func<T, int> hashCodeFunc)
    {
        return query.Unique(null, hashCodeFunc);
    }

    /// <summary>
    /// 去重
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="comparer">怎么比</param>
    /// <param name="hashCodeFunc">hash去重</param>
    public static IEnumerable<T> Distinct<T>(this IEnumerable<T> query, Func<T, T, bool> comparer, Func<T, int> hashCodeFunc)
    {
        return query.Unique(comparer, hashCodeFunc);
    }
    /// <summary>
    /// 去重
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="comparer">怎么比</param>
    /// <param name="hashCodeFunc">hash去重</param>
    public static IEnumerable<T> Unique<T>(this IEnumerable<T> query, Func<T, T, bool> comparer, Func<T, int> hashCodeFunc)
    {
        return query.Distinct(new CollapseNavEqualityComparer<T>(comparer, hashCodeFunc));
    }

    /// <summary>
    /// 去重
    /// </summary>
    /// <param name="query">源集合</param>
    public static IEnumerable<T> Unique<T>(this IEnumerable<T> query)
    {
        return query.Distinct();
    }

    /// <summary>
    /// 判断两个集合是否相等
    /// </summary>
    /// <param name="left">集合1</param>
    /// <param name="right">集合2</param>
    /// <param name="comparer">怎么比</param>
    /// <param name="hashCodeFunc">hash</param>
    public static bool SequenceEqual<T>(this IEnumerable<T> left, IEnumerable<T> right, Func<T, T, bool> comparer, Func<T, int> hashCodeFunc)
    {
        return left.SequenceEqual(right, new CollapseNavEqualityComparer<T>(comparer, hashCodeFunc));
    }

    /// <summary>
    /// 判断两个集合是否相等
    /// </summary>
    /// <param name="left">集合1</param>
    /// <param name="right">集合2</param>
    /// <param name="comparer">怎么比</param>
    public static bool SequenceEqual<T>(this IEnumerable<T> left, IEnumerable<T> right, Func<T, T, bool> comparer)
    {
        return left.SequenceEqual(right, comparer, null);
    }

    /// <summary>
    /// 判断两个集合是否相等
    /// </summary>
    /// <param name="left">集合1</param>
    /// <param name="right">集合2</param>
    /// <param name="hashCodeFunc">hash</param>
    public static bool SequenceEqual<T>(this IEnumerable<T> left, IEnumerable<T> right, Func<T, int> hashCodeFunc)
    {
        return left.SequenceEqual(right, null, hashCodeFunc);
    }

    /// <summary>
    /// WhereIf
    /// </summary>
    /// <param name="query">query</param>
    /// <param name="flag">bool 作为标记，true则应用 filter</param>
    /// <param name="filter">筛选条件</param>
    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> query, bool flag, Func<T, bool> filter)
    {
        return flag ? query.Where(filter) : query;
    }
    /// <summary>
    /// WhereIf
    /// </summary>
    /// <param name="query">query</param>
    /// <param name="flag">bool 作为标记，true则应用 filter</param>
    /// <param name="filter">筛选条件</param>
    public static IEnumerable<T> WhereIf<T, N>(this IEnumerable<T> query, N? flag, Func<T, bool> filter) where N : struct
    {
        return flag.HasValue ? query.Where(filter) : query;
    }

    /// <summary>
    /// WhereIf
    /// </summary>
    /// <param name="query">query</param>
    /// <param name="input">非空字符串则应用筛选条件</param>
    /// <param name="filter">筛选条件</param>
    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> query, string input, Func<T, bool> filter)
    {
        return query.WhereIf(input.NotEmpty(), filter);
    }

    /// <summary>
    /// WhereIf
    /// </summary>
    /// <param name="query">query</param>
    /// <param name="flag">bool 作为标记，true则应用 filter</param>
    /// <param name="filter">筛选条件</param>
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool flag, Expression<Func<T, bool>> filter)
    {
        return flag ? query.Where(filter) : query;
    }

    /// <summary>
    /// WhereIf
    /// </summary>
    /// <param name="query">query</param>
    /// <param name="input">非空字符串则应用筛选条件</param>
    /// <param name="filter">筛选条件</param>
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, string input, Expression<Func<T, bool>> filter)
    {
        return query.WhereIf(input.NotEmpty(), filter);
    }

    /// <summary>
    /// 空?
    /// </summary>
    /// <param name="query">源集合</param>
    public static bool IsEmpty<T>(this IEnumerable<T> query)
    {
        return query == null || !query.Any();
    }

    /// <summary>
    /// 没空?
    /// </summary>
    /// <param name="query">源集合</param>
    public static bool NotEmpty<T>(this IEnumerable<T> query)
    {
        return query != null && query.Any();
    }
    /// <summary>
    /// 打乱顺序
    /// </summary>
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> query, int round = 1)
    {
        var random = new Random();
        if (round > 0)
        {
            for (var i = 0; i < round; i++)
                query = query.OrderBy(item => Guid.NewGuid()).ToList();
        }
        return query;
    }


    /// <summary>
    /// 遍历执行(不知道为啥原来的IEnumerable不提供这功能)
    /// </summary>
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> query, Action<T> action)
    {
        foreach (var item in query)
            action(item);
        return query;
    }
    /// <summary>
    /// 生成包含index的元组集合(value,index)
    /// </summary>
    public static IEnumerable<(T value, int index)> SelectWithIndex<T>(this IEnumerable<T> origin, int zero = 0)
    {
        return origin.Select((value, index) => (value, index + zero));
    }
    /// <summary>
    /// 生成包含index的元组集合(value,index)
    /// </summary>
    /// <param name="origin">源集合</param>
    /// <param name="index">生成index的委托</param>
    public static IEnumerable<(T value, E index)> SelectWithIndex<T, E>(this IEnumerable<T> origin, Func<T, E> index)
    {
        return origin.Select(value => (value, index(value)));
    }
    /// <summary>
    /// 生成包含index的元组集合(value,index)
    /// </summary>
    /// <param name="origin">源集合</param>
    /// <param name="value">生成value的委托</param>
    /// <param name="index">生成index的委托</param>
    public static IEnumerable<(E value, F index)> SelectWithIndex<T, E, F>(this IEnumerable<T> origin, Func<T, E> value, Func<T, F> index)
    {
        return origin.Select(item => (value(item), index(item)));
    }

    /// <summary>
    /// 创建数组
    /// </summary>
    public static T[] BuildArray<T>(this T obj, int len = 1)
    {
        T[] array = new T[len];
#if NETSTANDARD2_0
        for (var i = 0; i < array.Length; i++)
            array[i] = obj;
#else
        Array.Fill(array, obj);
#endif
        return array;
    }
    /// <summary>
    /// 创建List集合
    /// </summary>
    public static List<T> BuildList<T>(this T obj, int len = 1)
    {
        List<T> list = new();
        T[] array = new T[len];
#if NETSTANDARD2_0
        for (var i = 0; i < array.Length; i++)
            array[i] = obj;
#else
        Array.Fill(array, obj);
#endif
        list.AddRange(array);
        return list;
    }
}
