namespace Collapsenav.Net.Tool.Excel;
[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class ExcelReadAttribute : Attribute
{
    readonly string excelField;
    public ExcelReadAttribute() { }
    public ExcelReadAttribute(string excelField)
    {
        this.excelField = excelField;
    }
    public string ExcelField { get => excelField; }
}