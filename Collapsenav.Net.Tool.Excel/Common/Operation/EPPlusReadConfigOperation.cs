using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;
public partial class ReadConfig<T>
{
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)-EPPlus
    /// </summary>
    public IEnumerable<string> EPPlusExcelHeader()
    {
        if (ExcelStream == null)
            throw new Exception();
        return EPPlusExcelReadTool.ExcelHeader(ExcelStream);
    }
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)-EPPlus
    /// </summary>
    public IEnumerable<string> EPPlusExcelHeader(string filepath)
    {
        return EPPlusExcelReadTool.ExcelHeader(filepath);
    }
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)-EPPlus
    /// </summary>
    public IEnumerable<string> EPPlusExcelHeader(Stream stream)
    {
        return EPPlusExcelReadTool.ExcelHeader(stream);
    }
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)-EPPlus
    /// </summary>
    public IEnumerable<string> EPPlusExcelHeader(ExcelWorksheet sheet)
    {
        return EPPlusExcelReadTool.ExcelHeader(sheet);
    }


    /// <summary>
    /// 获取表头及其index-EPPlus
    /// </summary>
    public Dictionary<string, int> EPPlusExcelHeaderByOptions()
    {
        if (ExcelStream == null)
            throw new Exception();
        return EPPlusExcelReadTool.ExcelHeaderByOptions(ExcelStream, this);
    }
    /// <summary>
    /// 获取表头及其index-EPPlus
    /// </summary>
    public Dictionary<string, int> EPPlusExcelHeaderByOptions(string filepath)
    {
        return EPPlusExcelReadTool.ExcelHeaderByOptions(filepath, this);
    }
    /// <summary>
    /// 获取表头及其index-EPPlus
    /// </summary>
    public Dictionary<string, int> EPPlusExcelHeaderByOptions(Stream stream)
    {
        return EPPlusExcelReadTool.ExcelHeaderByOptions(stream, this);
    }
    /// <summary>
    /// 获取表头及其index-EPPlus
    /// </summary>
    public Dictionary<string, int> EPPlusExcelHeaderByOptions(ExcelWorksheet sheet)
    {
        return EPPlusExcelReadTool.ExcelHeaderByOptions(sheet, this);
    }


    /// <summary>
    /// 将表格数据转换为指定的数据实体-EPPlus
    /// </summary>
    public async Task<IEnumerable<T>> EPPlusExcelToEntityAsync()
    {
        if (ExcelStream == null)
            throw new Exception();
        return await EPPlusExcelReadTool.ExcelToEntityAsync(ExcelStream, this);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-EPPlus
    /// </summary>
    public async Task<IEnumerable<T>> EPPlusExcelToEntityAsync(string filepath)
    {
        return await EPPlusExcelReadTool.ExcelToEntityAsync(filepath, this);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-EPPlus
    /// </summary>
    public async Task<IEnumerable<T>> EPPlusExcelToEntityAsync(Stream stream)
    {
        return await EPPlusExcelReadTool.ExcelToEntityAsync(stream, this);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-EPPlus
    /// </summary>
    public async Task<IEnumerable<T>> EPPlusExcelToEntityAsync(ExcelWorksheet sheet)
    {
        return await EPPlusExcelReadTool.ExcelToEntityAsync(sheet, this);
    }
}