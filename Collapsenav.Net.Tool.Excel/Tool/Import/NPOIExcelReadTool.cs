using NPOI.SS.UserModel;

namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 针对表格导入的一些操作(基于 NPOI)
/// </summary>
public partial class NPOIExcelReadTool
{
    #region 获取表头
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="filepath">文件路径</param>
    public static IEnumerable<string> ExcelHeader(string filepath)
    {
        filepath.IsXls();
        using var fs = filepath.OpenReadShareStream();
        return ExcelHeader(fs);
    }
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="stream">文件流</param>
    public static IEnumerable<string> ExcelHeader(Stream stream)
    {
        using var notCloseStream = new NPOINotCloseStream(stream);
        return ExcelHeader(notCloseStream.GetWorkBook().GetSheetAt(ExcelReadTool.NPOIZero));
    }
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="sheet">工作簿</param>
    public static IEnumerable<string> ExcelHeader(ISheet sheet)
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
        filepath.IsXls();
        using var fs = filepath.OpenReadShareStream();
        return await ExcelToEntityAsync(fs, options);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体
    /// </summary>
    /// <param name="stream">文件流</param>
    /// <param name="options">导出配置</param>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(Stream stream, ReadConfig<T> options)
    {
        using var notCloseStream = new NPOINotCloseStream(stream);
        return await ExcelToEntityAsync(notCloseStream.GetWorkBook().GetSheetAt(ExcelReadTool.NPOIZero), options);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体
    /// </summary>
    /// <param name="sheet">工作簿</param>
    /// <param name="options">导出配置</param>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(ISheet sheet, ReadConfig<T> options)
    {
        return await options.ToEntityAsync(IExcelRead.GetExcelRead(sheet));
    }
    #endregion
}
