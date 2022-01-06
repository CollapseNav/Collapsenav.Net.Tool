using System.Data;

namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 导出表格设置
/// </summary>
public partial class ExportConfig<T> : ExcelConfig<T, ExportCellOption<T>>
{
    public ExportConfig() { }
    public ExportConfig(IEnumerable<T> data)
    {
        if (data.IsEmpty())
            throw new NoNullAllowedException();
        Data = data;
    }
    /// <summary>
    /// 获取表头
    /// </summary>
    public virtual IEnumerable<object[]> ConvertHeader { get => new List<object[]> { Header.ToArray() }; }
    /// <summary>
    /// 获取数据
    /// </summary>
    public virtual IEnumerable<object[]> ConvertData { get => Data?.Select(item => FieldOption.Select(option => option.Action(item)).ToArray()); }
    /// <summary>
    /// 获取表头和数据
    /// </summary>
    public virtual IEnumerable<object[]> ExportData { get => ConvertHeader.Concat(ConvertData); }
}
