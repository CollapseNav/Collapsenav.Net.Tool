namespace Collapsenav.Net.Tool.WebApi;

/// <summary>
/// 使用 textjson 的序列化反序列化进行简单map, 无需做任何map配置
/// </summary>
public class JsonMap : IMap
{
    public JsonMap() { }
    public T Map<T>(object obj)
    {
        return obj.JsonMap<T>();
    }
}