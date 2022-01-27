namespace Collapsenav.Net.Tool.Excel;
public partial class ReadConfig<T>
{
    /// <summary>
    /// 将表格数据转换为指定的数据实体-Mini
    /// </summary>
    public async Task<IEnumerable<T>> MiniToEntityAsync()
    {
        if (ExcelStream == null)
            throw new Exception();
        return await MiniToEntityAsync(ExcelStream);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-Mini
    /// </summary>
    public async Task<IEnumerable<T>> MiniToEntityAsync(string filepath)
    {
        using var fs = filepath.OpenReadShareStream();
        return await MiniToEntityAsync(fs);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-Mini
    /// </summary>
    public async Task<IEnumerable<T>> MiniToEntityAsync(Stream stream)
    {
        return await ToEntityAsync(new MiniExcelRead(stream));
    }
}