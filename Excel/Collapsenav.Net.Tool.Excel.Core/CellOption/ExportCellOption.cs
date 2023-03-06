using System.Reflection;

namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 导出单元格设置
/// </summary>
public class ExportCellOption<T> : BaseCellOption<T>
{
    public ExportCellOption() { }
    public ExportCellOption(ICellOption cellOption)
    {
        ExcelField = cellOption.ExcelField;
        PropName = cellOption.PropName;
        Prop = cellOption.Prop;
    }

    public ExportCellOption(string excelField, string propName, Func<T, object> action = null) : base(excelField, propName)
    {
        Action = action;
    }
    public ExportCellOption(string excelField, PropertyInfo prop, Func<T, object> action = null) : base(excelField, prop)
    {
        Action = action;
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