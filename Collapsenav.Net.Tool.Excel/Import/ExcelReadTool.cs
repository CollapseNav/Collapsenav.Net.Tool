using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collapsenav.Net.Tool.Excel
{
    public class ExcelReadTool
    {
        /// <summary>
        /// 将表格数据转换为T类型的集合
        /// </summary>
        /// <param name="excelData">表格数据(需要包含表头且在第一行)</param>
        /// <param name="options">转换配置</param>
        /// <param name="init">读取成功之后调用的针对T的委托</param>
        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(string[][] excelData, IEnumerable<ReadCellOption<T>> options, Func<T, T> init)
        {
            var header = excelData[0].Select((key, index) => (key, index)).ToDictionary(item => item.key, item => item.index);
            excelData = excelData.Skip(1).ToArray();
            return await ExcelToEntityAsync(header, excelData, options, init);
        }

        /// <summary>
        /// 将表格数据转换为T类型的集合
        /// </summary>
        /// <param name="header">表头</param>
        /// <param name="excelData">表格数据</param>
        /// <param name="options">转换配置</param>
        /// <param name="init">读取成功之后调用的针对T的委托</param>
        public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(Dictionary<string, int> header, string[][] excelData, IEnumerable<ReadCellOption<T>> options, Func<T, T> init)
        {
            ConcurrentBag<T> data = new();
            await Task.Factory.StartNew(() =>
            {
                Parallel.For(0, excelData.Length, index =>
                {
                    // 根据对应传入的设置 为obj赋值
                    var obj = Activator.CreateInstance<T>();
                    foreach (var option in options)
                    {
                        if (!option.ExcelField.IsNull())
                        {
                            var value = excelData[index][header[option.ExcelField]];
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
