namespace Collapsenav.Net.Tool.Excel
{
    [System.AttributeUsage(System.AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class ExcelExportAttribute : System.Attribute
    {
        readonly string excelField;
        public ExcelExportAttribute() { }
        public ExcelExportAttribute(string excelField)
        {
            this.excelField = excelField;
        }
        public string ExcelField { get => excelField; }
    }
}