using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 针对表格导入的一些操作(基于 EPPlus)
/// </summary>
public partial class EPPlusExcelReadTool
{
    #region 获取表头
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="filepath">文件路径</param>
    public static IEnumerable<string> ExcelHeader(string filepath)
    {
        using var fs = filepath.OpenReadShareStream();
        return ExcelHeader(fs);
    }
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="stream">文件流</param>
    public static IEnumerable<string> ExcelHeader(Stream stream)
    {
        return ExcelHeader(ExcelTool.EPPlusSheet(stream));
    }
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="sheet">工作簿</param>
    public static IEnumerable<string> ExcelHeader(ExcelWorksheet sheet)
    {
        return ExcelReadTool.ExcelHeader(sheet);
    }
    #endregion

    #region 将表格数据转换为指定的数据实体
    /// <summary>
    /// 将表格数据转换为指定的数据实体
    /// </summary>
    /// <param name="filepath">文件路径</param>
    /// <param name="options">导出配置</param>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(string filepath, ReadConfig<T> options)
    {
        return await ExcelToEntityAsync(ExcelTool.EPPlusSheet(filepath), options);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体
    /// </summary>
    /// <param name="stream">文件流</param>
    /// <param name="options">导出配置</param>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(Stream stream, ReadConfig<T> options)
    {
        using ExcelPackage pack = new();
        return await ExcelToEntityAsync(ExcelTool.EPPlusSheet(stream), options);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体
    /// </summary>
    /// <param name="sheet">工作簿</param>
    /// <param name="options">导出配置</param>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(ExcelWorksheet sheet, ReadConfig<T> options)
    {
        return await options.ToEntityAsync(IExcelRead.GetExcelRead(sheet));
    }
    #endregion
}
