﻿using System;
using System.Reflection;

namespace Collapsenav.Net.Tool.Excel
{
    /// <summary>
    ///  导出单元格设置
    /// </summary>
    public class ExportCellOption<T>
    {
        /// <summary>
        /// 对应excel中的header字段
        /// </summary>
        public string ExcelField { get; set; }

        /// <summary>
        /// 转换 表格 数据的方法
        /// </summary>
        public Func<T, object> Action { get; set; }
        /// <summary>
        /// 就是一个看起来比较方便的标识
        /// </summary>
        public string PropName { get; set; }
        /// <summary>
        /// 对应字段的属性(实际上包含PropName)
        /// </summary>
        public PropertyInfo Prop { get; set; }
    }

    public class ExportCellOption : ExportCellOption<object>
    { }
}
