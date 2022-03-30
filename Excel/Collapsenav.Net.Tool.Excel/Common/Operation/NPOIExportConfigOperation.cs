namespace Collapsenav.Net.Tool.Excel;
public partial class ExportConfig<T>
{
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public async Task<Stream> NPOIExportAsync(IEnumerable<T> data = null) => await ExportAsync(data, ExcelType.NPOI);
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public async Task<Stream> NPOIExportAsync(string filePath, IEnumerable<T> data = null) => await ExportAsync(filePath, data, ExcelType.NPOI);
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public async Task<Stream> NPOIExportAsync(Stream stream, IEnumerable<T> data = null) => await ExportAsync(stream, data, ExcelType.NPOI);
    /// <summary>
    /// 导出表头
    /// </summary>
    public Stream NPOIExportHeader() => ExportHeader(ExcelType.NPOI);
    /// <summary>
    /// 导出表头
    /// </summary>
    public Stream NPOIExportHeader(string filePath) => ExportHeader(filePath, ExcelType.NPOI);
    /// <summary>
    /// 导出表头
    /// </summary>
    public Stream NPOIExportHeader(Stream stream) => ExportHeader(stream, ExcelType.NPOI);
}