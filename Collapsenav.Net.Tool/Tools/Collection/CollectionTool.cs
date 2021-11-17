using System;
using System.Collections.Generic;
using System.Linq;

namespace Collapsenav.Net.Tool
{
    public partial class CollectionTool
    {
        /// <summary>
        /// 去重
        /// </summary>
        /// <param name="query">源集合</param>
        /// <param name="groupBy">参考属性/字段/...</param>
        public static IEnumerable<T> Unique<T, E>(IEnumerable<T> query, Func<T, E> groupBy)
        {
            return query.GroupBy(groupBy).Select(item => item.First());
        }
        /// <summary>
        /// 去重
        /// </summary>
        /// <param name="query">源集合</param>
        /// <param name="comparer">怎么比啊</param>
        /// <param name="hashCodeFunc">hash</param>
        public static IEnumerable<T> Unique<T>(IEnumerable<T> query, Func<T, T, bool> comparer, Func<T, int> hashCodeFunc = null)
        {
            return query.Distinct(new CollapseNavEqualityComparer<T>(comparer, hashCodeFunc));
        }
        /// <summary>
        /// 去重
        /// </summary>
        /// <param name="query">源集合</param>
        /// <param name="hashCodeFunc">hash</param>
        public static IEnumerable<T> Unique<T>(IEnumerable<T> query, Func<T, int> hashCodeFunc)
        {
            return query.Distinct(new CollapseNavEqualityComparer<T>(hashCodeFunc));
        }
        /// <summary>
        /// 去重(默认去重)
        /// </summary>
        /// <param name="query">源集合</param>
        public static IEnumerable<T> Unique<T>(IEnumerable<T> query)
        {
            return query.Distinct();
        }

        /// <summary>
        /// 判断两个集合是否相等
        /// </summary>
        /// <param name="left">集合1</param>
        /// <param name="right">集合2</param>
        /// <param name="comparer">怎么比啊</param>
        /// <param name="hashCodeFunc">hash</param>
        public static bool SequenceEqual<T>(IEnumerable<T> left, IEnumerable<T> right, Func<T, T, bool> comparer, Func<T, int> hashCodeFunc = null)
        {
            return left.SequenceEqual(right, new CollapseNavEqualityComparer<T>(comparer, hashCodeFunc));
        }
        /// <summary>
        /// 判断两个集合是否相等
        /// </summary>
        /// <param name="left">集合1</param>
        /// <param name="right">集合2</param>
        /// <param name="hashCodeFunc">hash</param>
        public static bool SequenceEqual<T>(IEnumerable<T> left, IEnumerable<T> right, Func<T, int> hashCodeFunc)
        {
            return left.SequenceEqual(right, new CollapseNavEqualityComparer<T>(hashCodeFunc));
        }
        /// <summary>
        /// 合并集合
        /// </summary>
        /// <param name="querys">合并目标</param>
        /// <param name="unique">是否去重</param>
        public static IEnumerable<T> Merge<T>(IEnumerable<IEnumerable<T>> querys, bool unique = false)
        {
            if (querys.IsEmpty())
                return null;
            var result = querys.FirstOrDefault();
            foreach (var query in querys.Skip(1))
                result = result.Concat(query);
            return unique ? Unique(result) : result;
        }
        /// <summary>
        /// 合并集合
        /// </summary>
        /// <param name="querys">合并目标</param>
        /// <param name="comparer">怎么去重啊</param>
        /// <param name="hashCodeFunc">hash去重</param>
        public static IEnumerable<T> Merge<T>(IEnumerable<IEnumerable<T>> querys, Func<T, T, bool> comparer, Func<T, int> hashCodeFunc = null)
        {
            if (querys.IsEmpty())
                return null;
            var result = querys.FirstOrDefault();
            foreach (var query in querys.Skip(1))
                result = result.Union(query, new CollapseNavEqualityComparer<T>(comparer, hashCodeFunc));
            return result;
        }

        /// <summary>
        /// 全包含
        /// </summary>
        /// <param name="query">源集合</param>
        /// <param name="filters">条件</param>
        public static bool ContainAnd<T>(IEnumerable<T> query, params T[] filters)
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
        /// <param name="comparer">怎么去重啊</param>
        /// <param name="filters">条件</param>
        public static bool ContainAnd<T>(IEnumerable<T> query, Func<T, T, bool> comparer, params T[] filters)
        {
            foreach (var filter in filters)
                if (!query.Contains(filter, new CollapseNavEqualityComparer<T>(comparer)))
                    return false;
            return true;
        }

        /// <summary>
        /// 部分包含
        /// </summary>
        /// <param name="query">源集合</param>
        /// <param name="filters">条件</param>
        public static bool ContainOr<T>(IEnumerable<T> query, params T[] filters)
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
        /// <param name="comparer">怎么去重啊</param>
        /// <param name="filters">条件</param>
        public static bool ContainOr<T>(IEnumerable<T> query, Func<T, T, bool> comparer, params T[] filters)
        {
            foreach (var filter in filters)
                if (query.Contains(filter, new CollapseNavEqualityComparer<T>(comparer)))
                    return true;
            return false;
        }

        /// <summary>
        /// 分割集合
        /// </summary>
        /// <param name="query">源集合</param>
        /// <param name="size">每片大小</param>
        public static IEnumerable<IEnumerable<T>> SpliteCollection<T>(IEnumerable<T> query, int size)
        {
            for (int i = 0; i < (query.Count() / size) + (query.Count() % size == 0 ? 0 : 1); i++)
                yield return query.Skip(i * size).Take(size);
        }

        /// <summary>
        /// 空?
        /// </summary>
        /// <param name="query">源集合</param>
        public static bool IsEmpty<T>(IEnumerable<T> query)
        {
            return query == null || !query.Any();
        }
        /// <summary>
        /// 没空?
        /// </summary>
        /// <param name="query">源集合</param>
        public static bool NotEmpty<T>(IEnumerable<T> query)
        {
            return query != null && query.Any();
        }
    }
}
