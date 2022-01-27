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
        return await NPOIToEntityAsync(ExcelStream);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-NPOI
    /// </summary>
    public async Task<IEnumerable<T>> NPOIToEntityAsync(string filepath)
    {
        using var fs = filepath.OpenReadShareStream();
        return await NPOIToEntityAsync(fs);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-NPOI
    /// </summary>
    public async Task<IEnumerable<T>> NPOIToEntityAsync(Stream stream)
    {
        return await NPOIToEntityAsync(ExcelTool.NPOISheet(stream));
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-NPOI
    /// </summary>
    public async Task<IEnumerable<T>> NPOIToEntityAsync(ISheet sheet)
    {
        return await ToEntityAsync(IExcelRead.GetExcelRead(sheet));
    }
}