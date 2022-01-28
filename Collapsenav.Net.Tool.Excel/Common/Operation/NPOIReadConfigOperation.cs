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
            throw new NullReferenceException();
        return await ToEntityAsync(ExcelStream, ExcelType.NPOI);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-NPOI
    /// </summary>
    public async Task<IEnumerable<T>> NPOIToEntityAsync(string filepath)
    {
        return await ToEntityAsync(filepath, ExcelType.NPOI);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-NPOI
    /// </summary>
    public async Task<IEnumerable<T>> NPOIToEntityAsync(Stream stream)
    {
        return await ToEntityAsync(stream, ExcelType.NPOI);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-NPOI
    /// </summary>
    public async Task<IEnumerable<T>> NPOIToEntityAsync(ISheet sheet)
    {
        return await ToEntityAsync(new NPOIExcelRead(sheet));
    }
}