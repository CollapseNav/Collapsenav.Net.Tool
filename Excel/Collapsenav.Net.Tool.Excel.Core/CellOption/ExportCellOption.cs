namespace Collapsenav.Net.Tool.Excel;
/// <summary>
///  导出单元格设置
/// </summary>
public class ExportCellOption<T> : BaseCellOption<T>
{
    public ExportCellOption() { }
    public ExportCellOption(BaseCellOption<T> cellOption)
    {
        ExcelField = cellOption.ExcelField;
        PropName = cellOption.PropName;
    }
    /// <summary>
    /// 转换 表格 数据的方法
    /// </summary>
    public Func<T, object> Action
    {
        get
        {
            if (action != null)
                return action;
            action = item => item.GetValue(PropName);
            return action;
        }
        set => action = value;
    }
    private Func<T, object> action;
}

public class ExportCellOption : ExportCellOption<object>
{
    public ExportCellOption(BaseCellOption<object> fieldOptions) : base(fieldOptions)
    {
    }
}
