namespace Collapsenav.Net.Tool.Excel;
[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class ExcelReadAttribute : ExcelAttribute
{
    public ExcelReadAttribute(string excelField) : base(excelField)
    {
    }
}