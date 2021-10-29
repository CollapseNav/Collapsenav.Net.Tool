using System;
using System.Collections.Generic;
using System.Linq;

namespace Collapsenav.Net.Tool.Excel
{
    public class ExportConfig<T>
    {
        public IEnumerable<T> Data { get; set; }
        public ExportConfig()
        {
            FieldOption = new List<ExportCellOption<T>>();
        }
        public ExportConfig(IEnumerable<T> data)
        {
            Data = data;
            FieldOption = new List<ExportCellOption<T>>();
        }
        /// <summary>
        /// 依据表头的设置
        /// </summary>
        public IEnumerable<ExportCellOption<T>> FieldOption { get; set; }
        public IEnumerable<string> Header { get => FieldOption.Select(item => item.ExcelField); }
        public IEnumerable<object[]> ConvertHeader { get => new List<object[]> { Header.ToArray() }; }
        public IEnumerable<object[]> ConvertData { get => Data.Select(item => FieldOption.Select(option => option.Action(item)).ToArray()); }
        public IEnumerable<object[]> Export { get => ConvertHeader.Concat(ConvertData); }
    }
}
