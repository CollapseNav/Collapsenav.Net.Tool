using System;
using System.Reflection;

namespace Collapsenav.Net.Tool.Excel
{
    public class ReadCellOption<T>
    {
        /// <summary>
        /// 对应excel中的header字段
        /// </summary>
        public string ExcelField { get; set; }

        /// <summary>
        /// 对应字段的属性(实际上包含PropName)
        /// </summary>
        public PropertyInfo Prop { get; set; }

        /// <summary>
        /// 就是一个看起来比较方便的标识
        /// </summary>
        public string PropName { get; set; }

        /// <summary>
        /// 转换 表格 数据的方法
        /// </summary>
        public Func<string, object> Action { get; set; }
    }
}
