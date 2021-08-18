using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel
{
    /// <summary>
    /// 聊胜于无的扩展方法
    /// TODO 补全
    /// </summary>
    public static class ExcelSteamExt
    {
        public static async Task<ExcelOperation> GenExcelOperationAsync(this Stream excelStream)
        {
            return new ExcelOperation()
            {
                Header = ExcelOperation.GetExcelHeader(excelStream),
                Data = await ExcelOperation.GetExcelDataAsync(excelStream),
            };
        }
    }
    /// <summary>
    /// 针对表格的一些操作(基于EPPlus)
    /// </summary>
    public class ExcelOperation
    {
        public IEnumerable<string> Header { get; set; }
        public IEnumerable<IEnumerable<string>> Data { get; set; }
        public ExcelOperation() { }
        public ExcelOperation(Stream excelStream)
        {
            Header = GetExcelHeader(excelStream);
            Data = GetExcelDataAsync(excelStream).Result;
        }

        /// <summary>
        /// 从流中读取excel
        /// </summary>
        public static async Task<ExcelOperation> GenExcelOperationAsync(Stream excelStream)
        {
            return new ExcelOperation()
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

        public static Dictionary<string, int> GenExcelHeaderByOptions<T>(Stream excelStream, ReadConfig<T> options)
        {
            using ExcelPackage pack = new(excelStream);
            return GenExcelHeaderByOptions<T>(pack, options);
        }

        public static Dictionary<string, int> GenExcelHeaderByOptions<T>(ExcelPackage pack, ReadConfig<T> options)
        {
            // 合并 FieldOption 和 DefaultOption
            var sheet = pack.Workbook.Worksheets[1];
            return GenExcelHeaderByOptions<T>(sheet, options);
        }
        public static Dictionary<string, int> GenExcelHeaderByOptions<T>(ExcelWorksheet sheet, ReadConfig<T> options)
        {
            // 合并 FieldOption 和 DefaultOption
            var propOptions = options.FieldOption.Concat(options.DefaultOption);
            return GenExcelHeaderByOptions(sheet, propOptions);
        }
        public static Dictionary<string, int> GenExcelHeaderByOptions<T>(ExcelWorksheet sheet, IEnumerable<ReadCellOption<T>> options)
        {
            // 获取对应设置的 表头 以及其 column
            var header = sheet.Cells[1, 1, 1, sheet.Dimension.Columns]
            .Where(item => options.Any(opt => opt.ExcelField == item.Value?.ToString().Trim()))
            .ToDictionary(item => item.Value?.ToString().Trim(), item => item.End.Column);
            return header;
        }

        #endregion

        /// <summary>
        /// TODO 通过 span 组装表格数据,简化后期的查询操作
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="options"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        // public static async Dictionary<string, int> GenExcelDataByOptions<T>(ExcelWorksheet sheet, IEnumerable<ReadCellOption<T>> options)
        // {
        //     var header = GenExcelHeaderByOptions(sheet, options);

        // }



        /// <summary>
        /// 将表格数据转换为指定的数据实体
        /// </summary>
        public static async Task<IEnumerable<T>> ExcelToEntity<T>(Stream excelStream, ReadConfig<T> options)
        {
            using ExcelPackage pack = new(excelStream);
            var sheet = pack.Workbook.Worksheets[1];
            int rowCount = sheet.Dimension.Rows;
            int colCount = sheet.Dimension.Columns;
            // 合并 FieldOption 和 DefaultOption
            var propOptions = options.FieldOption.Concat(options.DefaultOption);
            var header = GenExcelHeaderByOptions(sheet, propOptions);

            ConcurrentBag<T> data = new();
            await Task.Factory.StartNew(() =>
            {
                Parallel.For(2, rowCount + 1, index =>
                {
                    // 将单行数据转换为 表头:数据 的键值对
                    var rowData = sheet.Cells[index, 1, index, colCount]
                    .Where(item => header.Any(title => title.Value == item.End.Column))
                    .Select(item => new KeyValuePair<string, string>(header.First(title => title.Value == item.End.Column).Key, item.Value?.ToString().Trim()))
                    .ToDictionary(item => item.Key, item => item.Value);

                    // 根据对应传入的设置 为obj赋值
                    var obj = Activator.CreateInstance<T>();
                    foreach (var option in propOptions)
                    {
                        if (!option.ExcelField.IsNull())
                        {
                            var value = rowData.ContainsKey(option.ExcelField) ? rowData[option.ExcelField] : string.Empty;
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
    }
}
