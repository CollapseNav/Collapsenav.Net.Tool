using System.ComponentModel.DataAnnotations;
namespace Collapsenav.Net.Tool.Data;

public partial class BaseEntity : Entity
{
    public bool? IsDeleted { get; set; }
    public DateTime? CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
    /// <summary>
    /// 获取当前时间
    /// </summary>
    public static Func<DateTime> GetNow = () => DateTime.Now;
}
public partial class BaseEntity<TKey> : BaseEntity, IBaseEntity<TKey>
{
    [Key]
    public TKey Id { get; set; }
    public TKey CreatorId { get; set; }
    public TKey LastModifierId { get; set; }
    public new virtual void Init()
    {
        if (GetKey != null)
            Id = GetKey();
        CreationTime = GetNow();
        LastModificationTime = GetNow();
        IsDeleted = false;
        InitModifyId();
    }
    /// <summary>
    /// 获取主键值
    /// </summary>
    public static Func<TKey> GetKey = null;

    public new virtual void InitModifyId()
    {
    }
    public new virtual void SoftDelete()
    {
        IsDeleted = true;
        InitModifyId();
    }
    public new virtual void Update()
    {
        LastModificationTime = GetNow();
        InitModifyId();
    }

    public new Type KeyType()
    {
        return typeof(TKey);
    }
}
