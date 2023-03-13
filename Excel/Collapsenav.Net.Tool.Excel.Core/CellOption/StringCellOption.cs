namespace Collapsenav.Net.Tool.Excel;

public class StringCellOption
{
    public string FieldName { get; set; }
    public string PropName { get; set; }
    public string Func { get; set; }
    public StringCellOption() { }

    public StringCellOption(string fieldName, string propName, string func)
    {
        FieldName = fieldName;
        PropName = propName;
        Func = func;
    }
}

public class StringExcelOption
{
    public string Name { get; set; }
    public IEnumerable<StringCellOption> Options { get; set; }
}