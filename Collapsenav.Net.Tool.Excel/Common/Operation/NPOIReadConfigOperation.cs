using NPOI.SS.UserModel;

namespace Collapsenav.Net.Tool.Excel;
public partial class ReadConfig<T>
{
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)-NPOI
    /// </summary>
    public IEnumerable<string> NPOIExcelHeader()
    {
        if (ExcelStream == null)
            throw new Exception();
        return NPOIExcelReadTool.ExcelHeader(ExcelStream);
    }
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)-NPOI
    /// </summary>x
    public IEnumerable<string> NPOIExcelHeader(string filepath)
    {
        return NPOIExcelReadTool.ExcelHeader(filepath);
    }
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)-NPOI
    /// </summary>
    public IEnumerable<string> NPOIExcelHeader(Stream stream)
    {
        return NPOIExcelReadTool.ExcelHeader(stream);
    }
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)-NPOI
    /// </summary>
    public IEnumerable<string> NPOIExcelHeader(ISheet sheet)
    {
        return NPOIExcelReadTool.ExcelHeader(sheet);
    }


    /// <summary>
    /// 根据传入配置 获取表头及其index-NPOI
    /// </summary>
    public Dictionary<string, int> NPOIExcelHeaderByOptions()
    {
        if (ExcelStream == null)
            throw new Exception();
        return NPOIExcelReadTool.ExcelHeaderByOptions(ExcelStream, this);
    }
    /// <summary>
    /// 根据传入配置 获取表头及其index-NPOI
    /// </summary>
    public Dictionary<string, int> NPOIExcelHeaderByOptions(string filepath)
    {
        return NPOIExcelReadTool.ExcelHeaderByOptions(filepath, this);
    }
    /// <summary>
    /// 根据传入配置 获取表头及其index-NPOI
    /// </summary>
    public Dictionary<string, int> NPOIExcelHeaderByOptions(Stream stream)
    {
        return NPOIExcelReadTool.ExcelHeaderByOptions(stream, this);
    }
    /// <summary>
    /// 根据传入配置 获取表头及其index-NPOI
    /// </summary>
    public Dictionary<string, int> NPOIExcelHeaderByOptions(ISheet sheet)
    {
        return NPOIExcelReadTool.ExcelHeaderByOptions(sheet, this);
    }


    /// <summary>
    /// 将表格数据转换为指定的数据实体-NPOI
    /// </summary>
    public async Task<IEnumerable<T>> NPOIExcelToEntityAsync()
    {
        if (ExcelStream == null)
            throw new Exception();
        return await NPOIExcelReadTool.ExcelToEntityAsync(ExcelStream, this);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-NPOI
    /// </summary>
    public async Task<IEnumerable<T>> NPOIExcelToEntityAsync(string filepath)
    {
        return await NPOIExcelReadTool.ExcelToEntityAsync(filepath, this);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-NPOI
    /// </summary>
    public async Task<IEnumerable<T>> NPOIExcelToEntityAsync(Stream stream)
    {
        return await NPOIExcelReadTool.ExcelToEntityAsync(stream, this);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-NPOI
    /// </summary>
    public async Task<IEnumerable<T>> NPOIExcelToEntityAsync(ISheet sheet)
    {
        return await NPOIExcelReadTool.ExcelToEntityAsync(sheet, this);
    }

}