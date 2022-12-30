using FreeRedis;

namespace Collapsenav.Net.Tool.Ext;

public sealed class FreeRedisCache : IRedisCache
{
    private readonly IRedisClient _client;
    public FreeRedisCache(IRedisClient client)
    {
        _client = client;
    }
    public string BuildCacheKey(string key)
    {
        return $"key_{GetType().Name}_{key}";
    }

    public string BuildHashKey(string key)
    {
        return $"hash_key_{GetType().Name}_{key}";
    }
    public string GetCache(string key)
    {
        return key.IsEmpty() ? null : _client.Get(BuildCacheKey(key));
    }
    public T GetCache<T>(string key)
    {
        return key.IsEmpty() ? default : _client.Get<T>(BuildCacheKey(key));
    }
    public IEnumerable<string> GetKeys(string pattern, long count = 10)
    {
        return _client.Scan(pattern, count, null).Merge().Unique();
    }
    public void RefreshCache(string key)
    {
        _client.Expire(BuildCacheKey(key), (int)_client.Ttl(BuildCacheKey(key)));
    }
    public void RemoveCache(string key)
    {
        _client.Del(BuildCacheKey(key));
    }
    public void SetCache(string key, object value)
    {
        _client.Set(BuildCacheKey(key), value);
    }
    public void SetCache(string key, object value, TimeSpan timeout)
    {
        _client.Set(BuildCacheKey(key), value, timeout);
    }

    public long SetHash(string key, string field, object value)
    {
        return _client.HSet(BuildHashKey(key), BuildCacheKey(field), value);
    }
    public string GetHash(string key, string field)
    {
        return _client.HGet(BuildHashKey(key), BuildCacheKey(field));
    }

}