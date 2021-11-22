namespace Collapsenav.Net.Tool.Excel
{
    [System.AttributeUsage(System.AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class ExcelReadAttribute : System.Attribute
    {
        readonly string excelField;
        public ExcelReadAttribute() { }
        public ExcelReadAttribute(string excelField)
        {
            this.excelField = excelField;
        }
        public string ExcelField { get => excelField; }
    }
}