using System;
using System.Collections.Generic;
using System.Linq;

namespace Collapsenav.Net.Tool
{
    public static class CollectionExt
    {

        /// <summary>
        /// query.contains( filters[0] ) and query.contains( filters[1] ) ...
        /// </summary>
        public static bool ContainAnd<T>(this IEnumerable<T> query, params T[] filters)
        {
            return CollectionTool.ContainAnd(query, filters);
        }
        /// <summary>
        /// query.contains( filters[0] ) and query.contains( filters[1] ) ...
        /// </summary>
        public static bool ContainAnd<T>(this IEnumerable<T> query, Func<T, T, bool> equalFunc, params T[] filters)
        {
            return CollectionTool.ContainAnd(query, equalFunc, filters);
        }

        /// <summary>
        /// query.contains( filters[0] ) or query.contains( filters[1] ) ...
        /// </summary>
        public static bool ContainOr<T>(this IEnumerable<T> query, params T[] filters)
        {
            return CollectionTool.ContainOr(query, filters);
        }
        /// <summary>
        /// query.contains( filters[0] ) or query.contains( filters[1] ) ...
        /// </summary>
        public static bool ContainOr<T>(this IEnumerable<T> query, Func<T, T, bool> equalFunc, params T[] filters)
        {
            return CollectionTool.ContainOr(query, equalFunc, filters);
        }

        public static IEnumerable<IEnumerable<T>> SpliteCollection<T>(this IEnumerable<T> query, int size)
        {
            return CollectionTool.SpliteCollection(query, size);
        }

        /// <summary>
        /// 去重
        /// </summary>
        public static IEnumerable<T> Unique<T, E>(this IEnumerable<T> query, Func<T, E> filter)
        {
            return CollectionTool.Unique(query, filter);
        }

        /// <summary>
        /// 去重
        /// </summary>
        public static IEnumerable<T> Unique<T>(this IEnumerable<T> query, Func<T, T, bool> comparer)
        {
            return CollectionTool.Unique(query, comparer);
        }

        /// <summary>
        /// 去重
        /// </summary>
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
        public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>> querys, Func<T, T, bool> comparer = null, Func<T, int> hashCodeFunc = null)
        {
            return comparer == null ? CollectionTool.Merge(querys) : CollectionTool.Merge(querys, comparer, hashCodeFunc);
        }

        /// <summary>
        /// 合并集合
        /// </summary>
        public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>> querys, IEnumerable<T> query, Func<T, T, bool> comparer = null, Func<T, int> hashCodeFunc = null)
        {
            querys = querys.Append(query);
            return comparer == null ? CollectionTool.Merge(querys) : CollectionTool.Merge(querys, comparer, hashCodeFunc);
        }

        /// <summary>
        /// 合并集合
        /// </summary>
        public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>> querys, IEnumerable<IEnumerable<T>> concatQuerys, Func<T, T, bool> comparer = null, Func<T, int> hashCodeFunc = null)
        {
            querys = querys.Concat(concatQuerys);
            return comparer == null ? CollectionTool.Merge(querys) : CollectionTool.Merge(querys, comparer, hashCodeFunc);
        }


        /// <summary>
        /// 合并集合
        /// </summary>
        public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>> querys, bool unique)
        {
            return unique ? CollectionTool.Merge(querys, unique) : CollectionTool.Merge(querys);
        }

        /// <summary>
        /// 合并集合
        /// </summary>
        public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>> querys, IEnumerable<T> query, bool unique)
        {
            querys = querys.Append(query);
            return unique ? CollectionTool.Merge(querys, unique) : CollectionTool.Merge(querys);
        }

        /// <summary>
        /// 合并集合
        /// </summary>
        public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>> querys, IEnumerable<IEnumerable<T>> concatQuerys, bool unique)
        {
            querys = querys.Concat(concatQuerys);
            return unique ? CollectionTool.Merge(querys, unique) : CollectionTool.Merge(querys);
        }

        /// <summary>
        /// 集合为空
        /// </summary>
        public static bool IsEmpty<T>(this IEnumerable<T> query)
        {
            return CollectionTool.IsEmpty(query);
        }

        /// <summary>
        /// 集合不空
        /// </summary>
        public static bool NotEmpty<T>(this IEnumerable<T> query)
        {
            return CollectionTool.NotEmpty(query);
        }
    }
}
