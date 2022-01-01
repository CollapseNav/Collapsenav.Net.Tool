namespace Collapsenav.Net.Tool.Excel;
[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class ExcelExportAttribute : Attribute
{
    readonly string excelField;
    public ExcelExportAttribute() { }
    public ExcelExportAttribute(string excelField)
    {
        this.excelField = excelField;
    }
    public string ExcelField { get => excelField; }
}