using System;
using System.Collections.Generic;

namespace Collapsenav.Net.Tool
{
    public partial class CollectionExt
    {

        /// <summary>
        /// 向一个集合中添加多个对象
        /// </summary>
        /// <param name="query">源</param>
        /// <param name="values">添加的对象</param>
        public static IEnumerable<T> AddRange<T>(this IEnumerable<T> query, params T[] values)
        {
            return CollectionTool.AddRange(query, values);
        }
        /// <summary>
        /// 向一个集合中添加多个对象(带去重)
        /// </summary>
        /// <param name="query">源</param>
        /// <param name="comparer">去重依据</param>
        /// <param name="values">添加的对象</param>
        public static IEnumerable<T> AddRange<T>(this IEnumerable<T> query, Func<T, T, bool> comparer, params T[] values)
        {
            return CollectionTool.AddRange(query, comparer, values);
        }
        /// <summary>
        /// 向一个集合中添加多个对象(带去重)
        /// </summary>
        /// <param name="query">源</param>
        /// <param name="hashCodeFunc">去重依据(hash)</param>
        /// <param name="values">添加的对象</param>
        public static IEnumerable<T> AddRange<T>(this IEnumerable<T> query, Func<T, int> hashCodeFunc, params T[] values)
        {
            return CollectionTool.AddRange(query, hashCodeFunc, values);
        }
        /// <summary>
        /// 向一个集合中添加多个对象(带去重)
        /// </summary>
        /// <param name="query">源</param>
        /// <param name="comparer">去重依据</param>
        /// <param name="hashCodeFunc">去重依据(hash)</param>
        /// <param name="values">添加的对象</param>
        public static IEnumerable<T> AddRange<T>(this IEnumerable<T> query, Func<T, T, bool> comparer, Func<T, int> hashCodeFunc, params T[] values)
        {
            return CollectionTool.AddRange(query, comparer, hashCodeFunc, values);
        }

        /// <summary>
        /// 向一个集合中添加多个对象
        /// </summary>
        /// <param name="query">源</param>
        /// <param name="values">添加的对象</param>
        public static IEnumerable<T> AddRange<T>(this IEnumerable<T> query, IEnumerable<T> values)
        {
            return CollectionTool.AddRange(query, values);
        }
        /// <summary>
        /// 向一个集合中添加多个对象(带去重)
        /// </summary>
        /// <param name="query">源</param>
        /// <param name="comparer">去重依据</param>
        /// <param name="values">添加的对象</param>
        public static IEnumerable<T> AddRange<T>(this IEnumerable<T> query, Func<T, T, bool> comparer, IEnumerable<T> values)
        {
            return CollectionTool.AddRange(query, comparer, values);
        }
        /// <summary>
        /// 向一个集合中添加多个对象(带去重)
        /// </summary>
        /// <param name="query">源</param>
        /// <param name="hashCodeFunc">去重依据(hash)</param>
        /// <param name="values">添加的对象</param>
        public static IEnumerable<T> AddRange<T>(this IEnumerable<T> query, Func<T, int> hashCodeFunc, IEnumerable<T> values)
        {
            return CollectionTool.AddRange(query, hashCodeFunc, values);
        }
        /// <summary>
        /// 向一个集合中添加多个对象(带去重)
        /// </summary>
        /// <param name="query">源</param>
        /// <param name="comparer">去重依据</param>
        /// <param name="hashCodeFunc">去重依据(hash)</param>
        /// <param name="values">添加的对象</param>
        public static IEnumerable<T> AddRange<T>(this IEnumerable<T> query, Func<T, T, bool> comparer, Func<T, int> hashCodeFunc, IEnumerable<T> values)
        {
            return CollectionTool.AddRange(query, comparer, hashCodeFunc, values);
        }





        /// <summary>
        /// 向一个集合中添加多个对象
        /// </summary>
        /// <param name="query">源</param>
        /// <param name="values">添加的对象</param>
        public static void AddRange<T>(this ICollection<T> query, params T[] values)
        {
            CollectionTool.AddRange(query, values);
        }
        /// <summary>
        /// 向一个集合中添加多个对象(带去重)
        /// </summary>
        /// <param name="query">源</param>
        /// <param name="comparer">去重依据</param>
        /// <param name="values">添加的对象</param>
        public static void AddRange<T>(this ICollection<T> query, Func<T, T, bool> comparer, params T[] values)
        {
            CollectionTool.AddRange(query, comparer, values);
        }
        /// <summary>
        /// 向一个集合中添加多个对象(带去重)
        /// </summary>
        /// <param name="query">源</param>
        /// <param name="hashCodeFunc">去重依据(hash)</param>
        /// <param name="values">添加的对象</param>
        public static void AddRange<T>(this ICollection<T> query, Func<T, int> hashCodeFunc, params T[] values)
        {
            CollectionTool.AddRange(query, hashCodeFunc, values);
        }
        /// <summary>
        /// 向一个集合中添加多个对象(带去重)
        /// </summary>
        /// <param name="query">源</param>
        /// <param name="comparer">去重依据</param>
        /// <param name="hashCodeFunc">去重依据(hash)</param>
        /// <param name="values">添加的对象</param>
        public static void AddRange<T>(this ICollection<T> query, Func<T, T, bool> comparer, Func<T, int> hashCodeFunc, params T[] values)
        {
            CollectionTool.AddRange(query, comparer, hashCodeFunc, values);
        }

        /// <summary>
        /// 向一个集合中添加多个对象
        /// </summary>
        /// <param name="query">源</param>
        /// <param name="values">添加的对象</param>
        public static void AddRange<T>(this ICollection<T> query, IEnumerable<T> values)
        {
            CollectionTool.AddRange(query, values);
        }
        /// <summary>
        /// 向一个集合中添加多个对象(带去重)
        /// </summary>
        /// <param name="query">源</param>
        /// <param name="comparer">去重依据</param>
        /// <param name="values">添加的对象</param>
        public static void AddRange<T>(this ICollection<T> query, Func<T, T, bool> comparer, IEnumerable<T> values)
        {
            CollectionTool.AddRange(query, comparer, values);
        }
        /// <summary>
        /// 向一个集合中添加多个对象(带去重)
        /// </summary>
        /// <param name="query">源</param>
        /// <param name="hashCodeFunc">去重依据(hash)</param>
        /// <param name="values">添加的对象</param>
        public static void AddRange<T>(this ICollection<T> query, Func<T, int> hashCodeFunc, IEnumerable<T> values)
        {
            CollectionTool.AddRange(query, hashCodeFunc, values);
        }
        /// <summary>
        /// 向一个集合中添加多个对象(带去重)
        /// </summary>
        /// <param name="query">源</param>
        /// <param name="comparer">去重依据</param>
        /// <param name="hashCodeFunc">去重依据(hash)</param>
        /// <param name="values">添加的对象</param>
        public static void AddRange<T>(this ICollection<T> query, Func<T, T, bool> comparer, Func<T, int> hashCodeFunc, IEnumerable<T> values)
        {
            CollectionTool.AddRange(query, comparer, hashCodeFunc, values);
        }
    }
}