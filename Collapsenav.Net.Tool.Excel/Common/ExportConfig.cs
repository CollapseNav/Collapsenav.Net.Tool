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
        /// 根据 T 生成默认的 Config
        /// </summary>
        public static ExportConfig<T> GenDefaultConfig(IEnumerable<T> data = null)
        {
            data ??= new List<T>();
            var config = new ExportConfig<T>(data);
            // 根据 T 中设置的 ExcelExportAttribute 创建导出配置
            var attrData = TypeTool.AttrValues<T, ExcelExportAttribute>();
            if (attrData != null)
            {
                foreach (var prop in attrData)
                    config.Add(prop.Value.ExcelField, item => item.GetValue(prop.Key.Name));
                return config;
            }
            foreach (var propName in typeof(T).BuildInTypePropNames())
                config.Add(propName, item => item.GetValue(propName));
            return config;
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
