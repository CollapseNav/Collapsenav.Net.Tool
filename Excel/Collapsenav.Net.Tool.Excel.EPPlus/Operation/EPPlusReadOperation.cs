using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;
public static partial class EPPlusReadOperation
{
    /// <summary>
    /// 将表格数据转换为指定的数据实体-EPPlus
    /// </summary>
    public static async Task<IEnumerable<T>> EPPlusToEntityAsync<T>(this ReadConfig<T> config)
    {
        return await config.ToEntityAsync(ExcelType.EPPlus);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-EPPlus
    /// </summary>
    public static async Task<IEnumerable<T>> EPPlusToEntityAsync<T>(this ReadConfig<T> config, string filepath)
    {
        return await config.ToEntityAsync(filepath, ExcelType.EPPlus);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-EPPlus
    /// </summary>
    public static async Task<IEnumerable<T>> EPPlusToEntityAsync<T>(this ReadConfig<T> config, Stream stream)
    {
        return await config.ToEntityAsync(stream, ExcelType.EPPlus);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-EPPlus
    /// </summary>
    public static async Task<IEnumerable<T>> EPPlusToEntityAsync<T>(this ReadConfig<T> config, ExcelWorksheet sheet)
    {
        return await config.ToEntityAsync(ExcelTool.GetExcelReader(sheet, ExcelType.EPPlus));
    }
}