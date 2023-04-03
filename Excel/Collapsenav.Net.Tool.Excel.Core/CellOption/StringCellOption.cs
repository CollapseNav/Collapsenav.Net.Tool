namespace Collapsenav.Net.Tool.Excel;

/// <summary>
/// 纯字符串的单元格配置
/// </summary>
public class StringCellOption
{
    /// <summary>
    /// 表头名称
    /// </summary>
    public string FieldName { get; set; }
    /// <summary>
    /// 属性名称
    /// </summary>
    public string PropName { get; set; }
    /// <summary>
    /// 表达式
    /// </summary>
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
    /// <summary>
    /// 配置名称
    /// </summary>
    public string Name { get; set; }
    public IEnumerable<StringCellOption> Options { get; set; }
}