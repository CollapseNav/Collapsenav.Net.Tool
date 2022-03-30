using System.ComponentModel.DataAnnotations;

namespace Collapsenav.Net.Tool.Data;
public interface IEntity
{
    void Init();
    void InitModifyId();
    void SoftDelete();
    void Update();
    Type KeyType();

#if NET6_0_OR_GREATER
    public static Type GetKeyType<T>()
    {
        var prop = typeof(T).AttrValues<KeyAttribute>().First().Key;
        return prop.PropertyType;
    }
#endif
}
public interface IEntity<TKey> : IEntity
{
}