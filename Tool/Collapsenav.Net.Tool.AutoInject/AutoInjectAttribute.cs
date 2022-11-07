namespace Collapsenav.Net.Tool.AutoInject;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class AutoInjectAttribute : Attribute
{
    public AutoInjectAttribute() { }
}