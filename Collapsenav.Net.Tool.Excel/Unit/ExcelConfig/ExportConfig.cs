namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 导出表格设置
/// </summary>
public partial class ExportConfig<T> : ExcelConfig<T, ExportCellOption<T>>
{
    public ExportConfig() { }
    public ExportConfig(IEnumerable<T> data)
    {
        Data = data;
    }
}
