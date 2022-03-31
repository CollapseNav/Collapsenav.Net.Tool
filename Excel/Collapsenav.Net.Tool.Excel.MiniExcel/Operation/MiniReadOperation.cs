namespace Collapsenav.Net.Tool.Excel;
public static partial class MiniReadOperation
{
    /// <summary>
    /// 将表格数据转换为指定的数据实体-Mini
    /// </summary>
    public static async Task<IEnumerable<T>> MiniToEntityAsync<T>(this ReadConfig<T> config)
    {
        return await config.ToEntityAsync(ExcelType.MiniExcel);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-Mini
    /// </summary>
    public static async Task<IEnumerable<T>> MiniToEntityAsync<T>(this ReadConfig<T> config, string filepath)
    {
        return await config.ToEntityAsync(filepath, ExcelType.MiniExcel);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-Mini
    /// </summary>
    public static async Task<IEnumerable<T>> MiniToEntityAsync<T>(this ReadConfig<T> config, Stream stream)
    {
        return await config.ToEntityAsync(new MiniExcelReader(stream));
    }
}