using System;

namespace Collapsenav.Net.Tool.Data
{
    public interface IBaseEntity
    {
        object Id { get; set; }
        bool IsDeleted { get; set; }
        DateTime? CreationTime { get; set; }
        DateTime? LastModificationTime { get; set; }
        void Init();
        void InitModifyId();
        void SoftDelete();
        void Update();
    }
    public interface IBaseEntity<TKey> : IBaseEntity
    {
        new TKey Id { get; set; }
        TKey CreatorId { get; set; }
        TKey LastModifierId { get; set; }
        IBaseEntity<TKey> Entity();
    }
}
