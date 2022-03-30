namespace Collapsenav.Net.Tool.Excel;

public partial class ExcelTool
{
    /// <summary>
    /// 配置导出表头为Excel
    /// </summary>
    public static Stream ExportHeader<T>(string path)
    {
        var config = ExportConfig<T>.GenDefaultConfig();
        return config.ExportHeader(path);
    }
    /// <summary>
    /// 配置导出表头为Excel
    /// </summary>
    public static Stream ExportHeader<T>(Stream stream)
    {
        var config = ExportConfig<T>.GenDefaultConfig();
        return config.ExportHeader(stream);
    }
    /// <summary>
    /// 导出excel
    /// </summary>
    public static async Task<Stream> ExportAsync<T>(string path, IEnumerable<T> data = null)
    {
        var config = ExportConfig<T>.GenDefaultConfig(data);
        return await config.ExportAsync(path, data);
    }
    /// <summary>
    /// 导出excel
    /// </summary>
    public static async Task<Stream> ExportAsync<T>(Stream stream, IEnumerable<T> data = null)
    {
        var config = ExportConfig<T>.GenDefaultConfig(data);
        return await config.ExportAsync(stream, data);
    }
    /// <summary>
    /// 导出excel
    /// </summary>
    public static async Task<Stream> ExportAsync<T>(Stream stream, ExportConfig<T> config, IEnumerable<T> data = null)
    {
        return await config.ExportAsync(stream, data);
    }
    /// <summary>
    /// 导出excel
    /// </summary>
    public static async Task<Stream> ExportAsync<T>(string path, ExportConfig<T> config, IEnumerable<T> data = null)
    {
        return await config.ExportAsync(path, data);
    }
}