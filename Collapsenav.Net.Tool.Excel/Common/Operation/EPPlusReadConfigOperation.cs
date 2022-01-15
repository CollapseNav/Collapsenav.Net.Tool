using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;
public partial class ReadConfig<T>
{
    /// <summary>
    /// 将表格数据转换为指定的数据实体-EPPlus
    /// </summary>
    public async Task<IEnumerable<T>> EPPlusToEntityAsync()
    {
        if (ExcelStream == null)
            throw new Exception();
        return await EPPlusExcelReadTool.ExcelToEntityAsync(ExcelStream, this);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-EPPlus
    /// </summary>
    public async Task<IEnumerable<T>> EPPlusToEntityAsync(string filepath)
    {
        return await EPPlusExcelReadTool.ExcelToEntityAsync(filepath, this);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-EPPlus
    /// </summary>
    public async Task<IEnumerable<T>> EPPlusToEntityAsync(Stream stream)
    {
        return await EPPlusExcelReadTool.ExcelToEntityAsync(stream, this);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-EPPlus
    /// </summary>
    public async Task<IEnumerable<T>> EPPlusToEntityAsync(ExcelWorksheet sheet)
    {
        return await ExcelReadTool.ExcelToEntityAsync(IExcelRead.GetExcelRead(sheet), this);
    }
}