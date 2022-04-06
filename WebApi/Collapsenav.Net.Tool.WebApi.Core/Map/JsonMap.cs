namespace Collapsenav.Net.Tool.WebApi;

public class JsonMap : IMap
{
    public JsonMap() { }
    public T Map<T>(object obj)
    {
        return obj.JsonMap<T>();
    }
}