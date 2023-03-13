using System.Reflection;

namespace Collapsenav.Net.Tool.Excel;

/// <summary>
/// 导出导出所使用的单元格设置
/// </summary>
/// <remarks>一般情况下 Prop 和 PropName 只需要设置一个即可</remarks>
public interface ICellOption
{
    /// <summary>
    /// 表头
    /// </summary>
    string ExcelField { get; set; }
    /// <summary>
    /// 属性
    /// </summary>
    PropertyInfo Prop { get; set; }
    /// <summary>
    /// 属性名称
    /// </summary>
    string PropName { get; set; }
}