using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Collapsenav.Net.Tool.Ids;
public class User<TKey> : IdentityUser<TKey> where TKey : IEquatable<TKey>
{
    [NotMapped]
    public string Pwd { get; set; }
    /// <summary>
    /// 是否禁用
    /// </summary>
    public bool? IsDisable { get; set; }
    [NotMapped]
    public string Role { get; set; }

    [NotMapped]
    public bool? IsFrozen
    {
        get => LockoutEnd.HasValue && LockoutEnd.Value.CompareTo(DateTime.UtcNow) > 0;
        private set { }
    }
    /// <summary>
    /// 最近一次登录的时间
    /// </summary>
    public DateTime? LastLoginTime { get; set; }
    public bool? IsDeleted { get; set; }
    public DateTime? CreationTime { get; set; }
    public Guid? CreatorId { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public Guid? LastModifierId { get; set; }
    public void Init()
    {
        CreationTime = DateTime.Now;
        IsDeleted ??= false;
        IsDisable ??= false;
        LockoutEnabled = true;
    }
}