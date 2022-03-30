using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;

namespace Collapsenav.Net.Tool.Ids;
public class IdentityResourceDto : IDSDto<IdentityResource, IdentityServer4.EntityFramework.Entities.IdentityResource>
{
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
    public bool? Required { get; set; }
    public IEnumerable<string> UserClaims { get; set; }
    public IdentityResource ToItem()
    {
        return new IdentityResource
        {
            Name = Name,
            DisplayName = DisplayName,
            Description = Description,
            UserClaims = UserClaims.ToList(),
            Required = Required ?? false
        };
    }

    public IdentityServer4.EntityFramework.Entities.IdentityResource ToEntity()
    {
        return ToItem().ToEntity();
    }
}