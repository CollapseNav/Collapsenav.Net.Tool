namespace Collapsenav.Net.Tool.Excel;
public partial class ExportConfig<T>
{
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public async Task<Stream> EPPlusExportAsync(IEnumerable<T> data = null) => await ExportAsync(data, ExcelType.EPPlus);
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public async Task<Stream> EPPlusExportAsync(string filePath, IEnumerable<T> data = null) => await ExportAsync(filePath, data, ExcelType.EPPlus);
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public async Task<Stream> EPPlusExportAsync(Stream stream, IEnumerable<T> data = null) => await ExportAsync(stream, data, ExcelType.EPPlus);
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public Stream EPPlusExportHeader() => ExportHeader(ExcelType.EPPlus);
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public Stream EPPlusExportHeader(string filePath) => ExportHeader(filePath, ExcelType.EPPlus);
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public Stream EPPlusExportHeader(Stream stream) => ExportHeader(stream, ExcelType.EPPlus);
}