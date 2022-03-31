using NPOI.SS.UserModel;

namespace Collapsenav.Net.Tool.Excel;
public static partial class EPPlusReadOperation
{
    /// <summary>
    /// 将表格数据转换为指定的数据实体-NPOI
    /// </summary>
    public static async Task<IEnumerable<T>> NPOIToEntityAsync<T>(this ReadConfig<T> config)
    {
        return await config.ToEntityAsync(ExcelType.NPOI);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-NPOI
    /// </summary>
    public static async Task<IEnumerable<T>> NPOIToEntityAsync<T>(this ReadConfig<T> config, string filepath)
    {
        return await config.ToEntityAsync(filepath, ExcelType.NPOI);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-NPOI
    /// </summary>
    public static async Task<IEnumerable<T>> NPOIToEntityAsync<T>(this ReadConfig<T> config, Stream stream)
    {
        return await config.ToEntityAsync(stream, ExcelType.NPOI);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-NPOI
    /// </summary>
    public static async Task<IEnumerable<T>> NPOIToEntityAsync<T>(this ReadConfig<T> config, ISheet sheet)
    {
        return await config.ToEntityAsync(ExcelTool.GetExcelReader(sheet, ExcelType.NPOI));
    }
}