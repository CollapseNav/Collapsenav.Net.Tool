namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 针对表格导出的一些操作(基于NPOI)
/// </summary>
public partial class NPOIExportTool
{
    /// <summary>
    /// 导出excel
    /// </summary>
    /// <param name="option">导出配置</param>
    /// <param name="data">指定数据</param>
    public static async Task<Stream> ExportAsync<T>(ExportConfig<T> option, IEnumerable<T> data = null)
    {
        var stream = new MemoryStream();
        return await ExportAsync(stream, option, data);
    }
    /// <summary>
    /// 导出excel
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="option">导出配置</param>
    /// <param name="data">指定数据</param>
    public static async Task<Stream> ExportAsync<T>(string filePath, ExportConfig<T> option, IEnumerable<T> data = null)
    {
        using var fs = new FileStream(filePath, FileMode.Create);
        return await ExportAsync(fs, option, data);
    }
    /// <summary>
    /// 导出excel
    /// </summary>
    /// <param name="stream">文件流</param>
    /// <param name="option">导出配置</param>
    /// <param name="data">指定数据</param>
    public static async Task<Stream> ExportAsync<T>(Stream stream, ExportConfig<T> option, IEnumerable<T> data = null)
    {
        return await ExcelExportTool.NPOIExportAsync(stream, option, data);
    }



    /// <summary>
    /// 导出表头
    /// </summary>
    /// <param name="option">导出配置</param>
    public static async Task<Stream> ExportHeaderAsync<T>(ExportConfig<T> option)
    {
        var stream = new MemoryStream();
        return await ExportHeaderAsync(stream, option);
    }
    /// <summary>
    /// 导出表头
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="option">导出配置</param>
    public static async Task<Stream> ExportHeaderAsync<T>(string filePath, ExportConfig<T> option)
    {
        using var fs = new FileStream(filePath, FileMode.Create);
        return await ExportHeaderAsync(fs, option);
    }
    /// <summary>
    /// 导出表头
    /// </summary>
    /// <param name="stream">文件流</param>
    /// <param name="option">导出配置</param>
    public static async Task<Stream> ExportHeaderAsync<T>(Stream stream, ExportConfig<T> option)
    {
        return await ExcelExportTool.NPOIExportHeaderAsync(stream, option);
    }
}
