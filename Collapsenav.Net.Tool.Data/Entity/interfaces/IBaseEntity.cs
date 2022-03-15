namespace Collapsenav.Net.Tool.Data;
#nullable enable
public interface IBaseEntity
{
    bool? IsDeleted { get; set; }
    DateTime? CreationTime { get; set; }
    DateTime? LastModificationTime { get; set; }
}
public interface IBaseEntity<TKey> : IBaseEntity, IEntity<TKey>
{
    TKey? Id { get; set; }
    TKey? CreatorId { get; set; }
    TKey? LastModifierId { get; set; }
}
