using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel
{
    /// <summary>
    /// 针对表格导入的一些操作(基于 EPPlus)
    /// </summary>
    public class EPPlusExcelReadTool
    {
        private const int Zero = 1;


        #region 获取表头
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)
        /// </summary>
        /// <param name="filepath">文件路径</param>
        public static IEnumerable<string> ExcelHeader(string filepath)
        {
            using var fs = new FileStream(filepath, FileMode.Open);
            return ExcelHeader(fs);
        }
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)
        /// </summary>
        /// <param name="stream">文件流</param>
        public static IEnumerable<string> ExcelHeader(Stream stream)
        {
            using ExcelPackage pack = new(stream);
            return ExcelHeader(pack);
        }
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)
        /// </summary>
        /// <param name="pack">excel workbook</param>
        public static IEnumerable<string> ExcelHeader(ExcelPackage pack)
        {
            var sheet = pack.Workbook.Worksheets[1];
            return ExcelHeader(sheet);
        }
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)
        /// </summary>
        /// <param name="sheet">工作簿</param>
        public static IEnumerable<string> ExcelHeader(ExcelWorksheet sheet)
        {
            return sheet.Cells[Zero, Zero, Zero, sheet.Dimension.Columns]
                    .Select(item => item.Value?.ToString().Trim()).ToArray();
        }
        #endregion


        #region 获取表格数据
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)
        /// </summary>
        /// <param name="filepath">文件路径</param>
        public static async Task<IEnumerable<IEnumerable<string>>> ExcelDataAsync(string filepath)
        {
            using var fs = new FileStream(filepath, FileMode.Open);
            return await ExcelDataAsync(fs);
        }
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)
        /// </summary>
        /// <param name="stream">文件流</param>
        public static async Task<IEnumerable<IEnumerable<string>>> ExcelDataAsync(Stream stream)
        {
            using ExcelPackage pack = new(stream);
            return await ExcelDataAsync(pack);
        }
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)
        /// </summary>
        /// <param name="pack">excel workbook</param>
        public static async Task<IEnumerable<IEnumerable<string>>> ExcelDataAsync(ExcelPackage pack)
        {
            var sheet = pack.Workbook.Worksheets[1];
            return await ExcelDataAsync(sheet);
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
                Parallel.For(Zero + 1, rowCount + Zero, index =>
                {
                    data.Add(sheet.Cells[index, Zero, index, colCount]
                    .Select(item => item.Value?.ToString().Trim()).ToList());
                });
            });
            return data;
        }
        #endregion


        #region 通过表格配置获取表头
        /// <summary>
        /// 获取表头及其index
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="options">导出配置</param>
        public static Dictionary<string, int> ExcelHeaderByOptions<T>(string filepath, ReadConfig<T> options)
        {
            using var fs = new FileStream(filepath, FileMode.Open);
            return ExcelHeaderByOptions(fs, options);
        }
        /// <summary>
        /// 获取表头及其index
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="options">导出配置</param>
        public static Dictionary<string, int> GetExcelHeaderByOptions<T>(string filepath, IEnumerable<ReadCellOption> options)
        {
            using var fs = new FileStream(filepath, FileMode.Open);
            return GetExcelHeaderByOptions<T>(fs, options);
        }
        /// <summary>
        /// 获取表头及其index
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="options">导出配置</param>
        public static Dictionary<string, int> ExcelHeaderByOptions<T>(Stream stream, ReadConfig<T> options)
        {
            using ExcelPackage pack = new(stream);
            return ExcelHeaderByOptions(pack, options);
        }
        /// <summary>
        /// 获取表头及其index
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="options">导出配置</param>
        public static Dictionary<string, int> GetExcelHeaderByOptions<T>(Stream stream, IEnumerable<ReadCellOption> options)
        {
            using ExcelPackage pack = new(stream);
            return GetExcelHeaderByOptions<T>(pack, options);
        }
        /// <summary>
        /// 获取表头及其index
        /// </summary>
        /// <param name="pack">excel workbook</param>
        /// <param name="options">导出配置</param>
        public static Dictionary<string, int> ExcelHeaderByOptions<T>(ExcelPackage pack, ReadConfig<T> options)
        {
            var sheet = pack.Workbook.Worksheets[Zero];
            return ExcelHeaderByOptions(sheet, options);
        }
        /// <summary>
        /// 获取表头及其index
        /// </summary>
        /// <param name="pack">excel workbook</param>
        /// <param name="options">导出配置</param>
        public static Dictionary<string, int> GetExcelHeaderByOptions<T>(ExcelPackage pack, IEnumerable<ReadCellOption> options)
        {
            var sheet = pack.Workbook.Worksheets[Zero];
            return GetExcelHeaderByOptions<T>(sheet, options);
        }
        /// <summary>
        /// 获取表头及其index
        /// </summary>
        /// <param name="sheet">工作簿</param>
        /// <param name="options">导出配置</param>
        public static Dictionary<string, int> ExcelHeaderByOptions<T>(ExcelWorksheet sheet, ReadConfig<T> options)
        {
            return GetExcelHeaderByOptions<T>(sheet, options.FieldOption);
        }
        /// <summary>
        /// 获取表头及其index
        /// </summary>
        /// <param name="sheet">工作簿</param>
        /// <param name="options">导出配置</param>
        public static Dictionary<string, int> GetExcelHeaderByOptions<T>(ExcelWorksheet sheet, IEnumerable<ReadCellOption> options)
        {
            // 获取对应设置的 表头 以及其 column
            var header = sheet.Cells[Zero, Zero, Zero, sheet.Dimension.Columns]
            .Where(item => options.Any(opt => opt.ExcelField == item.Value?.ToString().Trim()))
            .ToDictionary(item => item.Value?.ToString().Trim(), item => item.End.Column - Zero);
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
            using var fs = new FileStream(filepath, FileMode.Open);
            return await ExcelDataByOptionsAsync(fs, options);
        }
        /// <summary>
        /// 根据配置 获取表格数据
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="options">导出配置</param>
        public static async Task<string[][]> GetExcelDataByOptionsAsync<T>(string filepath, IEnumerable<ReadCellOption> options)
        {
            using var fs = new FileStream(filepath, FileMode.Open);
            return await GetExcelDataByOptionsAsync<T>(fs, options);
        }
        /// <summary>
        /// 根据配置 获取表格数据
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="options">导出配置</param>
        public static async Task<string[][]> ExcelDataByOptionsAsync<T>(Stream stream, ReadConfig<T> options)
        {
            using ExcelPackage pack = new(stream);
            return await ExcelDataByOptionsAsync(pack, options);
        }
        /// <summary>
        /// 根据配置 获取表格数据
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="options">导出配置</param>
        public static async Task<string[][]> GetExcelDataByOptionsAsync<T>(Stream stream, IEnumerable<ReadCellOption> options)
        {
            using ExcelPackage pack = new(stream);
            return await GetExcelDataByOptionsAsync<T>(pack, options);
        }
        /// <summary>
        /// 根据配置 获取表格数据
        /// </summary>
        /// <param name="pack">excel workbook</param>
        /// <param name="options">导出配置</param>
        public static async Task<string[][]> ExcelDataByOptionsAsync<T>(ExcelPackage pack, ReadConfig<T> options)
        {
            var sheet = pack.Workbook.Worksheets[1];
            return await ExcelDataByOptionsAsync(sheet, options);
        }
        /// <summary>
        /// 根据配置 获取表格数据
        /// </summary>
        /// <param name="pack">excel workbook</param>
        /// <param name="options">导出配置</param>
        public static async Task<string[][]> GetExcelDataByOptionsAsync<T>(ExcelPackage pack, IEnumerable<ReadCellOption> options)
        {
            var sheet = pack.Workbook.Worksheets[1];
            return await GetExcelDataByOptionsAsync<T>(sheet, options);
        }
        /// <summary>
        /// 根据配置 获取表格数据
        /// </summary>
        /// <param name="sheet">工作簿</param>
        /// <param name="options">导出配置</param>
        public async static Task<string[][]> ExcelDataByOptionsAsync<T>(ExcelWorksheet sheet, ReadConfig<T> options)
        {
            return await GetExcelDataByOptionsAsync<T>(sheet, options.FieldOption);
        }
        /// <summary>
        /// 根据配置 获取表格数据
        /// </summary>
        /// <param name="sheet">工作簿</param>
        /// <param name="options">导出配置</param>
        public static async Task<string[][]> GetExcelDataByOptionsAsync<T>(ExcelWorksheet sheet, IEnumerable<ReadCellOption> options)
        {
            var header = GetExcelHeaderByOptions<T>(sheet, options);
            var resultHeader = header.Select(item => item.Key).ToList();

            int rowCount = sheet.Dimension.Rows;
            int colCount = sheet.Dimension.Columns;
            ConcurrentBag<string[]> data = new();
            await Task.Factory.StartNew(() =>
            {
                Parallel.For(Zero + 1, rowCount + Zero, index =>
                {
                    Monitor.Enter(sheet);
                    var temp = sheet.Cells[index, Zero, index, colCount]
                    .Where(item => header.Any(col => col.Value == item.End.Column - Zero))
                    .Select(item => item.Text).ToArray();
                    Monitor.Exit(sheet);
                    data.Add(temp);
                });
            });
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
            using var fs = new FileStream(filepath, FileMode.Open);
            return await ExcelToEntityAsync(fs, options);
        }
        /// <summary>
        /// 将表格数据转换为指定的数据实体
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="options">导出配置</param>
        /// <param name="init">读取成功之后调用的针对T的委托</param>
        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(string filepath, IEnumerable<ReadCellOption> options, Func<T, T> init)
        {
            using var fs = new FileStream(filepath, FileMode.Open);
            return await ExcelToEntityAsync(fs, options, init);
        }
        /// <summary>
        /// 将表格数据转换为指定的数据实体
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="options">导出配置</param>
        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(Stream stream, ReadConfig<T> options)
        {
            using ExcelPackage pack = new(stream);
            return await ExcelToEntityAsync(pack, options);
        }
        /// <summary>
        /// 将表格数据转换为指定的数据实体
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="options">导出配置</param>
        /// <param name="init">读取成功之后调用的针对T的委托</param>
        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(Stream stream, IEnumerable<ReadCellOption> options, Func<T, T> init)
        {
            using ExcelPackage pack = new(stream);
            return await ExcelToEntityAsync(pack, options, init);
        }
        /// <summary>
        /// 将表格数据转换为指定的数据实体
        /// </summary>
        /// <param name="pack">excel workbook</param>
        /// <param name="options">导出配置</param>
        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(ExcelPackage pack, ReadConfig<T> options)
        {
            var sheet = pack.Workbook.Worksheets[1];
            return await ExcelToEntityAsync(sheet, options);
        }
        /// <summary>
        /// 将表格数据转换为指定的数据实体
        /// </summary>
        /// <param name="pack">excel workbook</param>
        /// <param name="options">导出配置</param>
        /// <param name="init">读取成功之后调用的针对T的委托</param>
        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(ExcelPackage pack, IEnumerable<ReadCellOption> options, Func<T, T> init)
        {
            var sheet = pack.Workbook.Worksheets[1];
            return await ExcelToEntityAsync(sheet, options, init);
        }
        /// <summary>
        /// 将表格数据转换为指定的数据实体
        /// </summary>
        /// <param name="sheet">工作簿</param>
        /// <param name="options">导出配置</param>
        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(ExcelWorksheet sheet, ReadConfig<T> options)
        {
            return await ExcelToEntityAsync(sheet, options.FieldOption, options.Init);
        }
        /// <summary>
        /// 将表格数据转换为指定的数据实体
        /// </summary>
        /// <param name="sheet">工作簿</param>
        /// <param name="options">导出配置</param>
        /// <param name="init">读取成功之后调用的针对T的委托</param>
        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(ExcelWorksheet sheet, IEnumerable<ReadCellOption> options, Func<T, T> init)
        {
            // 合并 FieldOption 和 DefaultOption
            var header = GetExcelHeaderByOptions<T>(sheet, options);
            var excelData = await GetExcelDataByOptionsAsync<T>(sheet, options);
            return await ExcelReadTool.ExcelToEntityAsync(header, excelData, options, init);
        }
        #endregion
    }
}
