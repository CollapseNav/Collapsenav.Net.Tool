using System;

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
    }
}
