namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// Excel 表格-单元格 读取设置
/// </summary>
public class ReadCellOption<T> : BaseCellOption<T>
{
    public ReadCellOption() { }
    public ReadCellOption(ICellOption<T> cellOption)
    {
        ExcelField = cellOption.ExcelField;
        PropName = cellOption.PropName;
    }
    /// <summary>
    /// 转换 表格 数据的方法
    /// </summary>
    public Func<string, object> Action
    {
        get
        {
            if (action != null)
                return action;
            action = Prop?.PropertyType.Name switch
            {
                nameof(String) => (item) => item,
                nameof(Int16) => (item) => short.Parse(item),
                nameof(Int32) => (item) => int.Parse(item),
                nameof(Int64) => (item) => long.Parse(item),
                nameof(Double) => (item) => double.Parse(item),
                nameof(Single) => (item) => float.Parse(item),
                nameof(Decimal) => (item) => decimal.Parse(item),
                nameof(Boolean) => (item) => bool.Parse(item),
                nameof(DateTime) => (item) => DateTime.Parse(item),
                _ => (item) => item,
            };
            return action;
        }
        set => action = value;
    }
    private Func<string, object> action;
}
