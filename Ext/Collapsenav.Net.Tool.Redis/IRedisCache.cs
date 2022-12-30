namespace Collapsenav.Net.Tool.Ext;

public interface IRedisCache : ICache
{
    IEnumerable<string> GetKeys(string pattern, long count = 10);
    long SetHash(string key, string field, object value);
    string GetHash(string key, string field);
}