namespace Collapsenav.Net.Tool.Excel;

public partial class ExportConfig<T>
{
    public async Task<Stream> ExportAsync(IEnumerable<T> data = null)
    {
        var stream = new MemoryStream();
        return await ExportAsync(stream, data);
    }
    /// <summary>
    /// 数据实体导出为Excel(暂时使用EPPlus实现)
    /// </summary>
    public async Task<Stream> ExportAsync(string path, IEnumerable<T> data = null)
    {
        return await EPPlusExportTool.ExportAsync(path, this, data);
    }
    /// <summary>
    /// 数据实体导出为Excel(暂时使用EPPlus实现)
    /// </summary>
    public async Task<Stream> ExportAsync(Stream stream, IEnumerable<T> data = null)
    {
        return await EPPlusExportTool.ExportAsync(stream, this, data);
    }


    /// <summary>
    /// 数据实体导出为Excel(暂时使用EPPlus实现)
    /// </summary>
    public static async Task<Stream> DataExportAsync(string path, IEnumerable<T> data)
    {
        var config = GenDefaultConfig(data);
        return await EPPlusExportTool.ExportAsync(path, config);
    }
    /// <summary>
    /// 数据实体导出为Excel(暂时使用EPPlus实现)
    /// </summary>
    public static async Task<Stream> DataExportAsync(Stream stream, IEnumerable<T> data)
    {
        var config = GenDefaultConfig(data);
        return await EPPlusExportTool.ExportAsync(stream, config);
    }
    /// <summary>
    /// 数据实体导出为Excel(暂时使用EPPlus实现)
    /// </summary>
    public static async Task<Stream> ConfigExportHeaderAsync(string path)
    {
        var config = GenDefaultConfig();
        return await EPPlusExportTool.ExportHeaderAsync(path, config);
    }
    /// <summary>
    /// 数据实体导出为Excel(暂时使用EPPlus实现)
    /// </summary>
    public static async Task<Stream> ConfigExportHeaderAsync(Stream stream)
    {
        var config = GenDefaultConfig();
        return await EPPlusExportTool.ExportHeaderAsync(stream, config);
    }
}