namespace Collapsenav.Net.Tool.Excel;
/// <summary>
///  导出单元格设置
/// </summary>
public class ExportCellOption<T> : BaseCellOption<T>
{
    /// <summary>
    /// 转换 表格 数据的方法
    /// </summary>
    public Func<T, object> Action { get; set; }
}

public class ExportCellOption : ExportCellOption<object>
{ }
