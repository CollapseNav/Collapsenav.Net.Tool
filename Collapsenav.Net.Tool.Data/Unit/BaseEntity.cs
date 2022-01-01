using System.ComponentModel.DataAnnotations;

namespace Collapsenav.Net.Tool.Data;
public partial class BaseEntity<T> : IBaseEntity<T>
{
    [Key]
    public T Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? CreationTime { get; set; }
    public T CreatorId { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public T LastModifierId { get; set; }
    object IBaseEntity.Id { get => Id; set => Console.WriteLine(""); }
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

    public virtual IBaseEntity<T> Entity()
    {
        if (IsDeleted) return null;
        return this;
    }
}
