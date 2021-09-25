using System;
using System.Collections.Generic;
using System.Linq;

namespace Collapsenav.Net.Tool
{
    public class CollectionTool
    {
        /*
            暂时还不确定用hashset是否会有性能问题
            TODO 需要后续额外的性能测试
        */
        public static IEnumerable<T> Unique<T>(IEnumerable<T> query)
        {
            HashSet<T> uniqueSet = new();
            foreach (var item in query)
                uniqueSet.Add(item);
            return uniqueSet;
        }
        public static IEnumerable<T> Unique<T, E>(IEnumerable<T> query, Func<T, E> groupBy)
        {
            return query.GroupBy(groupBy).Select(item => item.First());
        }

        public static IEnumerable<T> Merge<T>(IEnumerable<IEnumerable<T>> querys)
        {
            if (querys == null)
                return null;
            var result = querys.FirstOrDefault();
            foreach (var query in querys.Skip(1))
                result = result.Concat(query);
            return result;
        }
    }
}
