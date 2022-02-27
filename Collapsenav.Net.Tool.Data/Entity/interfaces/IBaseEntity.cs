namespace Collapsenav.Net.Tool.Data;
public interface IBaseEntity<TKey> : IEntity<TKey>
{
    TKey Id { get; set; }
    bool IsDeleted { get; set; }
    DateTime? CreationTime { get; set; }
    DateTime? LastModificationTime { get; set; }
    TKey CreatorId { get; set; }
    TKey LastModifierId { get; set; }
    IBaseEntity<TKey> Entity();
}
