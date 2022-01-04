using System.Collections.Concurrent;
using NPOI.SS.UserModel;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;
public class ExcelReadTool
{
    private const int EPPlusZero = 1;
    private const int NPOIZero = 0;
    /// <summary>
    /// 将表格数据转换为T类型的集合
    /// </summary>
    /// <param name="excelData">表格数据(需要包含表头且在第一行)</param>
    /// <param name="options">转换配置</param>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(string[][] excelData, ReadConfig<T> options)
    {
        var header = excelData[0].Select((key, index) => (key, index)).ToDictionary(item => item.key, item => item.index);
        excelData = excelData.Skip(1).ToArray();
        return await ExcelToEntityAsync(header, excelData, options);
    }

    /// <summary>
    /// 将表格数据转换为T类型的集合
    /// </summary>
    /// <param name="header">表头</param>
    /// <param name="excelData">表格数据</param>
    /// <param name="options">转换配置</param>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(Dictionary<string, int> header, string[][] excelData, ReadConfig<T> options)
    {
        ConcurrentBag<T> data = new();
        await Task.Factory.StartNew(() =>
        {
            Parallel.For(0, excelData.Length, index =>
            {
                // 根据对应传入的设置 为obj赋值
                var obj = Activator.CreateInstance<T>();
                foreach (var option in options.FieldOption)
                {
                    if (!option.ExcelField.IsNull())
                    {
                        var value = excelData[index][header[option.ExcelField]];
                        option.Prop.SetValue(obj, option.Action == null ? value : option.Action(value));
                    }
                    else
                        option.Prop.SetValue(obj, option.Action == null ? null : option.Action(string.Empty));
                }
                if (options.Init != null)
                    obj = options.Init(obj);
                data.Add(obj);
            });
        });
        return data;
    }





    /// <summary>
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="sheet">工作簿</param>
    public static IEnumerable<string> ExcelHeader(ExcelWorksheet sheet)
    {
        return sheet.Cells[EPPlusZero, EPPlusZero, EPPlusZero, sheet.Dimension.Columns]
                .Select(item => item.Value?.ToString().Trim()).ToArray();
    }
    /// <summary>
    /// 获取表格的数据(仅限简单的单行表头)
    /// </summary>
    /// <param name="sheet">工作簿</param>
    public static async Task<IEnumerable<IEnumerable<string>>> ExcelDataAsync(ExcelWorksheet sheet)
    {
        int rowCount = sheet.Dimension.Rows;
        int colCount = sheet.Dimension.Columns;
        ConcurrentBag<IEnumerable<string>> data = new();
        await Task.Factory.StartNew(() =>
        {
            Parallel.For(EPPlusZero + EPPlusZero, rowCount + EPPlusZero, index =>
            {
                data.Add(sheet.Cells[index, EPPlusZero, index, colCount]
                .Select(item => item.Value?.ToString().Trim()).ToList());
            });
        });
        return data;
    }
    /// <summary>
    /// 获取表格的数据(仅限简单的单行表头)
    /// </summary>
    /// <param name="sheet">工作簿</param>
    public static IEnumerable<IEnumerable<string>> ExcelData(ExcelWorksheet sheet)
    {
        int rowCount = sheet.Dimension.Rows;
        int colCount = sheet.Dimension.Columns;
        for (var index = 0; index < rowCount; index++)
            yield return sheet.Cells[index, EPPlusZero, index, colCount].Select(item => item.Value?.ToString().Trim()).ToList();
    }
    /// <summary>
    /// 获取表头及其index
    /// </summary>
    /// <param name="sheet">工作簿</param>
    /// <param name="options">导出配置</param>
    public static Dictionary<string, int> ExcelHeaderByOptions<T>(ExcelWorksheet sheet, ReadConfig<T> options)
    {
        // 获取对应设置的 表头 以及其 column
        var header = sheet.Cells[EPPlusZero, EPPlusZero, EPPlusZero, sheet.Dimension.Columns]
        .Where(item => options.FieldOption.Any(opt => opt.ExcelField == item.Value?.ToString().Trim()))
        .ToDictionary(item => item.Value?.ToString().Trim(), item => item.End.Column - EPPlusZero);
        return header;
    }
    /// <summary>
    /// 根据配置 获取表格数据
    /// </summary>
    /// <param name="sheet">工作簿</param>
    /// <param name="options">导出配置</param>
    public static async Task<string[][]> ExcelDataByOptionsAsync<T>(ExcelWorksheet sheet, ReadConfig<T> options)
    {
        var header = ExcelHeaderByOptions<T>(sheet, options);
        var resultHeader = header.Select(item => item.Key).ToList();

        int rowCount = sheet.Dimension.Rows;
        int colCount = sheet.Dimension.Columns;
        ConcurrentBag<string[]> data = new();
        await Task.Factory.StartNew(() =>
        {
            Parallel.For(EPPlusZero + EPPlusZero, rowCount + EPPlusZero, index =>
            {
                Monitor.Enter(sheet);
                var temp = sheet.Cells[index, EPPlusZero, index, colCount]
                .Where(item => header.Any(col => col.Value == item.End.Column - EPPlusZero))
                .Select(item => item.Text).ToArray();
                Monitor.Exit(sheet);
                data.Add(temp);
            });
        });
        return data.ToArray();
    }



