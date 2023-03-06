namespace Collapsenav.Net.Tool.Excel;
[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class ExcelExportAttribute : ExcelAttribute
{
    public ExcelExportAttribute() { }
    public ExcelExportAttribute(string excelField) : base(excelField)
    {
    }
}