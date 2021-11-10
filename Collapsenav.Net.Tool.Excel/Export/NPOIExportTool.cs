using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NPOI.XSSF.Streaming;

namespace Collapsenav.Net.Tool.Excel
{
    /// <summary>
    /// 针对表格导出的一些操作(基于NPOI)
    /// </summary>
    public class NPOIExportTool
    {
        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="option">导出配置</param>
        /// <param name="data">指定数据</param>
        public static async Task<Stream> ExportAsync<T>(string filePath, ExportConfig<T> option, IEnumerable<T> data = null)
        {
            using var fs = new FileStream(filePath, FileMode.Create);
            return await ExportAsync(fs, option, data);
        }
        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="option">导出配置</param>
        /// <param name="data">指定数据</param>
        public static async Task<Stream> ExportAsync<T>(Stream stream, ExportConfig<T> option, IEnumerable<T> data = null)
        {
            var workbook = new SXSSFWorkbook();
            var sheet = workbook.CreateSheet("Sheet1");
            var notCloseStream = new NPOINotCloseStream(stream);
            await Task.Run(() =>
            {
                // 加表头
                foreach (var (head, index) in option.Header.Select((item, i) => (item, i)))
                {
                    var cell = sheet.CreateRow(0).CreateCell(index);
                    cell.SetCellValue(head);
                }

                // 加数据
                foreach (var cellData in data ?? option.Data)
                {
                    foreach (var (option, index) in option.FieldOption.Select((item, i) => (item, i)))
                    {
                        var cell = sheet.CreateRow(sheet.LastRowNum + 1).CreateCell(index);
                        cell.SetCellValue(option.Action(cellData)?.ToString());
                    }
                }
                workbook.Write(notCloseStream);
            });
            notCloseStream.Seek(0, SeekOrigin.Begin);
            return notCloseStream;
        }

        /// <summary>
        /// 导出表头
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="option">导出配置</param>
        public static async Task<Stream> ExportHeaderAsync<T>(string filePath, ExportConfig<T> option)
        {
            using var fs = new FileStream(filePath, FileMode.Create);
            return await ExportHeaderAsync(fs, option);
        }
        /// <summary>
        /// 导出表头
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="option">导出配置</param>
        public static async Task<Stream> ExportHeaderAsync<T>(Stream stream, ExportConfig<T> option)
        {
            var workbook = new SXSSFWorkbook();
            var sheet = workbook.CreateSheet("Sheet1");
            var notCloseStream = new NPOINotCloseStream(stream);
            await Task.Run(() =>
            {
                // 加表头
                foreach (var (head, index) in option.Header.Select((item, i) => (item, i)))
                {
                    var cell = sheet.CreateRow(0).CreateCell(index);
                    cell.SetCellValue(head);
                }
                workbook.Write(notCloseStream);
            });
            notCloseStream.Seek(0, SeekOrigin.Begin);
            return notCloseStream;
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="option">导出配置</param>
        /// <param name="data">指定数据</param>
        public static async Task<Stream> ExportDataAsync<T>(string filePath, ExportConfig<T> option, IEnumerable<T> data = null)
        {
            using var fs = new FileStream(filePath, FileMode.Create);
            return await ExportDataAsync(fs, option, data);
        }
        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="option">导出配置</param>
        /// <param name="data">指定数据</param>
        public static async Task<Stream> ExportDataAsync<T>(Stream stream, ExportConfig<T> option, IEnumerable<T> data = null)
        {
            var workbook = new SXSSFWorkbook();
            var sheet = workbook.CreateSheet("Sheet1");
            var notCloseStream = new NPOINotCloseStream(stream);
            await Task.Run(() =>
            {
                // 加数据
                foreach (var cellData in data ?? option.Data)
                {
                    foreach (var (option, index) in option.FieldOption.Select((item, i) => (item, i)))
                    {
                        var cell = sheet.CreateRow(sheet.LastRowNum + 1).CreateCell(index);
                        cell.SetCellValue(option.Action(cellData)?.ToString());
                    }
                }
                workbook.Write(notCloseStream);
            });
            notCloseStream.Seek(0, SeekOrigin.Begin);
            return notCloseStream;
        }

    }
}
