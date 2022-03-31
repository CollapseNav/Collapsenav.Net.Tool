namespace Collapsenav.Net.Tool.Excel;
public static partial class EPPlusExporOperation
{
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public static async Task<Stream> EPPlusExportAsync<T>(this ExportConfig<T> config, IEnumerable<T> data = null)
    {
        return await config.ExportAsync(data, ExcelType.EPPlus);
    }
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public static async Task<Stream> EPPlusExportAsync<T>(this ExportConfig<T> config, string filePath, IEnumerable<T> data = null)
    {
        return await config.ExportAsync(filePath, data, ExcelType.EPPlus);
    }
    /// <summary>
    /// 列表数据导出到excel
    /// </summary>
    public static async Task<Stream> EPPlusExportAsync<T>(this ExportConfig<T> config, Stream stream, IEnumerable<T> data = null)
    {
        return await config.ExportAsync(stream, data, ExcelType.EPPlus);
    }
    /// <summary>
    /// 导出表头
    /// </summary>
    public static Stream EPPlusExportHeader<T>(this ExportConfig<T> config)
    {
        return config.ExportHeader(ExcelType.EPPlus);
    }
    /// <summary>
    /// 导出表头
    /// </summary>
    public static Stream EPPlusExportHeader<T>(this ExportConfig<T> config, string filePath)
    {
        return config.ExportHeader(filePath, ExcelType.EPPlus);
    }
    /// <summary>
    /// 导出表头
    /// </summary>
    public static Stream EPPlusExportHeader<T>(this ExportConfig<T> config, Stream stream)
    {
        return config.ExportHeader(stream, ExcelType.EPPlus);
    }
}