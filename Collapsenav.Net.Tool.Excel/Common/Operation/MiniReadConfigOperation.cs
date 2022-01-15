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
        return await MiniExcelReadTool.ExcelToEntityAsync(ExcelStream, this);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-Mini
    /// </summary>
    public async Task<IEnumerable<T>> MiniToEntityAsync(string filepath)
    {
        return await MiniExcelReadTool.ExcelToEntityAsync(filepath, this);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-Mini
    /// </summary>
    public async Task<IEnumerable<T>> MiniToEntityAsync(Stream stream)
    {
        return await MiniExcelReadTool.ExcelToEntityAsync(stream, this);
    }
}