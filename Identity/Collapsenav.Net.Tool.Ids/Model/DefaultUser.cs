using System.ComponentModel.DataAnnotations.Schema;
using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Identity;

namespace Collapsenav.Net.Tool.Ids;

public class DefaultUser<TKey> : IdentityUser<TKey>, IBaseEntity<TKey> where TKey : IEquatable<TKey>
{
    public new TKey? Id { get; set; }
    [NotMapped]
    public string Role { get; set; }

    [NotMapped]
    public bool? IsFrozen
    {
        get => LockoutEnd.HasValue && LockoutEnd.Value.CompareTo(DateTime.UtcNow) > 0;
    }
    /// <summary>
    /// 最近一次登录的时间
    /// </summary>
    public DateTime? LastLoginTime { get; set; }
    /// <summary>
    /// 是否删除
    /// </summary>
    public bool? IsDeleted { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreationTime { get; set; }
    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime? LastModificationTime { get; set; }
    /// <summary>
    /// 创建者Id
    /// </summary>
    public TKey? CreatorId { get; set; }
    /// <summary>
    /// 最后修改者Id
    /// </summary>
    public TKey? LastModifierId { get; set; }
    public static Func<TKey> GetKey = null;
    /// <summary>
    /// 获取当前时间
    /// </summary>
    public static Func<DateTime> GetNow = () => DateTime.Now;

    public void Init()
    {
        if (GetKey != null)
            Id = GetKey();
        CreationTime = GetNow();
        LastModificationTime = GetNow();
        IsDeleted = false;
        InitModifyId();
    }

    public void InitModifyId()
    {
    }

    public Type KeyType()
    {
        return typeof(TKey);
    }

    public void SoftDelete()
    {
        IsDeleted = true;
        InitModifyId();
    }

    public void Update()
    {
        LastModificationTime = GetNow();
        InitModifyId();
    }
}