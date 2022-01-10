namespace Collapsenav.Net.Tool.Excel;

public partial class ReadConfig<T>
{
    public virtual async Task<IEnumerable<T>> ExcelToEntityAsync(Stream stream, ExcelType? excelType = null)
    {
        var reader = IExcelRead.GetExcelRead(stream);
        return await ExcelReadTool.ExcelToEntityAsync(reader, this);
    }
}