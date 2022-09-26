using System.ComponentModel.DataAnnotations;
using System.Reflection;

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

    public PropertyInfo KeyProp()
    {
        var prop = GetType().AttrValues<KeyAttribute>().First().Key;
        return prop;
    }

    public Type KeyType()
    {
        return KeyProp().PropertyType;
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
    [Key]
    public TKey Id { get; set; }
}
