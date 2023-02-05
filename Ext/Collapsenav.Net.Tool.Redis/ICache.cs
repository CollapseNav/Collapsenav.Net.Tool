namespace Collapsenav.Net.Tool.Ext;

public interface ICacheAsync
{
    /// <summary>
    /// 设置缓存
    /// </summary>
    Task SetCacheAsync(string key, object value);
    /// <summary>
    /// 设置缓存
    /// </summary>
    Task SetCacheAsync(string key, object value, TimeSpan timeout);
    /// <summary>
    /// 获取缓存
    /// </summary>
    Task<string> GetCacheAsync(string key);
    /// <summary>
    /// 获取缓存
    /// </summary>
    Task<T> GetCacheAsync<T>(string key);
    /// <summary>
    /// 清除缓存
    /// </summary>
    Task RemoveCacheAsync(string key);
    /// <summary>
    /// 刷新缓存
    /// </summary>
    Task RefreshCacheAsync(string key);
}
public interface ICache
{
    string BuildCacheKey(string key);
    /// <summary>
    /// 设置缓存
    /// </summary>
    void SetCache(string key, object value);
    /// <summary>
    /// 设置缓存
    /// </summary>
    void SetCache(string key, object value, TimeSpan timeout);
    /// <summary>
    /// 获取缓存
    /// </summary>
    string GetCache(string key);
    /// <summary>
    /// 获取缓存
    /// </summary>
    T GetCache<T>(string key);
    /// <summary>
    /// 清除缓存
    /// </summary>
    void RemoveCache(string key);
    /// <summary>
    /// 刷新缓存
    /// </summary>
    void RefreshCache(string key);
}