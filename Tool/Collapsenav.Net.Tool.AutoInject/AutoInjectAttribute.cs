namespace Collapsenav.Net.Tool.AutoInject;

[System.AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class AutoInjectAttribute : System.Attribute
{
    public AutoInjectAttribute() { }
}