using System.Reflection;
#nullable enable
namespace Collapsenav.Net.Tool.Data;
public interface IEntity
{
    void Init();
    void InitModifyId();
    void SoftDelete();
    void Update();
    Type KeyType();
    PropertyInfo KeyProp();
}
public interface IEntity<TKey> : IEntity
{
    TKey? Id { get; set; }
}