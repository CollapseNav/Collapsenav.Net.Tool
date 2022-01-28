namespace Collapsenav.Net.Tool.Excel;
public partial class ReadConfig<T>
{
    /// <summary>
    /// 将表格数据转换为指定的数据实体-Mini
    /// </summary>
    public async Task<IEnumerable<T>> MiniToEntityAsync()
    {
        if (ExcelStream == null)
            throw new NullReferenceException();
        return await ToEntityAsync(ExcelStream, ExcelType.MiniExcel);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-Mini
    /// </summary>
    public async Task<IEnumerable<T>> MiniToEntityAsync(string filepath)
    {
        return await ToEntityAsync(filepath, ExcelType.MiniExcel);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-Mini
    /// </summary>
    public async Task<IEnumerable<T>> MiniToEntityAsync(Stream stream)
    {
        return await ToEntityAsync(new MiniExcelRead(stream));
    }
}