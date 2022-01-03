using System.Reflection;

namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// Excel 表格-单元格 读取设置
/// </summary>
public class ReadCellOption<T> : BaseCellOption<T>
{
    /// <summary>
    /// 转换 表格 数据的方法
    /// </summary>
    public Func<string, object> Action { get; set; }
}
