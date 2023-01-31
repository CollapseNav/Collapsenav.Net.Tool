namespace Collapsenav.Net.Tool.DynamicApi;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class DynamicApiAttribute : Attribute
{
    public DynamicApiAttribute() { }
}