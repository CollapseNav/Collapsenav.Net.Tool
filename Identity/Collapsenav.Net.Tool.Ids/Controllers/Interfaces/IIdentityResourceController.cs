using Collapsenav.Net.Tool.WebApi;
using IdentityServer4.EntityFramework.Entities;

namespace Collapsenav.Net.Tool.Ids;
#if NETCOREAPP3_1_OR_GREATER
public interface IIdentityResourceController : INoConstraintsCrudController<int, IdentityResource, IdentityResourceDto, IdentityResourceGet>
{
}
#endif