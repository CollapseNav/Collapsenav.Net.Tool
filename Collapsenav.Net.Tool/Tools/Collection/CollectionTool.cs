using System;
using System.Collections.Generic;
using System.Linq;

namespace Collapsenav.Net.Tool
{
    public partial class CollectionTool
    {
        public static IEnumerable<T> Unique<T, E>(IEnumerable<T> query, Func<T, E> groupBy)
        {
            return query.GroupBy(groupBy).Select(item => item.First());
        }
        public static IEnumerable<T> Unique<T>(IEnumerable<T> query, Func<T, T, bool> comparer, Func<T, int> hashCodeFunc = null)
        {
            return query.Distinct(new QueryEqualityComparer<T>(comparer, hashCodeFunc));
        }
        public static IEnumerable<T> Unique<T>(IEnumerable<T> query, Func<T, int> hashCodeFunc)
        {
            return query.Distinct(new QueryEqualityComparer<T>(hashCodeFunc));
        }
        public static IEnumerable<T> Unique<T>(IEnumerable<T> query)
        {
            return query.Distinct();
        }


        public static bool SequenceEqual<T>(IEnumerable<T> left, IEnumerable<T> right, Func<T, T, bool> comparer, Func<T, int> hashCodeFunc = null)
        {
            return left.SequenceEqual(right, new QueryEqualityComparer<T>(comparer, hashCodeFunc));
        }
        public static bool SequenceEqual<T>(IEnumerable<T> left, IEnumerable<T> right, Func<T, int> hashCodeFunc)
        {
            return left.SequenceEqual(right, new QueryEqualityComparer<T>(hashCodeFunc));
        }


        public static IEnumerable<T> Merge<T>(IEnumerable<IEnumerable<T>> querys, bool unique = false)
        {
            if (querys.IsEmpty())
                return null;
            var result = querys.FirstOrDefault();
            foreach (var query in querys.Skip(1))
                result = result.Concat(query);
            return unique ? Unique(result) : result;
        }
        public static IEnumerable<T> Merge<T>(IEnumerable<IEnumerable<T>> querys, Func<T, T, bool> comparer, Func<T, int> hashCodeFunc = null)
        {
            if (querys.IsEmpty())
                return null;
            var result = querys.FirstOrDefault();
            foreach (var query in querys.Skip(1))
                result = result.Union(query, new QueryEqualityComparer<T>(comparer, hashCodeFunc));
            return result;
        }


        /// <summary>
        /// query.contains( filters[0] ) and query.contains( filters[1] ) ...
        /// </summary>
        public static bool ContainAnd<T>(IEnumerable<T> query, params T[] filters)
        {
            foreach (var filter in filters)
                if (!query.Contains(filter))
                    return false;
            return true;
        }
        /// <summary>
        /// query.contains( filters[0] ) and query.contains( filters[1] ) ...
        /// </summary>
        public static bool ContainAnd<T>(IEnumerable<T> query, Func<T, T, bool> equalFunc, params T[] filters)
        {
            var comparer = new QueryEqualityComparer<T>(equalFunc);
            foreach (var filter in filters)
                if (!query.Contains(filter, comparer))
                    return false;
            return true;
        }

        /// <summary>
        /// query.contains( filters[0] ) or query.contains( filters[1] ) ...
        /// </summary>
        public static bool ContainOr<T>(IEnumerable<T> query, params T[] filters)
        {
            foreach (var filter in filters)
                if (query.Contains(filter))
                    return true;
            return false;
        }

        /// <summary>
        /// query.contains( filters[0] ) or query.contains( filters[1] ) ...
        /// </summary>
        public static bool ContainOr<T>(IEnumerable<T> query, Func<T, T, bool> equalFunc, params T[] filters)
        {
            var comparer = new QueryEqualityComparer<T>(equalFunc);
            foreach (var filter in filters)
                if (query.Contains(filter, comparer))
                    return true;
            return false;
        }

        public static IEnumerable<IEnumerable<T>> SpliteCollection<T>(IEnumerable<T> query, int size)
        {
            for (int i = 0; i < (query.Count() / size) + (query.Count() % size == 0 ? 0 : 1); i++)
                yield return query.Skip(i * size).Take(size);
        }

        public static bool IsEmpty<T>(IEnumerable<T> query)
        {
            return query == null || !query.Any();
        }

        public static bool NotEmpty<T>(IEnumerable<T> query)
        {
            return query != null && query.Any();
        }
    }
}
