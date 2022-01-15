using NPOI.XSSF.Streaming;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;

public class ExcelExportTool
{
    public const int EPPlusZero = 1;
    public const int NPOIZero = 0;
    /// <summary>
    /// 导出excel
    /// </summary>
    /// <param name="stream">文件流</param>
    /// <param name="option">导出配置</param>
    /// <param name="data">指定数据</param>
    /// <param name="exportType">导出类型</param>
    public static async Task<Stream> EPPlusExportAsync<T>(Stream stream, ExportConfig<T> option, IEnumerable<T> data = null, ExportType exportType = ExportType.All)
    {
        using var pack = new ExcelPackage();
        ExcelWorksheet sheet = pack.Workbook.Worksheets.Add($@"sheet{pack.Workbook.Worksheets.Count}");
        await Task.Run(() =>
        {
            var excelData = exportType switch
            {
                ExportType.All => data == null ? option.ExportData : option.GetExportData(data),
                ExportType.Header => option.ConvertHeader,
                ExportType.Data => data == null ? option.ConvertData : option.GetConvertData(data),
                _ => data == null ? option.ExportData : option.GetExportData(data)
            };
            foreach (var (dataRow, indexRow) in excelData.SelectWithIndex(EPPlusZero))
                foreach (var (dataCell, indexCell) in dataRow.SelectWithIndex(EPPlusZero))
                    sheet.Cells[indexRow, indexCell].Value = dataCell;
            pack.SaveAs(stream);
        });
        stream.SeekToOrigin();
        return stream;
    }
    /// <summary>
    /// 导出表头
    /// </summary>
    /// <param name="stream">文件流</param>
    /// <param name="option">导出配置</param>
    public static async Task<Stream> EPPlusExportHeaderAsync<T>(Stream stream, ExportConfig<T> option)
    {
        using var pack = new ExcelPackage();
        ExcelWorksheet sheet = pack.Workbook.Worksheets.Add($@"sheet{pack.Workbook.Worksheets.Count}");
        await Task.Run(() =>
        {
            foreach (var (item, index) in option.Header.SelectWithIndex(EPPlusZero))
                sheet.Cells[EPPlusZero, index].Value = item;
            pack.SaveAs(stream);
        });
        stream.SeekToOrigin();
        return stream;
    }


    /// <summary>
    /// 导出excel
    /// </summary>
    /// <param name="stream">文件流</param>
    /// <param name="option">导出配置</param>
    /// <param name="data">指定数据</param>
    public static async Task<Stream> NPOIExportAsync<T>(Stream stream, ExportConfig<T> option, IEnumerable<T> data = null)
    {
        var workbook = new SXSSFWorkbook();
        var sheet = workbook.CreateSheet("Sheet1");
        var notCloseStream = new NPOINotCloseStream(stream);
        await Task.Run(() =>
        {
            var row = sheet.CreateRow(NPOIZero);
            // 加表头
            foreach (var (head, index) in option.Header.Select((item, i) => (item, i)))
            {
                var cell = row.CreateCell(index);
                cell.SetCellValue(head);
            }

            // 加数据
            foreach (var cellData in data ?? option.Data)
            {
                row = sheet.CreateRow(sheet.LastRowNum + 1);
                foreach (var (item, index) in option.FieldOption.Select((item, i) => (item, i)))
                {
                    var cell = row.CreateCell(index);
                    cell.SetCellValue(item.Action(cellData)?.ToString());
                }
            }
            workbook.Write(notCloseStream);
        });
        await notCloseStream.SeekAndCopyToAsync(stream);
        return notCloseStream;
    }
    /// <summary>
    /// 导出表头
    /// </summary>
    /// <param name="stream">文件流</param>
    /// <param name="option">导出配置</param>
    public static async Task<Stream> NPOIExportHeaderAsync<T>(Stream stream, ExportConfig<T> option)
    {
        var workbook = new SXSSFWorkbook();
        var sheet = workbook.CreateSheet("Sheet1");
        var notCloseStream = new NPOINotCloseStream(stream);
        await Task.Run(() =>
        {
            var row = sheet.CreateRow(NPOIZero);
            // 加表头
            foreach (var (head, index) in option.Header.Select((item, i) => (item, i)))
            {
                var cell = row.CreateCell(index);
                cell.SetCellValue(head);
            }
            workbook.Write(notCloseStream);
        });
        await notCloseStream.SeekAndCopyToAsync(stream);
        return notCloseStream;
    }
}