    /// <summary>
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="sheet">工作簿</param>
    public static IEnumerable<string> ExcelHeader(ISheet sheet)
    {
        var header = sheet.GetRow(NPOIZero).Cells.Select(item => item.ToString()?.Trim());
        return header;
    }
    /// <summary>
    /// 获取表格的数据(仅限简单的单行表头)
    /// </summary>
    /// <param name="sheet">工作簿</param>
    public static async Task<IEnumerable<IEnumerable<string>>> ExcelDataAsync(ISheet sheet)
    {
        var rowCount = sheet.LastRowNum;
        var colCount = sheet.GetRow(NPOIZero).Cells.Count;
        ConcurrentBag<IEnumerable<string>> data = new();
        await Task.Factory.StartNew(() =>
        {
            Parallel.For(NPOIZero, rowCount, index =>
            {
                data.Add(sheet.GetRow(index).Cells
                .Select(item => item.ToString()?.Trim()).ToList());
            });
        });
        return data;
    }
    /// <summary>
    /// 根据传入配置 获取表头及其index
    /// </summary>
    /// <param name="sheet">工作簿</param>
    /// <param name="options">导出配置</param>
    public static Dictionary<string, int> ExcelHeaderByOptions<T>(ISheet sheet, ReadConfig<T> options)
    {
        // 获取对应设置的 表头 以及其 column
        var header = sheet.GetRow(NPOIZero).Cells
        .Where(item => options.FieldOption.Any(opt => opt.ExcelField == item.ToString()?.Trim()))
        .ToDictionary(item => item.ToString()?.Trim(), item => item.ColumnIndex);
        return header;
    }
    /// <summary>
    /// 根据配置 获取表格数据
    /// </summary>
    /// <param name="sheet">工作簿</param>
    /// <param name="options">导出配置</param>
    public static async Task<string[][]> ExcelDataByOptionsAsync<T>(ISheet sheet, ReadConfig<T> options)
    {
        var header = ExcelHeaderByOptions<T>(sheet, options);

        int rowCount = sheet.LastRowNum + 1;
        int colCount = sheet.GetRow(NPOIZero).Cells.Count;
        ConcurrentBag<string[]> data = new();
        await Task.Factory.StartNew(() =>
        {
            Parallel.For(NPOIZero + 1, rowCount, index =>
            {
                Monitor.Enter(sheet);
                var temp = sheet.GetRow(index).Cells
                .Where(item => header.Any(col => col.Value == item.ColumnIndex))
                .Select(item => item.ToString()?.Trim()).ToArray();
                Monitor.Exit(sheet);
                data.Add(temp);
            });
        });
        // data.Add(resultHeader.ToArray());
        return data.ToArray();
    }

}
