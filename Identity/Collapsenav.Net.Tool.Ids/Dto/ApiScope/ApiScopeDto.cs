using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;

namespace Collapsenav.Net.Tool.Ids;
#if NETCOREAPP3_1_OR_GREATER
public class ApiScopeDto : IDSDto<ApiScope, IdentityServer4.EntityFramework.Entities.ApiScope>
{
    public int? Id { get; set; }
    /// <summary>
    /// scope 名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 显示
    /// </summary>
    public string DisplayName { get; set; }
    /// <summary>
    /// 备注描述
    /// </summary>
    public string Description { get; set; }
    public ApiScope ToItem()
    {
        return new ApiScope
        {
            Name = Name,
            DisplayName = DisplayName,
            Description = Description
        };
    }

    public IdentityServer4.EntityFramework.Entities.ApiScope ToEntity()
    {
        return ToItem().ToEntity();
    }
}
#endif