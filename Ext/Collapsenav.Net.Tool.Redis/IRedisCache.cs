namespace Collapsenav.Net.Tool.Ext;

public interface IRedisCache : ICache
{
    string BuildHashKey(string key);
    IEnumerable<string> GetKeys(string pattern, long count = 10);
    long SetHash(string key, string field, object value);
    string GetHash(string key, string field);
    T GetHash<T>(string key, string field);
    Dictionary<string, string> GetHashAll(string key);
    Dictionary<string, T> GetHashAll<T>(string key);
    long SetList(string key, params object[] value);
    string GetList(string key);
    T GetList<T>(string key);
    string[] GetListAll(string key);
    T[] GetListAll<T>(string key);

    long SetAdd(string key, params object[] value);
    string[] GetSet(string key);
    T[] GetSet<T>(string key);
}