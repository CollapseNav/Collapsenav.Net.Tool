using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Collapsenav.Net.Tool.Excel
{
    /// <summary>
    /// 导出表格设置
    /// </summary>
    public partial class ExportConfig<T>
    {
        /// <summary>
        /// 表格数据
        /// </summary>
        public IEnumerable<T> Data { get; protected set; }
        public ExportConfig()
        {
            FieldOption = new List<ExportCellOption<T>>();
        }
        public ExportConfig(IEnumerable<T> data) : this()
        {
            if (data.IsEmpty())
                throw new NoNullAllowedException();
            Data = data;
        }


        /// <summary>
        /// 根据给出的表头筛选options
        /// </summary>
        public void FilterOptionByHeaders(IEnumerable<string> headers)
        {
            FieldOption = FilterOptionByHeaders(FieldOption, headers);
        }

        /// <summary>
        /// 根据给出的表头筛选options
        /// </summary>
        public static IEnumerable<ExportCellOption<T>> FilterOptionByHeaders(IEnumerable<ExportCellOption<T>> options, IEnumerable<string> headers)
        {
            return options.Where(item => headers.Any(head => head == item.ExcelField));
        }

        /// <summary>
        /// 设置需要导出的表格数据
        /// </summary>
        public virtual void SetExcelData(IEnumerable<T> data)
        {
            Data = data;
        }
        /// <summary>
        /// 依据表头的设置
        /// </summary>
        public virtual IEnumerable<ExportCellOption<T>> FieldOption { get; protected set; }

        /// <summary>
        /// 获取表头
        /// </summary>
        public virtual IEnumerable<string> Header { get => FieldOption.Select(item => item.ExcelField); }
        /// <summary>
        /// 获取表头
        /// </summary>
        public virtual IEnumerable<object[]> ConvertHeader { get => new List<object[]> { Header.ToArray() }; }
        /// <summary>
        /// 获取数据
        /// </summary>
        public virtual IEnumerable<object[]> ConvertData { get => Data?.Select(item => FieldOption.Select(option => option.Action(item)).ToArray()); }
        /// <summary>
        /// 获取数据
        /// </summary>
        public virtual IEnumerable<object[]> GetConvertData(IEnumerable<T> data)
        {
            return data?.Select(item => FieldOption.Select(option => option.Action(item)).ToArray());
        }
        /// <summary>
        /// 获取表头和数据
        /// </summary>
        public virtual IEnumerable<object[]> ExportData { get => ConvertHeader.Concat(ConvertData); }
        /// <summary>
        /// 获取表头和数据
        /// </summary>
        public virtual IEnumerable<object[]> GetExportData(IEnumerable<T> data)
        {
            return ConvertHeader.Concat(GetConvertData(data));
        }

        /// <summary>
        /// 添加普通单元格设置
        /// </summary>
        public virtual ExportConfig<T> Add(ExportCellOption<T> option)
        {
            FieldOption = FieldOption.Append(option);
            return this;
        }
        /// <summary>
        /// check条件为True时添加普通单元格设置
        /// </summary>
        public virtual ExportConfig<T> AddIf(bool check, ExportCellOption<T> option)
        {
            if (check)
                return Add(option);
            return this;
        }
        public virtual ExportConfig<T> Add(string field, string propName)
        {
            return Add(field, item => item.GetValue(propName).ToString());
        }
        public virtual ExportConfig<T> AddIf(bool check, string field, string propName)
        {
            return check ? Add(field, field) : this;
        }
        public virtual ExportConfig<T> Add(string field, string propName, string format)
        {
            var prop = typeof(T).GetProperty(propName);
            if (prop.PropertyType.Name == nameof(DateTime))
                return Add(field, item => ((DateTime)item.GetValue(propName)).ToString(format));
            return Add(field, propName);
        }
        public virtual ExportConfig<T> AddIf(bool check, string field, string propName, string format)
        {
            return check ? Add(field, field, format) : this;
        }
        public ExportConfig<T> Add(string field, Func<T, object> action)
        {
            FieldOption = FieldOption.Append(new ExportCellOption<T>
            {
                ExcelField = field,
                Action = action
            });
            return this;
        }
        public ExportConfig<T> AddIf(bool check, string field, Func<T, object> action)
        {
            if (check)
            {
                FieldOption = FieldOption.Append(new ExportCellOption<T>
                {
                    ExcelField = field,
                    Action = action
                });
            }
            return this;
        }
    }
}
