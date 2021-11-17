using System;
using System.Collections.Generic;
using System.Linq;

namespace Collapsenav.Net.Tool
{
    public static partial class CollectionExt
    {
        /// <summary>
        /// 全包含
        /// </summary>
        /// <param name="query">源集合</param>
        /// <param name="filters">条件</param>
        public static bool ContainAnd<T>(this IEnumerable<T> query, params T[] filters)
        {
            return CollectionTool.ContainAnd(query, filters);
        }
        /// <summary>
        /// 全包含
        /// </summary>
        /// <param name="query">源集合</param>
        /// <param name="comparer">怎么去重啊</param>
        /// <param name="filters">条件</param>
        public static bool ContainAnd<T>(this IEnumerable<T> query, Func<T, T, bool> comparer, params T[] filters)
        {
            return CollectionTool.ContainAnd(query, comparer, filters);
        }

        /// <summary>
        /// 部分包含
        /// </summary>
        /// <param name="query">源集合</param>
        /// <param name="filters">条件</param>
        public static bool ContainOr<T>(this IEnumerable<T> query, params T[] filters)
        {
            return CollectionTool.ContainOr(query, filters);
        }
        /// <summary>
        /// 部分包含
        /// </summary>
        /// <param name="query">源集合</param>
        /// <param name="comparer">怎么去重啊</param>
        /// <param name="filters">条件</param>
        public static bool ContainOr<T>(this IEnumerable<T> query, Func<T, T, bool> comparer, params T[] filters)
        {
            return CollectionTool.ContainOr(query, comparer, filters);
        }

        /// <summary>
        /// 分割集合
        /// </summary>
        /// <param name="query">源集合</param>
        /// <param name="size">每片大小</param>
        public static IEnumerable<IEnumerable<T>> SpliteCollection<T>(this IEnumerable<T> query, int size)
        {
            return CollectionTool.SpliteCollection(query, size);
        }

        /// <summary>
        /// 去重
        /// </summary>
        /// <param name="query">源集合</param>
        /// <param name="groupBy">参考属性/字段/...</param>
        public static IEnumerable<T> Unique<T, E>(this IEnumerable<T> query, Func<T, E> groupBy)
        {
            return CollectionTool.Unique(query, groupBy);
        }

        /// <summary>
        /// 去重
        /// </summary>
        /// <param name="query">源集合</param>
        /// <param name="comparer">怎么比啊</param>
        public static IEnumerable<T> Unique<T>(this IEnumerable<T> query, Func<T, T, bool> comparer)
        {
            return CollectionTool.Unique(query, comparer);
        }

        /// <summary>
        /// 去重
        /// </summary>
        /// <param name="query">源集合</param>
        public static IEnumerable<T> Unique<T>(this IEnumerable<T> query)
        {
            return CollectionTool.Unique(query);
        }

        public static bool SequenceEqual<T>(this IEnumerable<T> left, IEnumerable<T> right, Func<T, T, bool> comparer, Func<T, int> hashCodeFunc = null)
        {
            return CollectionTool.SequenceEqual(left, right, comparer, hashCodeFunc);
        }

        public static bool SequenceEqual<T>(this IEnumerable<T> left, IEnumerable<T> right, Func<T, int> hashCodeFunc)
        {
            return CollectionTool.SequenceEqual(left, right, hashCodeFunc);
        }

        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> query, bool flag, Func<T, bool> filter)
        {
            return flag ? query.Where(filter) : query;
        }

        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> query, string input, Func<T, bool> filter)
        {
            return input.IsNull() ? query : query.Where(filter);
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
        /// 合并集合
        /// </summary>
        /// <param name="querys">合并目标</param>
        /// <param name="comparer">怎么去重啊</param>
        /// <param name="hashCodeFunc">hash去重</param>
        public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>> querys, Func<T, T, bool> comparer = null, Func<T, int> hashCodeFunc = null)
        {
            return comparer == null ? CollectionTool.Merge(querys) : CollectionTool.Merge(querys, comparer, hashCodeFunc);
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
            return comparer == null ? CollectionTool.Merge(querys) : CollectionTool.Merge(querys, comparer, hashCodeFunc);
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
            return comparer == null ? CollectionTool.Merge(querys) : CollectionTool.Merge(querys, comparer, hashCodeFunc);
        }

        /// <summary>
        /// 合并集合
        /// </summary>
        /// <param name="querys">合并目标</param>
        /// <param name="unique">是否去重</param>
        public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>> querys, bool unique)
        {
            return unique ? CollectionTool.Merge(querys, unique) : CollectionTool.Merge(querys);
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
            return unique ? CollectionTool.Merge(querys, unique) : CollectionTool.Merge(querys);
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
            return unique ? CollectionTool.Merge(querys, unique) : CollectionTool.Merge(querys);
        }

        /// <summary>
        /// 空?
        /// </summary>
        /// <param name="query">源集合</param>
        public static bool IsEmpty<T>(this IEnumerable<T> query)
        {
            return CollectionTool.IsEmpty(query);
        }

        /// <summary>
        /// 没空?
        /// </summary>
        /// <param name="query">源集合</param>
        public static bool NotEmpty<T>(this IEnumerable<T> query)
        {
            return CollectionTool.NotEmpty(query);
        }
    }
}
