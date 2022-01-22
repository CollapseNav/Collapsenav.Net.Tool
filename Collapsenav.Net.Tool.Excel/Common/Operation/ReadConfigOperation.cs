namespace Collapsenav.Net.Tool.Excel;

public partial class ReadConfig<T>
{
    /// <summary>
    /// 转换到实体
    /// </summary>
    public async Task<IEnumerable<T>> ToEntityAsync(ExcelType? excelType = null)
    {
        return await ToEntityAsync(ExcelStream, excelType);
    }
    /// <summary>
    /// 转换到实体
    /// </summary>
    public async Task<IEnumerable<T>> ToEntityAsync(string path, ExcelType? excelType = null)
    {
        using var fs = path.OpenReadShareStream();
        return await ToEntityAsync(fs, excelType);
    }
    /// <summary>
    /// 转换到实体
    /// </summary>
    public virtual async Task<IEnumerable<T>> ToEntityAsync(Stream stream, ExcelType? excelType = null)
    {
        var reader = IExcelRead.GetExcelRead(stream, excelType);
        return await ToEntityAsync(reader);
    }
    /// <summary>
    /// 转换到实体
    /// </summary>
    public virtual async Task<IEnumerable<T>> ToEntityAsync(IExcelRead reader)
    {
        return await ExcelReadTool.ExcelToEntityAsync(reader, this);
    }

    /// <summary>
    /// 转换到实体
    /// </summary>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync(string path)
    {
        using var fs = path.OpenReadShareStream();
        return await ExcelToEntityAsync(fs);
    }
    /// <summary>
    /// 转换到实体
    /// </summary>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync(Stream stream)
    {
        var reader = IExcelRead.GetExcelRead(stream);
        var config = GenDefaultConfig();
        return await ExcelReadTool.ExcelToEntityAsync(reader, config);
    }
    /// <summary>
    /// 转换到实体
    /// </summary>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync(IExcelRead reader)
    {
        var config = GenDefaultConfig();
        return await ExcelReadTool.ExcelToEntityAsync(reader, config);
    }
}
