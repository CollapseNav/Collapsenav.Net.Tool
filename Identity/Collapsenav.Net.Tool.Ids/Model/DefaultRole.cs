using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Identity;

namespace Collapsenav.Net.Tool.Ids;

public class DefaultRoler<TKey> : IdentityRole<TKey>, IBaseEntity<TKey> where TKey : IEquatable<TKey>
{
    public string Sys { get; set; }
    public string RoleCode { get; set; }
    public bool? Enabled { get; set; }
    public bool? IsDeleted { get; set; }
    public TKey? CreatorId { get; set; }
    public TKey? LastModifierId { get; set; }
    public DateTime? CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }

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

    public Type KeyType()
    {
        return typeof(TKey);
    }
}