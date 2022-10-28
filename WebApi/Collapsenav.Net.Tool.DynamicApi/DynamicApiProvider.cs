using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Collapsenav.Net.Tool.DynamicApi;

public class DynamicApiProvider : ControllerFeatureProvider
{
    protected override bool IsController(TypeInfo typeInfo)
    {
        var type = typeInfo.AsType();
        if (type.IsDynamicApi())
            return true;
        return base.IsController(typeInfo);
    }
}