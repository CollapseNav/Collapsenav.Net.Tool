using System.ComponentModel.DataAnnotations;
namespace Collapsenav.Net.Tool.Data;
public partial class Entity : IEntity
{
    public virtual void Init()
    {
        InitModifyId();
    }
    public virtual void InitModifyId()
    {
    }

    public Type KeyType()
    {
        var prop = GetType().AttrValues<KeyAttribute>().First().Key;
        return prop.PropertyType;
    }

    public virtual void SoftDelete()
    {
        InitModifyId();
    }
    public virtual void Update()
    {
        InitModifyId();
    }
}
public partial class Entity<TKey> : Entity, IEntity<TKey>
{
}
