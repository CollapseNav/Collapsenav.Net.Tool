using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using System;

namespace Collapsenav.Net.Tool.Excel
{
    /// <summary>
    /// 针对表格导入的一些操作(基于 NPOI)
    /// </summary>
    public class NPOIExcelReadTool
    {
        private const int Zero = 0;


        #region 获取表头
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)
        /// </summary>
        /// <param name="filepath">文件路径</param>
        public static IEnumerable<string> ExcelHeader(string filepath)
        {
            filepath.IsXls();
            using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return ExcelHeader(fs);
        }
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)
        /// </summary>
        /// <param name="stream">文件流</param>
        public static IEnumerable<string> ExcelHeader(Stream stream)
        {
            using var notCloseStream = new NPOINotCloseStream(stream);
            return ExcelHeader(notCloseStream.GetWorkBook());
        }
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)
        /// </summary>
        /// <param name="workbook">excel workbook</param>
        public static IEnumerable<string> ExcelHeader(IWorkbook workbook)
        {
            var sheet = workbook.GetSheetAt(Zero);
            return ExcelHeader(sheet);
        }
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)
        /// </summary>
        /// <param name="sheet">工作簿</param>
        public static IEnumerable<string> ExcelHeader(ISheet sheet)
        {
            var header = sheet.GetRow(Zero).Cells.Select(item => item.ToString()?.Trim());
            return header;
        }
        #endregion


        #region 获取表格数据
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)
        /// </summary>
        /// <param name="filepath">文件路径</param>
        public static async Task<IEnumerable<IEnumerable<string>>> ExcelDataAsync(string filepath)
        {
            filepath.IsXls();
            using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return await ExcelDataAsync(fs);
        }
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)
        /// </summary>
        /// <param name="stream">文件流</param>
        public static async Task<IEnumerable<IEnumerable<string>>> ExcelDataAsync(Stream stream)
        {
            using var notCloseStream = new NPOINotCloseStream(stream);
            return await ExcelDataAsync(notCloseStream.GetWorkBook());
        }
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)
        /// </summary>
        /// <param name="workbook">excel workbook</param>
        public static async Task<IEnumerable<IEnumerable<string>>> ExcelDataAsync(IWorkbook workbook)
        {
            var sheet = workbook.GetSheetAt(Zero);
            return await ExcelDataAsync(sheet);
        }
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)
        /// </summary>
        /// <param name="sheet">工作簿</param>
        public static async Task<IEnumerable<IEnumerable<string>>> ExcelDataAsync(ISheet sheet)
        {
            var rowCount = sheet.LastRowNum;
            var colCount = sheet.GetRow(Zero).Cells.Count;
            ConcurrentBag<IEnumerable<string>> data = new();
            await Task.Factory.StartNew(() =>
            {
                Parallel.For(Zero, rowCount, index =>
                {
                    data.Add(sheet.GetRow(index).Cells
                    .Select(item => item.ToString()?.Trim()).ToList());
                });
            });
            return data;
        }
        #endregion


        #region 通过表格配置获取表头
        /// <summary>
        /// 根据传入配置 获取表头及其index
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="options">导出配置</param>
        public static Dictionary<string, int> ExcelHeaderByOptions<T>(string filepath, ReadConfig<T> options)
        {
            filepath.IsXls();
            using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return ExcelHeaderByOptions(fs, options);
        }
        /// <summary>
        /// 根据传入配置 获取表头及其index
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="options">导出配置</param>
        public static Dictionary<string, int> ExcelHeaderByOptions<T>(string filepath, IEnumerable<ReadCellOption<T>> options)
        {
            filepath.IsXls();
            using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return ExcelHeaderByOptions<T>(fs, options);
        }
        /// <summary>
        /// 根据传入配置 获取表头及其index
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="options">导出配置</param>
        public static Dictionary<string, int> ExcelHeaderByOptions<T>(Stream stream, ReadConfig<T> options)
        {
            using var notCloseStream = new NPOINotCloseStream(stream);
            return ExcelHeaderByOptions(notCloseStream.GetWorkBook(), options);
        }
        /// <summary>
        /// 根据传入配置 获取表头及其index
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="options">导出配置</param>
        public static Dictionary<string, int> ExcelHeaderByOptions<T>(Stream stream, IEnumerable<ReadCellOption<T>> options)
        {
            using var notCloseStream = new NPOINotCloseStream(stream);
            return ExcelHeaderByOptions<T>(notCloseStream.GetWorkBook(), options);
        }
        /// <summary>
        /// 根据传入配置 获取表头及其index
        /// </summary>
        /// <param name="workbook">excel workbook</param>
        /// <param name="options">导出配置</param>
        public static Dictionary<string, int> ExcelHeaderByOptions<T>(IWorkbook workbook, ReadConfig<T> options)
        {
            var sheet = workbook.GetSheetAt(Zero);
            return ExcelHeaderByOptions(sheet, options);
        }
        /// <summary>
        /// 根据传入配置 获取表头及其index
        /// </summary>
        /// <param name="workbook">excel workbook</param>
        /// <param name="options">导出配置</param>
        public static Dictionary<string, int> ExcelHeaderByOptions<T>(IWorkbook workbook, IEnumerable<ReadCellOption<T>> options)
        {
            var sheet = workbook.GetSheetAt(Zero);
            return ExcelHeaderByOptions<T>(sheet, options);
        }
        /// <summary>
        /// 根据传入配置 获取表头及其index
        /// </summary>
        /// <param name="sheet">工作簿</param>
        /// <param name="options">导出配置</param>
        public static Dictionary<string, int> ExcelHeaderByOptions<T>(ISheet sheet, ReadConfig<T> options)
        {
            return ExcelHeaderByOptions<T>(sheet, options.FieldOption);
        }
        /// <summary>
        /// 根据传入配置 获取表头及其index
        /// </summary>
        /// <param name="sheet">工作簿</param>
        /// <param name="options">导出配置</param>
        public static Dictionary<string, int> ExcelHeaderByOptions<T>(ISheet sheet, IEnumerable<ReadCellOption<T>> options)
        {
            // 获取对应设置的 表头 以及其 column
            var header = sheet.GetRow(Zero).Cells
            .Where(item => options.Any(opt => opt.ExcelField == item.ToString()?.Trim()))
            .ToDictionary(item => item.ToString()?.Trim(), item => item.ColumnIndex);
            return header;
        }
        #endregion



        #region 通过表格配置获取表格数据
        /// <summary>
        /// 根据配置 获取表格数据
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="options">导出配置</param>
        public static async Task<string[][]> ExcelDataByOptionsAsync<T>(string filepath, ReadConfig<T> options)
        {
            filepath.IsXls();
            using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return await ExcelDataByOptionsAsync(fs, options);
        }
        /// <summary>
        /// 根据配置 获取表格数据
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="options">导出配置</param>
        public static async Task<string[][]> ExcelDataByOptionsAsync<T>(string filepath, IEnumerable<ReadCellOption<T>> options)
        {
            filepath.IsXls();
            using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return await ExcelDataByOptionsAsync<T>(fs, options);
        }
        /// <summary>
        /// 根据配置 获取表格数据
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="options">导出配置</param>
        public static async Task<string[][]> ExcelDataByOptionsAsync<T>(Stream stream, ReadConfig<T> options)
        {
            using var notCloseStream = new NPOINotCloseStream(stream);
            return await ExcelDataByOptionsAsync(notCloseStream.GetWorkBook(), options);
        }
        /// <summary>
        /// 根据配置 获取表格数据
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="options">导出配置</param>
        public static async Task<string[][]> ExcelDataByOptionsAsync<T>(Stream stream, IEnumerable<ReadCellOption<T>> options)
        {
            using var notCloseStream = new NPOINotCloseStream(stream);
            return await ExcelDataByOptionsAsync<T>(notCloseStream.GetWorkBook(), options);
        }
        /// <summary>
        /// 根据配置 获取表格数据
        /// </summary>
        /// <param name="workbook">excel workbook</param>
        /// <param name="options">导出配置</param>
        public static async Task<string[][]> ExcelDataByOptionsAsync<T>(IWorkbook workbook, ReadConfig<T> options)
        {
            var sheet = workbook.GetSheetAt(Zero);
            return await ExcelDataByOptionsAsync(sheet, options);
        }
        /// <summary>
        /// 根据配置 获取表格数据
        /// </summary>
        /// <param name="workbook">excel workbook</param>
        /// <param name="options">导出配置</param>
        public static async Task<string[][]> ExcelDataByOptionsAsync<T>(IWorkbook workbook, IEnumerable<ReadCellOption<T>> options)
        {
            var sheet = workbook.GetSheetAt(Zero);
            return await ExcelDataByOptionsAsync<T>(sheet, options);
        }
        /// <summary>
        /// 根据配置 获取表格数据
        /// </summary>
        /// <param name="sheet">工作簿</param>
        /// <param name="options">导出配置</param>
        public static async Task<string[][]> ExcelDataByOptionsAsync<T>(ISheet sheet, ReadConfig<T> options)
        {
            return await ExcelDataByOptionsAsync<T>(sheet, options.FieldOption);
        }
        /// <summary>
        /// 根据配置 获取表格数据
        /// </summary>
        /// <param name="sheet">工作簿</param>
        /// <param name="options">导出配置</param>
        public static async Task<string[][]> ExcelDataByOptionsAsync<T>(ISheet sheet, IEnumerable<ReadCellOption<T>> options)
        {
            var header = ExcelHeaderByOptions<T>(sheet, options);

            int rowCount = sheet.LastRowNum + 1;
            int colCount = sheet.GetRow(Zero).Cells.Count;
            ConcurrentBag<string[]> data = new();
            await Task.Factory.StartNew(() =>
            {
                Parallel.For(Zero + 1, rowCount, index =>
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
        #endregion


        #region 将表格数据转换为指定的数据实体
        /// <summary>
        /// 将表格数据转换为指定的数据实体
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="options">导出配置</param>
        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(string filepath, ReadConfig<T> options)
        {
            filepath.IsXls();
            using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return await ExcelToEntityAsync(fs, options);
        }
        /// <summary>
        /// 将表格数据转换为指定的数据实体
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="options">导出配置</param>
        /// <param name="init">读取成功之后调用的针对T的委托</param>
        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(string filepath, IEnumerable<ReadCellOption<T>> options, Func<T, T> init)
        {
            filepath.IsXls();
            using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return await ExcelToEntityAsync(fs, options, init);
        }
        /// <summary>
        /// 将表格数据转换为指定的数据实体
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="options">导出配置</param>
        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(Stream stream, ReadConfig<T> options)
        {
            using var notCloseStream = new NPOINotCloseStream(stream);
            return await ExcelToEntityAsync(notCloseStream.GetWorkBook(), options);
        }
        /// <summary>
        /// 将表格数据转换为指定的数据实体
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="options">导出配置</param>
        /// <param name="init">读取成功之后调用的针对T的委托</param>
        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(Stream stream, IEnumerable<ReadCellOption<T>> options, Func<T, T> init)
        {
            using var notCloseStream = new NPOINotCloseStream(stream);
            return await ExcelToEntityAsync(notCloseStream.GetWorkBook(), options, init);
        }
        /// <summary>
        /// 将表格数据转换为指定的数据实体
        /// </summary>
        /// <param name="workbook">excel workbook</param>
        /// <param name="options">导出配置</param>
        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(IWorkbook workbook, ReadConfig<T> options)
        {
            var sheet = workbook.GetSheetAt(Zero);
            return await ExcelToEntityAsync(sheet, options);
        }
        /// <summary>
        /// 将表格数据转换为指定的数据实体
        /// </summary>
        /// <param name="workbook">excel workbook</param>
        /// <param name="options">导出配置</param>
        /// <param name="init">读取成功之后调用的针对T的委托</param>
        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(IWorkbook workbook, IEnumerable<ReadCellOption<T>> options, Func<T, T> init)
        {
            var sheet = workbook.GetSheetAt(Zero);
            return await ExcelToEntityAsync(sheet, options, init);
        }
        /// <summary>
        /// 将表格数据转换为指定的数据实体
        /// </summary>
        /// <param name="sheet">工作簿</param>
        /// <param name="options">导出配置</param>
        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(ISheet sheet, ReadConfig<T> options)
        {
            return await ExcelToEntityAsync(sheet, options.FieldOption, options.Init);
        }
        /// <summary>
        /// 将表格数据转换为指定的数据实体
        /// </summary>
        /// <param name="sheet">工作簿</param>
        /// <param name="options">导出配置</param>
        /// <param name="init">读取成功之后调用的针对T的委托</param>
        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(ISheet sheet, IEnumerable<ReadCellOption<T>> options, Func<T, T> init)
        {
            // 合并 FieldOption 和 DefaultOption
            var header = ExcelHeaderByOptions<T>(sheet, options);
            var excelData = await ExcelDataByOptionsAsync<T>(sheet, options);
            return await ExcelReadTool.ExcelToEntityAsync(header, excelData, options, init);
        }
        #endregion
    }
}
