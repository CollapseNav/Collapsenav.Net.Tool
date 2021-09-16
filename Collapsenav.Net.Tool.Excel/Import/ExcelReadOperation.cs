using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel
{
    /// <summary>
    /// 聊胜于无的扩展方法
    /// TODO 补全
    /// </summary>
    public static class ExcelSteamExt
    {
        public static async Task<ExcelReadOperation> GenExcelOperationAsync(this Stream excelStream)
        {
            return new ExcelReadOperation()
            {
                Header = ExcelReadOperation.GetExcelHeader(excelStream),
                Data = await ExcelReadOperation.GetExcelDataAsync(excelStream),
            };
        }
    }

    /// <summary>
    /// 针对表格导入的一些操作(基于EPPlus)
    /// </summary>
    public class ExcelReadOperation
    {
        public IEnumerable<string> Header { get; set; }
        public IEnumerable<IEnumerable<string>> Data { get; set; }
        public ExcelReadOperation() { }
        public ExcelReadOperation(Stream excelStream)
        {
            Header = GetExcelHeader(excelStream);
            Data = GetExcelDataAsync(excelStream).Result;
        }

        /// <summary>
        /// 从流中读取excel
        /// </summary>
        public static async Task<ExcelReadOperation> GetExcelOperationAsync(Stream excelStream)
        {
            return new ExcelReadOperation()
            {
                Header = GetExcelHeader(excelStream),
                Data = await GetExcelDataAsync(excelStream),
            };
        }

        #region 获取表头

        /// <summary>
        /// 获取表格header(仅限简单的单行表头)
        /// </summary>
        public static IEnumerable<string> GetExcelHeader(Stream excelStream)
        {
            using ExcelPackage pack = new(excelStream);
            return GetExcelHeader(pack);
        }
        public static IEnumerable<string> GetExcelHeader(ExcelPackage pack)
        {
            var sheet = pack.Workbook.Worksheets[1];
            return GetExcelHeader(sheet);
        }
        public static IEnumerable<string> GetExcelHeader(ExcelWorksheet sheet)
        {
            return sheet.Cells[1, 1, 1, sheet.Dimension.Columns]
                    .Select(item => item.Value?.ToString().Trim()).ToArray();
        }

        #endregion


        #region 获取表格数据
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)
        /// </summary>
        public static async Task<IEnumerable<IEnumerable<string>>> GetExcelDataAsync(Stream excelStream)
        {
            using ExcelPackage pack = new(excelStream);
            return await GetExcelDataAsync(pack);
        }
        public static async Task<IEnumerable<IEnumerable<string>>> GetExcelDataAsync(ExcelPackage pack)
        {
            var sheet = pack.Workbook.Worksheets[1];
            return await GetExcelDataAsync(sheet);
        }
        public static async Task<IEnumerable<IEnumerable<string>>> GetExcelDataAsync(ExcelWorksheet sheet)
        {
            int rowCount = sheet.Dimension.Rows;
            int colCount = sheet.Dimension.Columns;
            ConcurrentBag<IEnumerable<string>> data = new();
            await Task.Factory.StartNew(() =>
            {
                Parallel.For(2, rowCount + 1, index =>
                {
                    data.Add(sheet.Cells[index, 1, index, colCount]
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
        public static Dictionary<string, int> GetExcelHeaderByOptions<T>(Stream excelStream, ReadConfig<T> options)
        {
            using ExcelPackage pack = new(excelStream);
            return GetExcelHeaderByOptions(pack, options);
        }
        /// <summary>
        /// 获取表头及其index
        /// </summary>
        public static Dictionary<string, int> GetExcelHeaderByOptions<T>(ExcelPackage pack, ReadConfig<T> options)
        {
            var sheet = pack.Workbook.Worksheets[1];
            return GetExcelHeaderByOptions(sheet, options);
        }
        /// <summary>
        /// 获取表头及其index
        /// </summary>
        public static Dictionary<string, int> GetExcelHeaderByOptions<T>(ExcelWorksheet sheet, ReadConfig<T> options)
        {
            // 合并 FieldOption 和 DefaultOption
            return GetExcelHeaderByOptions(sheet, options.FieldOption);
        }


        public static Dictionary<string, int> GetExcelHeaderByOptions<T>(Stream excelStream, IEnumerable<ReadCellOption<T>> options)
        {
            using ExcelPackage pack = new(excelStream);
            return GetExcelHeaderByOptions(pack, options);
        }
        public static Dictionary<string, int> GetExcelHeaderByOptions<T>(ExcelPackage pack, IEnumerable<ReadCellOption<T>> options)
        {
            var sheet = pack.Workbook.Worksheets[1];
            return GetExcelHeaderByOptions(sheet, options);
        }
        /// <summary>
        /// 获取表头及其index
        /// </summary>
        public static Dictionary<string, int> GetExcelHeaderByOptions<T>(ExcelWorksheet sheet, IEnumerable<ReadCellOption<T>> options)
        {
            // 获取对应设置的 表头 以及其 column
            var header = sheet.Cells[1, 1, 1, sheet.Dimension.Columns]
            .Where(item => options.Any(opt => opt.ExcelField == item.Value?.ToString().Trim()))
            .ToDictionary(item => item.Value?.ToString().Trim(), item => item.End.Column);
            return header;
        }
        #endregion



        public static async Task<string[][]> GetExcelDataByOptionsAsync<T>(Stream excelStream, ReadConfig<T> options)
        {
            using ExcelPackage pack = new(excelStream);
            return await GetExcelDataByOptionsAsync(pack, options);
        }
        public static async Task<string[][]> GetExcelDataByOptionsAsync<T>(ExcelPackage pack, ReadConfig<T> options)
        {
            var sheet = pack.Workbook.Worksheets[1];
            return await GetExcelDataByOptionsAsync(sheet, options);
        }
        public async static Task<string[][]> GetExcelDataByOptionsAsync<T>(ExcelWorksheet sheet, ReadConfig<T> options)
        {
            return await GetExcelDataByOptionsAsync(sheet, options.FieldOption);
        }

        public static async Task<string[][]> GetExcelDataByOptionsAsync<T>(Stream excelStream, IEnumerable<ReadCellOption<T>> options)
        {
            using ExcelPackage pack = new(excelStream);
            return await GetExcelDataByOptionsAsync(pack, options);
        }
        public static async Task<string[][]> GetExcelDataByOptionsAsync<T>(ExcelPackage pack, IEnumerable<ReadCellOption<T>> options)
        {
            var sheet = pack.Workbook.Worksheets[1];
            return await GetExcelDataByOptionsAsync(sheet, options);
        }
        /// <summary>
        /// 可能会存在比较耗时的复杂计算,所以使用并行
        /// data线程安全,但是sheet并不是,所以加锁
        /// </summary>
        public static async Task<string[][]> GetExcelDataByOptionsAsync<T>(ExcelWorksheet sheet, IEnumerable<ReadCellOption<T>> options)
        {
            var header = GetExcelHeaderByOptions(sheet, options);
            var resultHeader = header.Select(item => item.Key).ToList();

            int rowCount = sheet.Dimension.Rows;
            int colCount = sheet.Dimension.Columns;
            ConcurrentBag<string[]> data = new();
            await Task.Factory.StartNew(() =>
            {
                Parallel.For(2, rowCount + 1, index =>
                {
                    Monitor.Enter(sheet);
                    var temp = sheet.Cells[index, 1, index, colCount]
                    .Where(item => header.Any(col => col.Value == item.End.Column))
                    .Select(item => item.Text).ToArray();
                    Monitor.Exit(sheet);
                    data.Add(temp);
                });
            });
            data.Add(resultHeader.ToArray());
            return data.ToArray();
        }



        /// <summary>
        /// 将表格数据转换为指定的数据实体
        /// </summary>
        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(Stream excelStream, ReadConfig<T> options)
        {
            using ExcelPackage pack = new(excelStream);
            return await ExcelToEntityAsync(pack, options);
        }
        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(ExcelPackage pack, ReadConfig<T> options)
        {
            var sheet = pack.Workbook.Worksheets[1];
            return await ExcelToEntityAsync(sheet, options);
        }
        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(ExcelWorksheet sheet, ReadConfig<T> options)
        {
            return await ExcelToEntityAsync(sheet, options.FieldOption, options.Init);
        }

        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(Stream excelStream, IEnumerable<ReadCellOption<T>> options, Func<T, T> init)
        {
            using ExcelPackage pack = new(excelStream);
            return await ExcelToEntityAsync(pack, options, init);
        }
        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(ExcelPackage pack, IEnumerable<ReadCellOption<T>> options, Func<T, T> init)
        {
            var sheet = pack.Workbook.Worksheets[1];
            return await ExcelToEntityAsync(sheet, options, init);
        }
        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(ExcelWorksheet sheet, IEnumerable<ReadCellOption<T>> options, Func<T, T> init)
        {
            // 合并 FieldOption 和 DefaultOption
            var excelData = await GetExcelDataByOptionsAsync(sheet, options);
            ConcurrentBag<T> data = new();
            await Task.Factory.StartNew(() =>
            {
                var indexMap = excelData[0].Select((key, index) => (key, index)).ToDictionary(item => item.key, item => item.index);
                Parallel.For(1, excelData.Length, index =>
                {
                    // 根据对应传入的设置 为obj赋值
                    var obj = Activator.CreateInstance<T>();
                    foreach (var option in options)
                    {
                        if (!option.ExcelField.IsNull())
                        {
                            var value = excelData[index][indexMap[option.ExcelField]];
                            option.Prop.SetValue(obj, option.Action == null ? value : option.Action(value));
                        }
                        else
                            option.Prop.SetValue(obj, option.Action == null ? null : option.Action(string.Empty));
                    }
                    if (init != null)
                        obj = init(obj);
                    data.Add(obj);
                });
            });
            return data;
        }
    }
}
