using NPOI.SS.UserModel;

namespace Collapsenav.Net.Tool.Excel;
public partial class ReadConfig<T>
{
    /// <summary>
    /// 将表格数据转换为指定的数据实体-NPOI
    /// </summary>
    public async Task<IEnumerable<T>> NPOIToEntityAsync()
    {
        if (ExcelStream == null)
            throw new Exception();
        return await NPOIExcelReadTool.ExcelToEntityAsync(ExcelStream, this);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-NPOI
    /// </summary>
    public async Task<IEnumerable<T>> NPOIToEntityAsync(string filepath)
    {
        return await NPOIExcelReadTool.ExcelToEntityAsync(filepath, this);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-NPOI
    /// </summary>
    public async Task<IEnumerable<T>> NPOIToEntityAsync(Stream stream)
    {
        return await NPOIExcelReadTool.ExcelToEntityAsync(stream, this);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-NPOI
    /// </summary>
    public async Task<IEnumerable<T>> NPOIToEntityAsync(ISheet sheet)
    {
        return await ExcelReadTool.ExcelToEntityAsync(IExcelRead.GetExcelRead(sheet), this);
    }

}