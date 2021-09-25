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
            foreach (var filter in filters)
                if (!query.Contains(filter))
                    return false;
            return true;
        }

        /// <summary>
        /// query.contains( filters[0] ) or query.contains( filters[1] ) ...
        /// </summary>
        public static bool ContainOr<T>(this IEnumerable<T> query, params T[] filters)
        {
            foreach (var filter in filters)
                if (query.Contains(filter))
                    return true;
            return false;
        }

        public static IEnumerable<IEnumerable<T>> SpliteCollection<T>(this IEnumerable<T> query, int size)
        {
            for (int i = 0; i < (query.Count() / size) + (query.Count() % size == 0 ? 0 : 1); i++)
                yield return query.Skip(i * size).Take(size);
        }
        /// <summary>
        /// 去重
        /// </summary>
        public static IEnumerable<T> Unique<T, E>(this IEnumerable<T> query, Func<T, E> filter)
        {
            return query.GroupBy(filter).Select(item => item.First()).ToList();
        }
        /// <summary>
        /// 去重
        /// </summary>
        public static IEnumerable<T> Unique<T>(this IEnumerable<T> query)
        {
            return CollectionTool.Unique(query);
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
            query = query.Unique();
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
        public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>> querys)
        {
            return CollectionTool.Merge(querys);
        }
        /// <summary>
        /// 合并集合
        /// </summary>
        public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>> querys, IEnumerable<T> query)
        {
            querys = querys.Append(query);
            return CollectionTool.Merge(querys);
        }
        /// <summary>
        /// 合并集合
        /// </summary>
        public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>> querys, IEnumerable<IEnumerable<T>> concatQuerys)
        {
            querys = querys.Concat(concatQuerys);
            return CollectionTool.Merge(querys);
        }
    }
}
