namespace Collapsenav.Net.Tool.Excel;

public partial class ReadConfig<T>
{
    public async Task<IEnumerable<T>> ToEntityAsync(string path, ExcelType? excelType = null)
    {
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        return await ToEntityAsync(fs, excelType);
    }
    public virtual async Task<IEnumerable<T>> ToEntityAsync(Stream stream, ExcelType? excelType = null)
    {
        var reader = IExcelRead.GetExcelRead(stream, excelType);
        return await ToEntityAsync(reader);
    }
    public virtual async Task<IEnumerable<T>> ToEntityAsync(IExcelRead reader)
    {
        return await ExcelReadTool.ExcelToEntityAsync(reader, this);
    }
    public static async Task<IEnumerable<T>> ExcelToEntityAsync(string path)
    {
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        return await ExcelToEntityAsync(fs);
    }

    public static async Task<IEnumerable<T>> ExcelToEntityAsync(Stream stream)
    {
        var reader = IExcelRead.GetExcelRead(stream);
        var config = GenDefaultConfig();
        return await ExcelReadTool.ExcelToEntityAsync(reader, config);
    }
    // public static async Task<IEnumerable<T>> ExcelToEntityAsync(IExcelRead reader)
    // {
    //     var config = GenDefaultConfig();
    //     return await ExcelReadTool.ExcelToEntityAsync(reader, config);
    // }
}
