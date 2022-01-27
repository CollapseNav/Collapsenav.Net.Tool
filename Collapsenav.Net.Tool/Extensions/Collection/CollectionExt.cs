﻿using System.Linq.Expressions;

namespace Collapsenav.Net.Tool;
public static partial class CollectionExt
{
    /// <summary>
    /// 分割集合
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="size">每片大小</param>
    public static IEnumerable<IEnumerable<T>> SpliteCollection<T>(this IEnumerable<T> query, int size)
    {
        for (int i = 0; i < (query.Count() / size) + (query.Count() % size == 0 ? 0 : 1); i++)
            yield return query.Skip(i * size).Take(size);
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
    public static IEnumerable<T> Unique<T>(this IEnumerable<T> query, Func<T, T, bool> comparer)
    {
        return query.Unique(comparer, null);
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
        return query.WhereIf(input.IsEmpty(), filter);
    }

    public static IEnumerable<T> RemoveRepeat<T>(this IEnumerable<T> query, IEnumerable<T> target = null)
    {
        query = query.Distinct();
        if (target == null)
            return query;
        List<T> result = new();
        foreach (var q in query)
        {
            if (!target.Contains(q))
                result.Add(q);
        }
        return result;
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
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> query)
    {
        // TODO 应该也可以改成使用 Guid.NewGuid() 然后 OrderBy 的方式做,但孰优孰劣就不清楚了
        var random = new Random();
        var resultList = new List<T>();
        foreach (var item in query)
            resultList.Insert(random.Next(resultList.Count), item);
        return resultList;
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
    public static IEnumerable<(T value, int index)> SelectWithIndex<T>(this IEnumerable<T> origin, int zero = 0)
    {
        return origin.Select((value, index) => (value, index + zero));
    }
    public static IEnumerable<(T value, E index)> SelectWithIndex<T, E>(this IEnumerable<T> origin, Func<T, E> index)
    {
        return origin.Select(value => (value, index(value)));
    }
    public static IEnumerable<(E value, F index)> SelectWithIndex<T, E, F>(this IEnumerable<T> origin, Func<T, E> value, Func<T, F> index)
    {
        return origin.Select(item => (value(item), index(item)));
    }
}
