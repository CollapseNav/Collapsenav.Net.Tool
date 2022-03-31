namespace Collapsenav.Net.Tool.Excel;
public static partial class NPOIExporOperation
{
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public static async Task<Stream> NPOIExportAsync<T>(this ExportConfig<T> config, IEnumerable<T> data = null)
    {
        return await config.ExportAsync(data, ExcelType.NPOI);
    }
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public static async Task<Stream> NPOIExportAsync<T>(this ExportConfig<T> config, string filePath, IEnumerable<T> data = null)
    {
        return await config.ExportAsync(filePath, data, ExcelType.NPOI);
    }
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public static async Task<Stream> NPOIExportAsync<T>(this ExportConfig<T> config, Stream stream, IEnumerable<T> data = null)
    {
        return await config.ExportAsync(stream, data, ExcelType.NPOI);
    }
    /// <summary>
    /// 导出表头
    /// </summary>
    public static Stream NPOIExportHeader<T>(this ExportConfig<T> config)
    {
        return config.ExportHeader(ExcelType.NPOI);
    }
    /// <summary>
    /// 导出表头
    /// </summary>
    public static Stream NPOIExportHeader<T>(this ExportConfig<T> config, string filePath)
    {
        return config.ExportHeader(filePath, ExcelType.NPOI);
    }
    /// <summary>
    /// 导出表头
    /// </summary>
    public static Stream NPOIExportHeader<T>(this ExportConfig<T> config, Stream stream)
    {
        return config.ExportHeader(stream, ExcelType.NPOI);
    }
}