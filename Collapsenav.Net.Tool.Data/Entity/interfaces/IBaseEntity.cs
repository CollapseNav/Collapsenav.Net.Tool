namespace Collapsenav.Net.Tool.Data;
public interface IBaseEntity : IBaseEntity<object>
{
}
public interface IBaseEntity<TKey>
{
    TKey Id { get; set; }
    bool IsDeleted { get; set; }
    DateTime? CreationTime { get; set; }
    DateTime? LastModificationTime { get; set; }
    TKey CreatorId { get; set; }
    TKey LastModifierId { get; set; }
    void Init();
    void InitModifyId();
    void SoftDelete();
    void Update();
    IBaseEntity<TKey> Entity();
}
