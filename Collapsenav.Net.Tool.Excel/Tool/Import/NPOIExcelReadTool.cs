using NPOI.SS.UserModel;

namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 针对表格导入的一些操作(基于 NPOI)
/// </summary>
public partial class NPOIExcelReadTool
{
    private const int NPOIZero = 0;

    #region 获取表头
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="filepath">文件路径</param>
    public static IEnumerable<string> ExcelHeader(string filepath)
    {
        filepath.IsXls();
        using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return ExcelHeader(fs);
    }
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="stream">文件流</param>
    public static IEnumerable<string> ExcelHeader(Stream stream)
    {
        using var notCloseStream = new NPOINotCloseStream(stream);
        return ExcelHeader(notCloseStream.GetWorkBook().GetSheetAt(NPOIZero));
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


    #region 通过表格配置获取表头
    /// <summary>
    /// 根据传入配置 获取表头及其index
    /// </summary>
    /// <param name="filepath">文件路径</param>
    /// <param name="options">导出配置</param>
    public static Dictionary<string, int> ExcelHeaderByOptions<T>(string filepath, ReadConfig<T> options)
    {
        filepath.IsXls();
        using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return ExcelHeaderByOptions(fs, options);
    }
    /// <summary>
    /// 根据传入配置 获取表头及其index
    /// </summary>
    /// <param name="stream">文件流</param>
    /// <param name="options">导出配置</param>
    public static Dictionary<string, int> ExcelHeaderByOptions<T>(Stream stream, ReadConfig<T> options)
    {
        using var notCloseStream = new NPOINotCloseStream(stream);
        return ExcelHeaderByOptions(notCloseStream.GetWorkBook().GetSheetAt(NPOIZero), options);
    }
    /// <summary>
    /// 根据传入配置 获取表头及其index
    /// </summary>
    /// <param name="sheet">工作簿</param>
    /// <param name="options">导出配置</param>
    public static Dictionary<string, int> ExcelHeaderByOptions<T>(ISheet sheet, ReadConfig<T> options)
    {
        return ExcelReadTool.ExcelHeaderByOptions(sheet, options);
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
        using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
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
        return await ExcelToEntityAsync(notCloseStream.GetWorkBook().GetSheetAt(NPOIZero), options);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体
    /// </summary>
    /// <param name="sheet">工作簿</param>
    /// <param name="options">导出配置</param>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(ISheet sheet, ReadConfig<T> options)
    {
        return await ExcelReadTool.ExcelToEntityAsync(IExcelRead.GetExcelRead(sheet), options);
    }
    #endregion
}
