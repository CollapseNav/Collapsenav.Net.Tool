using Microsoft.Extensions.Caching.Distributed;

namespace Collapsenav.Net.Tool.Ext;

public class DefaultCache : ICache, ICacheAsync
{
    protected readonly IDistributedCache _cache;

    public DefaultCache(IDistributedCache cache)
    {
        _cache = cache;
    }

    public string BuildCacheKey(string key)
    {
        return $"key_{GetType().Name}_{key}";
    }
    public string GetCache(string key)
    {
        return key.IsEmpty() ? null : _cache.GetString(BuildCacheKey(key));
    }

    public T GetCache<T>(string key)
    {
        return key.IsEmpty() ? default : _cache.GetString(BuildCacheKey(key)).ToObj<T>();
    }

    public async Task<string> GetCacheAsync(string key)
    {
        return key.IsEmpty() ? null : await _cache.GetStringAsync(BuildCacheKey(key));
    }

    public async Task<T> GetCacheAsync<T>(string key)
    {
        return key.IsEmpty() ? default : (await _cache.GetStringAsync(BuildCacheKey(key))).ToObj<T>();
    }

    public void RefreshCache(string key)
    {
        _cache.Refresh(BuildCacheKey(key));
    }

    public async Task RefreshCacheAsync(string key)
    {
        await _cache.RefreshAsync(BuildCacheKey(key));
    }

    public void RemoveCache(string key)
    {
        _cache.Remove(BuildCacheKey(key));
    }

    public async Task RemoveCacheAsync(string key)
    {
        await _cache.RemoveAsync(BuildCacheKey(key));
    }

    public void SetCache(string key, object value)
    {
        _cache.SetString(BuildCacheKey(key), value.ToJson());
    }

    public void SetCache(string key, object value, TimeSpan timeout)
    {
        _cache.SetString(BuildCacheKey(key), value.ToJson(), new DistributedCacheEntryOptions { SlidingExpiration = timeout });
    }

    public async Task SetCacheAsync(string key, object value)
    {
        await _cache.SetStringAsync(BuildCacheKey(key), value.ToJson());
    }

    public async Task SetCacheAsync(string key, object value, TimeSpan timeout)
    {
        await _cache.SetStringAsync(BuildCacheKey(key), value.ToJson(), new DistributedCacheEntryOptions { SlidingExpiration = timeout });
    }
}