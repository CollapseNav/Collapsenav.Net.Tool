namespace Collapsenav.Net.Tool.Excel;
public partial class ExportConfig<T>
{
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public async Task<Stream> NPOIExportAsync(IEnumerable<T> data = null)
    {
        return await NPOIExportTool.ExportAsync(this, data);
    }
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public async Task<Stream> NPOIExportAsync(string filePath, IEnumerable<T> data = null)
    {
        return await NPOIExportTool.ExportAsync(filePath, this, data);
    }
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public async Task<Stream> NPOIExportAsync(Stream stream, IEnumerable<T> data = null)
    {
        return await NPOIExportTool.ExportAsync(stream, this, data);
    }


    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public async Task<Stream> NPOIExportHeaderAsync()
    {
        return await NPOIExportTool.ExportHeaderAsync(this);
    }
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public async Task<Stream> NPOIExportHeaderAsync(string filePath)
    {
        return await NPOIExportTool.ExportHeaderAsync(filePath, this);
    }
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public async Task<Stream> NPOIExportHeaderAsync(Stream stream)
    {
        return await NPOIExportTool.ExportHeaderAsync(stream, this);
    }
}