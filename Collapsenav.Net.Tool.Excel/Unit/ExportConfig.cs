using System.Data;

namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 导出表格设置
/// </summary>
public partial class ExportConfig<T>
{
    /// <summary>
    /// 表格数据
    /// </summary>
    public IEnumerable<T> Data { get; protected set; }
    public ExportConfig()
    {
        FieldOption = new List<ExportCellOption<T>>();
    }
    public ExportConfig(IEnumerable<T> data) : this()
    {
        if (data.IsEmpty())
            throw new NoNullAllowedException();
        Data = data;
    }

    /// <summary>
    /// 依据表头的设置
    /// </summary>
    public virtual IEnumerable<ExportCellOption<T>> FieldOption { get; protected set; }

    /// <summary>
    /// 获取表头
    /// </summary>
    public virtual IEnumerable<string> Header { get => FieldOption.Select(item => item.ExcelField); }
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
