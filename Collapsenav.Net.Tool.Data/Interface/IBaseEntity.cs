using System;

namespace Collapsenav.Net.Tool.Data
{
    public interface IBaseEntity<TKey>
    {
        TKey Id { get; set; }
        TKey CreatorId { get; set; }
        TKey LastModifierId { get; set; }
        bool IsDeleted { get; set; }
        DateTime? CreationTime { get; set; }
        DateTime? LastModificationTime { get; set; }
        void Init();
        void InitModifyId();
        void SoftDelete();
        void Update();
        IBaseEntity<TKey> Entity();
    }
}
