using System.IO;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel
{
    public enum ExportType
    {
        All,
        Header,
        Data
    }
    /// <summary>
    /// 针对表格导出的一些操作(基于EPPlus)
    /// </summary>
    public class ExcelExportOperation
    {
        public ExcelExportOperation() { }
        public static async Task<Stream> ExportAsync<T>(string filePath, ExportConfig<T> option, ExportType exportType = ExportType.All, bool append = false)
        {
            using var fs = new FileStream(filePath, append ? FileMode.OpenOrCreate : FileMode.Create);
            return await ExportAsync(fs, option, exportType);
        }
        public static async Task<Stream> ExportAsync<T>(Stream stream, ExportConfig<T> option, ExportType exportType = ExportType.All)
        {
            using var pack = new ExcelPackage(stream);
            ExcelWorksheet sheet = pack.Workbook.Worksheets.Add($@"sheet{pack.Workbook.Worksheets.Count}");
            await Task.Run(() =>
            {
                sheet.Cells[1, 1].LoadFromArrays(
                    exportType switch
                    {
                        ExportType.All => option.Export,
                        ExportType.Header => option.ConvertHeader,
                        ExportType.Data => option.ConvertData,
                        _ => option.Export
                    }
                );
                pack.SaveAs(stream);
            });
            return stream;
        }

        public static async Task<Stream> ExportHeaderAsync<T>(string filePath, ExportConfig<T> option, bool append = false)
        {
            using var fs = new FileStream(filePath, append ? FileMode.OpenOrCreate : FileMode.Create);
            return await ExportHeaderAsync(fs, option);
        }
        public static async Task<Stream> ExportHeaderAsync<T>(Stream stream, ExportConfig<T> option)
        {
            using var pack = new ExcelPackage(stream);
            ExcelWorksheet sheet = pack.Workbook.Worksheets.Add($@"sheet{pack.Workbook.Worksheets.Count}");
            await Task.Run(() =>
            {
                sheet.Cells[1, 1].LoadFromArrays(option.ConvertHeader);
                pack.SaveAs(stream);
            });
            return stream;
        }

        public static async Task<Stream> ExportDataAsync<T>(string filePath, ExportConfig<T> option, bool append = false)
        {
            using var fs = new FileStream(filePath, append ? FileMode.OpenOrCreate : FileMode.Create);
            return await ExportHeaderAsync(fs, option);
        }
        public static async Task<Stream> ExportDataAsync<T>(Stream stream, ExportConfig<T> option)
        {
            using var pack = new ExcelPackage(stream);
            ExcelWorksheet sheet = pack.Workbook.Worksheets.Add($@"sheet{pack.Workbook.Worksheets.Count}");
            await Task.Run(() =>
            {
                sheet.Cells[1, 1].LoadFromArrays(option.ConvertData);
                pack.SaveAs(stream);
            });
            return stream;
        }

    }
}
