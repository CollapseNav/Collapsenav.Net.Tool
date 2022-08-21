namespace Collapsenav.Net.Tool;
public static partial class DictionaryExt
{
    /// <summary>
    /// 添加或更新
    /// </summary>
    /// <param name="dict">字典</param>
    /// <param name="item">键值对</param>
    public static IDictionary<K, V> AddOrUpdate<K, V>(this IDictionary<K, V> dict, KeyValuePair<K, V> item)
    {
        if (dict.ContainsKey(item.Key))
            dict[item.Key] = item.Value;
        else
            dict.Add(item.Key, item.Value);
        return dict;
    }
    /// <summary>
    /// 添加或更新
    /// </summary>
    /// <param name="dict">字典</param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static IDictionary<K, V> AddOrUpdate<K, V>(this IDictionary<K, V> dict, K key, V value)
    {
        if (dict.ContainsKey(key))
            dict[key] = value;
        else
            dict.Add(key, value);
        return dict;
    }
    /// <summary>
    /// 添加多个字典项
    /// </summary>
    /// <param name="dict">字典</param>
    /// <param name="values">值</param>
    public static IDictionary<K, V> AddRange<K, V>(this IDictionary<K, V> dict, IDictionary<K, V> values)
    {
        foreach (var item in values)
            dict.AddOrUpdate(item);
        return dict;
    }
    /// <summary>
    /// 添加多个字典项
    /// </summary>
    /// <param name="dict">字典</param>
    /// <param name="values">值</param>
    public static IDictionary<K, V> AddRange<K, V>(this IDictionary<K, V> dict, IEnumerable<KeyValuePair<K, V>> values)
    {
        foreach (var item in values)
            dict.AddOrUpdate(item);
        return dict;
    }
    /// <summary>
    /// 将键值对集合转为字典
    /// </summary>
    /// <param name="dict"></param>
    public static IDictionary<K, V> ToDictionary<K, V>(this IEnumerable<KeyValuePair<K, V>> dict)
    {
        return dict.ToDictionary(item => item.Key, item => item.Value);
    }
    /// <summary>
    /// 将集合转为键值对
    /// </summary>
    /// <param name="query">集合</param>
    /// <param name="keySelector">key选择器</param>
    public static IDictionary<K, V> ToDictionary<K, V>(this IEnumerable<V> query, Func<V, K> keySelector)
    {
        return query.ToDictionary(keySelector, item => item);
    }
    /// <summary>
    /// 获取值并且移除字典项
    /// </summary>
    /// <param name="dict"></param>
    /// <param name="key"></param>
    public static V GetAndRemove<K, V>(this IDictionary<K, V> dict, K key)
    {
        var flag = dict.TryGetValue(key, out V value);
        if (flag)
            dict.Remove(key);
        return value;
    }
    
    /// <summary>
    /// 获取值并且移除字典项
    /// </summary>
    /// <param name="dict"></param>
    /// <param name="key"></param>
    public static V Pop<K, V>(this IDictionary<K, V> dict, K key)
    {
        var flag = dict.TryGetValue(key, out V value);
        if (flag)
            dict.Remove(key);
        return value;
    }

    /// <summary>
    /// 字典解构
    /// </summary>
    /// <typeparam name="K">Key 作为 index</typeparam>
    /// <typeparam name="V">Value 作为 value</typeparam>
    public static IEnumerable<(V value, K index)> Deconstruct<K, V>(this IDictionary<K, V> dict)
    {
        foreach (var item in dict)
            yield return (item.Value, item.Key);
    }

    /// <summary>
    /// 字典解构
    /// </summary>
    /// <typeparam name="K">Key 作为 index</typeparam>
    /// <typeparam name="V">Value 作为 value</typeparam>
    /// <typeparam name="E">value</typeparam>
    /// <typeparam name="F">index</typeparam>
    public static IEnumerable<(E value, F index)> Deconstruct<K, V, E, F>(
        this IDictionary<K, V> dict,
        Func<V, E> value,
        Func<K, F> index)
    {
        foreach (var item in dict)
            yield return (value(item.Value), index(item.Key));
    }
}