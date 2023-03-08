namespace Collapsenav.Net.Tool.Excel;
[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
public class ExcelAttribute : Attribute
{
    readonly string excelField;
    public ExcelAttribute() { }
    public ExcelAttribute(string excelField) : this()
    {
        this.excelField = excelField;
    }
    public string ExcelField { get => excelField; }
}