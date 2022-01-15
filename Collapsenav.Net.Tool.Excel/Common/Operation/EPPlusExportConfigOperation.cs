namespace Collapsenav.Net.Tool.Excel;
public partial class ExportConfig<T>
{
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public async Task<Stream> EPPlusExportAsync(IEnumerable<T> data = null, ExportType exportType = ExportType.All)
    {
        return await EPPlusExportTool.ExportAsync(this, data, exportType);
    }
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public async Task<Stream> EPPlusExportAsync(string filePath, IEnumerable<T> data = null, ExportType exportType = ExportType.All)
    {
        return await EPPlusExportTool.ExportAsync(filePath, this, data, exportType);
    }
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public async Task<Stream> EPPlusExportAsync(Stream stream, IEnumerable<T> data = null, ExportType exportType = ExportType.All)
    {
        return await EPPlusExportTool.ExportAsync(stream, this, data, exportType);
    }


    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public async Task<Stream> EPPlusExportHeaderAsync()
    {
        return await EPPlusExportTool.ExportHeaderAsync(this);
    }
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public async Task<Stream> EPPlusExportHeaderAsync(string filePath)
    {
        return await EPPlusExportTool.ExportHeaderAsync(filePath, this);
    }
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public async Task<Stream> EPPlusExportHeaderAsync(Stream stream)
    {
        return await EPPlusExportTool.ExportHeaderAsync(stream, this);
    }
}