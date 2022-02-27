using System.ComponentModel.DataAnnotations;

namespace Collapsenav.Net.Tool.Data;
public partial class BaseEntity<TKey> : IBaseEntity<TKey>
{
    [Key]
    public TKey Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? CreationTime { get; set; }
    public TKey CreatorId { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public TKey LastModifierId { get; set; }
    public virtual void Init()
    {
        CreationTime = DateTime.Now;
        LastModificationTime = DateTime.Now;
        InitModifyId();
    }
    public virtual void InitModifyId()
    {
    }
    public virtual void SoftDelete()
    {
        IsDeleted = true;
        InitModifyId();
    }
    public virtual void Update()
    {
        LastModificationTime = DateTime.Now;
        InitModifyId();
    }

    public virtual IBaseEntity<TKey> Entity()
    {
        if (IsDeleted) return null;
        return this;
    }

    public Type KeyType()
    {
        return typeof(TKey);
    }
}
