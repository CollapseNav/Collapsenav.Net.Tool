using System.ComponentModel.DataAnnotations;
namespace Collapsenav.Net.Tool.Data;

public partial class BaseEntity : IBaseEntity
{
    public bool? IsDeleted { get; set; }
    public DateTime? CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public static Func<DateTime> GetNow = () => DateTime.Now;
}
public partial class BaseEntity<TKey> : BaseEntity, IBaseEntity<TKey>
{
    [Key]
    public TKey Id { get; set; }
    public TKey CreatorId { get; set; }
    public TKey LastModifierId { get; set; }
    public virtual void Init()
    {
        CreationTime = GetNow();
        LastModificationTime = GetNow();
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
        LastModificationTime = GetNow();
        InitModifyId();
    }

    public Type KeyType()
    {
        return typeof(TKey);
    }
}
