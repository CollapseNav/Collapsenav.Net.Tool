using System.Collections.Generic;
using System.Linq;

namespace Collapsenav.Net.Tool
{
    public static class CollectionExt
    {
        /// <summary>
        /// query.contains( filters[0] ) and query.contains( filters[1] ) ...
        /// </summary>
        public static bool ContainAnd<T>(this IEnumerable<T> query, IEnumerable<T> filters)
        {
            foreach (var filter in filters)
                if (!query.Contains(filter))
                    return false;
            return true;
        }

        /// <summary>
        /// query.contains( filters[0] ) or query.contains( filters[1] ) ...
        /// </summary>
        public static bool ContainOr<T>(this IEnumerable<T> query, IEnumerable<T> filters)
        {
            foreach (var filter in filters)
                if (query.Contains(filter))
                    return true;
            return false;
        }
        public static IEnumerable<IEnumerable<T>> SpliteCollection<T>(this IEnumerable<T> query, int size)
        {
            for (int i = 0; i <= query.Count() / 4; i++)
                yield return query.Skip(i * size).Take(size);
        }
    }
}
